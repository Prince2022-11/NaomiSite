<?php
  try{
      $pdo=new PDO("mysql:host=localhost;dbname=rva_bdd","root","", array(PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION));
  }
  catch(Exception $e){
    die('Erreur de connexion:'.$e->getMessage());
  }
?>