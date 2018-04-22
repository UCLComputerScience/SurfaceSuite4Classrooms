"use strict";
var express = require('express')
var app = express()

app.get('/', function (req, res) {
  console.log('Sample request received from client')
  res.json('Sample request received')
})


app.get('/info', function (req, res, next) {
  //app.locals.toTable = { "student1": "Dean Mohamedally", "student2": "Zuka Murvanidze", "student3": "Prashan Karunakaran", "student4": "Adam Peace", "definition1": "What animal goes oink?", "definition2": "What animal goes meow?", "definition3": "What animal goes moo?", "definition4": "What animal goes neigh?", "answer1": "pig", "answer2": "cat", "answer3": "cow", "answer4": "horse", "id":[48, 49, 50, 51]}
  console.log('served info')
  res.json(app.locals.toTable)

})

app.post('/edit/(:student_id)', function (req, res, next) {
  //req.checkParams('Invalid Student ID').isFloat()
  //req.checkBody('score', 'A score is required').isFloat({ min: 0, max: 100 }) //Validate score
  var errors = req.validationErrors()
  if (1==1) {
    req.getConnection(function (error, conn) {
      conn.query('UPDATE student SET scores = JSON_MERGE(scores, \'' + req.body.score + '\'), score_times = JSON_MERGE(score_times,\''  + Date.now() + '\') WHERE student_id = ' + req.params.student_id,
        function (err, result) {
          if (err) throw (err)
        })
    })
    console.log('Appended score of', req.body.score, 'to student', req.params.student_id)
    res.json('Appended score')
  } else {
    console.log(errors[0].msg + ' for dashboard/show/ route')
    res.json(errors[0].msg)
  }
  app.locals.toTable = []
})

module.exports = app
