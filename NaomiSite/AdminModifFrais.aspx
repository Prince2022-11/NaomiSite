<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminModifFrais.aspx.cs" Inherits="NaomiSite.AdminModifFrais" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
	<meta name="description" content="">
	<meta name="author" content="">

	<title>C.S.NAOMI</title>

	<!-- Main Styles -->
	<link rel="stylesheet" href="../assets/styles/style.min.css">
	
	<!-- Material Design Icon -->
	<link rel="stylesheet" href="../assets/fonts/material-design/css/materialdesignicons.css">

	<!-- mCustomScrollbar -->
	<link rel="stylesheet" href="../assets/plugin/mCustomScrollbar/jquery.mCustomScrollbar.min.css">

	<!-- Waves Effect -->
	<link rel="stylesheet" href="../assets/plugin/waves/waves.min.css">

	<!-- Sweet Alert -->
	<link rel="stylesheet" href="../assets/plugin/sweet-alert/sweetalert.css">
	
	<!-- Lightview -->
	<link rel="stylesheet" href="../assets/plugin/lightview/css/lightview/lightview.css">

	<!-- Favicons -->
    <link href="../img/slides/favicon.jpg" rel="icon">

	<!-- Dark Themes -->
	<!-- <link rel="stylesheet" href="../assets/styles/style-black.min.css"> -->

	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" integrity="sha512-SfTiTlX6kk+qitfevl/7LibUOeJWlt9rbyDn92a1DqWOw9vWG2MFoays0sgObmWazO5BQPiFucnnEAjpAB+/Sw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
	<link href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-glyphicons.css" rel="stylesheet">
</head>

<body>
<div class="main-menu">

	<header class="header">
		<a href="AdminEspace.aspx" class="logo"><i><img class="img-circle" height="40" width="40" src="../img/slides/favicon.jpg" alt=""></i> C.S NAOMI</a>
		<button type="button" class="button-close fa fa-times js__menu_close"></button>
		<div class="user">
			<a href="#" class="avatar"><img src="../img/face1.jpg" alt=""><span class="status online"></span></a>
			<h5 class="name"><asp:Label ID="txtLogin" runat="server" Text="Label" class="centered" ForeColor="Red"></asp:Label></h5>
			<h5 class="position"><asp:Label ID="txtRole" runat="server" Text="Label" class="centered" ForeColor="#0099FF"></asp:Label><br /></h5>
            <h5 class="position"><asp:Label ID="txtDesignationAnnee" runat="server" Text="Pas d'année" class="centered" ForeColor="#0099FF"></asp:Label><br /></h5>
            <h5 class="position"><asp:Label ID="txtIdAnnee" runat="server" Text="id" class="centered" ForeColor="#0099FF" Visible="false"></asp:Label><br /></h5>
             <h5 class="position"><asp:Label ID="txtIdEcoleAffectationUser" runat="server" Text="id" class="centered" ForeColor="#0099FF" Visible="false"></asp:Label><br /></h5>
		</div>
		<!-- /.user -->
	</header>
	<!-- /.header -->
	<!-- /.header -->
	<div class="content">
		<div class="navigation">
			<h5 class="title">MENU PRINCIPAL</h5>
			<!-- /.title -->
			<ul class="menu js__accordion">
				<li class="current">
					<a class="waves-effect" href="EspaceAdmin.aspx"><i class="menu-icon mdi mdi-view-dashboard"></i><span>ACCUEIL</span></a>
				</li>
                <li id="ctrlAnnee" runat="server">
					<a class="waves-effect" href="AdminAnneeScolaire.aspx"><i class="menu-icon mdi mdi-account-circle"></i><span>ANNEES SCOLAIRES</span></a>
				</li>
				<li id="ctrlInscription" runat="server">
					<a class="waves-effect" href="AdminInscription.aspx"><i class="menu-icon mdi mdi-account-circle"></i><span>GESTION DES ELEVES</span></a>
				</li>
				<li id="ctrlAgent" runat="server">
					<a class="waves-effect" href="AdminAgent.aspx"><i class="menu-icon mdi mdi-account-circle"></i><span>GESTION DES AGENTS</span></a>
				</li>
                <li id="ctrlFinance" runat="server">
					<a class="waves-effect" href="AdminFinance.aspx"><i class="menu-icon mdi mdi-account-circle"></i><span>GESTION FINANCIERE</span></a>
				</li>
				<li id="ctrlUtilisateur" runat="server">
					<a class="waves-effect" href="AdminUtilisateur.aspx"><i class="menu-icon mdi mdi-account-circle"></i><span>GESTION DES UTILISATEURS</span></a>
				</li>
			</ul>
			
		</div>
		<!-- /.navigation -->
	</div>
	<!-- /.content -->
