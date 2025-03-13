<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminChangeClasseExecute.aspx.cs" Inherits="NaomiSite.AdminChangeClasseExecute" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div> 
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
             <ContentTemplate>
                     <asp:TextBox ID="txtnumMat" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtAnnee" runat="server"></asp:TextBox>
              </ContentTemplate>
         </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
