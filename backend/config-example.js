// You must create a `config.js` file in the same directory as this file

var config = {
  database: {
    host: 'your_host', // database host
    user: 'your_usrname', // your database username
    password: 'your_passwd', // your database password
    port: 3306, // default MySQL port
    db: 'your_db' // your database name
  },
  server: { // No need to change this unless db is not local
    host: '127.0.0.1',
    port: '3000'
  }
}

module.exports = config
