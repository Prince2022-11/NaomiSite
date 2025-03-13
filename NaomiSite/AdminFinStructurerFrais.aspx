<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminFinStructurerFrais.aspx.cs" Inherits="NaomiSite.AdminFinStructurerFrais" %>

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
              <button aria-hidden="true" data-dismiss="modal" class="close" type="button" onclick="redirectToAdminInscription()">X</button>
                <h5 class="modal-title"><center><b>FORMULAIRE D'INSCRIPTION </b></center></h5>
            </div>
            <div class="modal-body">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox runat="server" ID="txtIdEcole" ForeColor="Red" Text="" AutoPostBack="True" OnTextChanged="txtIdEcole_TextChanged" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdOption" ForeColor="blue" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdClasse" ForeColor="black" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          
                                     <div class="col-lg-6 col-md-6">
                                         <label>Ecole-Niveau</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtEcole" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" required AutoPostBack="True" OnSelectedIndexChanged="txtEcole_SelectedIndexChanged">
                                                        <asp:ListItem>MATERNELLE</asp:ListItem>
                                                        <asp:ListItem>PRIMAIRE</asp:ListItem>
                                                        <asp:ListItem>SECONDAIRE</asp:ListItem>
                                                   </asp:DropDownList>
	                                      </div>
                                         <label>Option Concernée</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtOption" runat="server"  class="form-control" placeholder="Sélectionnez" required AutoPostBack="True" OnSelectedIndexChanged="txtOption_SelectedIndexChanged">
                                              </asp:DropDownList>
	                                      </div>
                                         <label>Classe Concernée</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtClasse" runat="server"  class="form-control" placeholder="Sélectionnez" required AutoPostBack="True" OnSelectedIndexChanged="txtClasse_SelectedIndexChanged">
                                              </asp:DropDownList>
	                                      </div>
                                         <label>Libellé du Frais</label>
								          <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                              <asp:TextBox runat="server" ID="txtLibelle" class="form-control" placeholder="Saisir l'intitulé du frais ici" required AutoPostBack="True"></asp:TextBox>
	                                      </div>
                                     </div>
                                     <div class="col-lg-6 col-md-6">
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
	                                              <asp:TextBox runat="server" ID="txtTranche1" class="form-control" placeholder="Combien Pour la Tranche 1" required AutoPostBack="True"></asp:TextBox>
	                                      </div>
                                        <label>Tranche2</label>
								          <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                              <asp:TextBox runat="server" ID="txtTranche2" class="form-control" placeholder="Saisir le nom de la mère" required AutoPostBack="True"></asp:TextBox>
	                                      </div>
                                          <label>Tranche3</label>
								          <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                              <asp:TextBox runat="server" ID="txtTranche3" class="form-control" placeholder="Saisir l'adresse de l'élève ici" required AutoPostBack="True"></asp:TextBox>
	                                      </div><br />

                                     </div>  
                                                  <CENTER><asp:Label runat="server" ID="txtMessage" Text="Au minimum 1 Tranche doit être supérieure à 0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Visible="False"></asp:Label></CENTER>
                                                  <CENTER><asp:Button runat="server" class="btn btn-primary" ID="btnAddStructure" Text="Soumettre" type="submit" style="background: #085ecf ;" AutoPostBack="True" OnClick="btnAddStructure_Click"></asp:Button><span></span></CENTER><br>
                                           
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
		<h1 class="page-title">ESPACE ADMIN --- STRUCTURATION DES FRAIS SCOLAIRES</h1>
		<!-- /.page-title -->
	</div>

	<div class="pull-right">
		<a href="acceuil.aspx" style="color: #ffffff;" class="ico-item mdi mdi-logout"></a>
	</div>
	<!-- /.pull-right -->
	</div>
<!-- /.fixed-navbar -->
</div>

<!-- Debut -->

<div id="wrapper">
	<div class="main-content">
		<div class="isotope-filter js__filter_isotope">
			<br>
        <div class="row" style="overflow:auto">
            <a data-scroll href="#myModal" data-toggle="modal" class="btn btn-primary animation animated-item-3" style="background: #085ecf ;"><h4 style="color: white;">Ajout d'un frais scolaire</h4></a>
            <div class="input-group">
                   <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Label ID="Label10" runat="server" Text="Recherche approfondie des frais " Visible="true"></asp:Label></span>
                    <asp:TextBox runat="server" ID="txtRecherche" class="form-control"  AutoPostBack="true" placeholder="Par Ecole,Classe, Section, Frais à payer..." OnTextChanged="txtRecherche_TextChanged"></asp:TextBox>
               </div> <br/>
            <asp:Repeater ID="Data1" runat="server">
                       <HeaderTemplate>
                     <table class="table table-condansed table-striped" border="2" style="border:medium ridge black;" >
                      <thead>
                        <tr style="background-color:#33CCFF; border:2px dashed black; color: #FFFFFF;">
                          <th> # </th>
                          <th> Ecole</th>
                          <th> Option </th>
                          <th> Classe</th>
                          <th> Libellé du Frais</th>
                          <th> Unité</th>
                          <th> Tranche1</th>
                            <th> Tranche2</th>
                            <th> Tranche3</th>
                            <th> Action</th>

                        </tr>
                      </thead>
                      <tbody>
                        </HeaderTemplate>

                       <ItemTemplate>
                        <tr>
                          <td style="border:1px solid black;"> <%#Eval("idfrais") %></td>
                          <td style="border:1px solid black;"> <%#Eval("nomEcole ") %></td>
                            <td style="border:1px solid black;"> <%#Eval("nomSection ") %></td>
                            <td style="border:1px solid black;"> <%#Eval("nomClasse") %></td>
                          <td style="border:1px solid black;"> <%#Eval("designation ") %></td>
                            <td style="border:1px solid black;"> <%#Eval("unite ") %></td>
                            <td style="border:1px solid black;"> <%#Eval("tranche1 ") %></td>
                            <td style="border:1px solid black;"> <%#Eval("tranche2 ") %></td>
                          <td style="border:1px solid black;"> <%#Eval("tranche3") %></td>
                          <td style="border:1px solid black;"><a  id="btnUpdate" class="fa fa-edit" style="color:blue;font-size: large;font-style: normal;border-color:black;font-weight: bold;" href="#?id=<%#Eval("idfrais") %>">MODIFIER</a></td>
                        </tr>
                       </ItemTemplate>

                       <FooterTemplate>
                        </tbody>
                        </table>
                       </FooterTemplate>
                    </asp:Repeater>
            
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

