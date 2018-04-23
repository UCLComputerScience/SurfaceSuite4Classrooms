"use strict";
var express = require('express')
var app = express()

app.get('/info', function (req, res, next) {
  //Structure: app.locals.toTable = { "student1": "studentname", "student2": "studentname", "student3": "studentname", "student4": "studentname", "definition1": "definitionbody", "definition2": "definitionbody", "definition3": "definitionbody", "definition4": "definitionbody", "answer1": "answerbody", "answer2": "answerbody", "answer3": "answerbody", "answer4": "answerbody", "id":[1, 2, 3, 4]}
  console.log('Served info with length', app.locals.toTable.length)
  res.json(app.locals.toTable)
})

app.get('/edit', function (req, res, next) {
  app.locals.toTable = []
  console.log('Wiped toTable')
  res.json('toTable var wiped')
})

app.post('/edit/(:student_id)', function (req, res, next) {
  req.checkParams('student_id', 'student_id must be a number').isDecimal();
  req.checkBody('score', 'score must be a number').isDecimal();
  var errors = req.validationErrors()
  if (!errors) {
    req.getConnection(function (error, conn) {
      conn.query('UPDATE student SET scores = JSON_MERGE(scores, \'' + req.body.score + '\'), score_times = JSON_MERGE(score_times,\''  + Date.now() + '\') WHERE student_id = ' + req.params.student_id,
        function (err, result) {
          if (err) throw (err)
        })
      res.end();
    })
    console.log('Appended score of', req.body.score, 'to student', req.params.student_id)
    res.json('Appended score')
  } else {
    console.log(errors[0].msg + ' for dashboard/show/ route')
    res.json(errors[0].msg)
  }
})

module.exports = app
