<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Accueil.aspx.cs" Inherits="PrototypeGestionRecettesETD.Accueil" %>

<!DOCTYPE html>
<html class="no-js">
<html lang="fr">

<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Acceuil</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet">

    <!-- Custom CSS -->
	<link rel="stylesheet" href="css/main.css">
    <link href="css/custom.css" rel="stylesheet">
	
	<script src="//use.edgefonts.net/bebas-neue.js"></script>

    <!-- Custom Fonts & Icons -->
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700,600,800' rel='stylesheet' type='text/css'>
	<link rel="stylesheet" href="css/icomoon-social.css">
	<link rel="stylesheet" href="css/font-awesome.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" integrity="sha512-SfTiTlX6kk+qitfevl/7LibUOeJWlt9rbyDn92a1DqWOw9vWG2MFoays0sgObmWazO5BQPiFucnnEAjpAB+/Sw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
	<link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet">

	<script src="js/modernizr-2.6.2-respond-1.1.0.min.js"></script>

    <!-- Favicons -->
    <link href="img/slides/favicon.jpg" rel="icon">
	

</head>

<body>
	<div class="contenair" style="margin-top: -90px;">
	<!-- Carousel Slide -->
    <section id="main-slider" class="no-margin">
        <div class="carousel slide">
            <ol class="carousel-indicators">
                <li data-target="#main-slider" data-slide-to="0" class="active"></li>
                <li data-target="#main-slider" data-slide-to="1"></li>
                <li data-target="#main-slider" data-slide-to="2"></li>
            </ol>
            <div class="carousel-inner">
                <div class="item active" style="background-image: url(img/slides/1.jpg)">
                    <div class="container">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="carousel-content centered">
                                    <center><h2 class="animation animated-item-1">PROTOTYPE D'APPLICATION DE GESTION DES RECETTES DES ENTITES TERRITORIALES DECENTRALISEES</h2></center>
									<br>
                                    <center><p class="animation animated-item-2">Par KEDI OKAPI Freddy, L2 Informatique de Gestion UNP 2022-2023</p></center>
									<br>
									<center><a data-scroll href="#myModal" data-toggle="modal" class="btn btn-primary animation animated-item-3" style="background: #085ecf ;"><h4 style="color: white;">Commencer</h4></a></center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div><!--/.item-->
                <div class="item" style="background-image: url(img/slides/2.jpg)">
                    <div class="container">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="carousel-content center centered">
                                    <center><h2 class="animation animated-item-1">PLATEFORME WEB DE COLLECTE DE TOUTES LES RECETTES ISSUES DES ETD</h2></center>
									<br>
                                    <center><p class="animation animated-item-2">FAITES VOUS PLAISIR EN COLLECTANTS LES RECETTES Où QUE VOUS SOYEZ</p></center>
                                    <br>
                                    <center><a data-scroll href="#myModal" data-toggle="modal" class="btn btn-primary animation animated-item-3" style="background: #085ecf ;"><h4 style="color: white;">Commencer</h4></a></center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div><!--/.item-->
                <div class="item" style="background-image: url(img/slides/3.jpg)">
                    <div class="container">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="carousel-content centered">
                                    <center><h2 class="animation animated-item-1">SOYEZ LE BIENVENU DANS VOTRE ESPACE DE TRAVAIL</h2></center>
									<br>
                                    <center><p class="animation animated-item-2">VOUS N'ALLEZ PAS REGRETTER VOTRE CHOIX</p></center>
                                    <br>
									<center><a data-scroll href="#myModal" data-toggle="modal" class="btn btn-primary animation animated-item-3" style="background: #085ecf ;"><h4 style="color: white;">Commencer</h4></a></center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div><!--/.item-->
            </div><!--/.carousel-inner-->
        </div><!--/.carousel-->
        <a class="prev hidden-xs" href="#main-slider" data-slide="prev">
            <i class="icon-angle-left"></i>
        </a>
        <a class="next hidden-xs" href="#main-slider" data-slide="next">
            <i class="icon-angle-right"></i>
        </a>
    </section><!--/#main-slider-->
	<!-- Formulaire Login Modal -->
	<div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
      <br></br><br></br>
      <div class="modal-dialog">
        <div class="modal-content" style="width: 100%;">
            <div class="modal-header">
              <button aria-hidden="true" data-dismiss="modal" class="close" type="button">x</button>
                <h5 class="modal-title"><center><b>PAGE DE CONNEXION</b></center></h5>
            </div>
            <div class="modal-body">
              <form class="login-page form" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <label>Login</label>
								                        <div class="form-group input-group" >
					                                <span class="input-group-addon"><span class="glyphicon-user"></span></span> 
	                                        <asp:TextBox runat="server" ID="txtLogin" class="form-control" placeholder="Votre Login " required AutoPostBack="True"></asp:TextBox>
                                   
	                                      </div>
			                                  <label>Mot de passe</label>
	                                      <div class="form-group input-group ">
					                              <span class="input-group-addon"><span class="glyphicon-lock"></span></span> 
					                              <asp:TextBox runat="server" ID="txtPassword" class="form-control" placeholder="Votre Mot de passe " required type="password" AutoPostBack="True"></asp:TextBox>
                      
					                              </div><br>
                                                  <CENTER><asp:Label runat="server" ID="txtMessage" Text="Vos informations ne sont pas correctes" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label></CENTER>
					                              <CENTER><asp:Button runat="server" class="btn btn-primary" ID="btnConnexion" Text="Connexion" type="submit" style="background: #085ecf ;" OnClick="btnConnexion_Click"/><span class="fa fa-key" AutoPostBack="True"></span></CENTER><br>
                                            <CENTER><a data-scroll href="#myModal2" data-toggle="modal"><i class="glyphicon glyphicon-user"></i> <strong> CREEZ UN NOUVEAU COMPTE CLIENT</strong> </a></CENTER>
                              </ContentTemplate>
                </asp:UpdatePanel>
              </form>
            </div>
          </div>
        </div>
      </div>
      <!-- Fin Login Modal -->

      <!-- Formulaire Création Compte Modal -->
  <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal2" class="modal fade">
      <br></br><br></br>
      <div class="modal-dialog">
        <div class="modal-content" style="width: 100%;">
            <div class="modal-header">
              <button aria-hidden="true" data-dismiss="modal" class="close" type="button">x</button>
                <h5 class="modal-title"><center><b>CREER UN COMPTE UTILISATEUR</b></center></h5>
            </div>
            <div class="modal-body">
                <form method="post" action="include/creerCompte.php" class="login-page form">
                    <label>Votre nom complet </label>
					<div class="form-group input-group" >
					    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span> 
	                    <input  type="text" name="nomUser" class="form-control" placeholder="Votre nom complet " >
	                 </div>
                    <label>Votre Login</label>
                    <div class="form-group input-group" >
                        <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span> 
                        <input  type="text" name="login" class="form-control" placeholder="Votre login " >
	                </div>
			        <label>Votre Mot de passe</label>
	                <div class="form-group input-group ">
					      <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span> 
					      <input  type="password" name="motpass" class="form-control" placeholder="Votre Mot de passe " >
					</div>
					<CENTER><button class="btn btn-md"  name="creer" value=" " style="background: #085ecf ;"><span class="glyphicon glyphicon-user "></span>   Créer Compte</button></CENTER><br>
                </form>
            </div>
          </div>
        </div>
      </div>
      <!-- Fin Création Compte Modal -->
	</div><!--  Fin Contenaire -->
	    <!-- Footer -->

        <!-- Javascripts -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
        <script>window.jQuery || document.write('<script src="js/jquery-1.9.1.min.js"><\/script>')</script>
        <script src="js/bootstrap.min.js"></script>
		
		<!-- Scrolling Nav JavaScript -->
		<script src="js/jquery.easing.min.js"></script>
		<script src="js/scrolling-nav.js"></script>		

    </body>
</html>