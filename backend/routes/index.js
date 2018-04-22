"use strict";
var express = require('express')
var app = express()

app.get('/', function (req, res) {
	// render to views/index.ejs template file
	if (1 != 1) {
		console.log('Served signin')
		res.render('signin')
	} else {
		res.redirect('/dashboard')
	}

})


module.exports = app
