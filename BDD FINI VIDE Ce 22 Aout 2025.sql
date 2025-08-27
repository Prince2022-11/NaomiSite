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
(1, 'Maternelle', 'Elèves école maternelle', '1'),
(2, 'Primaire', 'Elèves école Primaire', '2'),
(3, 'Education de base', 'Elèves éducation de base', '3'),
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
(1, 'USD', 0, 0, 0, '1'),
(2, 'CDF', 0, 0, 0, '1'),
(3, 'USD', 0, 0, 0, '2'),
(4, 'CDF', 0, 0, 0, '2'),
(5, 'USD', 0, 0, 0, '3'),
(6, 'CDF', 0, 0, 0, '3');

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
(1, 'ELIAS', 'Admin', 'Admin', 'Toutes les écoles', 'Actif');

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
  MODIFY `idAttribution` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- AUTO_INCREMENT pour la table `change_classe`
--
ALTER TABLE `change_classe`
  MODIFY `idClasse` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- AUTO_INCREMENT pour la table `compte_agent`
--
ALTER TABLE `compte_agent`
  MODIFY `idCompte` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- AUTO_INCREMENT pour la table `compte_eleve`
--
ALTER TABLE `compte_eleve`
  MODIFY `idCompte` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- AUTO_INCREMENT pour la table `depense`
--
ALTER TABLE `depense`
  MODIFY `idDepense` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- AUTO_INCREMENT pour la table `ecole`
--
ALTER TABLE `ecole`
  MODIFY `idEcole` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `frais_scolaire`
--
ALTER TABLE `frais_scolaire`
  MODIFY `idfrais` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- AUTO_INCREMENT pour la table `section`
--
ALTER TABLE `section`
  MODIFY `idSection` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT pour la table `situation_paye`
--
ALTER TABLE `situation_paye`
  MODIFY `idSituation` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

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
  MODIFY `id_payement` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- AUTO_INCREMENT pour la table `t_presence`
--
ALTER TABLE `t_presence`
  MODIFY `id_presence` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- AUTO_INCREMENT pour la table `t_salaire`
--
ALTER TABLE `t_salaire`
  MODIFY `id_salaire` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- AUTO_INCREMENT pour la table `t_salaire_avance`
--
ALTER TABLE `t_salaire_avance`
  MODIFY `idAvance` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1;

--
-- AUTO_INCREMENT pour la table `utilisateur`
--
ALTER TABLE `utilisateur`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
