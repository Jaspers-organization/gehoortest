USE [gehoortest];

INSERT INTO [branch] VALUES
	('vestiging 1'),
	('vestiging 2');

DECLARE @branchId1 AS INT, @branchId2 AS INT; 
SELECT @branchId1 = [id] FROM [branch] WHERE [name] = 'vestiging 1';
SELECT @branchId2 = [id] FROM [branch] WHERE [name] = 'vestiging 2';

INSERT INTO [employee_login] VALUES
	('medewerker@gehoortest.nl', '', 0),
	('administrator@gehoortest.nl', '', 1);

DECLARE @employeeLoginId1 AS INT, @employeeLoginId2 AS INT; 
SELECT @employeeLoginId1 = [id] FROM [employee_login] WHERE [email] = 'medewerker@gehoortest.nl';
SELECT @employeeLoginId2 = [id] FROM [employee_login] WHERE [email] = 'administrator@gehoortest.nl';

INSERT INTO [employee] VALUES
	(@employeeLoginId1, '#001', 'medewerker', NULL, 'klaas', 1),
	(@employeeLoginId2, '#002', 'administrator', NULL,  'anne', 1);

DECLARE @employeeId1 AS INT, @employeeId2 AS INT; 
SELECT @employeeId1 = [id] FROM [employee] WHERE [employee_number] = '#001';
SELECT @employeeId2 = [id] FROM [employee] WHERE [employee_number] = '#001';

INSERT INTO [target_audience] VALUES  
  (60, 69, '50-59'),
  (60, 69, '60-69'),
  (70, 79, '70-79');

DECLARE @targetAudienceId1 AS INT, @targetAudienceId2 AS INT;
SELECT @targetAudienceId1 = [id] FROM [target_audience] WHERE [label] = '50-59';
SELECT @targetAudienceId2 = [id] FROM [target_audience] WHERE [label] = '60-69';

INSERT INTO [test] VALUES 
	(@targetAudienceId1, @employeeId1, 'Gehoortest voor 50-59', 1),
	(@targetAudienceId2, @employeeId1, 'Gehoortest voor 60-69', 1);

DECLARE @testId1 AS INT, @testId2 AS INT; 
SELECT @testId1 = [id] FROM [test] WHERE [target_audience_id] = @targetAudienceId1;
SELECT @testId2 = [id] FROM [test] WHERE [target_audience_id] = @targetAudienceId2;

INSERT INTO [tone_audiometry_question] VALUES 
	(@testId1, 1, 250, 30),
	(@testId1, 2, 500, 30),
	(@testId1, 3, 1000, 30),
	(@testId1, 4, 2000, 30),
	(@testId1, 5, 4000, 30),
	(@testId1, 6, 8000, 30),
	(@testId2, 1, 250, 30),
	(@testId2, 2, 500, 30),
	(@testId2, 3, 1000, 30),
	(@testId2, 4, 2000, 30),
	(@testId2, 5, 4000, 30),
	(@testId2, 6, 8000, 30);

INSERT INTO [text_question] VALUES 
	(@testId1, 1, 'Wat voor werk doet u (of heeft u gedaan)?', 0, 1, NULL),
	(@testId1, 2, 'In wat voor omgeving woont u?', 0, 1, NULL),
	(@testId2, 1, 'Wat voor werk doet u (of heeft u gedaan)?', 0, 1, NULL),
	(@testId2, 2, 'In wat voor omgeving woont u?', 0, 1, NULL);

DECLARE @testQuestionId1 AS INT, @testQuestionId2 AS INT, @testQuestionId3 AS INT, @testQuestionId4 AS INT; 
SELECT @testQuestionId1 = [id] FROM [text_question] WHERE [test_id] = @testId1 AND [question_number] = 1;
SELECT @testQuestionId2 = [id] FROM [text_question] WHERE [test_id] = @testId1 AND [question_number] = 2;
SELECT @testQuestionId3 = [id] FROM [text_question] WHERE [test_id] = @testId2 AND [question_number] = 1;
SELECT @testQuestionId4 = [id] FROM [text_question] WHERE [test_id] = @testId2 AND [question_number] = 2;

INSERT INTO [text_question_option] VALUES 
	(@testQuestionId1, 'Kantoorbaan'),
	(@testQuestionId1, 'In de bouw'),
	(@testQuestionId1, 'Verkoper'),
	(@testQuestionId2, 'Grote stad'),
	(@testQuestionId2, 'Kleine stad'),
	(@testQuestionId2, 'Dorp'),
	(@testQuestionId3, 'Kantoorbaan'),
	(@testQuestionId3, 'In de bouw'),
	(@testQuestionId3, 'Verkoper'),
	(@testQuestionId4, 'Grote stad'),
	(@testQuestionId4, 'Kleine stad'),
	(@testQuestionId4, 'Dorp');

INSERT INTO [test_result] VALUES 
	(@targetAudienceId1, @branchId1, '2023-11-09 09:30:00', 180);

DECLARE @testResultId1 AS INT;
SELECT @testResultId1 = [id] FROM [test_result] WHERE [target_audience_id] = @targetAudienceId1;

INSERT INTO [tone_audiometry_question_result] VALUES 
	(@testResultId1, 250, 0, 30, 30),
	(@testResultId1, 500, 0, 30, 35),
	(@testResultId1, 1000, 0, 30, 30),
	(@testResultId1, 2000, 0, 30, 30),
	(@testResultId1, 4000, 0, 30, 35),
	(@testResultId1, 8000, 0, 30, 35),
	(@testResultId1, 250, 1, 30, 35),
	(@testResultId1, 500, 1, 30, 30),
	(@testResultId1, 1000, 1, 30, 30),
	(@testResultId1, 2000, 1, 30, 30),
	(@testResultId1, 4000, 1, 30, 30),
	(@testResultId1, 8000, 1, 30, 30);

INSERT INTO [text_question_result] VALUES 
	(@testResultId1, 'Wat voor werk doet u (of heeft u gedaan)?'),
	(@testResultId1, 'In wat voor omgeving woont u?');

DECLARE @testQuestionResultId1 AS INT, @testQuestionResultId2 AS INT; 
SELECT @testQuestionResultId1 = [id] FROM [text_question_result] WHERE [question] = 'Wat voor werk doet u (of heeft u gedaan)?';
SELECT @testQuestionResultId2 = [id] FROM [text_question_result] WHERE [question] = 'In wat voor omgeving woont u?';

INSERT INTO [text_question_option_result] VALUES 
	(@testQuestionResultId1, 'Kantoorbaan'),
	(@testQuestionResultId1, 'In de bouw'),
	(@testQuestionResultId1, 'Verkoper'),
	(@testQuestionResultId2, 'Grote stad'),
	(@testQuestionResultId2, 'Kleine stad'),
	(@testQuestionResultId2, 'Dorp');

INSERT INTO [text_question_answer_result] VALUES 
	(@testQuestionResultId1, 'Kantoorbaan'),
	(@testQuestionResultId2, 'Grote stad');