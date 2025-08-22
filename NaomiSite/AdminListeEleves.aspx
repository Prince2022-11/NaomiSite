<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminListeEleves.aspx.cs" Inherits="NaomiSite.AdminListeEleves" %>


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
		<h1 class="page-title"> --- EXTRAIRE LA LISTE DES ELEVES PAR CLASSE ---</h1>
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
                                            <label id="lblEcole" runat="server">Ecole-Niveau</label>
                                          <div class="form-group input-group" id="ctrlEcole" runat="server" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtEcole" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" required AutoPostBack="True" OnSelectedIndexChanged="txtEcole_SelectedIndexChanged">
                                                        <asp:ListItem>MATERNELLE</asp:ListItem>
                                                        <asp:ListItem>PRIMAIRE</asp:ListItem>
                                                        <asp:ListItem>SECONDAIRE</asp:ListItem>
                                                   </asp:DropDownList>
	                                      </div>
                                         <label>Option d'Etude</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtOption" runat="server"  class="form-control" placeholder="Sélectionnez" required AutoPostBack="True" OnSelectedIndexChanged="txtOption_SelectedIndexChanged">
                                              </asp:DropDownList>
	                                      </div>
                                        </div>
                                        <div class="col-lg-6 col-md-6">
                                          <label>Classe où prendre les élèves</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtClasse" runat="server"  class="form-control" placeholder="Sélectionnez" required AutoPostBack="True" OnSelectedIndexChanged="txtClasse_SelectedIndexChanged">
                                              </asp:DropDownList>
	                                      </div>
                                             <CENTER>
                                                <asp:Button runat="server" class="btn btn-primary" ID="BtnExtrairePDF" Text="Exporter en PDF" type="submit" style="background: #085ecf ;" AutoPostBack="True" OnClick="BtnExtrairePDF_Click"></asp:Button><br /><br />
                                                 <div>
                                                     <asp:Button runat="server" class="btn btn-primary" ID="btnExportExcel" Text="Exporter en Excel" type="submit" style="background: #0c791e ;" AutoPostBack="True" OnClick="btnExportExcel_Click"></asp:Button>
                                                 </div>
                                            </CENTER>
                                             <asp:TextBox runat="server" ID="txtIdEcole" ForeColor="Red" Text="" AutoPostBack="True" OnTextChanged="txtIdEcole_TextChanged" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdOption" ForeColor="blue" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdClasse" ForeColor="black" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdOptionMontante" ForeColor="blue" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdClasseMontante" ForeColor="black" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdAnneeMontante" ForeColor="black" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>

                                        </div><br /><br />
                                        <!--AFFCHAGE DES ELEMENTS DANS  LE  TABLEAU-->
                      <asp:GridView ID="DataGrid" runat="server" class="table table-condansed table-striped" border="2" style="border:medium ridge deepskyblue;overflow:auto;"
                          AutoGenerateColumns="False" BorderStyle="Solid" background-color="blue"
                          DataKeyNames="idClasse">
                          <Columns>
                              <asp:BoundField HeaderText="Matricule" DataField="matricule"/>
                              <asp:BoundField HeaderText="NomEleve" DataField="nom" />
                              <asp:BoundField HeaderText="PrenomEleve" DataField="prenom" />
                              <asp:BoundField HeaderText="Sexe" DataField="sexe" />
                              <asp:BoundField HeaderText="IdClasse" DataField="classe" />
                              <asp:BoundField HeaderText="IdOption" DataField="option" />
                              <asp:BoundField HeaderText="IdAnnee" DataField="idAnnee" />
                              <asp:BoundField HeaderText="IdEcole" DataField="idEcole" />
                          </Columns>
                      </asp:GridView>

                                             <asp:Repeater ID="Data1" runat="server">
                                             <HeaderTemplate>
                                             <table class="table table-condansed table-striped" border="2" style="border:medium ridge black;" >
                                              <thead>
                                                <tr style="background-color:deepskyblue; border:2px dashed black; color: #FFFFFF;">
                                                   <th> Matricule </th>
                                                      <th> Nom </th>
                                                      <th> Prénom </th>
                                                      <th> Sexe</th>
                                                      <th> Classe </th>
                                                      <th> Option </th>
                                                </tr>
                                              </thead>
                                              <tbody>
                                                </HeaderTemplate>

                                               <ItemTemplate>
                                                <tr>
                                                     <td style="border:1px solid black;"> <%#Eval("matricule ") %></td>
                                                      <td style="border:1px solid black;"> <%#Eval("nom") %></td>
                                                    <td style="border:1px solid black;"> <%#Eval("prenom") %></td>
                                                      <td style="border:1px solid black;"> <%#Eval("sexe ") %></td>
                                                      <td style="border:1px solid black;"> <%#Eval("classe") %></td>
                                                        <td style="border:1px solid black;"> <%#Eval("option") %></td>
                                                </tr>
                                               </ItemTemplate>

                                               <FooterTemplate>
                                                </tbody>
                                                </table>
                                               </FooterTemplate>
                                            </asp:Repeater>
                              </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportExcel" />
                                    </Triggers>
                                     <Triggers>
                                        <asp:PostBackTrigger ControlID="BtnExtrairePDF" />
                                    </Triggers>
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
