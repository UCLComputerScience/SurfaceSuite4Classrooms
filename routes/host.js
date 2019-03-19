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
			var msg = []
			if (req.query.msg) msg = [req.query.msg]
			res.render('dashboard', {
				notification: msg,
				data: rows,
				first_name: '',
				last_name: '',
			})
		})
	})
})

// Add new post
app.post('/', function (req, res, next) {
	req.assert('first_name', 'Forename is invalid').isAlpha().isLength({min:1, max:20}) //Validate first_name
	req.assert('last_name', 'Surname is invalid').isAlpha().isLength({min:1, max:20}) //Validate last_name
	var errors = req.validationErrors()
	if (!errors) { //No errors were found.  Passed Validation!
		var student = {
			first_name: req.sanitize('first_name').escape().trim().toLowerCase(),
			last_name: req.sanitize('last_name').escape().trim().toLowerCase(),
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
		res.redirect('/dashboard/?msg=' + 200)
	} else {
		console.log('ERROR:', errors)
		var msg = encodeURIComponent(errors[0].msg)
		res.redirect('/dashboard/?msg=' + msg)
	}

})

// -------------------- PER STUDENTS -------------------- //

// Get student by id

app.get('/show/(:student_id)', function (req, res, next) {
	req.checkParams('student_id', 'student_id must be a number').isDecimal();
	var errors = req.validationErrors()
	if (!errors) {
		req.getConnection(function (error, conn) {
			conn.query('SELECT * FROM student WHERE student_id = ' + req.params.student_id,
				function (err, rows, fields) {
					if (err) throw (err)
					if (rows.length <= 0) {
						var msg = encodeURIComponent('No user found with that ID')
						res.redirect('/dashboard/?msg=' + msg)
					} else {
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
		var msg = encodeURIComponent(errors[0].msg)
		res.redirect('/dashboard/?msg=' + msg)
	}
})

// Delete Student by id
app.get('/student/delete/(:student_id)', function (req, res, next) {
	req.checkParams('student_id', 'student_id must be a number').isDecimal();
	var errors = req.validationErrors()
	if (!errors) {
		req.getConnection(function (error, conn) {
			conn.query('DELETE FROM student WHERE student_id = ' + req.params.student_id, function (err, result) {
				if(err) throw err
				res.redirect('/dashboard/?msg=' + 201)
			})
		})
	} else {
		var msg = encodeURIComponent(errors[0].msg)
		res.redirect('/dashboard/?msg=' + msg)
	}
})

// -------------------- ALL DEFINITIONS -------------------- //

// Get all definitions

app.get('/definition', function (req, res, next) {
	req.getConnection(function (error, conn) {
		conn.query('SELECT * FROM definition ORDER BY word ASC', function (err, rows, fields) {
			if (err) throw err
			var msg = []
			if (req.query.msg) msg = [req.query.msg]
			res.render('definitions', {
				notification: msg,
				definitions: rows,
				word: '',
				meaning: '',
			})
		})
	})
})

// Add new definition

app.post('/definition', function (req, res, next) {
	req.assert('word', 'Word is invalid (<9 chars)').isAlpha().isLength({min:1, max:9})
	req.assert('meaning', 'Meaning is invalid').isAscii().isLength({min:1, max:30})
	var errors = req.validationErrors()
	if (!errors) {
		var definition = {
			meaning: req.sanitize('meaning').escape().trim(),
			word: req.sanitize('word').escape().trim(),
		}
		req.getConnection(function (error, conn) {
			conn.query('INSERT INTO definition SET ?', definition, function (err, result) {
				if (err) throw err
				res.redirect('/dashboard/definition/?msg=' + 100)
			})
		})
	} else {
		var msg = encodeURIComponent(errors[0].msg)
		res.redirect('/dashboard/definition/?msg=' + msg)
	}
})

// Delete definition by id

app.get('/definition/delete/(:definition_id)', function (req, res, next) {
	req.checkParams('definition_id', 'student_id must be a number').isDecimal();
	var errors = req.validationErrors()
	if (!errors) {
		req.getConnection(function (error, conn) {
			conn.query('DELETE FROM definition WHERE definition_id = ' + req.params.definition_id, function (err, result) {
				if (err) throw err
			})
			res.redirect('/dashboard/definition/?msg=' + 101)
		})
	} else {
		var msg = encodeURIComponent(errors[0].msg)
		res.redirect('/dashboard/definition/?msg=' + msg)
	}
})

// -------------------- STARTGAME -------------------- //

app.get('/startgame', function (req, res, next) {
	req.getConnection(function (error, conn) {
		conn.query('SELECT * FROM student ORDER BY last_name ASC', function (err, rows, fields) {
			if (err) throw err
			var students = rows
			conn.query('SELECT * FROM definition ORDER BY word ASC', function (err, rows, fields) {
				if (err) throw err
				var msg = []
				if (req.query.msg) msg = [req.query.msg]
				res.render('startgame', {
					notification: msg,
					students: students,
					definitions: rows,
				})
			})
		})
	})
})

app.post('/startgame', function (req, res, next) {
	if (req.body.definitions && req.body.students) {
		if (req.body.definitions.length == 4 && req.body.students.length == 4) {
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
								app.locals.toTable['id'].push(rows[i].student_id)
								j++
							}
						}
		      })
			})
			res.render('ingame')
		}
	}
	var msg = encodeURIComponent('You must choose 4 Students and Words')
	res.redirect('/dashboard/startgame/?msg=' + msg)

})
module.exports = app
