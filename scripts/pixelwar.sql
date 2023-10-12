-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : jeu. 12 oct. 2023 à 13:54
-- Version du serveur : 5.7.36
-- Version de PHP : 7.4.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `mysql`
--

-- --------------------------------------------------------

--
-- Structure de la table `pixelwar`
--

DROP TABLE IF EXISTS `pixelwar`;
CREATE TABLE IF NOT EXISTS `pixelwar` (
  `x` int(11) NOT NULL,
  `y` int(11) NOT NULL,
  `color` varchar(20) NOT NULL,
  PRIMARY KEY (`x`,`y`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `pixelwar`
--

INSERT INTO `pixelwar` (`x`, `y`, `color`) VALUES
(21, 36, 'red'),
(24, 28, 'green'),
(53, 21, 'red'),
(49, 43, 'red'),
(38, 50, 'red'),
(29, 46, 'red'),
(21, 51, 'red'),
(7, 17, 'green'),
(10, 38, 'green'),
(19, 9, 'green');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
