var express = require('express')
var app = express()
 
app.get('/', function(req, res) {
    // render to views/index.ejs template file
    if (1!=1) {
        res.render('signin')
    } else {
        res.redirect('/dashboard')
    }
    
})

app.get('/startgame', function(req, res) {
    // render to views/index.ejs template file
    res.render('startgame')
    
})
 
/** 
 * We assign app object to module.exports
 * 
 * module.exports exposes the app object as a module
 * 
 * module.exports should be used to return the object 
 * when this file is required in another module like app.js
 */ 
module.exports = app;