<?php
    session_start();
    require_once('../include/connexiondb.php');

    if(isset($_POST['con'])){

     $login=isset($_POST['login'])? $_POST['login']:"";
     $pwd=isset($_POST['motpass'])? $_POST['motpass']:"";
    
     $requete="select * from users where loginUser='$login' and pw='$pwd'";
     $resultat=$pdo->query($requete);
    
     if($user=$resultat->fetch()){
         if($user['etatUser']=="Activer" && $user['fonction']=="Admin"){
            $_SESSION['login']=$login;
            $_SESSION['nom']=$user['nomUser'];
            $_SESSION['fonction']=$user['fonction'];
            $_SESSION['id']=$user['idUser'];
            $_SESSION['image']=$user['imageUser'];
             header('Location:../Admin/accueilAdmin.php');
         }
         else if($user['etatUser']=="Activer" && $user['fonction']=="Commandant"){
            $_SESSION['login']=$login;
            $_SESSION['nom']=$user['nomUser'];
            $_SESSION['fonction']=$user['fonction'];
            $_SESSION['id']=$user['idUser'];
            $_SESSION['image']=$user['imageUser'];
            header('Location:../com/accueil.php');	
        }
        else if($user['etatUser']=="Activer" && $user['fonction']=="Client"){
            $_SESSION['login']=$login;
            $_SESSION['nom']=$user['nomUser'];
            $_SESSION['fonction']=$user['fonction'];
            $_SESSION['id']=$user['idUser'];
            $_SESSION['image']=$user['imageUser'];
            header('Location:../client/accueil.php');	
        }
         else{
            echo "<script> alert('<strong>Erreur!!</strong> Votre compte est desactivé.<br> Veuillez contacter votre admnistracteur') </script>";
            echo "<script>window.open('../index.php','_self')</script>";
         }
     }
     else{
        echo "<script> alert('<strong>Erreur!!</strong> Login ou mot de passe incorrecte!!!') </script>";
        echo "<script>window.open('../index.php','_self')</script>";
     }
    }
?>