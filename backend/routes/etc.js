
/*
// SHOW EDIT USER FORM
app.get('/edit/(:student_id)', function(req, res, next){
    req.getConnection(function(error, conn) {
        conn.query('SELECT * FROM student WHERE student_id = ' + req.params.student_id, function(err, rows, fields) {
            if(err) throw err

            // if student not found
            if (rows.length <= 0) {
                req.flash('error', 'Student not found with student_id = ' + req.params.student_id)
                res.redirect('/student')
            }
            else { // if student found
                // render to views/student/edit.ejs template file
                res.render('student/edit', {
                    title: 'Edit Student',
                    //data: rows[0],
                    student_id: rows[0].student_id,
                    first_name: rows[0].first_name,
                    last_name: rows[0].last_name,
                    score: rows[0].score
                })
            }
        })
    })
})

// EDIT student POST ACTION
app.put('/edit/(:student_id)', function(req, res, next) {
    req.assert('first_name', 'A first first_name is required').notEmpty()           //Validate first_name
    req.assert('last_name', 'A last first_name is required').notEmpty()             //Validate last_name
    req.assert('score', 'A score is required').isEmail()  //Validate score

    var errors = req.validationErrors()

    if( !errors ) {   //No errors were found.  Passed Validation!

        var student = {
            first_name: req.sanitize('first_name').escape().trim(),
            first_name: req.sanitize('first_name').escape().trim(),
            score: req.sanitize('number').escape().trim()
        }

        req.getConnection(function(error, conn) {
            conn.query('UPDATE student SET ? WHERE student_id = ' + req.params.student_id, student, function(err, result) {
                //if(err) throw err
                if (err) {
                    req.flash('error', err)

                    // render to views/student/add.ejs
                    res.render('student/edit', {
                        title: 'Edit Student',
                        student_id: req.params.student_id,
                        first_name: req.body.first_name,
                        last_name: req.body.last_name,
                        score: req.body.score
                    })
                } else {
                    req.flash('success', 'Data updated successfully!')

                    // render to views/student/add.ejs
                    res.render('student/edit', {
                        title: 'Edit Student',
                        student_id: req.params.student_id,
                        first_name: req.body.first_name,
                        last_name: req.body.last_name,
                        score: req.body.score
                    })
                }
            })
        })
    }
    else {   //Display errors to student
        var error_msg = ''
        errors.forEach(function(error) {
            error_msg += error.msg + '<br>'
        })
        req.flash('error', error_msg)

        res.render('student/edit', {
            title: 'Edit Student',
            student_id: req.params.student_id,
            first_name: req.body.first_name,
            last_name: req.body.last_name,
            score: req.body.score
        })
    }
})
 */
