---------------------------------------
--            NOT UPDATED            --
---------------------------------------

USE `gehoortest`;

INSERT INTO `branch` (`name`) VALUES 
	("vestiging 1"),
	("vestiging 2");

SET @branchId1 = (SELECT `id` FROM `branch` WHERE `name` = "vestiging 1");
SET @branchId2 = (SELECT `id` FROM `branch` WHERE `name` = "vestiging 2");

-- The password for all accounts is: Test1234!
INSERT INTO `employee_login` (`email`, `password`) VALUES 
	("medewerker1@gehoortest.nl", "0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b"),
	("medewerker2@gehoortest.nl", "0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b");

SET @employeeLoginId1 = (SELECT `id` FROM `employee_login` WHERE `email` = "medewerker1@gehoortest.nl");
SET @employeeLoginId2 = (SELECT `id` FROM `employee_login` WHERE `email` = "medewerker2@gehoortest.nl");

INSERT INTO `employee` (`employee_number`, `first_name`, `infix`, `last_name`, `branch_id`, `employee_login_id`, `active`) VALUES 
	("#0001", "medewerker", null, "1", @branchId1, @employeeLoginId1, 1),
	("#0002", "medewerker", null, "2", @branchId2, @employeeLoginId2, 1);

SET @employeeId1 = (SELECT `id` FROM `employee` WHERE `first_name` = "medewerker" AND `last_name` = "1");
SET @employeeId2 = (SELECT `id` FROM `employee` WHERE `first_name` = "medewerker" AND `last_name` = "2");

INSERT INTO `target_audience` (`from`, `to`, `label`) VALUES  
  (60, 69, "60-69"),
  (70, 79, "70-79"),
  (80, 89, "80-89");

SET @targetAudienceId1 = (SELECT `id` FROM `target_audience` WHERE `label` = "60-69");
SET @targetAudienceId2 = (SELECT `id` FROM `target_audience` WHERE `label` = "70-79");

INSERT INTO `test` (`title`, `employee_id`, `target_audience_id`, `test_data`, `active`) VALUES 
	("Gehoortest voor 60-69", @employeeId1, @targetAudienceId1, "[]", 1),
    ("Gehoortest voor 70-79", @employeeId2, @targetAudienceId2, "[]", 1);

-- The password for all accounts is: Test1234!
INSERT INTO `client_login` (`email`, `password`) VALUES 
	("client1@gehoortest.nl", "0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b"),
	("client2@gehoortest.nl", "0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b");

SET @clientLoginId1 = (SELECT `id` FROM `client_login` WHERE `email` = "client1@gehoortest.nl");
SET @clientLoginId2 = (SELECT `id` FROM `client_login` WHERE `email` = "client2@gehoortest.nl");

-- The password for all accounts is: Test1234!
INSERT INTO `client` (`first_name`, `infix`, `last_name`, `client_login_id`) VALUES 
	("client", null, "1", @clientLoginId1),
	("client", null, "2", @clientLoginId2);

SET @clientId1 = (SELECT `id` FROM `client` WHERE `first_name` = "client" AND `last_name` = "1");
SET @clientId2 = (SELECT `id` FROM `client` WHERE `first_name` = "client" AND `last_name` = "2");

INSERT INTO `test_result` (`branch_id`, `client_id`, `start_date_time`, `duration`, `test_answers`) VALUES 
	(@branchId1, @clientId1, "2023-10-01 09:00:00", 130, "[]"),
	(@branchId1, null, "2023-10-02 09:30:00", 145, "[]"),
	(@branchId2, @clientId2, "2023-10-03 10:00:00", 90, "[]"),
	(@branchId2, @clientId2, "2023-10-04 11:00:00", 150, "[]");