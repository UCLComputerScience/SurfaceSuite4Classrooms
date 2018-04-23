CREATE DATABASE IF NOT EXISTS ss4c_DB;
CREATE TABLE `student` (
  `student_id` int(11) NOT NULL AUTO_INCREMENT,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `scores` json NOT NULL,
  `score_times` json NOT NULL,
  UNIQUE KEY `student_id` (`student_id`)
)
CREATE TABLE `definition` (
  `definition_id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `word` varchar(10) NOT NULL DEFAULT '',
  `meaning` varchar(60) NOT NULL DEFAULT '',
  PRIMARY KEY (`definition_id`)
)