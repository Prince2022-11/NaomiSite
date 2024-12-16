DROP DATABASE IF EXISTS rva_bdd;
CREATE DATABASE rva_bdd;
USE rva_bdd;

CREATE TABLE `users` (
  `idUser` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `nomUser` varchar(100) NOT NULL,
  `loginUser` varchar(50) NOT NULL,
  `pw` varchar(50) NOT NULL,
  `fonction` varchar(50) NOT NULL, 
  `imageUser` varchar(50) NOT NULL,
  `etatUser` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

INSERT INTO `users` (`idUser`,`nomUser`,`loginUser`,`pw`,`fonction`,`imageUser`,`etatUser`) VALUES
(1, 'ARMAND KALIBANYA', 'admin', 'admin','Admin','user.png','Activer');

CREATE TABLE `domaine` (
  `idD` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `idUser` int(11) NOT NULL,
  `designD` varchar(50) NOT NULL,
  `typeD` varchar(50) NOT NULL,
  `tauxD` double NOT NULL
) ENGINE=InnoDB  DEFAULT CHARSET=utf8mb4;

CREATE TABLE `demande` (
  `idDe` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `idUser` int(11) NOT NULL,
  `idD` int(11) NOT NULL,
  `nbrMois` int(11) NOT NULL,
  `indiceDe` double NOT NULL,
  `etatDe` varchar(10) NOT NULL,
  `dateDe` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB  DEFAULT CHARSET=utf8mb4;

CREATE TABLE `paiement` (
  `idPaye` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `idDe` int(11) NOT NULL,
  `montantP` double NOT NULL,
  `deviseP` varchar(15) NOT NULL,
  `dateP` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


  