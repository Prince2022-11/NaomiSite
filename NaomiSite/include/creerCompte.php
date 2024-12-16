<?php
    session_start(); 
    require_once('../include/connexiondb.php');
    if(isset($_POST['creer'])){
 
        $nom = $_POST['nomUser'];
        $session = "Client";
        $login = $_POST['login'];
        $mdp = $_POST['motpass'];
        $etat = "Activer";
        $nouveau_nom = "user.png";

        if(!empty($nom) || !empty($login) || !empty($mdp)){
        
            // Vérification du nom d'utilisateur s'il existe déjà
            $sql = "SELECT * FROM users WHERE nomUser = ? AND loginUser = ?";
            $req = $pdo->prepare($sql);
            $req->execute([$nom, $login]);

            if ($req->rowCount() > 0) {
                echo "<script> alert('Vous avez deja un compte utilisateur, Veillez vous connectez') </script>";
                echo "<script>window.open('../index.php','_self')</script>";
            } else {
                # Insertion des données dans la table 
                $sql = "INSERT INTO users 
                (`nomUser`,`loginUser`,`pw`,`fonction`,`imageUser`,`etatUser`) 
                VALUES (?,?,?,?,?,?)";
                $req = $pdo->prepare($sql);
                $req->execute([$nom,$login,$mdp,$session,$nouveau_nom,$etat]);

                #Message de succès
                echo "<script> alert('Compte créé avec succès') </script>";
                echo "<script>window.open('../index.php','_self')</script>";
            }
            
        }else{
            echo "<script> alert('Les champs sont vides') </script>";
            echo "<script>window.open('../index.php','_self')</script>";
        }
    }
?>