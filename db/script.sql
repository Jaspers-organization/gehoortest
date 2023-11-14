CREATE SCHEMA IF NOT EXISTS `gehoortest` DEFAULT CHARACTER SET utf8;
USE `gehoortest` ;

CREATE TABLE IF NOT EXISTS `branch` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`name` VARCHAR(50) NOT NULL,
	`deleted` BIT NOT NULL DEFAULT 0,
	PRIMARY KEY (`id`)
);

CREATE TABLE IF NOT EXISTS `employee_login` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`employee_number` VARCHAR(50) NULL,
	`full_name` VARCHAR(50) NULL,
	`email` VARCHAR(50) NOT NULL,
	`password` CHAR(64) NOT NULL,
	`active` BIT NOT NULL DEFAULT 1,
	PRIMARY KEY (`id`)
);

CREATE TABLE IF NOT EXISTS `employee_branch` (
	`employee_id` INT NOT NULL,
	`branch_id` INT NOT NULL,
	FOREIGN KEY (`employee_id`) REFERENCES `employee_login` (`id`),
	FOREIGN KEY (`branch_id`) REFERENCES `branch` (`id`)
);

CREATE TABLE IF NOT EXISTS `test` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`title` VARCHAR(50) NOT NULL,
	`employee_id` INT NOT NULL,
	`test_data` JSON NOT NULL,
	`active` BIT NOT NULL DEFAULT 0,
	`deleted` BIT NOT NULL DEFAULT 0,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`employee_id`) REFERENCES `employee_login` (`id`)
);

CREATE TABLE IF NOT EXISTS `target_audience` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`from` TINYINT(2) UNSIGNED NULL,
	`to` TINYINT(2) UNSIGNED NULL,
	`label` VARCHAR(50) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE IF NOT EXISTS `test_target_audience` (
	`test_id` INT NOT NULL,
	`target_audience_id` INT NOT NULL,
	FOREIGN KEY (`target_audience_id`) REFERENCES `target_audience` (`id`),
	FOREIGN KEY (`test_id`) REFERENCES `test` (`id`)
);

CREATE TABLE IF NOT EXISTS `client_login` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`email` VARCHAR(50) NOT NULL,
	`password` CHAR(64) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE IF NOT EXISTS `test_result` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`test_id` INT NOT NULL,
	`branch_id` INT NOT NULL,
    `client_id` INT NULL,
	`start_date_time` DATETIME NOT NULL,
	`test_duration` INT NOT NULL,
	`test_answers` JSON NOT NULL,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`test_id`) REFERENCES `test` (`id`),
	FOREIGN KEY (`branch_id`) REFERENCES `branch` (`id`),
	FOREIGN KEY (`client_id`) REFERENCES `client_login` (`id`)
);