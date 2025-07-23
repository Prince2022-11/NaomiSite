<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminFinDepense.aspx.cs" Inherits="NaomiSite.AdminFinDepense" %>

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
	<div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
      <br></br><br></br>
      <div class="modal-dialog modal-lg">
        <div class="modal-content" style="width: 100%;">
            <div class="modal-header">
              <button aria-hidden="true" data-dismiss="modal" class="close" type="button" onclick="redirectToAdminAgentAjout()">X</button>
                <h5 class="modal-title"><center><b>EFFECTUER UNE DEPENSE </b></center></h5>
            </div>
            <div class="modal-body">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                 <ContentTemplate>
                                          <asp:TextBox runat="server" ID="txtIdEcole" ForeColor="Red" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:label runat="server" ID="Label1" ForeColor="Red" Text="Agent N° " AutoPostBack="True"></asp:label>
                                          <asp:label runat="server" ID="txtmat" ForeColor="Red" Text="matr" AutoPostBack="True" Font-Size="Medium" Font-Bold="True"></asp:label>
                                      <asp:TextBox runat="server" ID="txtMatricule" ForeColor="black" Text="" AutoPostBack="true" Visible="true" Font-Bold="True" Font-Size="Medium" BackColor="Transparent" BorderColor="Transparent" OnTextChanged="txtMatricule_TextChanged"></asp:TextBox>
                                       <asp:label runat="server" ID="txtDernierMat" ForeColor="Red" Text="matr" AutoPostBack="True" Visible="false"></asp:label> <br />
                                        
                                     <div class="col-lg-6 col-md-6">
                                         
                                         <label>Opération dans la caisse de l'école</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtEcole" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" required AutoPostBack="True" OnSelectedIndexChanged="txtEcole_SelectedIndexChanged">
                                                        <asp:ListItem>MATERNELLE</asp:ListItem>
                                                        <asp:ListItem>PRIMAIRE</asp:ListItem>
                                                        <asp:ListItem>SECONDAIRE</asp:ListItem>
                                                   </asp:DropDownList>
	                                      </div>
                                         <label>Libellé de l'opération (Motif Dépense)</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                          <asp:TextBox runat="server" ID="txtMotif" class="form-control" placeholder="Saisir le nom et le post-nom ici" required AutoPostBack="True"></asp:TextBox>
	                                      </div> 
                                         <label>Unité</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtUnite" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" required AutoPostBack="True">
                                                        <asp:ListItem>USD</asp:ListItem>
                                                        <asp:ListItem>CDF</asp:ListItem>
                                                   </asp:DropDownList>
	                                      </div>
                                         <label>Montant</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                          <asp:TextBox runat="server" ID="txtMontant" class="form-control" placeholder="Saisir le prénom ici" required AutoPostBack="True"></asp:TextBox>
	                                      </div> 
                                     </div>
                                     <div class="col-lg-6 col-md-6"><br />

                                     </div>  
                                                  <CENTER><asp:Label runat="server" ID="txtMessage" Text="Un de vos champs est vide" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Visible="False"></asp:Label></CENTER>
                                                  <CENTER><asp:Button runat="server" class="btn btn-primary" ID="btnAddStructure" Text="Soumettre" type="submit" style="background: #085ecf ;" AutoPostBack="True" OnClick="btnAddStructure_Click" ></asp:Button><span></span></CENTER>
                                                  <CENTER><asp:Button runat="server" class="btn btn-primary" ID="btnModification" Text="Mettre à jour" type="submit" style="background: #085ecf ;" AutoPostBack="True" OnClick="btnModification_Click" ></asp:Button><span></span></CENTER><br><br />
                                           
                         <script type="text/javascript">
                             function setmatricule(num) {
                                 // Récupérer le contrôle TextBox par son ID et lui assigner la valeur du numéro matricule
                                 document.getElementById('<%= txtMatricule.ClientID %>').value = num;
                             }

                        </script>
                        <script type="text/javascript">
                             function redirectToAdminAgentAjout() {
                                 // Actualisé la page
                                 window.location.href = 'AdminAgentAjout.aspx';
                             }
                        </script>
                      </ContentTemplate>
                     </asp:UpdatePanel>
                    

            </div>
          </div>
        </div>
      </div>
      <!-- Fin Login Modal -->

<!-- /.main-menu -->

<div class="fixed-navbar">
	<div class="pull-left">
		<button type="button" style="margin-left: -80px;" class="menu-mobile-button glyphicon glyphicon-menu-hamburger js__menu_mobile"></button>
		<h2 class="page-title">ESPACE ADMIN --- GESTION DES DEPENSES SCOLAIRES</h2>
		<!-- /.page-title -->
	</div>

	<div class="pull-right">
		<a href="acceuil.aspx" style="color: #ffffff;" class="ico-item mdi mdi-logout"></a>
	</div>
	<!-- /.pull-right -->
</div>
<!-- /.fixed-navbar -->

<!-- Debut -->

<div id="wrapper">
	<div class="main-content">
		<div class="isotope-filter js__filter_isotope">
			<br>
        <div class="modal-body row" style="overflow:auto">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
             <ContentTemplate>
                 <a data-scroll href="#myModal" data-toggle="modal" class="btn btn-primary animation animated-item-3" style="background: #085ecf ;"><h4 style="color: white;">Ajouter un agent + </h4></a>
                <div class="input-group">
                   <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Label ID="Label10" runat="server" Text="Recherche " Visible="true"></asp:Label></span>
                        <asp:TextBox runat="server" ID="txtRecherche" class="form-control"  AutoPostBack="true" placeholder="Par nom de l'agent, domaine,niveau,ecole,..." OnTextChanged="txtRecherche_TextChanged"></asp:TextBox>
                        <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Button runat="server" class="fa fa-download"  ID="btnRechApproFondie" Text="Exporter en PDF" ForeColor="White" type="submit" style="background: #085ecf ;" CausesValidation="false" AutoPostBack="True" OnClick="btnRechApproFondie_Click"/></span>
                      <span></span>
                  </div><br />
            <%--Affichage des inscriptions--%>
            <asp:Repeater ID="Data1" runat="server" OnItemCommand="Data1_ItemCommand">
                       <HeaderTemplate>
                     <table class="table table-condansed table-striped" border="2" style="border:medium ridge black;" >
                      <thead>
                        <tr style="background-color:#33CCFF; border:2px dashed black; color: #FFFFFF;">
                          <th> Matricule </th>
                          <th> Nom et Post-nom </th>
                          <th> Prénom </th>
                          <th> Sexe</th>
                          <th> Niveau </th>
                        </tr>
                      </thead>
                      <tbody>
                        </HeaderTemplate>

                       <ItemTemplate>
                        <tr>
                          <td style="border:1px solid black;"> <%#Eval("matricule ") %></td>
                          <td style="border:1px solid black;"> <%#Eval("nom") %></td>
                        <td style="border:1px solid black;"> <%#Eval("prenom") %></td>
                            <td style="border:1px solid black;"> <%#Eval("nomEcole") %></td>
                          <td style="border:1px solid black;"><a data-scroll href="#myModal" data-toggle="modal" onclick="setmatricule('<%# Eval("matricule") %>')" class="mdi mdi-pencil btn btn-primary animation animated-item-3" style="color:white;font-size: large;font-style: normal;border-color:black;font-weight: bold;">Modifier</a></td>
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
</div>
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

