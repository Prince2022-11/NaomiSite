<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminFinPayerSalaire.aspx.cs" Inherits="NaomiSite.AdminFinPayerSalaire" %>

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
            fenetreImpression.document.write('<html<head><title>Bordereau de Payement</title>');
            fenetreImpression.document.write('</head><body>');
            fenetreImpression.document.write(impressionDiv.innerHTML);
            fenetreImpression.document.write('</body></html>');
            fenetreImpression.document.close();
            fenetreImpression.print();
        }

        function setFactureData(matricule, nom, ecole, dateRecu, nbHeure, Valheure, SalBase, Avance, Remb, NetRecu, reste, login) {
            document.getElementById('factureMatricule').innerText = matricule;
            document.getElementById('factureNom').innerText = nom;
            document.getElementById('factureEcole').innerText = ecole;
            document.getElementById('factureDateRecu').innerText = dateRecu;
            document.getElementById('factureNbHeure').innerText = nbHeure;
            document.getElementById('factureValHeure').innerText = Valheure;
            document.getElementById('factureSalBase').innerText = SalBase;
            document.getElementById('factureAvance').innerText = Avance;
            document.getElementById('factureRemboursement').innerText = Remb;
            document.getElementById('factureNetRecu').innerText = NetRecu;
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
             <h5 class="position"><asp:Label ID="txtIdUser" runat="server" Text="0" class="centered" ForeColor="#0099FF" Visible="false"></asp:Label><br /></h5>
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
		<h1 class="page-title">--- PAYEMENT DU SALAIRE DE L'AGENT ---</h1>
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
                                        <asp:Label runat="server" ID="Label2" Text="Payement de: " ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label>
                                            <asp:Label runat="server" ID="txtNomEleve" Text="0" ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label><br />
                                            <asp:Label runat="server" ID="txtMatricule" Text="M" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label>  
                                            <asp:Label runat="server" ID="txtEcole" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True"></asp:Label>
                                            <hr style="border:1px solid red; width:100%; margin:20px auto;">
                                        <asp:Label runat="server" ID="Label0" Text="Sélectionnez un mois à Payer" ForeColor="Black" Font-Bold="True" AutoPostBack="True"></asp:Label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtMois" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" OnSelectedIndexChanged="txtMois_SelectedIndexChanged" AutoPostBack="True">
                                                    <asp:ListItem>Septembre</asp:ListItem>
                                                        <asp:ListItem>Octobre</asp:ListItem>
                                                        <asp:ListItem>Novembre</asp:ListItem>
                                                        <asp:ListItem>Décembre</asp:ListItem>
                                                        <asp:ListItem>Janvier</asp:ListItem>
                                                        <asp:ListItem>Février</asp:ListItem>
                                                        <asp:ListItem>Mars</asp:ListItem>
                                                        <asp:ListItem>Avril</asp:ListItem>
                                                        <asp:ListItem>Mai</asp:ListItem>
                                                        <asp:ListItem>Juin</asp:ListItem>
                                              </asp:DropDownList>
	                                      </div>
                                            
                                            <div class="col-lg-6 col-md-6">
                                                <asp:Label runat="server" ID="lblSalBase" Text="Salaire de base de l'agent pour ce mois en USD " ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtMontantPrimMat" class="form-control" placeholder="Saisir ici le montant à payer" ReadOnly="false" OnTextChanged="txtRembourse_TextChanged"  AutoPostBack="True"></asp:TextBox>

                                            <asp:Label runat="server" ID="Label3" Text="Nbr H.Prestées au mois sélectionné = " ForeColor="black" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label>  
                                            <asp:Label runat="server" ID="txtNbrHeure" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label><br />
                                            <asp:Label runat="server" ID="Label5" Text="Nbr H.Prestées/Semaine = " ForeColor="black" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label>  
                                            <asp:Label runat="server" ID="txtNbrHeureSemaine" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label>
                                            </div>
                                            <div class="col-lg-6 col-md-6">
                                                 <asp:Label runat="server" ID="Label9" Text="Salaire déjà reçu au mois sélectioné= " ForeColor="black" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label>  
                                                <asp:Label runat="server" ID="txtSalDejaRecu" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label>
                                                 <asp:Label runat="server" ID="Label11" Text="USD" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label><br /><br /><br />
                                            </div>
                                           <asp:Label runat="server" ID="Label7" Text="La valeur monétaire de l'Heure, si décimal, utilisez la virgue (,)" ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label><br />
								          <div class="form-group input-group" id="ctrlValHeur" runat="server">
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                              <asp:TextBox runat="server" ID="txtValeurHeure" class="form-control" placeholder="Saisir ici la valeur monétaire de l'heure" ReadOnly="false" OnTextChanged="txtValeurHeure_TextChanged" AutoPostBack="True"></asp:TextBox>
                                              <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Button runat="server" class="fa fa-download"  ID="btnConvertir" Text="Calculer..." ForeColor="White" type="submit" style="background: #085ecf ;" OnClick="btnConvertir_Click" AutoPostBack="True"/></span>
	                                      </div>
                                            <div class="col-lg-6 col-md-6">
                                                <asp:Label runat="server" ID="Label8" Text="Salaire de Base =" ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label> 
                                                <asp:Label runat="server" ID="txtSalBase" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label>
                                                <asp:Label runat="server" ID="Label10" Text="USD" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label><br />
                                                <asp:Label runat="server" ID="Label1" Text="Avance sur salaire =" ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label> 
                                                <asp:Label runat="server" ID="txtAvance" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label>
                                                <asp:Label runat="server" ID="Label4" Text="USD" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label><br />

                                                <asp:Label runat="server" ID="lblResteAvance" Text="Reste sur l'avance = " ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label>
                                                <asp:Label runat="server" ID="txtResteAvance" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label>
                                                <asp:Label runat="server" ID="lblUnite1" Text="USD" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label><br />
                                                <asp:Label runat="server" ID="Label6" Text="Net à payer =" ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label> 
                                                <asp:Label runat="server" ID="txtNetRecu" Text=" 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label>
                                                <asp:Label runat="server" ID="lblUnite2" Text="USD" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="Medium"></asp:Label><br />
                                            </div>
                                            <div class="col-lg-6 col-md-6">
                                                 <asp:Label runat="server" ID="lblRembourse" Text="A Rembourser =" ForeColor="Black" Font-Bold="True" AutoPostBack="True" Font-Size="small"></asp:Label>
                                                 <asp:TextBox runat="server" ID="txtRembourse" class="form-control" placeholder="Saisir ici le montant à rembourser" ReadOnly="false" OnTextChanged="txtRembourse_TextChanged"  AutoPostBack="True"></asp:TextBox><br />
                                            </div>
                                            <asp:TextBox runat="server" ID="txtSept" class="form-control" AutoPostBack="True" Visible="false"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtOcto" class="form-control" AutoPostBack="True" Visible="false"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtNov" class="form-control" AutoPostBack="True" Visible="false"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtDec" class="form-control" AutoPostBack="True" Visible="false"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtJan" class="form-control" AutoPostBack="True" Visible="false"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtFev" class="form-control" AutoPostBack="True" Visible="false"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtMar" class="form-control" AutoPostBack="True" Visible="false"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtAvr" class="form-control" AutoPostBack="True" Visible="false"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtMai" class="form-control" AutoPostBack="True" Visible="false"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtJuin" class="form-control" AutoPostBack="True" Visible="false"></asp:TextBox>
                                           

                                        <asp:Label runat="server" ID="txtUnite" Text="0" ForeColor="Black" Font-Bold="True" AutoPostBack="True" Visible="false"></asp:Label><br />
                                             <asp:TextBox runat="server" ID="txtIdEcole" ForeColor="Red" Text="" AutoPostBack="True" Visible="FALSE"></asp:TextBox>
                                            <asp:TextBox runat="server" ID="txtIdRecu" ForeColor="Red" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                             <asp:Label runat="server" ID="txtDispo" ForeColor="Red" Text="0" AutoPostBack="True" Visible="false"></asp:Label>
                                             <asp:Label runat="server" ID="txtEntree" ForeColor="Red" Text="0" AutoPostBack="True" Visible="false"></asp:Label>
                                            <asp:TextBox runat="server" ID="txtDernierOperation" ForeColor="Red" Text="0" AutoPostBack="True" Visible="false"></asp:TextBox>
                                             <asp:Label runat="server" ID="txtSortie" ForeColor="Red" Text="0" AutoPostBack="True" Visible="false"></asp:Label>

                                             <%-- Création de la base pour le message après avoir importer son script avant la fermeture de Body --%>
	                                    <div class="alert  alert-danger"  id="error" runat="server">
		                                    <strong> Vérifiez bien vos champs, ou soit le montant saisi est supérieur au disponible en caisse...</strong>
	                                    </div>
	                                    <div class="alert  alert-success"  id="success" runat="server">
		                                    <strong> Avance octroyée avec succès</strong>
	                                    </div>
                                    <%-- fin de la balise--%>
                                            <CENTER><asp:Button runat="server" class="btn btn-primary" ID="btnAddStructure" Text="Payer et Imprimer" type="submit" style="background: #085ecf ;" OnClick="btnAddStructure_Click" CausesValidation="false" AutoPostBack="True"/><span></span></CENTER><br />
                                            <%--<asp:Button runat="server" class="btn btn-primary" ID="Button1" Text="Effectuer" type="submit" style="background: #085ecf ;" OnClientClick="confirmPayment(); return false;" OnClick="btnAddStructure_Click"/><span></span>--%>
                                                  
                                                    <asp:Label runat="server" ID="txtCompteExitant" Text="Non" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Font-Size="Medium" Visible="false"></asp:Label>
                                        </div>
                                        <CENTER><asp:Label runat="server" ID="txtMessage" Text="Verifiez bien vos champs ou soit le disponible en caisse est insuffisant pour couvrir ce payement" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Visible="False"></asp:Label></CENTER><br>
                                        <div class="col-lg-6 col-md-6">
                                           <div id="divFacture" style="display:none;">
                                                <h6>
                                                    C.S NAOMI
                                                    <p>BORDEREAU DE PAYEMENT</p>
                                                    <p>N°<span id="factureMatricule"></span></p>
                                                    <p>Nom : <span id="factureNom"></span></p>
                                                    <p>Ecole : <span id="factureEcole"></span></p>
                                                    .................................................
                                                    <p>Date : <span id="factureDateRecu"></span></p>
                                                    <p>NombreHeure/Semaine : <span id="factureNbHeure"></span></p>
                                                    <p>Valeur de l'Heure : <span id="factureValHeure"></span></p>
                                                    <p>Salaire de Base en USD :<span id="factureSalBase"></span></p>
                                                    <p>Avance/Sal en USD :<span id="factureAvance"></span></p>
                                                    .................................................
                                                    <p>Remboursement en USD : <span id="factureRemboursement"></span></p>
                                                    <p>Reste/Avance : <span id="factureReste"></span></p>
                                                    <p>Net Reçu en USD =<span id="factureNetRecu"></span></p>
                                                    <p>Imprimé par <span id="factureLogin"></span></p>
                                                    .................................................
                                                </h6>

                                            </div>
	                                      </div> <br />
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

