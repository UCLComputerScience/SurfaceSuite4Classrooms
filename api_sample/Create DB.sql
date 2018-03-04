DROP DATABASE ss4c_DB;
CREATE DATABASE ss4c_DB;
USE ss4c_DB;
CREATE TABLE student (
	student_id BIGINT UNSIGNED NOT NULL UNIQUE,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
	score INT,
    teacher_id BIGINT UNSIGNED NOT NULL
);
CREATE TABLE teacher (
	teacher_id BIGINT UNSIGNED NOT NULL UNIQUE,
    first_name VARCHAR(255) NOT NULL,
    last_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE,
    pw VARCHAR(60) 
);

CREATE TRIGGER before_insert_students
	BEFORE INSERT ON student
	FOR EACH ROW
    SET NEW.student_id = UUID_SHORT();

CREATE TRIGGER before_insert_teachers
	BEFORE INSERT ON teacher
	FOR EACH ROW
    SET NEW.teacher_id = UUID_SHORT();