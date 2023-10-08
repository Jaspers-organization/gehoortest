USE [gehoortest];

INSERT INTO [branch] VALUES
	('vestiging 1'),
	('vestiging 2');

DECLARE @branchId1 AS INT, @branchId2 AS INT; 
SELECT @branchId1 = [id] FROM [branch] WHERE [name] = 'vestiging 1';
SELECT @branchId2 = [id] FROM [branch] WHERE [name] = 'vestiging 2';

-- The password for all accounts is: Test1234!
<<<<<<< Updated upstream:gehoortest.application-Repository/Database Scripts/ssms.data.sql
INSERT INTO [employee] VALUES
	('#0001', 'medewerker 1', 'medewerker1@gehoortest.nl', '0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b', @branchId1, 1),
	('#0002', 'medewerker 2', 'medewerker2@gehoortest.nl', '0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b', @branchId2, 1);

DECLARE @employeeId1 AS INT, @employeeId2 AS INT; 
SELECT @employeeId1 = [id] FROM [employee] WHERE [email] = 'medewerker1@gehoortest.nl';
SELECT @employeeId2 = [id] FROM [employee] WHERE [email] = 'medewerker2@gehoortest.nl';
=======
INSERT INTO [employee_login] VALUES
	('medewerker1@gehoortest.nl', '0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b'),
	('medewerker2@gehoortest.nl', '0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b');

DECLARE @employeeLoginId1 AS INT, @employeeLoginId2 AS INT; 
SELECT @employeeLoginId1 = [id] FROM [employee_login] WHERE [email] = 'medewerker1@gehoortest.nl';
SELECT @employeeLoginId2 = [id] FROM [employee_login] WHERE [email] = 'medewerker2@gehoortest.nl';

INSERT INTO [employee] VALUES
	('#0001', 'medewerker', null, '1', @branchId1, @employeeLoginId1, 1),
	('#0002', 'medewerker', null, '2', @branchId2, @employeeLoginId2, 1);

DECLARE @employeeId1 AS INT, @employeeId2 AS INT; 
SELECT @employeeId1 = [id] FROM [employee] WHERE [first_name] = 'medewerker' AND [last_name] = '1';
SELECT @employeeId2 = [id] FROM [employee] WHERE [first_name] = 'medewerker' AND [last_name] = '2';
>>>>>>> Stashed changes:gehoortest.application-Repository/Database/ssms.data.sql

INSERT INTO [target_audience] VALUES  
  (60, 69, '60-69'),
  (70, 79, '70-79'),
  (80, 89, '80-89');

DECLARE @targetAudienceId1 AS INT, @targetAudienceId2 AS INT;
SELECT @targetAudienceId1 = [id] FROM [target_audience] WHERE [label] = '60-69';
SELECT @targetAudienceId2 = [id] FROM [target_audience] WHERE [label] = '70-79';

INSERT INTO [test] VALUES ('Gehoortest voor 60-69', @employeeId1, @targetAudienceId1, '[]', 1);
INSERT INTO [test] VALUES ('Gehoortest voor 70-79', @employeeId2, @targetAudienceId2, '[]', 1);

-- The password for all accounts is: Test1234!
<<<<<<< Updated upstream:gehoortest.application-Repository/Database Scripts/ssms.data.sql
INSERT INTO [client] VALUES 
	('client 1', 'client1@gehoortest.nl', '0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b'),
	('client 2', 'client2@gehoortest.nl', '0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b');

DECLARE @clientId1 AS INT, @clientId2 AS INT; 
SELECT @clientId1 = [id] FROM [client] WHERE [email] = 'client1@gehoortest.nl';
SELECT @clientId2 = [id] FROM [client] WHERE [email] = 'client2@gehoortest.nl';
=======
INSERT INTO [client_login] VALUES 
	('client1@gehoortest.nl', '0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b'),
	('client2@gehoortest.nl', '0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b');

DECLARE @clientLoginId1 AS INT, @clientLoginId2 AS INT; 
SELECT @clientLoginId1 = [id] FROM [client_login] WHERE [email] = 'client1@gehoortest.nl';
SELECT @clientLoginId2 = [id] FROM [client_login] WHERE [email] = 'client2@gehoortest.nl';

INSERT INTO [client] VALUES
	('client', null, '1', @clientLoginId1),
	('client', null, '2', @clientLoginId2);

DECLARE @clientId1 AS INT, @clientId2 AS INT; 
SELECT @clientId1 = [id] FROM [client] WHERE [first_name] = 'client' AND [last_name] = '1';
SELECT @clientId2 = [id] FROM [client] WHERE [first_name] = 'client' AND [last_name] = '2';
>>>>>>> Stashed changes:gehoortest.application-Repository/Database/ssms.data.sql

INSERT INTO [test_result] VALUES 
	(@branchId1, @clientId1, '2023-10-01 09:00:00', 130, '[]'),
	(@branchId1, null, '2023-10-02 09:30:00', 145, '[]'),
	(@branchId2, @clientId2, '2023-10-03 10:00:00', 90, '[]'),
	(@branchId2, @clientId2, '2023-10-04 11:00:00', 150, '[]');