</div>

<!-- Formulaire Modal -->
<form class="login-page form" runat="server" id="Form1">
<div class="fixed-navbar">
	<div class="pull-left">
		<button type="button" style="margin-left: -80px;" class="menu-mobile-button glyphicon glyphicon-menu-hamburger js__menu_mobile"></button>
		<h1 class="page-title">--- MODIFICATION D'UN FRAIS SCOLAIRE POUR LA CLASSE ---</h1>
		<!-- /.page-title -->
	</div>

	<div class="pull-right">
		<a href="acceuil.aspx" style="color: #ffffff;" class="ico-item mdi mdi-logout"></a>
	</div>
	<!-- /.pull-right -->
	</div>
<!-- /.fixed-navbar -->
</div>
    </div>

<!-- Debut -->

<div id="wrapper">
	<div class="main-content">
		<div class="isotope-filter js__filter_isotope">
			<br>
        <div class="row" style="overflow:auto">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="col-lg-6 col-md-6">
                                             <asp:TextBox runat="server" ID="txtIdEcole" ForeColor="Red" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtIdFrais" ForeColor="Red" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdOption" ForeColor="blue" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdClasse" ForeColor="black" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                        <asp:Label runat="server" ID="Label1" Text="Classe : " ForeColor="Black" Font-Bold="True" AutoPostBack="True"></asp:Label> 
                                          <asp:Label runat="server" ID="txtClasse" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label>
                                         <asp:Label runat="server" ID="txtOption" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label>
                                         <asp:Label runat="server" ID="txtEcole" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                        <asp:Label runat="server" ID="Label0" Text="Libellé du Frais : " ForeColor="Black" Font-Bold="True" AutoPostBack="True"></asp:Label>
                                        <asp:Label runat="server" ID="txtFrais" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                         <label>Unité monetaire concernée</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtUnite" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" required AutoPostBack="True">
                                                        <asp:ListItem>USD</asp:ListItem>
                                                        <asp:ListItem>CDF</asp:ListItem>
                                                   </asp:DropDownList>
	                                      </div> 
                                         <label>Tranche 1</label>
								          <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                              <asp:TextBox runat="server" ID="txtTranche1" class="form-control" placeholder="Combien Pour la Tranche 1" ReadOnly="false" required AutoPostBack="True"></asp:TextBox>
	                                      </div>
                                        <label>Tranche2</label>
								          <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                              <asp:TextBox runat="server" ID="txtTranche2" class="form-control" placeholder="Saisir le nom de la mère" ReadOnly="false" required AutoPostBack="True"></asp:TextBox>
	                                      </div>
                                          <label>Tranche3</label>
								          <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                              <asp:TextBox runat="server" ID="txtTranche3" class="form-control" placeholder="Saisir l'adresse de l'élève ici" ReadOnly="false" required AutoPostBack="True"></asp:TextBox>
	                                      </div><br />
                                                  <CENTER><asp:Label runat="server" ID="txtMessage" Text="Au minimum 1 Tranche doit être supérieure à 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Visible="False"></asp:Label></CENTER>
                                                  <CENTER><asp:Button runat="server" class="btn btn-primary" ID="btnAddStructure" Text="Mettre à jour" type="submit" style="background: #085ecf ;" AutoPostBack="True" OnClick="btnAddStructure_Click"></asp:Button><span></span></CENTER><br>
                                           
                                        </div>
                              </ContentTemplate>
                </asp:UpdatePanel>
	    </div>
		<!-- /.isotope-filter js__filter_isotope -->		
		<?php require_once("../include/footer.php");?>
	</div>
	<!-- /.main-content -->
</div><!--/#wrapper -->
</form>
<!-- Fin -->
	<!-- Plugin JavaScript -->
	<script src="../assets/scripts/jquery.min.js"></script>
	<script src="../assets/scripts/modernizr.min.js"></script>
	<script src="../assets/plugin/bootstrap/js/bootstrap.min.js"></script>
	<script src="../assets/plugin/mCustomScrollbar/jquery.mCustomScrollbar.concat.min.js"></script>
	<script src="../assets/plugin/nprogress/nprogress.js"></script>
	<script src="../assets/plugin/sweet-alert/sweetalert.min.js"></script>
	<script src="../assets/plugin/waves/waves.min.js"></script>

	<!-- Isotope -->
	<script src="../assets/scripts/isotope.pkgd.min.js"></script>
	<script src="../assets/scripts/cells-by-row.min.js"></script>

	<!-- Lightview -->
	<script src="../assets/plugin/lightview/js/lightview/lightview.js"></script>

	<script src="../assets/scripts/main.min.js"></script>

</body>
</html>

