
<?php
function getNbUser($pdo)
{
    $sql = $pdo->prepare("SELECT * FROM users where fonction='Commandant' or fonction='Client'");
    $sql->execute();
    $nbm = $sql->rowCount();
    return $nbm;
}

function getNbCliennt($pdo)
{
    $sql = $pdo->prepare("SELECT * FROM users WHERE fonction = ?");
    $sql->execute(["Client"]);
    $nbm = $sql->rowCount();
    return $nbm;
}

function getNbDomaine($id, $pdo)
{
    $sql = $pdo->prepare("SELECT * FROM domaine INNER JOIN users ON domaine.idUser=users.idUser AND users.idUser = ?");
    $sql->execute([$id]);
    $nbm = $sql->rowCount();
    return $nbm;
}

function getNbDemande($pdo)
{
    $sql = $pdo->prepare("select * from demande inner join domaine on domaine.idD=demande.idD inner join paiement on paiement.idDe=demande.idDe");
    $sql->execute();
    $nbm = $sql->rowCount();
    return $nbm;
}

function getNbDemandeValidee($pdo)
{
    $sql = $pdo->prepare("select * from demande inner join domaine on domaine.idD=demande.idD inner join paiement on paiement.idDe=demande.idDe and demande.etatDe=?");
    $sql->execute(['Valider']);
    $nbm = $sql->rowCount();
    return $nbm;
}

function getNbDemandeInvalidee($pdo)
{
    $sql = $pdo->prepare("select * from demande inner join domaine on domaine.idD=demande.idD inner join paiement on paiement.idDe=demande.idDe and demande.etatDe=?");
    $sql->execute(['Invalider']);
    $nbm = $sql->rowCount();
    return $nbm;
}

function getNbDemandeClient($id, $pdo)
{
    $sql = $pdo->prepare("SELECT * FROM demande INNER JOIN users ON demande.idUser=users.idUser AND users.idUser = ?");
    $sql->execute([$id]);
    $nbm = $sql->rowCount();
    return $nbm;
}

function getNbDemandeInvalideeClient($id, $pdo)
{
    $sql = $pdo->prepare("SELECT * FROM demande INNER JOIN users ON demande.idUser=users.idUser AND users.idUser = ? AND demande.etatDe = ?");
    $sql->execute([$id, 'Invalider']);
    $nbm = $sql->rowCount();
    return $nbm;
}

function getNbDemandeValideeClient($id, $pdo)
{
    $sql = $pdo->prepare("SELECT * FROM demande INNER JOIN users ON demande.idUser=users.idUser AND users.idUser = ? AND demande.etatDe = ?");
    $sql->execute([$id, 'Valider']);
    $nbm = $sql->rowCount();
    return $nbm;
}