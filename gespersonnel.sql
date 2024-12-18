-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1
-- Généré le :  mer. 18 déc. 2024 à 15:06
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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Déchargement des données de la table `anneescol`
--

INSERT INTO `anneescol` (`anneeScolaire`, `designation`, `etat`) VALUES
(1, '2022-2023', 'Inactif'),
(2, '2023-2024', 'Inactif'),
(3, '2024-2025', 'Actif');

-- --------------------------------------------------------

--
-- Structure de la table `attribution_horaire`
--

CREATE TABLE `attribution_horaire` (
  `idAttribution` int(11) NOT NULL,
  `coursAttribue` varchar(200) NOT NULL,
  `totalHeure` int(11) NOT NULL,
  `Matricule` varchar(30) NOT NULL,
  `anneeScolaire` varchar(30) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `avance`
--

CREATE TABLE `avance` (
  `idAvance` int(11) NOT NULL,
  `Matricule` varchar(20) NOT NULL,
  `Montant` int(11) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  `anneeScolaire` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `depense`
--

CREATE TABLE `depense` (
  `idDepense` int(11) NOT NULL,
  `dateDepense` varchar(20) NOT NULL,
  `designation` varchar(100) NOT NULL,
  `montant` int(11) NOT NULL,
  `unite` varchar(10) NOT NULL,
  `anneeScolaire` varchar(30) NOT NULL,
  `idOperateur` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `ecole`
--

CREATE TABLE `ecole` (
  `idEcole` int(11) NOT NULL,
  `nomEcole` varchar(100) NOT NULL,
  `description` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  `tranche1` int(11) NOT NULL,
  `tranche2` int(11) NOT NULL,
  `tranche3` int(11) NOT NULL,
  `unite` varchar(10) NOT NULL,
  `classe` varchar(30) NOT NULL,
  `optionConcerne` varchar(30) NOT NULL,
  `anneescolaire` varchar(30) NOT NULL,
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `recupayement`
--

CREATE TABLE `recupayement` (
  `idRecu` int(11) NOT NULL,
  `Matricule` varchar(50) NOT NULL,
  `nom` varchar(30) NOT NULL,
  `postnom` varchar(30) NOT NULL,
  `anneeScolaire` varchar(30) NOT NULL,
  `classe` varchar(30) NOT NULL,
  `option` varchar(30) NOT NULL,
  `prevuT1` varchar(30) NOT NULL,
  `prevuT2` varchar(30) NOT NULL,
  `prevuT3` varchar(30) NOT NULL,
  `paye1` varchar(30) NOT NULL,
  `paye2` varchar(30) NOT NULL,
  `paye3` varchar(30) NOT NULL,
  `montant` varchar(30) NOT NULL,
  `unite` varchar(20) NOT NULL,
  `datePaye` varchar(20) NOT NULL,
  `motif` varchar(40) NOT NULL,
  `idEcole` varchar(30) NOT NULL,
  `idOperateur` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `section`
--

CREATE TABLE `section` (
  `idSection` int(11) NOT NULL,
  `nomSection` varchar(50) NOT NULL,
  `description` varchar(50) NOT NULL,
  `idEcole` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

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
  `idAgent` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
(1, 'USD', 0, 0, 3, '1'),
(2, 'CDF', 0, 0, 0, '1'),
(3, 'USD', 0, 0, 10, '2'),
(4, 'CDF', 0, 0, 2000, '2'),
(5, 'USD', 0, 0, 0, '3'),
(6, 'CDF', 0, 0, 500, '3');

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
  `idSection` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
('33634', 'BAHATI', 'MPESHE', 'M', '1', 'CONGOLAISE', 'BAHATI ISHARA', 'YVETTE LWAKASI', 'BUKAVU', '2', '18/12/2024', 'BUKAVU', '18/12/2010', 'INSTITUT BAKITA', '1', '80', '1');

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
  `date_payement` varchar(30) NOT NULL,
  `anneescolaire` varchar(20) NOT NULL,
  `idEcole` varchar(30) NOT NULL,
  `idOperateur` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `t_presence`
--

CREATE TABLE `t_presence` (
  `id_presence` int(11) NOT NULL,
  `matricule` varchar(30) NOT NULL,
  `heure_arriver` varchar(30) NOT NULL,
  `jour` varchar(30) NOT NULL,
  `mois` varchar(30) NOT NULL,
  `datep` varchar(30) NOT NULL,
  `annee` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `t_salaire`
--

CREATE TABLE `t_salaire` (
  `id_salaire` int(11) NOT NULL,
  `matricule` varchar(30) NOT NULL,
  `salbase` varchar(30) NOT NULL,
  `nombreHeure` varchar(20) NOT NULL,
  `avance` varchar(30) NOT NULL,
  `rembourser` varchar(30) NOT NULL,
  `net_payer` varchar(30) NOT NULL,
  `mois_payer` varchar(30) NOT NULL,
  `annee` varchar(30) NOT NULL,
  `datepaye` varchar(30) NOT NULL,
  `idOperateur` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `t_salaire_avance`
--

CREATE TABLE `t_salaire_avance` (
  `id_salaire_avance` int(11) NOT NULL,
  `matricule` varchar(30) NOT NULL,
  `avance` varchar(30) NOT NULL,
  `mois` varchar(30) NOT NULL,
  `anneescol` varchar(50) NOT NULL,
  `date_avance` varchar(30) NOT NULL,
  `idOperateur` varchar(30) NOT NULL
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
  `idEcole` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `utilisateur`
--

INSERT INTO `utilisateur` (`id`, `login`, `password`, `service`, `idEcole`) VALUES
(1, 'ELIAS', '2022', 'Admin', '');

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
-- Index pour la table `avance`
--
ALTER TABLE `avance`
  ADD PRIMARY KEY (`idAvance`);

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
-- Index pour la table `recupayement`
--
ALTER TABLE `recupayement`
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
  ADD PRIMARY KEY (`id_salaire_avance`);

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
  MODIFY `anneeScolaire` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `attribution_horaire`
--
ALTER TABLE `attribution_horaire`
  MODIFY `idAttribution` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `avance`
--
ALTER TABLE `avance`
  MODIFY `idAvance` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `change_classe`
--
ALTER TABLE `change_classe`
  MODIFY `idClasse` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `compte_agent`
--
ALTER TABLE `compte_agent`
  MODIFY `idCompte` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `compte_eleve`
--
ALTER TABLE `compte_eleve`
  MODIFY `idCompte` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `depense`
--
ALTER TABLE `depense`
  MODIFY `idDepense` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `ecole`
--
ALTER TABLE `ecole`
  MODIFY `idEcole` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT pour la table `frais_scolaire`
--
ALTER TABLE `frais_scolaire`
  MODIFY `idfrais` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `recupayement`
--
ALTER TABLE `recupayement`
  MODIFY `idRecu` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `section`
--
ALTER TABLE `section`
  MODIFY `idSection` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `situation_paye`
--
ALTER TABLE `situation_paye`
  MODIFY `idSituation` int(11) NOT NULL AUTO_INCREMENT;

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

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
  MODIFY `id_payement` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `t_presence`
--
ALTER TABLE `t_presence`
  MODIFY `id_presence` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `t_salaire`
--
ALTER TABLE `t_salaire`
  MODIFY `id_salaire` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `t_salaire_avance`
--
ALTER TABLE `t_salaire_avance`
  MODIFY `id_salaire_avance` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pour la table `utilisateur`
--
ALTER TABLE `utilisateur`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
