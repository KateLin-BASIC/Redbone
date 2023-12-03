CREATE DATABASE IF NOT EXISTS `redbone_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;

USE `redbone_db`;

CREATE TABLE IF NOT EXISTS `Image` (
    `id` int unsigned NOT NULL AUTO_INCREMENT,
    `location` text NOT NULL,
    `date` timestamp NOT NULL DEFAULT current_timestamp(),
    PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `Post` (
    `id` int NOT NULL AUTO_INCREMENT,
    `title` varchar(256) NOT NULL,
    `content` longtext NOT NULL,
    `date` timestamp NOT NULL DEFAULT current_timestamp(),
    `views` int NOT NULL DEFAULT 0,
    PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;