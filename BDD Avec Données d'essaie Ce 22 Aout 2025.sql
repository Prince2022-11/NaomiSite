-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le :  ven. 22 août 2025 à 07:14
-- Version du serveur :  10.1.38-MariaDB
-- Version de PHP :  7.3.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

DROP DATABASE IF EXISTS gespersonnel;
CREATE DATABASE IF NOT EXISTS gespersonnel;
USE gespersonnel;

--
-- Base de données :  `gespersonnel`
--

-- --------------------------------------------------------

--
-- Structure de la table `anneescol`
--

CREATE TABLE `anneescol` (
  `anneeScolaire` int(11) NOT NULL,
  `designation` varchar(30) NOT NULL,
  `etat` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `anneescol`
--

INSERT INTO `anneescol` (`anneeScolaire`, `designation`, `etat`) VALUES
(1, '2022-2023', 'Inactif'),
(2, '2023-2024', 'Inactif'),
(3, '2024-2025', 'Actif'),
(4, '2025-2026', 'Inactif'),
(5, '2026-2027', 'Inactif'),
(6, '2027-2028', 'Inactif');

-- --------------------------------------------------------

--
-- Structure de la table `attribution_horaire`
--

CREATE TABLE `attribution_horaire` (
  `idAttribution` int(11) NOT NULL,
  `coursAttribue` varchar(200) NOT NULL,
  `totalHeure` int(11) NOT NULL,
  `Matricule` varchar(30) NOT NULL,
  `Lundi` varchar(15) NOT NULL,
  `Mardi` varchar(15) NOT NULL,
  `Mercredi` varchar(15) NOT NULL,
  `Jeudi` varchar(15) NOT NULL,
  `Vendredi` varchar(15) NOT NULL,
  `Samedi` varchar(15) NOT NULL,
  `nbHlundi` int(11) NOT NULL,
  `nbHmardi` int(11) NOT NULL,
  `nbHmercredi` int(11) NOT NULL,
  `nbHjeudi` int(11) NOT NULL,
  `nbHvendredi` int(11) NOT NULL,
  `nbHsamedi` int(11) NOT NULL,
  `anneeScolaire` varchar(30) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `attribution_horaire`
--

INSERT INTO `attribution_horaire` (`idAttribution`, `coursAttribue`, `totalHeure`, `Matricule`, `Lundi`, `Mardi`, `Mercredi`, `Jeudi`, `Vendredi`, `Samedi`, `nbHlundi`, `nbHmardi`, `nbHmercredi`, `nbHjeudi`, `nbHvendredi`, `nbHsamedi`, `anneeScolaire`, `idEcole`) VALUES
(3, 'Mathématiques', 24, '0002/24-25', 'Oui', 'Oui', 'Oui', 'Oui', 'Oui', 'Non', 3, 7, 3, 7, 4, 0, '3', '3'),
(5, 'Musique', 13, '0001/24-25', 'Oui', 'Oui', 'Non', 'Non', 'Non', 'Oui', 7, 3, 0, 0, 0, 3, '3', '3'),
(7, 'Langues', 15, '0003/24-25', 'Oui', 'Oui', 'Oui', 'Oui', 'Oui', 'Non', 1, 2, 3, 4, 5, 0, '3', '3'),
(8, 'Psychologie_Methode de l-étudiant', 16, '0006/24-25', 'Oui', 'Oui', 'Non', 'Non', 'Oui', 'Non', 7, 2, 0, 0, 7, 0, '3', '3'),
(9, 'Ecopole_Chimie', 18, '0007/24-25', 'Oui', 'Non', 'Non', 'Oui', 'Oui', 'Non', 7, 0, 0, 6, 5, 0, '3', '3');

-- --------------------------------------------------------

--
-- Structure de la table `change_classe`
--

CREATE TABLE `change_classe` (
  `idClasse` int(11) NOT NULL,
  `matricule` varchar(20) NOT NULL,
  `nomEleve` varchar(40) NOT NULL,
  `prenom` varchar(30) NOT NULL,
  `sexe` text NOT NULL,
  `classe` varchar(30) NOT NULL,
  `optionEtude` varchar(30) NOT NULL,
  `anneeScolaire` varchar(20) NOT NULL,
  `idEcole` varchar(30) NOT NULL,
  `etat` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `change_classe`
--

INSERT INTO `change_classe` (`idClasse`, `matricule`, `nomEleve`, `prenom`, `sexe`, `classe`, `optionEtude`, `anneeScolaire`, `idEcole`, `etat`) VALUES
(1, '0001/24-25', 'IYUMA MBALE', 'HERITIER', 'Masculin', '12', '4', '3', '3', 'Actif'),
(2, '0002/24-25', 'NTAMUNTU NSHOBOLE', 'FRANCINE', 'Féminin', '24', '7', '3', '3', 'Inactif'),
(3, '0003/24-25', 'ELIAS NAMUJIMBO', 'MARDOCHE', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(5, '0005/24-25', 'RACHEL MWAMBA', 'ELODIE', 'Féminin', '24', '7', '3', '3', 'Inactif'),
(9, '0009/24-25', 'BULONZA MARUME', 'AURORE', 'Féminin', '24', '7', '3', '3', 'Inactif'),
(11, '0010/24-25', 'SIFA BAYONGWA', 'ASIFIWE', 'Féminin', '24', '7', '3', '3', 'Inactif'),
(12, '0012/24-25', 'SIFA BAYONGWA', 'ASIFIWE', 'Féminin', '1', '1', '3', '1', 'Inactif'),
(13, '0013/24-25', 'NTAMWENGE NZINE', 'GABRIELLA', 'Féminin', '27', '7', '3', '3', 'Inactif'),
(14, '0012/24-25', 'SIFA BAYONGWA', 'ASIFIWE', 'Féminin', '2', '1', '4', '1', 'Actif'),
(15, '0015/24-25', 'BARAKA NTAMUNTU ', 'FRANKLIN ', 'Masculin', '4', '2', '3', '2', 'Actif'),
(16, '0016/24-25', 'CEKANABO MAISHA', 'MASHEKA', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(37, '0013/24-25', 'MWENEWIRHU BISIMWA', 'JANVIER', 'Masculin', '24', '7', '3', '4', 'Inactif'),
(38, '0020/24-26', 'KENGE WITANE', 'MAISHA', 'F&#233;minin', '24', '7', '3', '3', 'Inactif'),
(39, '0017/24-25', 'ELISHA TOTOBANE', 'Josu&#233;', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(40, '0018/24-25', 'AKONKWA CIMANUKA', 'DIDIER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(41, '0019/24-25', 'ELIENGAMA NTIBONERA', 'ELIEZER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(42, '0014/24-25', 'IRAGI SHAMAVU', 'DIEUDONNE', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(43, '0013/24-25', 'MWENEWIRHU BISIMWA', 'JANVIER', 'Masculin', '24', '7', '3', '4', 'Inactif'),
(44, '0020/24-26', 'KENGE WITANE', 'MAISHA', 'F&#233;minin', '24', '7', '3', '3', 'Inactif'),
(45, '0017/24-25', 'ELISHA TOTOBANE', 'Josu&#233;', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(46, '0018/24-25', 'AKONKWA CIMANUKA', 'DIDIER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(47, '0019/24-25', 'ELIENGAMA NTIBONERA', 'ELIEZER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(48, '0014/24-25', 'IRAGI SHAMAVU', 'DIEUDONNE', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(49, '0013/24-25', 'MWENEWIRHU BISIMWA', 'JANVIER', 'Masculin', '24', '7', '3', '4', 'Inactif'),
(50, '0020/24-26', 'KENGE WITANE', 'MAISHA', 'F&#233;minin', '24', '7', '3', '3', 'Inactif'),
(51, '0017/24-25', 'ELISHA TOTOBANE', 'Josu&#233;', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(52, '0018/24-25', 'AKONKWA CIMANUKA', 'DIDIER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(53, '0019/24-25', 'ELIENGAMA NTIBONERA', 'ELIEZER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(54, '0014/24-25', 'IRAGI SHAMAVU', 'DIEUDONNE', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(55, '0013/24-25', 'MWENEWIRHU BISIMWA', 'JANVIER', 'Masculin', '24', '7', '3', '4', 'Inactif'),
(56, '0020/24-26', 'KENGE WITANE', 'MAISHA', 'F&#233;minin', '24', '7', '3', '3', 'Inactif'),
(57, '0017/24-25', 'ELISHA TOTOBANE', 'Josu&#233;', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(58, '0018/24-25', 'AKONKWA CIMANUKA', 'DIDIER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(59, '0019/24-25', 'ELIENGAMA NTIBONERA', 'ELIEZER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(60, '0014/24-25', 'IRAGI SHAMAVU', 'DIEUDONNE', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(61, '0013/24-25', 'MWENEWIRHU BISIMWA', 'JANVIER', 'Masculin', '24', '7', '3', '4', 'Inactif'),
(62, '0020/24-26', 'KENGE WITANE', 'MAISHA', 'F&#233;minin', '24', '7', '3', '3', 'Inactif'),
(63, '0017/24-25', 'ELISHA TOTOBANE', 'Josu&#233;', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(64, '0018/24-25', 'AKONKWA CIMANUKA', 'DIDIER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(65, '0019/24-25', 'ELIENGAMA NTIBONERA', 'ELIEZER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(66, '0014/24-25', 'IRAGI SHAMAVU', 'DIEUDONNE', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(67, '0013/24-25', 'MWENEWIRHU BISIMWA', 'JANVIER', 'Masculin', '24', '7', '3', '4', 'Actif'),
(68, '0020/24-26', 'KENGE WITANE', 'MAISHA', 'F&#233;minin', '24', '7', '3', '3', 'Inactif'),
(69, '0017/24-25', 'ELISHA TOTOBANE', 'Josu&#233;', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(70, '0018/24-25', 'AKONKWA CIMANUKA', 'DIDIER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(71, '0019/24-25', 'ELIENGAMA NTIBONERA', 'ELIEZER', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(72, '0014/24-25', 'IRAGI SHAMAVU', 'DIEUDONNE', 'Masculin', '24', '7', '3', '3', 'Inactif'),
(73, '0018/24-25', 'AKONKWA CIMANUKA', 'DIDIER', 'Masculin', '24', '7', '4', '3', 'Inactif'),
(74, '0009/24-25', 'BULONZA MARUME', 'AURORE', 'F&#233;minin', '24', '7', '4', '3', 'Inactif'),
(75, '0016/24-25', 'CEKANABO MAISHA', 'MASHEKA', 'Masculin', '24', '7', '4', '3', 'Inactif'),
(76, '0003/24-25', 'ELIAS NAMUJIMBO', 'MARDOCHE', 'Masculin', '24', '7', '4', '3', 'Inactif'),
(77, '0019/24-25', 'ELIENGAMA NTIBONERA', 'ELIEZER', 'Masculin', '24', '7', '4', '3', 'Inactif'),
(78, '0017/24-25', 'ELISHA TOTOBANE', 'Josu&amp;#233;', 'Masculin', '24', '7', '4', '3', 'Inactif'),
(79, '0014/24-25', 'IRAGI SHAMAVU', 'DIEUDONNE', 'Masculin', '24', '7', '4', '3', 'Inactif'),
(80, '0020/24-26', 'KENGE WITANE', 'MAISHA', 'F&amp;#233;minin', '24', '7', '4', '3', 'Inactif'),
(81, '0005/24-25', 'RACHEL MWAMBA', 'ELODIE', 'F&#233;minin', '24', '7', '4', '3', 'Inactif'),
(82, '0010/24-25', 'SIFA BAYONGWA', 'ASIFIWE', 'F&#233;minin', '24', '7', '4', '3', 'Inactif'),
(83, '0018/24-25', 'AKONKWA CIMANUKA', 'DIDIER', 'Masculin', '24', '7', '3', '3', 'Actif'),
(84, '0009/24-25', 'BULONZA MARUME', 'AURORE', 'F&#233;minin', '24', '7', '3', '3', 'Actif'),
(85, '0016/24-25', 'CEKANABO MAISHA', 'MASHEKA', 'Masculin', '24', '7', '3', '3', 'Actif'),
(86, '0003/24-25', 'ELIAS NAMUJIMBO', 'MARDOCHE', 'Masculin', '24', '7', '3', '3', 'Actif'),
(87, '0019/24-25', 'ELIENGAMA NTIBONERA', 'ELIEZER', 'Masculin', '24', '7', '3', '3', 'Actif'),
(88, '0017/24-25', 'ELISHA TOTOBANE', 'Josu&amp;#233;', 'Masculin', '24', '7', '3', '3', 'Actif'),
(89, '0014/24-25', 'IRAGI SHAMAVU', 'DIEUDONNE', 'Masculin', '24', '7', '3', '3', 'Actif'),
(90, '0020/24-26', 'KENGE WITANE', 'MAISHA', 'F&amp;#233;minin', '24', '7', '3', '3', 'Actif'),
(91, '0005/24-25', 'RACHEL MWAMBA', 'ELODIE', 'F&#233;minin', '24', '7', '3', '3', 'Actif'),
(92, '0010/24-25', 'SIFA BAYONGWA', 'ASIFIWE', 'F&#233;minin', '24', '7', '3', '3', 'Actif');

-- --------------------------------------------------------

--
-- Structure de la table `compte_agent`
--

CREATE TABLE `compte_agent` (
  `idCompte` int(11) NOT NULL,
  `Matricule` varchar(30) NOT NULL,
  `septembre` double NOT NULL,
  `octobre` double NOT NULL,
  `novembre` double NOT NULL,
  `decembre` double NOT NULL,
  `janvier` double NOT NULL,
  `fevrier` double NOT NULL,
  `mars` double NOT NULL,
  `avril` double NOT NULL,
  `mai` double NOT NULL,
  `juin` double NOT NULL,
  `avance` double NOT NULL,
  `anneeScolaire` varchar(20) NOT NULL,
  `idEcole` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `compte_agent`
--

INSERT INTO `compte_agent` (`idCompte`, `Matricule`, `septembre`, `octobre`, `novembre`, `decembre`, `janvier`, `fevrier`, `mars`, `avril`, `mai`, `juin`, `avance`, `anneeScolaire`, `idEcole`) VALUES
(1, '0001/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '3', '3'),
(2, '0002/24-25', 18, 85.15, 0, 0, 0, 0, 0, 0, 0, 0, 0, '3', '3'),
(3, '0003/24-25', 0, 41.125, 0, 0, 0, 0, 0, 0, 0, 0, 0, '3', '3'),
(4, '0004/24-25', 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 17.2, '3', '1'),
(5, '0005/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '3', '2'),
(6, '0006/24-25', 8.125, 8.125, 0, 0, 0, 0, 0, 0, 0, 0, 10, '3', '3'),
(7, '0007/24-25', 0, 6.125, 0, 0, 0, 0, 0, 0, 0, 0, 10, '3', '3'),
(8, '0006/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, '4', '3'),
(9, '0002/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, '4', '3');

-- --------------------------------------------------------

--
-- Structure de la table `compte_eleve`
--

CREATE TABLE `compte_eleve` (
  `idCompte` int(11) NOT NULL,
  `Matricule` varchar(30) NOT NULL,
  `septembre` double NOT NULL,
  `octobre` double NOT NULL,
  `novembre` double NOT NULL,
  `decembre` double NOT NULL,
  `janvier` double NOT NULL,
  `fevrier` double NOT NULL,
  `mars` double NOT NULL,
  `avril` double NOT NULL,
  `mai` double NOT NULL,
  `juin` double NOT NULL,
  `anneeScolaire` varchar(30) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `compte_eleve`
--

INSERT INTO `compte_eleve` (`idCompte`, `Matricule`, `septembre`, `octobre`, `novembre`, `decembre`, `janvier`, `fevrier`, `mars`, `avril`, `mai`, `juin`, `anneeScolaire`, `idEcole`) VALUES
(1, '0001/24-25', 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 1.5, '3', '3'),
(2, '0002/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '3', '3'),
(3, '0003/24-25', 6.5, 6.5, 6.5, 6.5, 2, 0, 0, 0, 0, 0, '3', '3'),
(4, '0005/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '3', '3'),
(6, '0002/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '4', '3'),
(7, '0009/24-25', 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 6, '3', '3'),
(8, '0010/24-25', 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 6.5, 6, 0, '3', '3'),
(9, '0010/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '4', '3'),
(10, '0012/24-25', 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, '3', '1'),
(11, '0013/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '3', '3'),
(12, '0012/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '4', '1'),
(13, '0015/24-25', 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, '3', '2'),
(14, '0016/24-25', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, '3', '3');

-- --------------------------------------------------------

--
-- Structure de la table `depense`
--

CREATE TABLE `depense` (
  `idDepense` int(11) NOT NULL,
  `dateDepense` varchar(20) NOT NULL,
  `designation` varchar(100) NOT NULL,
  `montant` varchar(20) NOT NULL,
  `unite` varchar(10) NOT NULL,
  `anneeScolaire` varchar(30) NOT NULL,
  `idOperateur` varchar(30) NOT NULL,
  `idEcole` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `depense`
--

INSERT INTO `depense` (`idDepense`, `dateDepense`, `designation`, `montant`, `unite`, `anneeScolaire`, `idOperateur`, `idEcole`) VALUES
(2, '20/07/2025', 'Achat craies', '10000', 'CDF', '3', '1', '3'),
(3, '20/07/2025', 'Achat Lattes', '8000', 'CDF', '3', '1', '3'),
(4, '22/07/2025', 'Achat Stylo', '10', 'USD', '3', '1', '3'),
(5, '22/07/2025', 'Transport', '3', 'USD', '3', '1', '3'),
(8, '26/07/2025', 'Craie', '0,8', 'USD', '3', '1', '3'),
(9, '26/07/2025', 'Transport', '2,6', 'USD', '3', '1', '3'),
(10, '20/08/2025', 'Transport', '1000', 'CDF', '3', '8', '3');

-- --------------------------------------------------------

--
-- Structure de la table `ecole`
--

CREATE TABLE `ecole` (
  `idEcole` int(11) NOT NULL,
  `nomEcole` varchar(100) NOT NULL,
  `description` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `ecole`
--

INSERT INTO `ecole` (`idEcole`, `nomEcole`, `description`) VALUES
(1, 'MATERNELLE', 'Tous les élèves'),
(2, 'PRIMAIRE', 'Tous les élèves du Primaire'),
(3, 'SECONDAIRE', 'Tous les élèves du secondaire');

-- --------------------------------------------------------

--
-- Structure de la table `frais_scolaire`
--

CREATE TABLE `frais_scolaire` (
  `idfrais` int(11) NOT NULL,
  `designation` varchar(50) NOT NULL,
  `tranche1` double NOT NULL,
  `tranche2` double NOT NULL,
  `tranche3` double NOT NULL,
  `unite` varchar(10) NOT NULL,
  `classe` varchar(30) NOT NULL,
  `optionConcerne` varchar(30) NOT NULL,
  `anneescolaire` varchar(30) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `frais_scolaire`
--

INSERT INTO `frais_scolaire` (`idfrais`, `designation`, `tranche1`, `tranche2`, `tranche3`, `unite`, `classe`, `optionConcerne`, `anneescolaire`, `idEcole`) VALUES
(1, 'Reprographie', 1000, 1000, 1000, 'CDF', '10', '3', '3', '3'),
(2, 'Reprographie', 5000, 5000, 0, 'CDF', '11', '3', '3', '3'),
(3, 'Reprographie', 5000, 5000, 0, 'CDF', '12', '4', '3', '3'),
(4, 'Reprographie', 5000, 5000, 0, 'CDF', '13', '4', '3', '3'),
(5, 'Reprographie', 5000, 5000, 0, 'CDF', '14', '4', '3', '3'),
(6, 'Reprographie', 5000, 5000, 0, 'CDF', '15', '4', '3', '3'),
(7, 'Reprographie', 5000, 5000, 0, 'CDF', '16', '5', '3', '3'),
(8, 'Reprographie', 5000, 5000, 0, 'CDF', '17', '5', '3', '3'),
(9, 'Reprographie', 5000, 5000, 0, 'CDF', '18', '5', '3', '3'),
(10, 'Reprographie', 5000, 5000, 0, 'CDF', '19', '5', '3', '3'),
(11, 'Reprographie', 5000, 5000, 0, 'CDF', '20', '6', '3', '3'),
(12, 'Reprographie', 5000, 5000, 0, 'CDF', '21', '6', '3', '3'),
(13, 'Reprographie', 5000, 5000, 0, 'CDF', '22', '6', '3', '3'),
(14, 'Reprographie', 5000, 5000, 0, 'CDF', '23', '6', '3', '3'),
(15, 'Reprographie', 5000, 5000, 0, 'CDF', '24', '7', '3', '3'),
(16, 'Reprographie', 5000, 5000, 0, 'CDF', '25', '7', '3', '3'),
(17, 'Reprographie', 5000, 5000, 0, 'CDF', '26', '7', '3', '3'),
(18, 'Reprographie', 5000, 5000, 0, 'CDF', '27', '7', '3', '3'),
(19, 'Reprographie', 5000, 5000, 0, 'CDF', '28', '8', '3', '3'),
(20, 'Reprographie', 5000, 5000, 0, 'CDF', '29', '8', '3', '3'),
(21, 'Reprographie', 5000, 5000, 0, 'CDF', '30', '8', '3', '3'),
(22, 'Reprographie', 5000, 5000, 0, 'CDF', '31', '8', '3', '3'),
(23, 'Reprographie', 5000, 5000, 0, 'CDF', '32', '9', '3', '3'),
(24, 'Reprographie', 5000, 5000, 0, 'CDF', '33', '9', '3', '3'),
(25, 'Reprographie', 5000, 5000, 0, 'CDF', '34', '9', '3', '3'),
(26, 'Reprographie', 5000, 5000, 0, 'CDF', '35', '9', '3', '3'),
(27, 'Reprographie', 5000, 5000, 0, 'CDF', '36', '10', '3', '3'),
(28, 'Reprographie', 5000, 5000, 0, 'CDF', '37', '10', '3', '3'),
(29, 'Reprographie', 5000, 5000, 0, 'CDF', '38', '10', '3', '3'),
(30, 'Reprographie', 5000, 5000, 0, 'CDF', '39', '10', '3', '3'),
(31, 'Reprographie', 5000, 5000, 0, 'CDF', '40', '11', '3', '3'),
(32, 'Reprographie', 5000, 5000, 0, 'CDF', '41', '11', '3', '3'),
(33, 'Reprographie', 5000, 5000, 0, 'CDF', '42', '11', '3', '3'),
(34, 'Reprographie', 5000, 5000, 0, 'CDF', '43', '11', '3', '3'),
(35, 'Frais Stage', 10, 10, 0, 'USD', '15', '4', '3', '3'),
(36, 'Frais scolaires', 30, 20, 15, 'USD', '10', '3', '3', '3'),
(37, 'Frais scolaires', 30, 20, 15, 'USD', '11', '3', '3', '3'),
(38, 'Frais scolaires', 30, 20, 15, 'USD', '12', '4', '3', '3'),
(39, 'Frais scolaires', 30, 20, 15, 'USD', '13', '4', '3', '3'),
(40, 'Frais scolaires', 30, 20, 15, 'USD', '14', '4', '3', '3'),
(41, 'Frais scolaires', 30, 20, 15, 'USD', '15', '4', '3', '3'),
(42, 'Frais scolaires', 30, 20, 15, 'USD', '16', '5', '3', '3'),
(43, 'Frais scolaires', 30, 20, 15, 'USD', '17', '5', '3', '3'),
(44, 'Frais scolaires', 30, 20, 15, 'USD', '18', '5', '3', '3'),
(45, 'Frais scolaires', 30, 20, 15, 'USD', '19', '5', '3', '3'),
(46, 'Frais scolaires', 30, 20, 15, 'USD', '20', '6', '3', '3'),
(47, 'Frais scolaires', 30, 20, 15, 'USD', '21', '6', '3', '3'),
(48, 'Frais scolaires', 30, 20, 15, 'USD', '22', '6', '3', '3'),
(49, 'Frais scolaires', 30, 20, 15, 'USD', '23', '6', '3', '3'),
(50, 'Frais scolaires', 30, 20, 15, 'USD', '24', '7', '3', '3'),
(51, 'Frais scolaires', 30, 20, 15, 'USD', '25', '7', '3', '3'),
(52, 'Frais scolaires', 30, 20, 15, 'USD', '26', '7', '3', '3'),
(53, 'Frais scolaires', 30, 20, 15, 'USD', '27', '7', '3', '3'),
(54, 'Frais scolaires', 30, 20, 15, 'USD', '28', '8', '3', '3'),
(55, 'Frais scolaires', 30, 20, 15, 'USD', '29', '8', '3', '3'),
(56, 'Frais scolaires', 30, 20, 15, 'USD', '30', '8', '3', '3'),
(57, 'Frais scolaires', 30, 20, 15, 'USD', '31', '8', '3', '3'),
(58, 'Frais scolaires', 30, 20, 15, 'USD', '32', '9', '3', '3'),
(59, 'Frais scolaires', 30, 20, 15, 'USD', '33', '9', '3', '3'),
(60, 'Frais scolaires', 30, 20, 15, 'USD', '34', '9', '3', '3'),
(61, 'Frais scolaires', 30, 20, 15, 'USD', '35', '9', '3', '3'),
(62, 'Frais scolaires', 30, 20, 15, 'USD', '36', '10', '3', '3'),
(63, 'Frais scolaires', 30, 20, 15, 'USD', '37', '10', '3', '3'),
(64, 'Frais scolaires', 30, 20, 15, 'USD', '38', '10', '3', '3'),
(65, 'Frais scolaires', 30, 20, 15, 'USD', '39', '10', '3', '3'),
(66, 'Frais scolaires', 30, 20, 15, 'USD', '40', '11', '3', '3'),
(67, 'Frais scolaires', 30, 20, 15, 'USD', '41', '11', '3', '3'),
(68, 'Frais scolaires', 30, 20, 15, 'USD', '42', '11', '3', '3'),
(69, 'Frais scolaires', 30, 20, 15, 'USD', '43', '11', '3', '3'),
(70, 'Participation Exetat', 42, 0, 0, 'USD', '15', '4', '3', '3'),
(71, 'Participation Exetat', 42, 0, 0, 'USD', '19', '5', '3', '3'),
(72, 'Participation Exetat', 42, 0, 0, 'USD', '23', '6', '3', '3'),
(73, 'Participation Exetat', 42, 0, 0, 'USD', '27', '7', '3', '3'),
(74, 'Participation Exetat', 42, 0, 0, 'USD', '31', '8', '3', '3'),
(75, 'Participation Exetat', 40, 0, 0, 'USD', '35', '9', '3', '3'),
(76, 'Participation Exetat', 42, 0, 0, 'USD', '39', '10', '3', '3'),
(77, 'Participation Exetat', 42, 0, 0, 'USD', '43', '11', '3', '3'),
(78, 'Pratique', 5, 5, 0, 'USD', '24', '7', '3', '3'),
(79, 'Pratique', 5, 5, 0, 'USD', '25', '7', '3', '3'),
(80, 'Pratique', 5, 5, 0, 'USD', '26', '7', '3', '3'),
(81, 'Pratique', 5, 5, 0, 'USD', '27', '7', '3', '3'),
(88, 'Frais scolaires', 20.7, 18.3, 11, 'USD', '1', '1', '3', '1'),
(89, 'Frais scolaires', 20.7, 18.3, 11, 'USD', '2', '1', '3', '1'),
(90, 'Frais scolaires', 20.7, 18.3, 11, 'USD', '3', '1', '3', '1'),
(91, 'Uniformes', 25000, 20000, 0, 'CDF', '1', '1', '3', '1'),
(92, 'Uniformes', 25000, 20000, 0, 'CDF', '2', '1', '3', '1'),
(93, 'Uniformes', 25000, 20000, 0, 'CDF', '3', '1', '3', '1'),
(95, 'Bulletin', 5000, 0, 0, 'CDF', '10', '3', '3', '3'),
(96, 'Bulletin', 5000, 0, 0, 'CDF', '11', '3', '3', '3'),
(97, 'Bulletin', 5000, 0, 0, 'CDF', '12', '4', '3', '3'),
(98, 'Bulletin', 5000, 0, 0, 'CDF', '13', '4', '3', '3'),
(99, 'Bulletin', 5000, 0, 0, 'CDF', '14', '4', '3', '3'),
(100, 'Bulletin', 5000, 0, 0, 'CDF', '15', '4', '3', '3'),
(101, 'Bulletin', 5000, 0, 0, 'CDF', '16', '5', '3', '3'),
(102, 'Bulletin', 5000, 0, 0, 'CDF', '17', '5', '3', '3'),
(103, 'Bulletin', 5000, 0, 0, 'CDF', '18', '5', '3', '3'),
(104, 'Bulletin', 5000, 0, 0, 'CDF', '19', '5', '3', '3'),
(105, 'Bulletin', 5000, 0, 0, 'CDF', '20', '6', '3', '3'),
(106, 'Bulletin', 5000, 0, 0, 'CDF', '21', '6', '3', '3'),
(107, 'Bulletin', 5000, 0, 0, 'CDF', '22', '6', '3', '3'),
(108, 'Bulletin', 5000, 0, 0, 'CDF', '23', '6', '3', '3'),
(109, 'Bulletin', 5000, 0, 0, 'CDF', '24', '7', '3', '3'),
(110, 'Bulletin', 5000, 0, 0, 'CDF', '25', '7', '3', '3'),
(111, 'Bulletin', 5000, 0, 0, 'CDF', '26', '7', '3', '3'),
(112, 'Bulletin', 5000, 0, 0, 'CDF', '27', '7', '3', '3'),
(113, 'Bulletin', 5000, 0, 0, 'CDF', '28', '8', '3', '3'),
(114, 'Bulletin', 5000, 0, 0, 'CDF', '29', '8', '3', '3'),
(115, 'Bulletin', 5000, 0, 0, 'CDF', '30', '8', '3', '3'),
(116, 'Bulletin', 5000, 0, 0, 'CDF', '31', '8', '3', '3'),
(117, 'Bulletin', 5000, 0, 0, 'CDF', '32', '9', '3', '3'),
(118, 'Bulletin', 5000, 0, 0, 'CDF', '33', '9', '3', '3'),
(119, 'Bulletin', 5000, 0, 0, 'CDF', '34', '9', '3', '3'),
(120, 'Bulletin', 5000, 0, 0, 'CDF', '35', '9', '3', '3'),
(121, 'Bulletin', 5000, 0, 0, 'CDF', '36', '10', '3', '3'),
(122, 'Bulletin', 5000, 0, 0, 'CDF', '37', '10', '3', '3'),
(123, 'Bulletin', 5000, 0, 0, 'CDF', '38', '10', '3', '3'),
(124, 'Bulletin', 5000, 0, 0, 'CDF', '39', '10', '3', '3'),
(125, 'Bulletin', 5000, 0, 0, 'CDF', '40', '11', '3', '3'),
(126, 'Bulletin', 5000, 0, 0, 'CDF', '41', '11', '3', '3'),
(127, 'Bulletin', 5000, 0, 0, 'CDF', '42', '11', '3', '3'),
(128, 'Bulletin', 5000, 0, 0, 'CDF', '43', '11', '3', '3'),
(129, 'Souches', 16000, 0, 0, 'CDF', '15', '4', '3', '3'),
(130, 'Souches', 16000, 0, 0, 'CDF', '19', '5', '3', '3'),
(131, 'Souches', 16000, 0, 0, 'CDF', '23', '6', '3', '3'),
(132, 'Souches', 16000, 0, 0, 'CDF', '27', '7', '3', '3'),
(133, 'Souches', 16000, 0, 0, 'CDF', '31', '8', '3', '3'),
(134, 'Souches', 16000, 0, 0, 'CDF', '35', '9', '3', '3'),
(135, 'Souches', 16000, 0, 0, 'CDF', '39', '10', '3', '3'),
(136, 'Souches', 16000, 0, 0, 'CDF', '43', '11', '3', '3'),
(137, 'Frais scolaires', 15, 10, 5, 'USD', '4', '2', '3', '2'),
(138, 'Frais scolaires', 15, 10, 5, 'USD', '5', '2', '3', '2'),
(139, 'Frais scolaires', 15, 10, 5, 'USD', '6', '2', '3', '2'),
(140, 'Frais scolaires', 15, 10, 5, 'USD', '7', '2', '3', '2'),
(141, 'Frais scolaires', 15, 10, 5, 'USD', '8', '2', '3', '2'),
(142, 'Frais scolaires', 15, 10, 5, 'USD', '9', '2', '3', '2'),
(143, 'Labo Informatique', 10000, 0, 0, 'CDF', '1', '1', '3', '1'),
(144, 'Labo Informatique', 10000, 0, 0, 'CDF', '2', '1', '3', '1'),
(145, 'Labo Informatique', 10000, 0, 0, 'CDF', '3', '1', '3', '1');

-- --------------------------------------------------------

--
-- Structure de la table `recu_payement`
--

CREATE TABLE `recu_payement` (
  `idRecu` varchar(30) NOT NULL,
  `datePayement` varchar(30) NOT NULL,
  `matricule` varchar(15) NOT NULL,
  `idEcole` varchar(15) NOT NULL,
  `fraisPaye` varchar(15) NOT NULL,
  `unite` varchar(30) NOT NULL,
  `idAnnee` varchar(15) NOT NULL,
  `prevuT1` varchar(15) NOT NULL,
  `prevuT2` varchar(15) NOT NULL,
  `prevuT3` varchar(15) NOT NULL,
  `payeT1` varchar(15) NOT NULL,
  `payeT2` varchar(15) NOT NULL,
  `payeT3` varchar(15) NOT NULL,
  `reste` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `recu_payement`
--

INSERT INTO `recu_payement` (`idRecu`, `datePayement`, `matricule`, `idEcole`, `fraisPaye`, `unite`, `idAnnee`, `prevuT1`, `prevuT2`, `prevuT3`, `payeT1`, `payeT2`, `payeT3`, `reste`) VALUES
('128-0001/24-25', '06/27/2025', '0001/24-25', '3', 'Bulletin', 'CDF', '3', '5000', '0', '0', '4900', '0', '0', '100 CDF'),
('129-0001/24-25', '06/27/2025', '0001/24-25', '3', 'Bulletin', 'CDF', '3', '5000', '0', '0', '5000', '0', '0', '0 CDF'),
('130-0010/24-25', '06/27/2025', '0010/24-25', '3', 'Frais scolaires', 'USD', '3', '30', '20', '15', '30', '20', '8', '7 USD'),
('131-0010/24-25', '07/29/2025', '0010/24-25', '3', 'Bulletin', 'CDF', '3', '5000', '0', '0', '3000', '0', '0', '2000 CDF'),
('132-0010/24-25', '07/29/2025', '0010/24-25', '3', 'Bulletin', 'CDF', '3', '5000', '0', '0', '4500', '0', '0', '500 CDF'),
('133-0009/24-25', '08/18/2025', '0009/24-25', '3', 'Bulletin', 'CDF', '3', '5000', '0', '0', '3000', '0', '0', '2000 CDF'),
('135-0009/24-25', '08/18/2025', '0009/24-25', '3', 'Frais scolaires', 'USD', '3', '30', '20', '15', '30', '20', '14.5', '0.5 USD'),
('137-0009/24-25', '08/18/2025', '0009/24-25', '3', 'Bulletin', 'CDF', '3', '5000', '0', '0', '4000', '0', '0', '1000 CDF'),
('138-0009/24-25', '08/18/2025', '0009/24-25', '3', 'Bulletin', 'CDF', '3', '5000', '0', '0', '4500', '0', '0', '500 CDF'),
('140-0015/24-25', '08/18/2025', '0015/24-25', '2', 'Frais scolaires', 'USD', '3', '15', '10', '5', '15', '3', '0', '12 USD');

-- --------------------------------------------------------

--
-- Structure de la table `section`
--

CREATE TABLE `section` (
  `idSection` int(11) NOT NULL,
  `nomSection` varchar(50) NOT NULL,
  `description` varchar(50) NOT NULL,
  `idEcole` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `section`
--

INSERT INTO `section` (`idSection`, `nomSection`, `description`, `idEcole`) VALUES
(1, 'Maternelle', 'Elèves de l\'école maternelle', '1'),
(2, 'Primaire', 'Elèves de l\'école Primaire', '2'),
(3, 'Education de base', 'Elève de l\'éducation de base', '3'),
(4, 'Pédagogie Générale', 'Elèves de la Péda. Générale', '3'),
(5, 'Latin-Philo', 'Elèves du Latin-Philo', '3'),
(6, 'Secrétariat Informatique', 'Elèves du Secrétariat Inform', '3'),
(7, 'Commercial et Gestion', 'Elèves du Commercial et Gestion', '3'),
(8, 'Biologie-Chimie', 'Elèves de la Biologie-Chimie', '3'),
(9, 'Nutrition', 'Elèves de la Nutrition', '3'),
(10, 'Construction', 'Elève de la Construction', '3'),
(11, 'Technique Sociale', 'Elèves du sociale', '3');

-- --------------------------------------------------------

--
-- Structure de la table `situation_paye`
--

CREATE TABLE `situation_paye` (
  `idSituation` int(11) NOT NULL,
  `idFrais` int(11) NOT NULL,
  `tranche1` double NOT NULL,
  `tranche2` double NOT NULL,
  `tranche3` double NOT NULL,
  `Matricule` varchar(30) NOT NULL,
  `anneeScolaire` varchar(30) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `situation_paye`
--

INSERT INTO `situation_paye` (`idSituation`, `idFrais`, `tranche1`, `tranche2`, `tranche3`, `Matricule`, `anneeScolaire`, `idEcole`) VALUES
(15, 38, 30, 20, 10, '0001/24-25', '3', '3'),
(16, 50, 28, 0, 0, '0003/24-25', '3', '3'),
(17, 50, 30, 20, 14.5, '0009/24-25', '3', '3'),
(18, 3, 5000, 500, 0, '0001/24-25', '3', '3'),
(19, 78, 5, 0.3, 0, '0010/24-25', '3', '3'),
(20, 50, 30, 20, 8, '0010/24-25', '3', '3'),
(22, 15, 5000, 1000, 0, '0010/24-25', '3', '3'),
(29, 88, 20.7, 18.3, 11, '0012/24-25', '3', '1'),
(30, 91, 25000, 8920, 0, '0012/24-25', '3', '1'),
(31, 109, 4500, 0, 0, '0009/24-25', '3', '3'),
(32, 97, 5000, 0, 0, '0001/24-25', '3', '3'),
(33, 109, 4500, 0, 0, '0010/24-25', '3', '3'),
(34, 137, 15, 3, 0, '0015/24-25', '3', '2'),
(35, 137, 15, 3, 0, '0015/24-25', '3', '2');

-- --------------------------------------------------------

--
-- Structure de la table `tire_excel`
--

CREATE TABLE `tire_excel` (
  `idClasse` int(11) NOT NULL,
  `matricule` varchar(20) NOT NULL,
  `nomEleve` varchar(40) NOT NULL,
  `prenom` varchar(30) NOT NULL,
  `sexe` text NOT NULL,
  `classe` varchar(30) NOT NULL,
  `optionEtude` varchar(30) NOT NULL,
  `anneeScolaire` varchar(20) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `t_absence`
--

CREATE TABLE `t_absence` (
  `id_absence` int(11) NOT NULL,
  `matricule` varchar(30) NOT NULL,
  `jour` varchar(30) NOT NULL,
  `mois` varchar(30) NOT NULL,
  `annee` varchar(30) NOT NULL,
  `motif` varchar(30) NOT NULL,
  `date_absence` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `t_agent`
--

CREATE TABLE `t_agent` (
  `matricule` varchar(30) NOT NULL,
  `nom` varchar(30) NOT NULL,
  `prenom` varchar(30) NOT NULL,
  `sexe` varchar(10) NOT NULL,
  `niveau` varchar(20) NOT NULL,
  `domaine` varchar(30) NOT NULL,
  `fonction` varchar(30) NOT NULL,
  `etat_civil` varchar(30) NOT NULL,
  `telephone` varchar(20) NOT NULL,
  `adresse` varchar(50) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `t_agent`
--

INSERT INTO `t_agent` (`matricule`, `nom`, `prenom`, `sexe`, `niveau`, `domaine`, `fonction`, `etat_civil`, `telephone`, `adresse`, `idEcole`) VALUES
('0001/24-25', 'NDAGANO NALULELA', 'PATIENT', 'Masculin', 'Licence', 'FINANCE', 'Enseignant', 'Marié(e)', '0997867567', 'BUKAVU', '3'),
('0002/24-25', 'MWENEMWAMI MARUME', 'PRIMAR', 'Masculin', 'Licence', 'INFORMATIQUE', 'Préfet école', 'Marié(e)', '093396007', 'BUKAVU', '3'),
('0003/24-25', 'MELISSA NZITA', 'MELL', 'Féminin', 'Grade', 'FRANCAIS', 'Enseignant', 'Célibataire', '0990397678', 'BUKAVU', '3'),
('0004/24-25', 'NSHOBOLE NTAMWENGE', 'VIVIANE', 'Féminin', 'Grade', 'PEDA', 'Enseignant', 'Marié(e)', '0990307456', 'BUKAVU', '1'),
('0005/24-25', 'LEMO MATELE', 'ADONIS', 'Masculin', 'Licence', 'PEDAGOGIE', 'Enseignant', 'Marié(e)', '0895678974', 'Bukavu', '2'),
('0006/24-25', 'BONGA MUTUZA', 'VANDRICE', 'Masculin', 'Grade', 'SCIENCES EDUCATION', 'Enseignant', 'Marié(e)', '0896756445', 'BUKAVU', '3'),
('0007/24-25', 'BAHATI NAMUKAMA', 'NATHAN', 'Masculin', 'Licence', 'Chimie', 'Enseignant', 'Célibataire', '0897867564', 'Bukavu', '3');

-- --------------------------------------------------------

--
-- Structure de la table `t_caisse`
--

CREATE TABLE `t_caisse` (
  `idCaisse` int(11) NOT NULL,
  `libelle` varchar(10) NOT NULL,
  `Entree` double NOT NULL,
  `Sortie` double NOT NULL,
  `Solde` double NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `t_caisse`
--

INSERT INTO `t_caisse` (`idCaisse`, `libelle`, `Entree`, `Sortie`, `Solde`, `idEcole`) VALUES
(1, 'USD', 28.3, 27.2, 1.1, '1'),
(2, 'CDF', 20028.3, 0, 20028.3, '1'),
(3, 'USD', 36, 0, 36, '2'),
(4, 'CDF', 0, 0, 0, '2'),
(5, 'USD', 317.8, 246.05, 71.75, '3'),
(6, 'CDF', 27000, 19000, 8000, '3');

-- --------------------------------------------------------

--
-- Structure de la table `t_carte_service`
--

CREATE TABLE `t_carte_service` (
  `id_cartes` int(11) NOT NULL,
  `matricule` varchar(30) NOT NULL,
  `photos` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `t_classe`
--

CREATE TABLE `t_classe` (
  `id` int(11) NOT NULL,
  `classe` varchar(50) NOT NULL,
  `idSection` varchar(30) NOT NULL,
  `idEcole` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `t_classe`
--

INSERT INTO `t_classe` (`id`, `classe`, `idSection`, `idEcole`) VALUES
(1, '1ère', '1', '1'),
(2, '2ème', '1', '1'),
(3, '3ème', '1', '1'),
(4, '1ère', '2', '2'),
(5, '2ème', '2', '2'),
(6, '3ème', '2', '2'),
(7, '4ème', '2', '2'),
(8, '5ème', '2', '2'),
(9, '6ème', '2', '2'),
(10, '7ème', '3', '3'),
(11, '8ème', '3', '3'),
(12, '1ère', '4', '3'),
(13, '2ème', '4', '3'),
(14, '3ème', '4', '3'),
(15, '4ème', '4', '3'),
(16, '1ère', '5', '3'),
(17, '2ème', '5', '3'),
(18, '3ème', '5', '3'),
(19, '4ème', '5', '3'),
(20, '1ère', '6', '3'),
(21, '2ème', '6', '3'),
(22, '3ème', '6', '3'),
(23, '4ème', '6', '3'),
(24, '1ère', '7', '3'),
(25, '2ème', '7', '3'),
(26, '3ème', '7', '3'),
(27, '4ème', '7', '3'),
(28, '1ère', '8', '3'),
(29, '2ème', '8', '3'),
(30, '3ème', '8', '3'),
(31, '4ème', '8', '3'),
(32, '1ère', '9', '3'),
(33, '2ème', '9', '3'),
(34, '3ème', '9', '3'),
(35, '4ème', '9', '3'),
(36, '1ère', '10', '3'),
(37, '2ème', '10', '3'),
(38, '3ème', '10', '3'),
(39, '4ème', '10', '3'),
(40, '1ère', '11', '3'),
(41, '2ème', '11', '3'),
(42, '3ème', '11', '3'),
(43, '4ème', '11', '3');

-- --------------------------------------------------------

--
-- Structure de la table `t_conge`
--

CREATE TABLE `t_conge` (
  `id_conge` int(11) NOT NULL,
  `matricule` varchar(30) NOT NULL,
  `date_debut` varchar(30) NOT NULL,
  `date_fin` varchar(30) NOT NULL,
  `type_conge` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `t_eleve`
--

CREATE TABLE `t_eleve` (
  `matricule` varchar(50) NOT NULL,
  `nom` varchar(50) NOT NULL,
  `prenom` varchar(30) NOT NULL,
  `sexe` varchar(15) NOT NULL,
  `classe` varchar(15) NOT NULL,
  `nationalite` varchar(30) NOT NULL,
  `nom_du_pere` varchar(30) NOT NULL,
  `nom_de_la_mere` varchar(30) NOT NULL,
  `adresse` varchar(50) NOT NULL,
  `anneescol` varchar(50) NOT NULL,
  `dateInscription` varchar(20) NOT NULL,
  `lieuNaiss` varchar(30) NOT NULL,
  `dateNaiss` varchar(40) NOT NULL,
  `ecoleProv` varchar(50) NOT NULL,
  `option` varchar(40) NOT NULL,
  `pourcReussite` varchar(10) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `t_eleve`
--

INSERT INTO `t_eleve` (`matricule`, `nom`, `prenom`, `sexe`, `classe`, `nationalite`, `nom_du_pere`, `nom_de_la_mere`, `adresse`, `anneescol`, `dateInscription`, `lieuNaiss`, `dateNaiss`, `ecoleProv`, `option`, `pourcReussite`, `idEcole`) VALUES
('0001/24-25', 'IYUMA MBALE', 'HERITIER', 'Masculin', '12', 'CONGOLAISE', 'MBALE KAPINGA', 'KAPINGA WABIWA', 'BUKAVU', '3', '12/01/2025', 'Lugendo', '2006-06-15', 'Institut Rwabika', '4', '57', '3'),
('0002/24-25', 'NTAMUNTU NSHOBOLE', 'FRANCINE', 'Féminin', '24', 'CONGOLAISE', 'BAHATI MEME', 'IRENE BAGUNDA', 'BUKAVU', '3', '12/01/2025', 'BUKAVU', '2007-11-21', 'Institut Olimba', '7', '69', '3'),
('0003/24-25', 'ELIAS NAMUJIMBO', 'MARDOCHE', 'Masculin', '24', 'CONGOLAISE', 'NAMUJIMBO', 'ESTHER', 'BUKAVU', '3', '13/01/2025', 'BUKAVU', '2013-01-23', 'SANS', '7', '70', '3'),
('0005/24-25', 'RACHEL MWAMBA', 'ELODIE', 'Féminin', '24', 'CONGOLAISE', 'MWAMBA MASTAKI', 'AMPIRE', 'BUKAVU', '3', '13/02/2025', 'BUKAVU', '2007-10-12', 'KIMPESE', '7', '78%', '3'),
('0009/24-25', 'BULONZA MARUME', 'AURORE', 'Féminin', '24', 'CONGOLAISE', 'MARUME BIHEMBE', 'NYOTA BIKABA', 'BUKAVU', '3', '25/02/2025', 'COMBO', '2002-06-12', 'RWABIKA', '7', '78%', '3'),
('0010/24-25', 'SIFA BAYONGWA', 'ASIFIWE', 'Féminin', '11', 'CONGOLAISE', 'BAYONGWA', 'MAISHA', 'BUKAVU', '3', '03/05/2025', 'BUKAVU', '2000-02-01', 'CS NAOMI PRIMAIRE', '3', '70', '3'),
('0012/24-25', 'SIFA BAYONGWA', 'ASIFIWE', 'Féminin', '1', 'CONGOLAISE', 'BAYONGWA', 'ANUARITE', 'BUKAVU', '3', '03/05/2025', 'BUKAVU', '2020-02-12', 'RAS', '1', 'RAS', '1'),
('0013/24-25', 'NTAMWENGE NZINE', 'GABRIELLA', 'Féminin', '27', 'CONGOLAISE', 'NZINE MALU', 'CEKANABO JOVANIE', 'BUKAVU', '3', '11/08/2025', 'BUKAVU', '2000-08-20', 'CAHI', '7', '68', '3'),
('0015/24-25', 'BARAKA NTAMUNTU ', 'FRANKLIN ', 'Masculin', '4', 'CONGOLAISE ', 'NTAMUNTU', 'IRENE', 'BUJAVU', '3', '18/08/2025', 'BUKAVU', '2022-08-18', 'SANS', '2', '0', '2'),
('0016/24-25', 'CEKANABO MAISHA', 'MASHEKA', 'Masculin', '24', 'CONGOLAISE', 'MAISHA', 'ESTHER', 'BUKAVU', '3', '19/08/2025', 'BUKAVU', '2009-03-12', 'ZAKO', '7', '61', '3');

-- --------------------------------------------------------

--
-- Structure de la table `t_oublier`
--

CREATE TABLE `t_oublier` (
  `id` int(11) NOT NULL,
  `nom` varchar(30) NOT NULL,
  `prenom` varchar(30) NOT NULL,
  `login` varchar(30) NOT NULL,
  `password` varchar(30) NOT NULL,
  `confirm` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `t_payement_frais`
--

CREATE TABLE `t_payement_frais` (
  `id_payement` int(11) NOT NULL,
  `matricule` varchar(30) NOT NULL,
  `motif` varchar(30) NOT NULL,
  `montant_payer` varchar(30) NOT NULL,
  `unite` varchar(10) NOT NULL,
  `date_payement` varchar(15) NOT NULL,
  `anneescolaire` varchar(20) NOT NULL,
  `idEcole` varchar(30) NOT NULL,
  `idOperateur` varchar(30) NOT NULL,
  `idRecu` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `t_payement_frais`
--

INSERT INTO `t_payement_frais` (`id_payement`, `matricule`, `motif`, `montant_payer`, `unite`, `date_payement`, `anneescolaire`, `idEcole`, `idOperateur`, `idRecu`) VALUES
(88, '0001/24-25', '38', '33', 'USD', '01/05/2025', '3', '3', 'ELIAS', ''),
(90, '0003/24-25', '50', '28', 'USD', '01/04/2025', '3', '3', 'ELIAS', ''),
(91, '0009/24-25', '50', '40', 'USD', '01/04/2025', '3', '3', 'ELIAS', ''),
(92, '0001/24-25', '38', '26.4', 'USD', '15/04/2025', '3', '3', 'ELIAS', ''),
(93, '0001/24-25', '38', '0.4', 'USD', '17/04/2025', '3', '3', 'ELIAS', ''),
(94, '0001/24-25', '38', '0.2', 'USD', '20/04/2025', '3', '3', 'ELIAS', ''),
(95, '0001/24-25', '3', '3000', 'CDF', '25/04/2025', '3', '3', 'ELIAS', ''),
(96, '0001/24-25', '3', '2500', 'CDF', '01/05/2025', '3', '3', 'ELIAS', ''),
(97, '0009/24-25', '50', '22.5', 'USD', '01/05/2025', '3', '3', 'ELIAS', ''),
(98, '0010/24-25', '78', '3', 'USD', '03/05/2025', '3', '3', 'ELIAS', ''),
(99, '0010/24-25', '78', '2.3', 'USD', '03/05/2025', '3', '3', 'ELIAS', ''),
(100, '0010/24-25', '50', '55', 'USD', '03/05/2025', '3', '3', 'ELIAS', ''),
(102, '0010/24-25', '15', '4000', 'CDF', '03/05/2025', '3', '3', 'NEEMA', ''),
(103, '0010/24-25', '15', '2000', 'CDF', '03/05/2025', '3', '3', 'NEEMA', ''),
(115, '0012/24-25', '88', '40000', 'CDF', '08/05/2025', '3', '1', 'ELIAS', ''),
(116, '0012/24-25', '91', '8.2', 'USD', '08/05/2025', '3', '1', 'ELIAS', ''),
(117, '0012/24-25', '91', '2.4', 'USD', '08/05/2025', '3', '1', 'ELIAS', ''),
(118, '0012/24-25', '88', '12.4', 'USD', '08/05/2025', '3', '1', 'ELIAS', ''),
(119, '0012/24-25', '88', '3.1', 'USD', '08/05/2025', '3', '1', 'ELIAS', ''),
(120, '0012/24-25', '88', '2.2', 'USD', '08/05/2025', '3', '1', 'ELIAS', ''),
(121, '0012/24-25', '88', '20000', 'CDF', '08/05/2025', '3', '1', 'ELIAS', ''),
(122, '0009/24-25', '109', '2000', 'CDF', '25/06/2025', '3', '3', 'ELIAS', ''),
(123, '0001/24-25', '97', '3000', 'CDF', '27/06/2025', '3', '3', 'ELIAS', ''),
(124, '0001/24-25', '97', '1000', 'CDF', '27/06/2025', '3', '3', 'ELIAS', '123-0001/24-25'),
(125, '0001/24-25', '97', '200', 'CDF', '27/06/2025', '3', '3', 'ELIAS', '125-0001/24-25'),
(126, '0001/24-25', '97', '300', 'CDF', '27/06/2025', '3', '3', 'ELIAS', '126-0001/24-25'),
(127, '0001/24-25', '97', '300', 'CDF', '27/06/2025', '3', '3', 'ELIAS', '127-0001/24-25'),
(128, '0001/24-25', '97', '100', 'CDF', '27/06/2025', '3', '3', 'ELIAS', '128-0001/24-25'),
(129, '0001/24-25', '97', '100', 'CDF', '27/06/2025', '3', '3', 'ELIAS', '129-0001/24-25'),
(130, '0010/24-25', '50', '3', 'USD', '27/06/2025', '3', '3', 'ELIAS', '130-0010/24-25'),
(131, '0010/24-25', '109', '3000', 'CDF', '29/07/2025', '3', '3', 'ELIAS', '131-0010/24-25'),
(132, '0010/24-25', '109', '1500', 'CDF', '29/07/2025', '3', '3', 'ELIAS', '132-0010/24-25'),
(133, '0009/24-25', '109', '1000', 'CDF', '18/08/2025', '3', '3', 'ELIAS', '133-0009/24-25'),
(134, '0009/24-25', '109', '1000', 'CDF', '18/08/2025', '3', '3', 'ELIAS', '133-0009/24-25'),
(135, '0009/24-25', '50', '2', 'USD', '18/08/2025', '3', '3', 'ELIAS', '135-0009/24-25'),
(136, '0009/24-25', '50', '2', 'USD', '18/08/2025', '3', '3', 'ELIAS', '135-0009/24-25'),
(137, '0009/24-25', '109', '1000', 'CDF', '18/08/2025', '3', '3', 'ELIAS', '137-0009/24-25'),
(138, '0009/24-25', '109', '500', 'CDF', '18/08/2025', '3', '3', 'ELIAS', '138-0009/24-25'),
(139, '0009/24-25', '109', '500', 'CDF', '18/08/2025', '3', '3', 'ELIAS', '138-0009/24-25'),
(140, '0015/24-25', '137', '18', 'USD', '18/08/2025', '3', '2', 'ELIAS', '140-0015/24-25'),
(141, '0015/24-25', '137', '18', 'USD', '18/08/2025', '3', '2', 'ELIAS', '140-0015/24-25');

-- --------------------------------------------------------

--
-- Structure de la table `t_presence`
--

CREATE TABLE `t_presence` (
  `id_presence` int(11) NOT NULL,
  `matricule` varchar(30) NOT NULL,
  `heure_arriver` varchar(30) NOT NULL,
  `motif` varchar(100) NOT NULL,
  `datep` varchar(30) NOT NULL,
  `nbHeure` int(11) NOT NULL,
  `heureDepart` varchar(30) NOT NULL,
  `nbHenseigne` varchar(11) NOT NULL,
  `moisEnseigne` varchar(20) NOT NULL,
  `annee` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `t_presence`
--

INSERT INTO `t_presence` (`id_presence`, `matricule`, `heure_arriver`, `motif`, `datep`, `nbHeure`, `heureDepart`, `nbHenseigne`, `moisEnseigne`, `annee`) VALUES
(3, '0003/24-25', '20:06', 'Présent', '25/06/2025', 3, '11:11', '3', 'Octobre', '3'),
(17, '0003/24-25', '18:54', 'Présent', '26/06/2025', 4, '12:27', '4', 'Octobre', '3'),
(18, '0002/24-25', '18:54', 'Présent', '26/06/2025', 7, '12:27', '7', 'Octobre', '3'),
(20, '0001/24-25', '12:26', 'Présent', '26/07/2025', 3, '12:27', '3', 'Octobre', '3'),
(34, '0006/24-25', '10:07', 'Présent', '28/07/2025', 7, '11:11', '6', 'Octobre', '3'),
(35, '0003/24-25', '10:07', 'Présent', '28/07/2025', 1, '10:10', '1', 'Octobre', '3'),
(36, '0002/24-25', '10:07', 'Présent', '28/07/2025', 3, '10:11', '2', 'Octobre', '3'),
(38, '0006/24-25', '10:30', 'Présent', '29/07/2025', 2, '10:31', '2', 'Octobre', '3'),
(39, '0003/24-25', '-', 'Absent', '29/07/2025', 2, '-', '-', 'Octobre', '3'),
(41, '0001/24-25', '-', 'Absent', '29/07/2025', 3, '-', '-', 'Octobre', '3'),
(42, '0002/24-25', '10:31', 'Présent', '29/07/2025', 7, '10:31', '7', 'Octobre', '3'),
(43, '0003/24-25', '10:11', 'Présent', '30/07/2025', 3, '11:10', '2', 'Octobre', '3'),
(44, '0002/24-25', '10:11', 'Présent', '30/07/2025', 3, '11:05', '3', 'Octobre', '3'),
(45, '0003/24-25', '10:23', 'Présent', '31/07/2025', 4, '10:24', '4', 'Octobre', '3'),
(46, '0002/24-25', '10:23', 'Présent', '31/07/2025', 7, '10:24', '7', 'Octobre', '3'),
(47, '0006/24-25', '14:10', 'Présent', '01/08/2025', 7, '14:11', '7', 'Octobre', '3'),
(48, '0003/24-25', '14:10', 'Présent', '01/08/2025', 5, '14:11', '5', 'Octobre', '3'),
(49, '0002/24-25', '14:10', 'Présent', '01/08/2025', 4, '14:12', '4', 'Octobre', '3'),
(50, '0003/24-25', '08:56', 'Présent', '06/08/2025', 3, '08:56', '3', 'Octobre', '3'),
(51, '0002/24-25', '08:56', 'Présent', '06/08/2025', 3, '09:06', '2', 'Octobre', '3'),
(52, '0007/24-25', '21:22', 'Présent', '11/08/2025', 7, '21:23', '7', 'Octobre', '3'),
(53, '0006/24-25', '21:22', 'Présent', '11/08/2025', 7, '21:23', '7', 'Octobre', '3'),
(54, '0003/24-25', '21:22', 'Présent', '11/08/2025', 1, '21:23', '1', 'Octobre', '3'),
(55, '0002/24-25', '21:22', 'Présent', '11/08/2025', 3, '21:23', '3', 'Octobre', '3'),
(56, '0001/24-25', '21:22', 'Présent', '11/08/2025', 7, '21:23', '7', 'Octobre', '3'),
(57, '0006/24-25', '13:05', 'Présent', '12/08/2025', 2, '13:06', '2', 'Octobre', '3'),
(58, '0003/24-25', '13:05', 'Présent', '12/08/2025', 2, '13:06', '2', 'Octobre', '3'),
(59, '0002/24-25', '13:05', 'Présent', '12/08/2025', 7, '13:06', '7', 'Octobre', '3'),
(60, '0001/24-25', '13:06', 'Présent', '12/08/2025', 3, '13:06', '3', 'Octobre', '3'),
(61, '0007/24-25', '13:47', 'Présent', '18/08/2025', 7, 'En cours', 'En cours', 'Août', '3'),
(62, '0006/24-25', '13:47', 'Présent', '18/08/2025', 7, 'En cours', 'En cours', 'Août', '3'),
(63, '0003/24-25', '13:47', 'Présent', '18/08/2025', 1, 'En cours', 'En cours', 'Août', '3'),
(64, '0002/24-25', '13:47', 'Présent', '18/08/2025', 3, 'En cours', 'En cours', 'Août', '3'),
(65, '0001/24-25', '13:47', 'Présent', '18/08/2025', 7, 'En cours', 'En cours', 'Août', '3'),
(66, '0003/24-25', '09:27', 'Présent', '20/08/2025', 3, '09:28', '3', 'Août', '3'),
(67, '0002/24-25', '09:27', 'Présent', '20/08/2025', 3, '09:28', '3', 'Août', '3');

-- --------------------------------------------------------

--
-- Structure de la table `t_salaire`
--

CREATE TABLE `t_salaire` (
  `id_salaire` int(11) NOT NULL,
  `matricule` varchar(30) NOT NULL,
  `salbase` double NOT NULL,
  `nombreHeure` varchar(20) NOT NULL,
  `avance` double NOT NULL,
  `rembourser` double NOT NULL,
  `net_payer` double NOT NULL,
  `mois_payer` varchar(30) NOT NULL,
  `annee` varchar(30) NOT NULL,
  `datepaye` varchar(20) NOT NULL,
  `idOperateur` varchar(30) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `t_salaire`
--

INSERT INTO `t_salaire` (`id_salaire`, `matricule`, `salbase`, `nombreHeure`, `avance`, `rembourser`, `net_payer`, `mois_payer`, `annee`, `datepaye`, `idOperateur`, `idEcole`) VALUES
(1, '0002/24-25', 28, '32', 10, 10, 18, 'Septembre', '3', '2025-05-12', '1', '3'),
(2, '0006/24-25', 13.125, '15', 20, 5, 8.125, 'Septembre', '3', '2025-06-13', '1', '3'),
(3, '0006/24-25', 13.125, '15', 15, 5, 8.125, 'Octobre', '3', '2025-06-22', '1', '3'),
(4, '0002/24-25', 28, '32', 10, 10, 18, 'Octobre', '3', '2025-07-01', '1', '3'),
(5, '0002/24-25', 30.4, '32', 0, 0, 30.4, 'Octobre', '3', '2025-07-20', '1', '3'),
(6, '0003/24-25', 19.25, '22', 0, 0, 19.25, 'Octobre', '3', '2025-08-01', '1', '3'),
(7, '0004/24-25', 10, 'Tous les jours', 17, 0, 10, 'Septembre', '3', '2025-08-05', '1', '1'),
(8, '0007/24-25', 6.125, '7', 0, 0, 6.125, 'Octobre', '3', '2025-08-11', '1', '3'),
(9, '0003/24-25', 21.875, '25', 0, 0, 21.875, 'Octobre', '3', '2025-08-12', '1', '3'),
(10, '0002/24-25', 36.75, '42', 0, 0, 36.75, 'Octobre', '3', '2025-08-18', '1', '3');

-- --------------------------------------------------------

--
-- Structure de la table `t_salaire_avance`
--

CREATE TABLE `t_salaire_avance` (
  `idAvance` int(11) NOT NULL,
  `matricule` varchar(30) NOT NULL,
  `montantAvance` varchar(20) NOT NULL,
  `mois` varchar(30) NOT NULL,
  `anneescol` varchar(50) NOT NULL,
  `dateAvance` varchar(30) NOT NULL,
  `idOperateur` varchar(30) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `t_salaire_avance`
--

INSERT INTO `t_salaire_avance` (`idAvance`, `matricule`, `montantAvance`, `mois`, `anneescol`, `dateAvance`, `idOperateur`, `idEcole`) VALUES
(5, '0004/24-25', '10', 'Septembre', '3', '29/07/2025', '1', '1'),
(6, '0004/24-25', '5', 'Octobre', '3', '29/07/2025', '1', '1'),
(7, '0004/24-25', '2.2', 'Novembre', '3', '29/07/2025', '1', '1'),
(8, '0006/24-25', '20', 'Septembre', '3', '29/07/2025', '1', '3'),
(9, '0002/24-25', '10', 'Septembre', '3', '31/07/2025', '1', '3'),
(10, '0006/24-25', '10', 'Septembre', '4', '18/08/2025', '1', '3'),
(11, '0006/24-25', '5', 'Septembre', '4', '18/08/2025', '1', '3'),
(12, '0006/24-25', '3', 'Septembre', '4', '18/08/2025', '1', '3'),
(13, '0002/24-25', '3', 'Septembre', '4', '18/08/2025', '1', '3'),
(14, '0002/24-25', '2', 'Septembre', '4', '18/08/2025', '1', '3'),
(15, '0007/24-25', '10', 'Septembre', '3', '20/08/2025', '8', '3');

-- --------------------------------------------------------

--
-- Structure de la table `utilisateur`
--

CREATE TABLE `utilisateur` (
  `id` int(11) NOT NULL,
  `login` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `service` varchar(50) NOT NULL,
  `idEcole` varchar(30) NOT NULL,
  `etat` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `utilisateur`
--

INSERT INTO `utilisateur` (`id`, `login`, `password`, `service`, `idEcole`, `etat`) VALUES
(1, 'ELIAS', 'gestion', 'Admin', 'Toutes les écoles', 'Actif'),
(7, 'FURAHA', '12345', 'Comptable', '3', 'Actif'),
(8, 'NAMUSUBA', '2022', 'Préfet Secondaire', '3', 'Actif'),
(9, 'MAISHA', '2025', 'Directeur', '2', 'Actif'),
(10, 'FRANCINE', '2025', 'Directeur', '1', 'Actif');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `anneescol`
--
ALTER TABLE `anneescol`
  ADD PRIMARY KEY (`anneeScolaire`);

--
-- Index pour la table `attribution_horaire`
--
ALTER TABLE `attribution_horaire`
  ADD PRIMARY KEY (`idAttribution`);

--
-- Index pour la table `change_classe`
--
ALTER TABLE `change_classe`
  ADD PRIMARY KEY (`idClasse`);

--
-- Index pour la table `compte_agent`
--
ALTER TABLE `compte_agent`
  ADD PRIMARY KEY (`idCompte`);

--
-- Index pour la table `compte_eleve`
--
ALTER TABLE `compte_eleve`
  ADD PRIMARY KEY (`idCompte`);

--
-- Index pour la table `depense`
--
ALTER TABLE `depense`
  ADD PRIMARY KEY (`idDepense`);

--
-- Index pour la table `ecole`
--
ALTER TABLE `ecole`
  ADD PRIMARY KEY (`idEcole`);

--
-- Index pour la table `frais_scolaire`
--
ALTER TABLE `frais_scolaire`
  ADD PRIMARY KEY (`idfrais`);

--
-- Index pour la table `recu_payement`
--
ALTER TABLE `recu_payement`
  ADD PRIMARY KEY (`idRecu`);

--
-- Index pour la table `section`
--
ALTER TABLE `section`
  ADD PRIMARY KEY (`idSection`);

--
-- Index pour la table `situation_paye`
--
ALTER TABLE `situation_paye`
  ADD PRIMARY KEY (`idSituation`);

--
-- Index pour la table `tire_excel`
--
ALTER TABLE `tire_excel`
  ADD PRIMARY KEY (`idClasse`);

--
-- Index pour la table `t_absence`
--
ALTER TABLE `t_absence`
  ADD PRIMARY KEY (`id_absence`);

--
-- Index pour la table `t_agent`
--
ALTER TABLE `t_agent`
  ADD PRIMARY KEY (`matricule`);

--
-- Index pour la table `t_caisse`
--
ALTER TABLE `t_caisse`
  ADD PRIMARY KEY (`idCaisse`);

--
-- Index pour la table `t_carte_service`
--
ALTER TABLE `t_carte_service`
  ADD PRIMARY KEY (`id_cartes`);

--
-- Index pour la table `t_classe`
--
ALTER TABLE `t_classe`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `t_conge`
--
ALTER TABLE `t_conge`
  ADD PRIMARY KEY (`id_conge`);

--
-- Index pour la table `t_eleve`
--
ALTER TABLE `t_eleve`
  ADD PRIMARY KEY (`matricule`);

--
-- Index pour la table `t_oublier`
--
ALTER TABLE `t_oublier`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `t_payement_frais`
--
ALTER TABLE `t_payement_frais`
  ADD PRIMARY KEY (`id_payement`);

--
-- Index pour la table `t_presence`
--
ALTER TABLE `t_presence`
  ADD PRIMARY KEY (`id_presence`);

--
-- Index pour la table `t_salaire`
--
ALTER TABLE `t_salaire`
  ADD PRIMARY KEY (`id_salaire`);

--
-- Index pour la table `t_salaire_avance`
--
ALTER TABLE `t_salaire_avance`
  ADD PRIMARY KEY (`idAvance`);

--
-- Index pour la table `utilisateur`
--
ALTER TABLE `utilisateur`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `anneescol`
--
ALTER TABLE `anneescol`
  MODIFY `anneeScolaire` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT pour la table `attribution_horaire`
--
ALTER TABLE `attribution_horaire`
  MODIFY `idAttribution` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT pour la table `change_classe`
--
ALTER TABLE `change_classe`
  MODIFY `idClasse` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=93;

--
-- AUTO_INCREMENT pour la table `compte_agent`
--
ALTER TABLE `compte_agent`
  MODIFY `idCompte` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT pour la table `compte_eleve`
--
ALTER TABLE `compte_eleve`
  MODIFY `idCompte` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT pour la table `depense`
--
ALTER TABLE `depense`
  MODIFY `idDepense` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT pour la table `ecole`
--
ALTER TABLE `ecole`
  MODIFY `idEcole` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `frais_scolaire`
--
ALTER TABLE `frais_scolaire`
  MODIFY `idfrais` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=146;

--
-- AUTO_INCREMENT pour la table `section`
--
ALTER TABLE `section`
  MODIFY `idSection` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT pour la table `situation_paye`
--
ALTER TABLE `situation_paye`
  MODIFY `idSituation` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT pour la table `tire_excel`
--
ALTER TABLE `tire_excel`
  MODIFY `idClasse` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `t_absence`
--
ALTER TABLE `t_absence`
  MODIFY `id_absence` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `t_caisse`
--
ALTER TABLE `t_caisse`
  MODIFY `idCaisse` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT pour la table `t_carte_service`
--
ALTER TABLE `t_carte_service`
  MODIFY `id_cartes` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `t_classe`
--
ALTER TABLE `t_classe`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- AUTO_INCREMENT pour la table `t_conge`
--
ALTER TABLE `t_conge`
  MODIFY `id_conge` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `t_oublier`
--
ALTER TABLE `t_oublier`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `t_payement_frais`
--
ALTER TABLE `t_payement_frais`
  MODIFY `id_payement` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=142;

--
-- AUTO_INCREMENT pour la table `t_presence`
--
ALTER TABLE `t_presence`
  MODIFY `id_presence` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=68;

--
-- AUTO_INCREMENT pour la table `t_salaire`
--
ALTER TABLE `t_salaire`
  MODIFY `id_salaire` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT pour la table `t_salaire_avance`
--
ALTER TABLE `t_salaire_avance`
  MODIFY `idAvance` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT pour la table `utilisateur`
--
ALTER TABLE `utilisateur`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
