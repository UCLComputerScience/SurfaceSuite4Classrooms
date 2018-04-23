"use strict";
var express = require('express')
var app = express()
app.use(require('sanitize').middleware)
// -------------------- ALL STUDENTS -------------------- //

// Get all students

app.get('/', function (req, res, next) {
	req.getConnection(function (error, conn) {
		conn.query('SELECT * FROM student ORDER BY last_name ASC', function (err, rows, fields) {
			if (err) throw err
			res.render('dashboard', {
				notification: ['hello'],
				data: rows,
				first_name: '',
				last_name: '',
			})
			console.log('Served Dashboard with', rows.length, 'rows')
		})
	})
})

app.get('/ingame', function (req, res, next) {
	res.render('ingame')
})


// Add new post
app.post('/', function (req, res, next) {
	req.assert('first_name', 'Forename is invalid').isAlpha().isLength({min:1, max:20}) //Validate first_name
	req.assert('last_name', 'Surname is invalid').isAlpha().isLength({min:1, max:20}) //Validate last_name
	var errors = req.validationErrors()
	if (!errors) { //No errors were found.  Passed Validation!
		var student = {
			first_name: req.sanitize('first_name').escape().trim(),
			last_name: req.sanitize('last_name').escape().trim(),
			scores: '[]',
			score_times: '[]'
		}
		req.getConnection(function (error, conn) {
			conn.query('INSERT INTO student SET ?', student, function (err, result) {
				if (err) {
					console.log('ERROR:', err)
				} else {
					console.log('Successfully created student')
				}
			})
		})
	} else {
		console.log('ERROR:', errors)
	}
	res.redirect('/dashboard')
})

// -------------------- PER STUDENTS -------------------- //

// Get student by id

app.get('/show/(:student_id)', function (req, res, next) {
	//req.checkParams('Invalid Student ID').isFloat()
	var errors = req.validationErrors()
	if (!errors) {
		req.getConnection(function (error, conn) {
			conn.query('SELECT * FROM student WHERE student_id = ' + req.params.student_id,
				function (err, rows, fields) {
					if (err) throw (err)
					if (rows.length <= 0) {
						console.log('ERROR: No student found with id:', req.params.student_id)
						res.redirect('/dashboard')
					} else {
						console.log('Served show for student:', req.params.student_id)
						if (JSON.parse(rows[0].scores).length != 0) {
							var avg = Math.floor((JSON.parse(rows[0].scores).reduce((previous, current) => current += previous)) / (JSON.parse(rows[0].scores).length))
						} else {
							var avg = 0
						}

						res.render('showStudent', {
							student_id: rows[0].student_id,
							first_name: rows[0].first_name,
							last_name: rows[0].last_name,
							scores: rows[0].scores,
							score_times: rows[0].score_times,
							score: avg,
							notification: [],
						})
					}
				})
		})
	} else {
		console.log(errors[0].msg + ' for dashboard/show/ route')
		res.json(errors[0].msg)
	}

})

// Delete Student by id
app.get('/student/delete/(:student_id)', function (req, res, next) {
	console.log("Deleted user", req.params.student_id)
	req.getConnection(function (error, conn) {
		conn.query('DELETE FROM student WHERE student_id = ' + req.params.student_id, function (err, result) {
			//if(err) throw err
			if (err) {
				console.log('ERROR:', err)
				res.redirect('/dasboard')
			} else {
				console.log('Student with student_id:', req.params.student_id, 'removed')
				res.redirect('/dashboard')
			}
		})
	})
})

// -------------------- ALL DEFINITIONS -------------------- //

// Get all definitions

app.get('/definitions', function (req, res, next) {
	req.getConnection(function (error, conn) {
		conn.query('SELECT * FROM definition ORDER BY word ASC', function (err, rows, fields) {
			if (err) console.log(err)
			console.log('Served Definitions with ', rows.length, ' rows')
			if (err) throw err
			res.render('definitions', {
				notification: [],
				definitions: rows,
				word: '',
				meaning: '',
			})
		})
	})
})

