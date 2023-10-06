USE `gehoortest`;

INSERT INTO `branch` VALUES (1, "vestiging 1", 0);
INSERT INTO `branch` VALUES (2, "vestiging 2", 0);

-- The password for all accounts is: Test1234!
INSERT INTO `employee_login` VALUES (1, "#0001", "medewerker 1", "medewerker1@gehoortest.nl", "0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b", 1);
INSERT INTO `employee_login` VALUES (2, "#0002", "medewerker 2", "medewerker2@gehoortest.nl", "0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b", 1);

INSERT INTO `employee_branch` VALUES (1, 1);
INSERT INTO `employee_branch` VALUES (2, 1);
INSERT INTO `employee_branch` VALUES (2, 2);

INSERT INTO `test` VALUES (1, "Gehoortest voor 60-69", 1, 0, 0, "[]");
INSERT INTO `test` VALUES (2, "Gehoortest voor 70-79", 2, 0, 0, "[]");

INSERT INTO `target_audience` VALUES (1, 50, 59, "50-59");
INSERT INTO `target_audience` VALUES (2, 60, 69, "60-69");
INSERT INTO `target_audience` VALUES (3, 70, 79, "70-79");
INSERT INTO `target_audience` VALUES (4, 80, 89, "80-89");

INSERT INTO [gehoortest].[dbo].[target_audience] VALUES  
  (0, 18, '-18'),
  (19, 29, '19-29'),
  (30, 39, '30-39'),
  (40, 49, '40-49'),
  (50, 59, '50-59'),
  (60, 69, '60-69'),
  (70, 79, '70-79'),
  (80, 89, '80-89'),
  (90, 99, '90-99'),
  (100, 109, '100+');

INSERT INTO `test_target_audience` VALUES (1, 2);
INSERT INTO `test_target_audience` VALUES (2, 3);

-- The password for all accounts is: Test1234!
INSERT INTO `client_login` VALUES (1, "client1@gehoortest.nl", "0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b");
INSERT INTO `client_login` VALUES (2, "client2@gehoortest.nl", "0fadf52a4580cfebb99e61162139af3d3a6403c1d36b83e4962b721d1c8cbd0b");

INSERT INTO `test_result` VALUES (null, 1, 1, 1, "2023-10-01 09:00:00", 130, "[]");
INSERT INTO `test_result` VALUES (null, 1, 1, null, "2023-10-02 09:30:00", 145, "[]");
INSERT INTO `test_result` VALUES (null, 2, 2, 2, "2023-10-03 10:00:00", 90, "[]");
INSERT INTO `test_result` VALUES (null, 2, 2, 2, "2023-10-04 11:00:00", 150, "[]");