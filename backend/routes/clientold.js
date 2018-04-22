"use strict";
var express = require('express');
var app = express();
var moment = require('moment');
var express = require('express');
var app = express();

app.get('/', function (req, res) {
  console.log('Sample request received from client');
  res.json('Sample request received');
});


app.get('/info', function (req, res, next) {
  req.getConnection(function (error, conn) {
    conn.query('SELECT * FROM student ORDER BY last_name ASC;', function (err, rows, fields) {
      //if(err) throw err
      console.log("There were", rows.length, 'rows found for get request');
      if (err) {
        res.json('error', err);
      } else {
        res.json([rows[0].first_name]);
      }
    });
  });
});

// EDIT score POST ACTION
app.post('/edit/(:student_id)', function (req, res, next) {
  req.checkBody('score', 'A score is required').notEmpty(); //Validate score
  var errors = req.validationErrors()
  if (!errors) {
    req.getConnection(function (error, conn) {
      conn.query('UPDATE student SET scores = JSON_MERGE(scores, \'' + req.body.score + '\') WHERE student_id = ' + req.params.student_id,
        function (err, result) {
          if (err) throw (err);
        })

      conn.query('UPDATE student SET score_times = JSON_MERGE(score_times,\''  + Date.now() + '\') WHERE student_id = ' + req.params.student_id,
        function (err, result) {
          if (err) throw (err);
        })
    })
    console.log('Appended score of', req.body.score, 'to student', req.params.student_id);
    res.redirect('/dashboard')
  } else console.log(errors);
});

module.exports = app;
