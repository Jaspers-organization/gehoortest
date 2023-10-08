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
        employee_number NVARCHAR(50) NULL,
        full_name NVARCHAR(50) NULL,
        email NVARCHAR(50) NOT NULL,
        password CHAR(64) NOT NULL,
        branch_id INT NOT NULL,
        active BIT NOT NULL DEFAULT 1,
        PRIMARY KEY (id),
        FOREIGN KEY (branch_id) REFERENCES branch (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'target_audience')
BEGIN
    CREATE TABLE target_audience (
        id INT NOT NULL IDENTITY(1,1),
        [from] TINYINT NULL,
        [to] TINYINT NULL,
        label NVARCHAR(50) NOT NULL,
        PRIMARY KEY (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'test')
BEGIN
    CREATE TABLE test (
        id INT NOT NULL IDENTITY(1,1),
        title NVARCHAR(50) NOT NULL,
        employee_id INT NOT NULL,
        target_audience_id INT NOT NULL,
		test_data NVARCHAR(MAX) NOT NULL,
        active BIT NOT NULL DEFAULT 0,
        PRIMARY KEY (id),
        FOREIGN KEY (employee_id) REFERENCES employee_login (id),
		FOREIGN KEY (target_audience_id) REFERENCES target_audience (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'client_login')
BEGIN
    CREATE TABLE client_login (
        id INT NOT NULL IDENTITY(1,1),
		full_name NVARCHAR(50) NULL,
        email NVARCHAR(50) NOT NULL,
        password CHAR(64) NOT NULL,
        PRIMARY KEY (id)
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'test_result')
BEGIN
    CREATE TABLE test_result (
        id INT NOT NULL IDENTITY(1,1),
        branch_id INT NOT NULL,
        client_id INT NULL,
        start_date_time DATETIME NOT NULL,
        duration INT NOT NULL,
        test_answers NVARCHAR(MAX) NOT NULL,
        PRIMARY KEY (id),
        FOREIGN KEY (branch_id) REFERENCES branch (id),
        FOREIGN KEY (client_id) REFERENCES client_login (id)
    );
END;