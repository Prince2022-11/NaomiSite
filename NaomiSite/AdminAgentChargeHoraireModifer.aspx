<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAgentChargeHoraireModifer.aspx.cs" Inherits="NaomiSite.AdminAgentChargeHoraireModifer" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">
	<meta name="description" content="">
	<meta name="author" content="">

	<title>EspaceAdmin</title>

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

     <script type="text/javascript">
        function imprimerFacture() {
            var impressionDiv = document.getElementById("divFacture");
            var fenetreImpression = window.open('', '', 'height=400,width=600');
            fenetreImpression.document.write('<html<head><title>Reçu Payement</title>');
            fenetreImpression.document.write('</head><body>');
            fenetreImpression.document.write(impressionDiv.innerHTML);
            fenetreImpression.document.write('</body></html>');
            fenetreImpression.document.close();
            fenetreImpression.print();
        }

        function setFactureData(matricule, nom, classe, dateRecu, montant, frais, unite1, prevu, unite2, apayer, reste, login) {
            document.getElementById('factureMatricule').innerText = matricule;
            document.getElementById('factureNom').innerText = nom;
            document.getElementById('factureClasse').innerText = classe;
            document.getElementById('factureDateRecu').innerText = dateRecu;
            document.getElementById('factureMontant').innerText = montant;
            document.getElementById('factureFrais').innerText = frais;
            document.getElementById('factureUnite1').innerText = unite1;
            document.getElementById('factureTranchePrevu').innerText = prevu;
            document.getElementById('factureUnite2').innerText = unite2;
            document.getElementById('factureTranchePaye').innerText = apayer;
            document.getElementById('factureReste').innerText = reste;
            document.getElementById('factureLogin').innerText = login;
        }
    </script>
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
                <li>
					<a class="waves-effect" href="AdminAnneeScolaire.aspx"><i class="menu-icon mdi mdi-account-circle"></i><span>ANNEES SCOLAIRES</span></a>
				</li>
				<li>
					<a class="waves-effect" href="AdminInscription.aspx"><i class="menu-icon mdi mdi-account-circle"></i><span>GESTION DES ELEVES</span></a>
				</li>
				<li>
					<a class="waves-effect" href="AdminAgent.aspx"><i class="menu-icon mdi mdi-account-circle"></i><span>GESTION DES AGENTS</span></a>
				</li>
                <li>
					<a class="waves-effect" href="AdminFinance.aspx"><i class="menu-icon mdi mdi-account-circle"></i><span>GESTION FINANCIERE</span></a>
				</li>
				<li>
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
		<h1 class="page-title">ESPACE ADMIN --- EFFECTUER LE PAYEMENT D'UN FRAIS POUR ELEVE</h1>
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
                                         <asp:TextBox runat="server" ID="txtmat" ForeColor="Red" Text="" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false" OnTextChanged="txtmat_TextChanged"></asp:TextBox>
                                        <asp:label runat="server" ID="txtIdAttribution" ForeColor="Red" Text="matr" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label>
                                         <asp:label runat="server" ID="lblLundi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label> 
                                         <asp:label runat="server" ID="lblMardi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label> 
                                         <asp:label runat="server" ID="lblMercredi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label> 
                                         <asp:label runat="server" ID="lblJeudi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label> 
                                         <asp:label runat="server" ID="lblVendredi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label> 
                                           <asp:label runat="server" ID="lblSamedi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label>  <br />
                                        
                                     <div class="col-lg-6 col-md-6">
                                         <label>Nom de l'Agent</label>
                                         <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                          <asp:TextBox runat="server" ID="txtNomAgent" class="form-control" placeholder="Nom de l'agent" AutoPostBack="True"></asp:TextBox>
	                                      </div>
                                         <label>Les cours attribués</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                          <asp:TextBox runat="server" ID="txtCours" class="form-control" placeholder="Saisir les cours de l'agent ici" required AutoPostBack="True"></asp:TextBox>
	                                      </div>
                                         <label>Les heures par semaine</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                          <asp:TextBox runat="server" ID="txtHeure" class="form-control" placeholder="Saisir les cours de l'agent ici" required AutoPostBack="True"></asp:TextBox>
	                                      </div>  
                                          
                                     </div>
                                     <div class="col-lg-6 col-md-6"> 
                                          <label>Cochez les jours de prestation de l'agent par semaine</label>
                                          <div class="col-lg-4 col-md-4">
                                              <div class="form-group input-group" >
                                                  <asp:CheckBox ID="btnLundi" runat="server" Text=" Lundi" AutoPostBack="true" Checked="True" Font-Size="Small" ForeColor="Blue"  OnCheckedChanged="btnLundi_CheckedChanged"/>
	                                          <asp:TextBox runat="server" ID="txtLundi" class="form-control" placeholder="Combien d'heure ?"  AutoPostBack="True" OnTextChanged="txtLundi_TextChanged"></asp:TextBox>
	                                          </div>
                                              <div class="form-group input-group" >
                                                <asp:CheckBox ID="btnMardi" runat="server" Text=" Mardi" AutoPostBack="true" Checked="True"  Font-Size="Small" ForeColor="Blue" OnCheckedChanged="btnLundi_CheckedChanged"/>
	                                            <asp:TextBox runat="server" ID="txtMardi" class="form-control" placeholder="Combien d'heure ?"  AutoPostBack="True" OnTextChanged="txtLundi_TextChanged"></asp:TextBox>
	                                          </div>
                                              <div class="form-group input-group" >
                                                <asp:CheckBox ID="btnMercredi" runat="server" Text=" Mercredi" AutoPostBack="true" Checked="True" Font-Size="Small" ForeColor="Blue" OnCheckedChanged="btnLundi_CheckedChanged"/>
                                                <asp:TextBox runat="server" ID="txtMercredi" class="form-control" placeholder="Combien d'heure ?"  AutoPostBack="True" OnTextChanged="txtLundi_TextChanged"></asp:TextBox>
	                                          </div>
                                          </div>
                                          <div class="col-lg-5 col-md-5">
                                              <div class="form-group input-group" >
                                                <asp:CheckBox ID="btnJeudi" runat="server" Text=" Jeudi" AutoPostBack="true" Checked="True" Font-Size="Small" ForeColor="Blue" OnCheckedChanged="btnLundi_CheckedChanged"/>
	                                          <asp:TextBox runat="server" ID="txtJeudi" class="form-control" placeholder="Combien d'heure ?"  AutoPostBack="True" OnTextChanged="txtLundi_TextChanged"></asp:TextBox>
	                                          </div>
                                              <div class="form-group input-group" >
                                                <asp:CheckBox ID="btnVendredi" runat="server" Text=" Vendredi" AutoPostBack="true" Checked="True" Font-Size="Small" ForeColor="Blue" OnCheckedChanged="btnLundi_CheckedChanged"/>
	                                            <asp:TextBox runat="server" ID="txtVendredi" class="form-control" placeholder="Combien d'heure ?"  AutoPostBack="True" OnTextChanged="txtLundi_TextChanged"></asp:TextBox>
	                                          </div>
                                              <div class="form-group input-group" >
                                                <asp:CheckBox ID="btnSamedi" runat="server" Text=" Samedi" AutoPostBack="true" Checked="True" Font-Size="Small" ForeColor="Blue"  OnCheckedChanged="btnLundi_CheckedChanged"/>
	                                            <asp:TextBox runat="server" ID="txtSamedi" class="form-control" placeholder="Combien d'heure ?"  AutoPostBack="True" OnTextChanged="txtLundi_TextChanged"></asp:TextBox><br />
	                                          </div>
                                          </div>
                                                
                                            
                                     </div>  
                                                  <CENTER><asp:Label runat="server" ID="txtMessage" Text="Un de vos champs est vide ou vous n'avez pas coché un des jours de prestation et compléter les heures" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Visible="False"></asp:Label></CENTER>
                                                    <CENTER><asp:Label runat="server" ID="txtSuccess" Text="Modification faite avec succès..." ForeColor="green" Font-Bold="True" AutoPostBack="True" Visible="False" Font-Size="Medium"></asp:Label></CENTER>
                                                  <CENTER><asp:Button runat="server" class="btn btn-primary" ID="btnAddStructure" Text="Mettre à jour" type="submit" style="background: #085ecf ;" AutoPostBack="True" OnClick="btnAddStructure_Click" ></asp:Button></CENTER>

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

