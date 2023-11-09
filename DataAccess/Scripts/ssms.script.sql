IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'gehoortest')
BEGIN
	CREATE DATABASE gehoortest;
END
GO
	USE [gehoortest];
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'branch')
BEGIN
    CREATE TABLE branch (
        id INT NOT NULL IDENTITY(1,1),
        name NVARCHAR(50) NOT NULL,
        PRIMARY KEY (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'employee_login')
BEGIN
    CREATE TABLE employee_login (
        id INT NOT NULL IDENTITY(1,1),
        email NVARCHAR(50) NOT NULL,
        password NCHAR(64) NOT NULL,
        role INT NOT NULL,
        PRIMARY KEY (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'employee')
BEGIN
    CREATE TABLE employee (
        id INT NOT NULL IDENTITY(1,1),
        employee_login_id INT NOT NULL,
        employee_number NVARCHAR(50) NULL,
        first_name NVARCHAR(50) NOT NULL,
        infix NVARCHAR(10) NULL,
        last_name NVARCHAR(50) NOT NULL,
        active BIT NOT NULL DEFAULT 1,
        PRIMARY KEY (id),
        FOREIGN KEY (employee_login_id) REFERENCES employee_login (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'target_audience')
BEGIN
    CREATE TABLE target_audience (
        id INT NOT NULL IDENTITY(1,1),
        [from] INT NOT NULL,
        [to] INT NOT NULL,
        label NVARCHAR(50) NOT NULL,
        PRIMARY KEY (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'test')
BEGIN
    CREATE TABLE test (
        id INT NOT NULL IDENTITY(1,1),
        target_audience_id INT NOT NULL,
        employee_id INT NOT NULL,
        title NVARCHAR(50) NOT NULL,
        active BIT NOT NULL DEFAULT 0,
        PRIMARY KEY (id),
		FOREIGN KEY (target_audience_id) REFERENCES target_audience (id),
        FOREIGN KEY (employee_id) REFERENCES employee (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'tone_audiometry_question')
BEGIN
    CREATE TABLE tone_audiometry_question (
        id INT NOT NULL IDENTITY(1,1),
        test_id INT NOT NULL,
        question_number INT NOT NULL,
        frequecy INT NOT NULL,
        starting_decibels INT NOT NULL,
        PRIMARY KEY (id),
		FOREIGN KEY (test_id) REFERENCES test (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'text_question')
BEGIN
    CREATE TABLE text_question (
        id INT NOT NULL IDENTITY(1,1),
        test_id INT NOT NULL,
        question_number INT NOT NULL,
        question NVARCHAR(100) NOT NULL,
        is_multiple_select BIT NOT NULL DEFAULT 0,
        has_input_field BIT NOT NULL DEFAULT 0,
        [image] NVARCHAR(50) NULL,
        PRIMARY KEY (id),
		FOREIGN KEY (test_id) REFERENCES test (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'text_question_option')
BEGIN
    CREATE TABLE text_question_option (
        id INT NOT NULL IDENTITY(1,1),
        text_question_id INT NOT NULL,
        [option] NVARCHAR(50) NOT NULL,
        PRIMARY KEY (id),
		FOREIGN KEY (text_question_id) REFERENCES text_question (id),
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'test_result')
BEGIN
    CREATE TABLE test_result (
        id INT NOT NULL IDENTITY(1,1),
        target_audience_id INT NOT NULL,
        branch_id INT NOT NULL,
        test_date_time DATETIME NOT NULL,
        duration INT NOT NULL,
        PRIMARY KEY (id),
        FOREIGN KEY (target_audience_id) REFERENCES target_audience (id),
        FOREIGN KEY (branch_id) REFERENCES branch (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'tone_audiometry_question_result')
BEGIN
    CREATE TABLE tone_audiometry_question_result (
        id INT NOT NULL IDENTITY(1,1),
        test_result_id INT NOT NULL,
        frequecy INT NOT NULL,
		ear INT NOT NULL,
        starting_decibels INT NOT NULL,
		answer INT NOT NULL,
        PRIMARY KEY (id),
        FOREIGN KEY (test_result_id) REFERENCES test_result (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'text_question_result')
BEGIN
    CREATE TABLE text_question_result (
        id INT NOT NULL IDENTITY(1,1),
        test_result_id INT NOT NULL,
        question NVARCHAR(100) NOT NULL,
        PRIMARY KEY (id),
        FOREIGN KEY (test_result_id) REFERENCES test_result (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'text_question_option_result')
BEGIN
    CREATE TABLE text_question_option_result (
        id INT NOT NULL IDENTITY(1,1),
        test_question_result_id INT NOT NULL,
        [option] NVARCHAR(50) NOT NULL,
        PRIMARY KEY (id),
        FOREIGN KEY (test_question_result_id) REFERENCES text_question_result (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'text_question_answer_result')
BEGIN
    CREATE TABLE text_question_answer_result (
        id INT NOT NULL IDENTITY(1,1),
        test_question_result_id INT NOT NULL,
        answer NVARCHAR(50) NOT NULL,
        PRIMARY KEY (id),
        FOREIGN KEY (test_question_result_id) REFERENCES text_question_result (id)
    );
END;
