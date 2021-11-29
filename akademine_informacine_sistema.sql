-- phpMyAdmin SQL Dump
-- version 4.7.9
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Nov 28, 2021 at 09:11 PM
-- Server version: 5.7.21
-- PHP Version: 5.6.35

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `akademine_informacine_sistema`
--

-- --------------------------------------------------------

--
-- Table structure for table `administratorius`
--

DROP TABLE IF EXISTS `administratorius`;
CREATE TABLE IF NOT EXISTS `administratorius` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `prisijungimo_vardas` varchar(50) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  `slaptazodis` varchar(50) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `administratorius`
--

INSERT INTO `administratorius` (`id`, `prisijungimo_vardas`, `slaptazodis`) VALUES
(1, 'admin1', '123');

-- --------------------------------------------------------

--
-- Table structure for table `dalykas`
--

DROP TABLE IF EXISTS `dalykas`;
CREATE TABLE IF NOT EXISTS `dalykas` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `pavadinimas` varchar(200) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  `kodas` varchar(8) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `kodas` (`kodas`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `dalykas`
--

INSERT INTO `dalykas` (`id`, `pavadinimas`, `kodas`) VALUES
(1, 'Duomenų bazių projektavimas', 'P175B113'),
(2, 'Skaitiniai ir optimizavimo metodai', 'P000B001'),
(3, 'Informacijos sistemos', 'T120B042'),
(4, 'Objektinis programavimas', 'P000B017');

-- --------------------------------------------------------

--
-- Table structure for table `destytojas`
--

DROP TABLE IF EXISTS `destytojas`;
CREATE TABLE IF NOT EXISTS `destytojas` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `vardas` varchar(50) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  `pavarde` varchar(50) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  `prisijungimo_vardas` varchar(50) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  `slaptazodis` varchar(50) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `destytojas`
--

INSERT INTO `destytojas` (`id`, `vardas`, `pavarde`, `prisijungimo_vardas`, `slaptazodis`) VALUES
(1, 'Jonas', 'Jonaitis', 'd', '123'),
(2, 'Ignas', 'Ignatis', 'ignas', 'ignaitis');

-- --------------------------------------------------------

--
-- Table structure for table `destytojo_dalykas`
--

DROP TABLE IF EXISTS `destytojo_dalykas`;
CREATE TABLE IF NOT EXISTS `destytojo_dalykas` (
  `destytojo_id` int(10) UNSIGNED NOT NULL,
  `dalyko_id` int(10) UNSIGNED NOT NULL,
  KEY `dėstytojo_id` (`destytojo_id`),
  KEY `dalyko_id` (`dalyko_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `destytojo_dalykas`
--

INSERT INTO `destytojo_dalykas` (`destytojo_id`, `dalyko_id`) VALUES
(1, 3),
(1, 4),
(2, 1),
(1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `fakultetas`
--

DROP TABLE IF EXISTS `fakultetas`;
CREATE TABLE IF NOT EXISTS `fakultetas` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `pavadinimas` varchar(200) COLLATE utf8_lithuanian_ci NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `pavadinimas` (`pavadinimas`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Dumping data for table `fakultetas`
--

INSERT INTO `fakultetas` (`id`, `pavadinimas`) VALUES
(1, 'Elektronikos ir informatikos fakultetas');

-- --------------------------------------------------------

--
-- Table structure for table `grupe`
--

DROP TABLE IF EXISTS `grupe`;
CREATE TABLE IF NOT EXISTS `grupe` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `pavadinimas` varchar(10) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  `stojimo_metai` year(4) NOT NULL,
  `studiju_programos_id` int(10) UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  KEY `studiju_programos_id` (`studiju_programos_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `grupe`
--

INSERT INTO `grupe` (`id`, `pavadinimas`, `stojimo_metai`, `studiju_programos_id`) VALUES
(1, 'PI20B', 2020, 1),
(3, 'PI20A', 2020, 1);

-- --------------------------------------------------------

--
-- Table structure for table `grupes_dalykas`
--

DROP TABLE IF EXISTS `grupes_dalykas`;
CREATE TABLE IF NOT EXISTS `grupes_dalykas` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `dalyko_id` int(10) UNSIGNED NOT NULL,
  `grupes_id` int(10) UNSIGNED NOT NULL,
  `destytojo_id` int(10) UNSIGNED NOT NULL,
  `semestras` int(2) UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `dalyko_id_2` (`dalyko_id`,`grupes_id`),
  KEY `dalyko_id` (`dalyko_id`),
  KEY `grupes_id` (`grupes_id`),
  KEY `destytojas_id` (`destytojo_id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `grupes_dalykas`
--

INSERT INTO `grupes_dalykas` (`id`, `dalyko_id`, `grupes_id`, `destytojo_id`, `semestras`) VALUES
(1, 1, 1, 1, 1),
(2, 3, 1, 1, 2),
(3, 2, 3, 2, 1),
(14, 4, 3, 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `pazymys`
--

DROP TABLE IF EXISTS `pazymys`;
CREATE TABLE IF NOT EXISTS `pazymys` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `studento_id` int(10) UNSIGNED NOT NULL,
  `grupes_dalyko_id` int(10) UNSIGNED NOT NULL,
  `ivertinimas` int(2) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `studento_id_2` (`studento_id`,`grupes_dalyko_id`),
  KEY `studento_id` (`studento_id`),
  KEY `grupes_dalyko_id` (`grupes_dalyko_id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pazymys`
--

INSERT INTO `pazymys` (`id`, `studento_id`, `grupes_dalyko_id`, `ivertinimas`) VALUES
(1, 1, 1, 4),
(2, 2, 1, NULL),
(3, 1, 2, 8),
(4, 2, 2, 9),
(7, 3, 1, NULL),
(14, 3, 2, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `studentas`
--

DROP TABLE IF EXISTS `studentas`;
CREATE TABLE IF NOT EXISTS `studentas` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `vardas` varchar(50) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  `pavarde` varchar(50) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  `grupes_id` int(10) UNSIGNED DEFAULT NULL,
  `prisijungimo_vardas` varchar(50) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  `slaptazodis` varchar(50) CHARACTER SET utf8 COLLATE utf8_lithuanian_ci NOT NULL,
  PRIMARY KEY (`id`),
  KEY `grupes_id` (`grupes_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `studentas`
--

INSERT INTO `studentas` (`id`, `vardas`, `pavarde`, `grupes_id`, `prisijungimo_vardas`, `slaptazodis`) VALUES
(1, 'Petras', 'Petraitis', 1, 'petras', 'petraitis'),
(2, 'Laura', 'Lauraitė', 1, 's', '123'),
(3, 'Inga', 'Ingaite', NULL, 'Inga', 'Ingaite');

-- --------------------------------------------------------

--
-- Table structure for table `studiju_programa`
--

DROP TABLE IF EXISTS `studiju_programa`;
CREATE TABLE IF NOT EXISTS `studiju_programa` (
  `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT,
  `pavadinimas` varchar(200) COLLATE utf8_lithuanian_ci NOT NULL,
  `kodas` varchar(9) COLLATE utf8_lithuanian_ci NOT NULL,
  `fakulteto_id` int(10) UNSIGNED NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `kodas` (`kodas`),
  KEY `fakulteto_id` (`fakulteto_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COLLATE=utf8_lithuanian_ci;

--
-- Dumping data for table `studiju_programa`
--

INSERT INTO `studiju_programa` (`id`, `pavadinimas`, `kodas`, `fakulteto_id`) VALUES
(1, 'Programų sistemos', '6531BX028', 1);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `destytojo_dalykas`
--
ALTER TABLE `destytojo_dalykas`
  ADD CONSTRAINT `destytojo_dalykas_ibfk_1` FOREIGN KEY (`dalyko_id`) REFERENCES `dalykas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `destytojo_dalykas_ibfk_2` FOREIGN KEY (`destytojo_id`) REFERENCES `destytojas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `grupe`
--
ALTER TABLE `grupe`
  ADD CONSTRAINT `grupe_ibfk_1` FOREIGN KEY (`studiju_programos_id`) REFERENCES `studiju_programa` (`id`) ON UPDATE CASCADE;

--
-- Constraints for table `grupes_dalykas`
--
ALTER TABLE `grupes_dalykas`
  ADD CONSTRAINT `grupes_dalykas_ibfk_1` FOREIGN KEY (`grupes_id`) REFERENCES `grupe` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `grupes_dalykas_ibfk_2` FOREIGN KEY (`destytojo_id`) REFERENCES `destytojas` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `grupes_dalykas_ibfk_3` FOREIGN KEY (`dalyko_id`) REFERENCES `dalykas` (`id`) ON UPDATE CASCADE;

--
-- Constraints for table `pazymys`
--
ALTER TABLE `pazymys`
  ADD CONSTRAINT `pazymys_ibfk_1` FOREIGN KEY (`studento_id`) REFERENCES `studentas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `pazymys_ibfk_2` FOREIGN KEY (`grupes_dalyko_id`) REFERENCES `grupes_dalykas` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `studentas`
--
ALTER TABLE `studentas`
  ADD CONSTRAINT `studentas_ibfk_1` FOREIGN KEY (`grupes_id`) REFERENCES `grupe` (`id`) ON UPDATE CASCADE;

--
-- Constraints for table `studiju_programa`
--
ALTER TABLE `studiju_programa`
  ADD CONSTRAINT `studiju_programa_ibfk_1` FOREIGN KEY (`fakulteto_id`) REFERENCES `fakultetas` (`id`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
