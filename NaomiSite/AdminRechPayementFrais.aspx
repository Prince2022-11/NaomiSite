<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminRechPayementFrais.aspx.cs" Inherits="NaomiSite.AdminRechPayementFrais" %>

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
		<h2 class="page-title">--- RECHERCHES DANS LES PAYEMENTS FAITS PAR LES ELEVES ---</h2>
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
        <div class="modal-body row" style="overflow:auto">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
             <ContentTemplate>
              <asp:TextBox runat="server" ID="txtIdEcole" ForeColor="Red" Text="" AutoPostBack="True" OnTextChanged="txtIdEcole_TextChanged" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdOption" ForeColor="blue" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdClasse" ForeColor="black" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtMatricule" ForeColor="black" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdOptionMontante" ForeColor="blue" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdClasseMontante" ForeColor="black" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdAnneeMontante" ForeColor="black" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtIdFrais" ForeColor="Red" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>
                                          <asp:TextBox runat="server" ID="txtPrenomEleve" ForeColor="Red" Text="" AutoPostBack="True" Visible="false"></asp:TextBox>

                                     </div>
                                     <div class="col-lg-6 col-md-6">
                                          <asp:TextBox runat="server" ID="TextBox1" ReadOnly="false" AutoPostBack="True" Font-Size="Smaller" ForeColor="Transparent" Width="1px" BorderColor="Transparent"></asp:TextBox>
                                         <label>1. Faire une recherche approfondie</label>
                                          <div class="input-group">
                                               <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Label ID="Label10" runat="server" Text="Recherche " Visible="true"></asp:Label></span>
                                                <asp:TextBox runat="server" ID="txtRecherche" class="form-control"  AutoPostBack="true" placeholder="Par Date de paye, frais payé,Nom élève, Ecole,Classe, Section..." OnTextChanged="txtRecherche_TextChanged"></asp:TextBox>
                                              <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Button runat="server" class="fa fa-download"  ID="btnRechApproFondie" Text="Voir" ForeColor="White" type="submit" style="background: #085ecf ;" CausesValidation="false" AutoPostBack="True" OnClick="btnRechApproFondie_Click"/></span>
                                            <span></span>
                                          </div><br />
                                         <div class="col-lg-6 col-md-6">
                                             <label>2. Recherche par périodes </label>
                                             <div class="input-group">
                                               <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Label ID="Label1" runat="server" Text="D1: " Visible="true"></asp:Label></span>
                                                <asp:TextBox runat="server" ID="txtDate1" type="Date" class="form-control"  AutoPostBack="true"></asp:TextBox>
                                            </div>
                                         </div>
                                         <div class="col-lg-6 col-md-6">
                                             <label>Jusqu'au</label>
                                              <div class="input-group">
                                               <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Label ID="Label2" runat="server" Text="D2: " Visible="true"></asp:Label></span>
                                                <asp:TextBox runat="server" ID="txtDate2" type="Date" class="form-control"  AutoPostBack="true"></asp:TextBox>
                                                  <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Button runat="server" class="fa fa-download"  ID="btnRechIntervalle" Text="Voir" ForeColor="White" type="submit" style="background: #085ecf ;" CausesValidation="false" AutoPostBack="True" OnClick="btnRechIntervalle_Click"/></span>
                                            </div><br />
                                         </div>
                                           <label>4. Compte annuelle de (Completez les listes déroulantes à droit d'abord): </label>
                                              <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                                  <asp:DropDownList ID="txtNomELeve" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" AutoPostBack="True" OnSelectedIndexChanged="txtNomELeve_SelectedIndexChanged">
                                                  </asp:DropDownList>
                                                  <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Button runat="server" class="fa fa-download"  ID="btnCompteELeve" Text="Voir" ForeColor="White" type="submit" style="background: #085ecf ;" CausesValidation="false" AutoPostBack="True" OnClick="btnCompteELeve_Click"/></span>
	                                          </div>
                                         
                                     </div>
                                         <div class="col-lg-6 col-md-6">
                                             <div class="col-lg-6 col-md-6">
                                                 <label>3. Cherchez à l'école : </label>
                                              <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                                  <asp:DropDownList ID="txtEcole" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" AutoPostBack="True" OnSelectedIndexChanged="txtEcole_SelectedIndexChanged">
                                                            <asp:ListItem>MATERNELLE</asp:ListItem>
                                                            <asp:ListItem>PRIMAIRE</asp:ListItem>
                                                            <asp:ListItem>SECONDAIRE</asp:ListItem>
                                                       </asp:DropDownList>
	                                          </div>
                                                 <label>Classe des élèves</label>
                                                  <div class="form-group input-group" >
                                                      <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                                      <asp:DropDownList ID="txtClasse" runat="server"  class="form-control" placeholder="Sélectionnez" AutoPostBack="True" OnSelectedIndexChanged="txtClasse_SelectedIndexChanged">
                                                      </asp:DropDownList>
	                                              </div>
                                                 <div class="form-group input-group" >
                                                  <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                                  <asp:DropDownList ID="txtTranche" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" AutoPostBack="True" OnSelectedIndexChanged="txtEcole_SelectedIndexChanged">
                                                            <asp:ListItem>Tranche1</asp:ListItem>
                                                            <asp:ListItem>Tranche2</asp:ListItem>
                                                            <asp:ListItem>Tranche3</asp:ListItem>
                                                       </asp:DropDownList>
	                                            </div> 
                                                  <asp:Label runat="server" ID="txtT1" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Visible="false"></asp:Label>
                                                  <asp:Label runat="server" ID="txtT2" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Visible="false"></asp:Label>
                                                  <asp:Label runat="server" ID="txtT3" Text="0" ForeColor="Red" Font-Bold="True" AutoPostBack="True" Visible="false"></asp:Label>
                                                  <asp:Label runat="server" ID="txtUnite" Text="0" ForeColor="Black" Font-Bold="True" AutoPostBack="True" Visible="false"></asp:Label>
                                             </div>
                                             <div class="col-lg-6 col-md-6">
                                                 <label>Option d'Etude</label>
                                                <div class="form-group input-group" >
                                                          <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                                          <asp:DropDownList ID="txtOption" runat="server"  class="form-control" placeholder="Sélectionnez" AutoPostBack="True" OnSelectedIndexChanged="txtOption_SelectedIndexChanged">
                                                          </asp:DropDownList>
	                                            </div>
                                                     <label>Sélectionnez un frais </label>
                                                  <div class="form-group input-group" >
                                                      <span class="input-group-addon"><span class="glyphicon glyphicon"></span></span> 
                                                      <asp:DropDownList ID="txtFrais" runat="server"  class="form-control" placeholder="Sélectionnez un sexe" OnSelectedIndexChanged="txtFrais_SelectedIndexChanged" AutoPostBack="True">
                                                           </asp:DropDownList>
	                                              </div> 
                                                 <div>
                                                    <asp:RadioButton ID="btnOrdre" runat="server" Text=" - Ceux qui sont en ordre-" AutoPostBack="true" Checked="True" Font-Size="Medium" ForeColor="Blue" OnCheckedChanged="btnOrdre_CheckedChanged"/><br />
                                                    <asp:RadioButton ID="btnPasenOrdre" runat="server" Text=" - Qui ne sont pas en ordre" AutoPostBack="true" Checked="True"  Font-Size="Medium" ForeColor="Red" OnCheckedChanged="btnPasenOrdre_CheckedChanged"/>
                                                  <span class="input-group-addon"> <span class="fa fa-name"></span><asp:Button runat="server" class="fa fa-download"  ID="btnRechEnOrdreEtPasEnOrdre" Text="Produire un PDF" ForeColor="White" type="submit" style="background: #085ecf ;" CausesValidation="false" AutoPostBack="True" OnClick="btnRechEnOrdreEtPasEnOrdre_Click"/></span>
                                            </div>

                                             </div>
                                            

                                     </div>
                                       <hr style="border:1px solid black; width:100%; margin:20px auto;">
                                         <center><asp:Label runat="server" ID="txtMessage" Text="Tous les payements de cette année" ForeColor="black" Font-Size="Large" Font-Bold="True" AutoPostBack="True"></asp:Label></center>

                        <asp:Repeater ID="Data1" runat="server" OnItemCommand="Data1_ItemCommand">
                                   <HeaderTemplate>
                                 <table class="table table-condansed table-striped" border="2" style="overflow:auto; border:medium ridge black;" >
                                  <thead>
                                    <tr style="background-color:#33CCFF; border:2px dashed black; color: #FFFFFF;">
                                      <%--<th> # </th>--%>
                                      <th> Date </th>
                                      <th> Frais Payé </th>
                                      <th> Montant </th>
                                      <th> Unité </th>
                                      <th> Nom </th>
                                      <th> Prénom </th>
                                      <th> Sexe</th>
                                      <th> Classe </th>
                                      <th> Option </th>
                                      <th> N°Reçu </th>
                                        <th> Action </th>
                                    </tr>
                                  </thead>
                                  <tbody>
                                    </HeaderTemplate>

                                   <ItemTemplate>
                                    <tr>
                                      <%--<td style="border:1px solid black;"> <%#Eval("id_Payement") %></td>--%>
                                        <td style="border:1px solid black;"> <%#Eval("date_payement ") %></td>
                                        <td style="border:1px solid black;"> <%#Eval("DFrais ") %></td>
                                        <td style="border:1px solid black;"> <%#Eval("montantPaye ") %></td>
                                        <td style="border:1px solid black;"> <%#Eval("unite ") %></td>
                                      <td style="border:1px solid black;"> <%#Eval("nom") %></td>
                                    <td style="border:1px solid black;"> <%#Eval("prenom") %></td>
                                      <td style="border:1px solid black;"> <%#Eval("sexe ") %></td>
                                      <td style="border:1px solid black;"> <%#Eval("classe") %></td>
                                        <td style="border:1px solid black;"> <%#Eval("option") %></td>
                                        <td style="border:1px solid black;"> <%#Eval("idRecu ") %></td>
                                        <td style="border:1px solid black;"><a  id="btnImprimer" class="btn btn-primary" style="color:white;font-size: large;font-style: normal;border-color:black;font-weight: bold;background: #085ecf ;" href="AdminRechImprimerRecu.aspx?id=<%#Eval("idRecu")%>">Imprimer Reçu</a></td>
                                    </tr>
                                   </ItemTemplate>

                                   <FooterTemplate>
                                    </tbody>
                                    </table>
                                   </FooterTemplate>
                                </asp:Repeater>
            </ContentTemplate>
             <Triggers>
                    <asp:PostBackTrigger ControlID="btnRechApproFondie" />
             </Triggers>
              <Triggers>
                    <asp:PostBackTrigger ControlID="btnRechIntervalle" />
             </Triggers>
              <Triggers>
                    <asp:PostBackTrigger ControlID="btnCompteELeve" />
             </Triggers>
              <Triggers>
                    <asp:PostBackTrigger ControlID="btnRechEnOrdreEtPasEnOrdre" />
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