// Add new definition

app.post('/definitions', function (req, res, next) {
	req.assert('word', 'Name is required').notEmpty()
	req.assert('meaning', 'Surname is required').notEmpty()
	var errors = req.validationErrors()
	if (!errors) {
		var definition = {
			meaning: req.sanitize('meaning').escape().trim(),
			word: req.sanitize('word').escape().trim(),
		}
		req.getConnection(function (error, conn) {
			conn.query('INSERT INTO definition SET ?', definition, function (err, result) {
				if (err) throw err
			})
			conn.query('SELECT * FROM definition ORDER BY word ASC', function (err, rows, fields) {
				if (err) console.log(err)
				console.log('Served Definitions with ', rows.length, ' rows')
				if (err) throw err
				res.render('definitions', {
					definitions: rows,
					word: '',
					meaning: '',
					notification: ['Added new definition'],
				})
			})
		})
	} else {
		console.log('ERROR:', errors)
	}
})

// Delete definition by id

app.get('/definition/delete/(:definition_id)', function (req, res, next) {
	req.getConnection(function (error, conn) {
		conn.query('DELETE FROM definition WHERE definition_id = ' + req.params.definition_id, function (err, result) {
			if (err) throw err
			console.log('Definition deleted successfully! definition_id:', req.params.definition_id)
		})
		conn.query('SELECT * FROM definition ORDER BY word ASC', function (err, rows, fields) {
			if (err) console.log(err)
			console.log('Served Definitions with ', rows.length, ' rows')
			if (err) throw err
			res.render('definitions', {
				definitions: rows,
				word: '',
				meaning: '',
				notification: ['Deleted definition'],
			})
		})
	})
})

// -------------------- STARTGAME -------------------- //

app.get('/startgame', function (req, res, next) {

	req.getConnection(function (error, conn) {
		conn.query('SELECT * FROM student ORDER BY last_name ASC', function (err, rows, fields) {
			if (err) {
				console.log('ERROR:', err)
				res.render('startgame', {
					students: '',
					definitions: '',
					output: ''
				})
			} else {
				var students = rows
				conn.query('SELECT * FROM definition ORDER BY word ASC', function (err, rows, fields) {
					console.log('Served startgame with', rows.length, 'definitions and', students.length, 'students')
					if (err) {
						console.log('ERROR:', err)
						res.render('startgame', {
							students: students,
							definitions: '',
							output: ''
						})
					} else {
						res.render('startgame', {
							students: students,
							definitions: rows,
							definition1: '',
							definition2: '',
							definition3: '',
							definition4: '',
							answer1: '',
							answer2: '',
							answer3: '',
							answer4: '',
							id1: '',
							id2: '',
							id3: '',
							id4: '',
							output: ''
						})
					}
				})
			}
		})
	})
})

app.post('/startgame', function (req, res, next) {
	console.log(app.locals.toTable)
	app.locals.toTable = {}
	req.getConnection(function (error, conn) {
			conn.query('SELECT * FROM definition;', function (err, rows, fields) {
        if (err) throw (err)
				for (var i=0, j=1; i<rows.length; i++) {
					if (req.body.definitions.includes(rows[i].definition_id.toString())) {
						var meaning = 'definition' + j
						var word = 'answer' + j
						app.locals.toTable[meaning] = rows[i].meaning
						app.locals.toTable[word] = rows[i].word
						j++
					}
				}
      })
			conn.query('SELECT student_id, first_name, last_name FROM student;', function (err, rows, fields) {
        if (err) throw (err)
				app.locals.toTable['id'] = []
				for (var i=0, j=1; i<rows.length; i++) {
					if (req.body.students.includes(rows[i].student_id.toString())) {
						var target = 'student' + j
						app.locals.toTable[target] = rows[i].first_name + ' ' + rows[i].last_name
						console.log(app.locals.toTable)
						app.locals.toTable['id'].push(rows[i].student_id)
						j++
					}
				}
      })
		console.log(app.locals.toTable)
	})
	res.render('ingame')
})
module.exports = app
