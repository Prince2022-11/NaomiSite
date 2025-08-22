<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAgentPresenceNonCloturee.aspx.cs" Inherits="NaomiSite.AdminAgentPresenceNonCloturee" %>

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
		<h1 class="page-title"> --- PRESENCES NON CLOTUREES DU PERSONNELS AYANT PRESTE --- </h1>
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
                                        <CENTER>
                                            <asp:Label runat="server" ID="txtMessage" Text="NB:Assurez-vous que l'heure et la date de votre ordinateur sont à jours avant de cloturer la présence" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Visible="true" Font-Size="medium"></asp:Label><br />
                                        </CENTER>
                                        </div>
                                        <div class="col-lg-6 col-md-6">
                                            <asp:Label runat="server" ID="txtNomEnseignant" Text="L" ForeColor="blue" Font-Bold="True" AutoPostBack="True" Visible="true" Font-Size="medium"></asp:Label>
                                          <div class="input-group">
                                               <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Label ID="Label10" runat="server" Text="Nbre d'Heures prestées" Visible="true"></asp:Label></span>
                                                <asp:TextBox runat="server" ID="txtHeurePrestee" class="form-control"  AutoPostBack="true" placeholder="Saisir un chiffre svp..."></asp:TextBox>
                                              <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Button runat="server" class="fa fa-download"  ID="btnCloturerCours" Text="Finaliser ?" ForeColor="White" type="submit" style="background: #085ecf ;" CausesValidation="false" AutoPostBack="True" OnClick="btnCloturerCours_Click"/></span>
                                            <span></span>
                                          </div>
                                        </div><br /><br />
                                             <asp:Label runat="server" ID="txtM" Text="Le personnel présent pour lequel la cloture de la présence n'a pas encore été faite jusqu'à aujourd'hui " ForeColor="black" Font-Bold="True" AutoPostBack="True" Visible="true" Font-Size="medium"></asp:Label>
                                             <asp:Label runat="server" ID="txtJour" Text="L" ForeColor="blue" Font-Bold="True" AutoPostBack="True" Visible="true" Font-Size="medium"></asp:Label>
                                             <asp:Label runat="server" ID="txtJourAnglais" Text="L" ForeColor="blue" Font-Bold="True" AutoPostBack="True" Visible="false" Font-Size="medium"></asp:Label>
                                             <asp:Label runat="server" ID="txtDate" Text="L" ForeColor="blue" Font-Bold="True" AutoPostBack="True" Visible="true" Font-Size="medium"></asp:Label>
                                             <asp:TextBox runat="server" ID="txtMatricule" ForeColor="blue" Font-Bold="True" AutoPostBack="True" Visible="false" Font-Size="medium"></asp:TextBox>
                                             <asp:TextBox runat="server" ID="txtIdPresence" ForeColor="blue" Font-Bold="True" AutoPostBack="True" Visible="false" Font-Size="medium"></asp:TextBox>
                                             
                                         <script type="text/javascript">
                                             function setmatricule(num) {
                                                 // Récupérer le contrôle TextBox par son ID et lui assigner la valeur du numéro matricule
                                                 document.getElementById('<%= txtMatricule.ClientID %>').value = num;
                                             }
                                        </script>

                                             <asp:Repeater ID="Data2" runat="server" OnItemCommand="Data1_ItemCommand">
                                             <HeaderTemplate>
                                             <table class="table table-condansed table-striped" border="2" style="border:medium ridge black;" >
                                              <thead>
                                                <tr style="background-color:lightgreen; border:2px dashed black; color: #FFFFFF;">
                                                  <th>N°</th>
                                                    <th> Date Présence </th>
                                                    <th> Nom et Post-nom </th>
                                                    <th> Mois de</th>
                                                    <th> Motif</th>
                                                    <th> Heures Prévues</th>
                                                    <th> Pointer à</th>
                                                    <th> Cloturer à</th>
                                                    <th> Heures Ens</th>
                                                    <th> Action</th>
                                                </tr>
                                              </thead>
                                              <tbody>
                                                </HeaderTemplate>

                                               <ItemTemplate>
                                                <tr>
                                                    <td style="border:1px solid black;"> <%#Eval("id_presence") %></td>
                                                  <td style="border:1px solid black;"> <%#Eval("datep") %></td>
                                                  <td style="border:1px solid black;"> <%#Eval("nom") +"-"%><%#Eval("prenom") %></td>
                                                     <td style="border:1px solid black;"> <%#Eval("moisEnseigne") %></td>
                                                    <td style="border:1px solid black;"> <%#Eval("motif") %></td>
                                                    <td style="border:1px solid black;"> <%#Eval("nbHeure")+"H" %></td>
                                                    <td style="border:1px solid black;"> <%#Eval("heure_arriver") %></td>
                                                    <td style="border:1px solid black;"> <%#Eval("heureDepart") %></td>
                                                     <td style="border:1px solid black;"> <%#Eval("nbHenseigne")+ "H" %></td>
                                                    <td style="border:1px solid black;">
                                                      <asp:Button runat="server" class="btn btn-primary" ID="btnCloture" Text="Cloturer ?" type="submit" style="background:#085ecf;" AutoPostBack="True" OnClick="btnCloture_Click" CommandArgument= '<%#Eval("id_presence") %>'></asp:Button>
                                                  </td>
                                                </tr>
                                               </ItemTemplate>

                                               <FooterTemplate>
                                                </tbody>
                                                </table>
                                               </FooterTemplate>
                                            </asp:Repeater>
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
