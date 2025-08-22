<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAgentChargeHoraire.aspx.cs" Inherits="NaomiSite.AdminAgentChargeHoraire" %>

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
	<div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
      <br></br><br></br>
      <div class="modal-dialog modal-lg">
        <div class="modal-content" style="width: 100%;">
            <div class="modal-header">
              <button aria-hidden="true" data-dismiss="modal" class="close" type="button" onclick="redirectToAdminAgentChargeHoraire()">X</button>
                <h5 class="modal-title"><center><b>FORMULAIRE D'ATTRIBUTION DES CHARGES HORAIRES </b></center></h5>
            </div>
            <div class="modal-body">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                 <ContentTemplate>
                                          <asp:label runat="server" ID="txtmat" ForeColor="Red" Text="matr" AutoPostBack="True" Font-Size="Medium" Font-Bold="True"></asp:label>
                                         <asp:label runat="server" ID="lblLundi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label> 
                                         <asp:label runat="server" ID="lblMardi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label> 
                                         <asp:label runat="server" ID="lblMercredi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label> 
                                         <asp:label runat="server" ID="lblJeudi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label> 
                                         <asp:label runat="server" ID="lblVendredi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label> 
                                           <asp:label runat="server" ID="lblSamedi" ForeColor="Red" Text="Non" AutoPostBack="True" Font-Size="Medium" Font-Bold="True" Visible="false"></asp:label>  <br />
                                        
                                     <div class="col-lg-6 col-md-6">
                                         
                                         <label>Attribuer à quel agent ?</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                              <asp:DropDownList ID="txtAgent" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" required AutoPostBack="True" OnSelectedIndexChanged="txtAgent_SelectedIndexChanged">
                                                   </asp:DropDownList>
	                                      </div>
                                         <label>Quels sont les cours à lui attribuer ?</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                          <asp:TextBox runat="server" ID="txtCours" class="form-control" placeholder="Saisir les cours de l'agent ici" required AutoPostBack="True"></asp:TextBox>
	                                      </div>
                                         <label>Calcul automatique des Heures/Semaine</label>
                                          <div class="form-group input-group" >
                                              <span class="input-group-addon"><span class="glyphicon glyphicon-money"></span></span> 
	                                          <asp:TextBox runat="server" ID="txtHeure" class="form-control" placeholder="Saisir les cours de l'agent ici" required AutoPostBack="True"></asp:TextBox>
	                                      </div>  
                                          
                                     </div>
                                     <div class="col-lg-6 col-md-6"> 
                                          <label>Cochez les jours de prestation de l'agent par semaine et attribuez les heures aux jours cochés</label>
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
                                                    <CENTER><asp:Label runat="server" ID="txtSuccess" Text="Attribution faite avec succès..." ForeColor="green" Font-Bold="True" AutoPostBack="True" Visible="False" Font-Size="Medium"></asp:Label></CENTER>
                                                  <CENTER><asp:Button runat="server" class="btn btn-primary" ID="btnAddStructure" Text="Soumettre" type="submit" style="background: #085ecf ;" AutoPostBack="True" OnClick="btnAddStructure_Click" ></asp:Button></CENTER>
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
		<h2 class="page-title"> --- LES AGENTS AVEC UNE CHARGE HORAIRE ANNUELLE ---</h2>
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
                 <a data-scroll href="#myModal" data-toggle="modal" class="btn btn-primary animation animated-item-3" style="background: #085ecf ;"><h4 style="color: white;">+ Attribuer une charge Horaire </h4></a>
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
                          <th> # </th>
                          <th> Nom et Post-nom </th>
                          <th> Prénom </th>
                          <th> Cours </th>
                            <th> TotHeure </th>
                          <th> Lundi </th>
                            <th> Mardi </th>
                            <th> Mercredi </th>
                            <th> Jeudi </th>
                            <th> Vendredi </th>
                            <th> Samedi </th>
                          <th> Action</th>
                        </tr>
                      </thead>
                      <tbody>
                        </HeaderTemplate>

                       <ItemTemplate>
                        <tr>
                          <td style="border:1px solid black;"> <%#Eval("idAttribution") %></td>
                          <td style="border:1px solid black;"> <%#Eval("nom") %></td>
                        <td style="border:1px solid black;"> <%#Eval("prenom") %></td>
                          <td style="border:1px solid black;"> <%#Eval("coursAttribue") %></td>
                            <td style="border:1px solid black;"> <%#Eval("totalHeure")+ "H /Semaine" %></td>
                            <td style="border:1px solid black;"> <%#Eval("Lundi ") + "-" %><%#Eval("nbHlundi ") + "H" %></td>
                            <td style="border:1px solid black;"> <%#Eval("Mardi ") + "-" %><%#Eval("nbHmardi ") + "H" %></td>
                            <td style="border:1px solid black;"> <%#Eval("Mercredi")+ "-" %><%#Eval("nbHmercredi") + "H" %></td>
                            <td style="border:1px solid black;"> <%#Eval("Jeudi") + "-" %><%#Eval("nbHjeudi") + "H" %></td>
                            <td style="border:1px solid black;"> <%#Eval("Vendredi ") + "-" %><%#Eval("nbHvendredi ") + "H" %></td>
                            <td style="border:1px solid black;"> <%#Eval("Samedi ") + "-" %><%#Eval("nbHsamedi") + "H" %></td>
                          <td style="border:1px solid black;"><a  id="btnUpdate" class="mdi mdi-select btn btn-success" style="color:white;font-size: large;font-style: normal;border-color:black;font-weight: bold;" href="AdminAgentChargeHoraireModifer.aspx?id=<%#Eval("idAttribution") %>">Modifier</a></td>
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
