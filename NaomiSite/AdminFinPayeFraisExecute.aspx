<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminFinPayeFraisExecute.aspx.cs" Inherits="NaomiSite.AdminFinPayeFraisExecute" %>

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
                                        <div class="col-lg-6 col-md-6">
                                            <asp:Label runat="server" ID="txtUnite" Text="0" ForeColor="Black" Font-Bold="True" AutoPostBack="True" Visible="false"></asp:Label><br />
                                            <asp:TextBox runat="server" ID="TextBox1" ReadOnly="false" AutoPostBack="True" Font-Size="Smaller" ForeColor="Transparent" Width="0px" BorderColor="Transparent"></asp:TextBox>
                                             <asp:TextBox runat="server" ID="txtIdEcole" ForeColor="Red" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtIdRecu" ForeColor="Red" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                             <asp:Label runat="server" ID="txtDispo" ForeColor="Red" Text="0" AutoPostBack="True" Visible="false"></asp:Label>
                                             <asp:Label runat="server" ID="txtEntree" ForeColor="Red" Text="0" AutoPostBack="True" Visible="false"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtDernierOperation" ForeColor="Red" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                             <asp:Label runat="server" ID="txtSortie" ForeColor="Red" Text="0" AutoPostBack="True" Visible="false"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtIdFrais" ForeColor="Red" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdOption" ForeColor="blue" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                        <asp:Label runat="server" ID="Label2" Text="Opération de : " ForeColor="Black" Font-Bold="True" AutoPostBack="True"></asp:Label>
                                            <asp:Label runat="server" ID="txtNomEleve" Text="0" ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label>
                                            <asp:Label runat="server" ID="txtMatricule" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label>    
                                            <asp:TextBox runat="server" ID="txtIdClasse" ForeColor="black" Text="" AutoPostBack="True" Visible="false"></asp:TextBox><br />
                                        <asp:Label runat="server" ID="Label1" Text="Classe : " ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label> 
                                          <asp:Label runat="server" ID="txtClasse" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label>
                                         <asp:Label runat="server" ID="txtOption" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label>
                                         <asp:Label runat="server" ID="txtEcole" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                            <hr style="border:1px solid red; width:100%; margin:20px auto;">
                                        <asp:Label runat="server" ID="Label0" Text="Sélectionnez un frais à payer par l'élève" ForeColor="Black" Font-Bold="True" AutoPostBack="True"></asp:Label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtFrais" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" OnSelectedIndexChanged="txtFrais_SelectedIndexChanged" required AutoPostBack="True">
                                                   </asp:DropDownList>
	                                      </div><br />
                                          <div class="col-lg-6 col-md-6">
                                              <asp:Label runat="server" ID="txtUnite1" Text="Le montant prévu en " ForeColor="Black" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                              <label>Tr1 = </label>
                                              <asp:Label runat="server" ID="txtT1" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                              <label>Tr2 = </label>
                                              <asp:Label runat="server" ID="txtT2" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                              <label>Tr3 = </label>
                                              <asp:Label runat="server" ID="txtT3" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                              <label>Le montant à payer</label>
                                           </div>
                                           <div class="col-lg-6 col-md-6">
                                               <asp:Label runat="server" ID="txtUnite2" Text="Evolution en payement en " ForeColor="Blue" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                              <label>Tr1 = </label>
                                              <asp:Label runat="server" ID="txtT11" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                              <label>Tr2 = </label>
                                              <asp:Label runat="server" ID="txtT22" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                              <label>Tr3 = </label>
                                              <asp:Label runat="server" ID="txtT33" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                               <label>Reste à payer = </label>
                                              <asp:Label runat="server" ID="txtReste" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label><br />
                                           </div>  
								          <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                              <asp:TextBox runat="server" ID="txtmontant" class="form-control" placeholder="Saisir ici le montant à payer" ReadOnly="false" OnTextChanged="txtmontant_TextChanged"  required AutoPostBack="True"></asp:TextBox>
                                              <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Button runat="server" class="fa fa-download"  ID="btnConvertir" Text="Convertir dans autre Unité" ForeColor="White" type="submit" style="background: #085ecf ;" AutoPostBack="True" OnClick="btnConvertir_Click" /></span>
	                                      </div> 
                                            <asp:Label runat="server" ID="Label3" Text="NB: Si le montant est un décimal, utilisez la virgule (,) pas un point" ForeColor="Red" Font-Bold="True" Font-Size="Smaller" AutoPostBack="True"></asp:Label><br />
                                            <asp:Label runat="server" ID="txtConvertir" Text="Veuiller Saisir d'abord le montant que l'élève est venue avec à la caise" ForeColor="Red" Font-Bold="True" Font-Size="Smaller" AutoPostBack="True" Visible="false"></asp:Label><br />
                                             <%-- Création de la base pour le message après avoir importer son script avant la fermeture de Body --%>
	                                    <div class="alert  alert-danger"  id="error" runat="server">
		                                    <strong> Vérifiez bien vos champs, ou soit le montant saisi est supérieur au reste de l'élève...</strong>
	                                    </div>
	                                    <div class="alert  alert-success"  id="success" runat="server">
		                                    <strong> Payement fait avec succès, Si vous voulez payer encore sélectionnez encore un frais</strong>
	                                    </div>
                                    <%-- fin de la balise--%>
                                            <CENTER><asp:Button runat="server" class="btn btn-primary" ID="btnAddStructure" Text="Soumettre et Imprimer" type="submit" style="background: #085ecf ;" OnClick="btnAddStructure_Click" CausesValidation="false" AutoPostBack="True"/><span></span></CENTER>
                                            <%--<asp:Button runat="server" class="btn btn-primary" ID="Button1" Text="Effectuer" type="submit" style="background: #085ecf ;" OnClientClick="confirmPayment(); return false;" OnClick="btnAddStructure_Click"/><span></span>--%>
                                                  <CENTER><asp:Label runat="server" ID="txtMessage" Text="Au minimum 1 Tranche doit être supérieure à 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Visible="False"></asp:Label></CENTER><br>
                                          
                                        </div>
                                        <div class="col-lg-6 col-md-6">
                                            <asp:Label runat="server" ID="lblTaux" Text="Taux de change en FC (Ex: 2850)" ForeColor="black" Font-Bold="True" AutoPostBack="True" Visible="false"></asp:Label>
                                             <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                              <asp:TextBox runat="server" ID="txtTaux" class="form-control" placeholder="Saisir ici le taux de change" ReadOnly="false" AutoPostBack="True" Visible="false" OnTextChanged="txtTaux_TextChanged"></asp:TextBox>
	                                      </div>
                                            <asp:Label runat="server" ID="txtMontantVenuAvec" Text="0" ForeColor="black" Font-Bold="True" AutoPostBack="True" Visible="false"></asp:Label>
                                            <asp:Label runat="server" ID="lblEquivalence" Text="Equivalence dans l'autre unité " ForeColor="black" Font-Bold="True" AutoPostBack="True" Visible="false"></asp:Label>
                                             <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                              <asp:TextBox runat="server" ID="txtEquivalenceMontant" class="form-control" placeholder="Equivalence" ReadOnly="false" AutoPostBack="True" Visible="false"></asp:TextBox>
	                                      </div> <br />
                                            <CENTER><asp:Button runat="server" class="btn btn-primary" ID="btnValiderAvecConversion" Text="Convertir" type="submit" style="background: #085ecf ;" CausesValidation="false" AutoPostBack="True" OnClick="btnValiderAvecConversion_Click" Visible="false"/></CENTER>

                                            <div id="divFacture" style="display:none;">
                                                <h6>
                                                    C.S NAOMI
                                                    <p>RECU DE PAYEMENT</p>
                                                    <p>N°<span id="factureMatricule"></span></p>
                                                    <p>Nom : <span id="factureNom"></span></p>
                                                    <p>Classe : <span id="factureClasse"></span></p>
                                                    .................................................
                                                    <p>Date : <span id="factureDateRecu"></span></p>
                                                    <p>Montant Payé : <span id="factureMontant"></span></p>
                                                    <p>Frais payé : <span id="factureFrais"></span></p>
                                                    <p>Somme prévue à payer en <span id="factureUnite1"></span></p>
                                                    <p>Tr1=<span id="factureTranchePrevu"></span></p>
                                                    .................................................
                                                    <p>Votre Niveau en payement <span id="factureUnite2"></span></p>
                                                    <p>Tr1=<span id="factureTranchePaye"></span></p>
                                                    <p>Reste : <span id="factureReste"></span></p>
                                                    <p>Imprimé par <span id="factureLogin"></span></p>
                                                    .................................................
                                                </h6>

                                            </div>
                                            <%--<div>
                                                 <asp:Button ID="btnImprimerFacture" runat="server" Text="Imprimer Facture" OnClick="btnImprimerFacture_Click"/>
                                                
                                            </div>--%>
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

     <!--Script pour message erreur ou succes-->
    <script src="~/js/jquery.js"></script>
		 <script>
		$(document).ready(function(){
			$("#error").fadeTo(1000, 100).slideUp(1000, function(){
					$("#error").slideUp(1000);
			});
			
			$("#success").fadeTo(1000, 100).slideUp(1000, function(){
					$("#success").slideUp(1000);
			});
		});
	</script>
	<!-- Fin script message-->

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

