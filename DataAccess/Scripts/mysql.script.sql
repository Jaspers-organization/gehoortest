---------------------------------------
--            NOT UPDATED            --
---------------------------------------

CREATE SCHEMA IF NOT EXISTS `gehoortest` DEFAULT CHARACTER SET utf8;
USE `gehoortest` ;

CREATE TABLE IF NOT EXISTS `branch` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`name` VARCHAR(50) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE IF NOT EXISTS `employee_login` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`email` VARCHAR(50) NOT NULL,
	`password` CHAR(64) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE IF NOT EXISTS `employee` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`employee_number` VARCHAR(50) NULL,
	`first_name` VARCHAR(50) NOT NULL,
	`infix` VARCHAR(10) NULL,
	`last_name` VARCHAR(50) NOT NULL,
	`branch_id` INT NOT NULL,
	`employee_login_id` INT NOT NULL,
	`active` BIT NOT NULL DEFAULT 1,
	PRIMARY KEY (`id`),
    FOREIGN KEY (`branch_id`) REFERENCES `branch` (`id`),
    FOREIGN KEY (`employee_login_id`) REFERENCES `employee_login` (`id`)
);

CREATE TABLE IF NOT EXISTS `target_audience` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`from` TINYINT(2) UNSIGNED NULL,
	`to` TINYINT(2) UNSIGNED NULL,
	`label` VARCHAR(50) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE IF NOT EXISTS `test` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`title` VARCHAR(50) NOT NULL,
	`employee_id` INT NOT NULL,
	`target_audience_id` INT NOT NULL,
	`test_data` JSON NOT NULL,
	`active` BIT NOT NULL DEFAULT 0,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`employee_id`) REFERENCES `employee_login` (`id`),
	FOREIGN KEY (`target_audience_id`) REFERENCES `target_audience` (`id`)
);

CREATE TABLE IF NOT EXISTS `client_login` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`email` VARCHAR(50) NOT NULL,
	`password` CHAR(64) NOT NULL,
	PRIMARY KEY (`id`)
);

CREATE TABLE IF NOT EXISTS `client` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`first_name` VARCHAR(50) NULL,
	`infix` VARCHAR(10) NULL,
	`last_name` VARCHAR(50) NULL,
	`client_login_id` INT NOT NULL,
	PRIMARY KEY (`id`),
    FOREIGN KEY (`client_login_id`) REFERENCES `client_login` (`id`)
);

CREATE TABLE IF NOT EXISTS `test_result` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`branch_id` INT NOT NULL,
    `client_id` INT NULL,
	`start_date_time` DATETIME NOT NULL,
	`duration` INT NOT NULL,
	`test_answers` JSON NOT NULL,
	PRIMARY KEY (`id`),
	FOREIGN KEY (`branch_id`) REFERENCES `branch` (`id`),
	FOREIGN KEY (`client_id`) REFERENCES `client_login` (`id`)
);