"use strict";
const path = require("path");
const express = require("express");
//const layout = require('express-layout')
const bodyParser = require("body-parser");
const routes = require("./routes");
const validator = require("express-validator");
const cookieParser = require("cookie-parser");
const session = require("express-session");
const flash = require("express-flash");
const helmet = require("helmet");
const methodOverride = require("method-override");
const app = express();

var sqlite3 = require("sqlite3").verbose();
var db = new sqlite3.Database("ss4c.db");
var check;

db.serialize(function() {
  db.run(
    "CREATE TABLE if not exists student (student_id INTEGER PRIMARY KEY ASC, first_name TEXT NOT NULL, last_name TEXT NOT NULL, scores json NOT NULL, score_times json NOT NULL)"
  ); /*
  var stmt = db.prepare("INSERT INTO student VALUES (?)");
  for (var i = 0; i < 10; i++) {
    stmt.run("User " + i, "Last" + i, "[50 * i]", "[12345]");
  }
  stmt.finalize();

  db.each("SELECT rowid AS id, info FROM user_info", function(err, row) {
    console.log(row.id + ": " + row.info);
  });*/
});

db.close();

// Set templating language
app.set("view engine", "ejs");
app.use("/", express.static(__dirname));

// Set routes
const index = require("./routes/index");
const host = require("./routes/host");
const client = require("./routes/client");

// Set up object to send to client devices
app.locals.toTable = [];
// Allow above variable to be used by routes
client.locals = app.locals;
host.locals = app.locals;

const middlewares = [
  helmet(),
  //layout(),
  express.static(path.join(__dirname, "public")),
  //  myConnection(mysql, dbOptions, "pool"),
  bodyParser.urlencoded({
    extended: true
  }),
  bodyParser.json(),
  validator(),
  cookieParser("super-secret-key"),
  session({
    secret: "super-secret-key",
    key: "super-secret-cookie",
    resave: false,
    saveUninitialized: false,
    cookie: { maxAge: 60000 }
  }),
  flash(),
  methodOverride(function(req) {
    if (req.body && typeof req.body === "object" && "_method" in req.body) {
      // look in urlencoded POST bodies and delete it
      var method = req.body._method;
      delete req.body._method;
      return method;
    }
  })
];

app.use(middlewares);
app.use("/", index);
app.use("/dashboard", host);
app.use("/client", client);

app.use((req, res, next) => {
  res.status(404).send("Turn around now! Nothing exists under this URL");
});

app.use((err, req, res, next) => {
  console.error(err.stack);
  res.status(500).send("Something broke!");
});

const port = 3000;
app.listen(port, function() {
  console.log("Server running at http://localhost:" + port);
});
