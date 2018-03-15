var express = require('express')
var app = express()
 
app.get('/', function(req, res) {
    // render to views/index.ejs template file
    if (1!=1) {
        console.log('Served signin')
        res.render('signin')
    } else {
        console.log('Served Dashboard')
        res.redirect('/dashboard')
    }
    
})


app.get('/startgame', function (req, res, next) {
    req.getConnection(function (error, conn) {
        conn.query('SELECT * FROM student ORDER BY last_name ASC;', function (err, rows, fields) {
            //if(err) throw err
            console.log("There were", rows.length, 'rows found for get request')
            if (err) {
                req.flash('error', err)
                res.render('startgame', {
                    data: '',
                    output: ''
                })
            } else {
                res.render('startgame', {
                    data: rows,
                    output: ''
                })
            }
        })
    })
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