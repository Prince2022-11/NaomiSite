using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Text;
using System.Data.OleDb;


namespace NaomiSite
{
    public partial class AdminListeEleves : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            //id = Convert.ToInt32(Request.QueryString["id"].ToString());
            //Vérification de la connexion de la varibale session
            try
            {
                if (Session["autorisation"] != null && (bool)Session["autorisation"] == true)
                {

                    if (!IsPostBack) //Pour que la page ait le pouvoir de modifier le rendue
                    {
                        txtLogin.Text = Session["login"].ToString();

                        // Vérifier l'admin connecté
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("", con);
                        MySqlCommand cmde = con.CreateCommand();
                        cmde.CommandType = CommandType.Text;
                        cmd.CommandText = ("select * from utilisateur WHERE login='" + txtLogin.Text + "'");
                        MySqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            txtRole.Text = dr["service"].ToString();

                            //Controle sur ce qui doit s'afficher selon les restructions
                            ctrlAnnee.Visible = false;
                            ctrlAgent.Visible = false;
                            ctrlFinance.Visible = false;
                            ctrlInscription.Visible = false;
                            ctrlUtilisateur.Visible = false;

                            if (dr["service"].ToString() == "Admin" && dr["idEcole"].ToString() == "Toutes les écoles")
                            {
                                ctrlAnnee.Visible = true;
                                ctrlAgent.Visible = true;
                                ctrlFinance.Visible = true;
                                ctrlInscription.Visible = true;
                                ctrlUtilisateur.Visible = true;
                                txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                            }
                            if (dr["service"].ToString() == "Préfet Secondaire" && dr["idEcole"].ToString() == "3")
                            {
                                ctrlAnnee.Visible = false;
                                ctrlAgent.Visible = true;
                                ctrlFinance.Visible = true;
                                ctrlInscription.Visible = true;
                                ctrlUtilisateur.Visible = false;
                                txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                            }
                            if (dr["service"].ToString() == "Directeur" && (dr["idEcole"].ToString() == "2" || dr["idEcole"].ToString() == "1"))
                            {
                                ctrlAnnee.Visible = false;
                                ctrlAgent.Visible = true;
                                ctrlFinance.Visible = true;
                                ctrlInscription.Visible = true;
                                ctrlUtilisateur.Visible = false;
                                txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                            }
                            if (dr["service"].ToString() == "Comptable" && (dr["idEcole"].ToString() == "3" || dr["idEcole"].ToString() == "2" || dr["idEcole"].ToString() == "1"))
                            {
                                ctrlAnnee.Visible = false;
                                ctrlAgent.Visible = true;
                                ctrlFinance.Visible = true;
                                ctrlInscription.Visible = true;
                                ctrlUtilisateur.Visible = false;
                                txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                            }
                        }
                        con.Close();

                        //Vérification de l'année Active
                        con.Open();
                        MySqlCommand cmd1 = new MySqlCommand("", con);
                        MySqlCommand cmde1 = con.CreateCommand();
                        cmde1.CommandType = CommandType.Text;
                        cmd1.CommandText = ("select * from anneescol WHERE etat='Actif'");
                        MySqlDataReader dr1 = cmd1.ExecuteReader();
                        while (dr1.Read())
                        {
                            txtIdAnnee.Text = dr1["anneeScolaire"].ToString();
                            txtDesignationAnnee.Text = dr1["designation"].ToString();
                        }
                        con.Close();
                        AfficherInscription();
                        TrouverIdEcole();
                        TrouverSection();
                        TrouverIdSection();
                        TrouverClasser();
                        TrouverIdClasse();
                        //AfficherInscription();
                        ListeElev();
                        Data1.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("Acceuil.aspx");
                }

            }
            catch
            {

            }
        }

        public void AfficherInscription()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole FROM change_classe,t_classe,section,ecole WHERE change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND change_classe.idEcole='" + txtIdEcole.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.anneeScolaire='" + txtIdAnnee.Text + "' AND etat='Actif' ORDER BY nom", con);
            cmdB1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
            da.Fill(dt);
            Data1.DataSource = dt;
            Data1.DataBind();
            con.Close();
            Data1.Visible = false;

        }
        public void TrouverIdEcole()
        {
            con.Close();
            con.Open();
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles" || txtRole.Text == "Comptable")
            {
                lblEcole.Visible = true;
                txtEcole.Visible = true;
                MySqlCommand cmdB = con.CreateCommand();
                cmdB.CommandType = CommandType.Text;
                cmdB.CommandText = ("SELECT *from ecole WHERE nomEcole='" + txtEcole.SelectedValue + "'");
                MySqlDataReader dr = cmdB.ExecuteReader();
                while (dr.Read())
                {
                    txtIdEcole.Text = dr["idEcole"].ToString();
                }
            }
            else
            {
                ctrlEcole.Visible = false;
                txtEcole.Visible = false;
                lblEcole.Visible = false;
                txtIdEcole.Text = txtIdEcoleAffectationUser.Text;
            }
            con.Close();
        }
        public void TrouverSection()
        {
            con.Open();
            txtOption.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT nomSection from section WHERE idEcole='" + txtIdEcole.Text + "' ORDER BY nomSection ASC");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtOption.Items.Add(dr["nomSection"].ToString());
            }
            con.Close();
        }
        public void TrouverIdSection()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from section WHERE nomSection='" + txtOption.SelectedValue + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdOption.Text = dr["idSection"].ToString();
            }
            con.Close();
        }
        public void TrouverClasser()
        {
            con.Open();
            txtClasse.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT classe from t_classe WHERE idSection='" + txtIdOption.Text + "' ORDER BY classe ASC");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtClasse.Items.Add(dr["classe"].ToString());
            }
            con.Close();
        }
        public void TrouverIdClasse()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_classe WHERE classe='" + txtClasse.SelectedValue + "' AND idSection='" + txtIdOption.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdClasse.Text = dr["id"].ToString();
            }
            con.Close();
        }
        protected void Data1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void txtEcole_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdEcole();
            TrouverSection();
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
            //AfficherInscription();
            ListeElev();
        }
        protected void txtIdEcole_TextChanged(object sender, EventArgs e)
        {
            TrouverSection();
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
           // AfficherInscription();
            ListeElev();
        }

        protected void txtOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
            //AfficherInscription();
            ListeElev();
        }

        protected void txtClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdClasse();
            //AfficherInscription();
            ListeElev(); 
        }

        protected void DataGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void BtnExtrairePDF_Click(object sender, EventArgs e)
        {
            try
            {
                Random rnd = new Random();
                Document dc = new Document();
                using (System.IO.MemoryStream ms=new System.IO.MemoryStream())
                {
                    PdfWriter.GetInstance(dc, ms);
                    dc.Open();
                    //Le code
                    dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                         Liste des élèves de la : " + txtClasse.SelectedValue + " " + txtOption.SelectedValue + " /" + (txtEcole.SelectedValue).ToUpper() + "\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                         Année Scolaire : " + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                    PdfPTable table = new PdfPTable(4);
                    PdfPCell cell0 = new PdfPCell(new Paragraph("Matricule", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                    cell0.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell0.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell = new PdfPCell(new Paragraph("Nom & Post-nom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell2 = new PdfPCell(new Paragraph("Prénom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell1 = new PdfPCell(new Paragraph("Sexe", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.BackgroundColor = BaseColor.BLACK;

                    table.AddCell(cell0);
                    table.AddCell(cell);
                    table.AddCell(cell2);
                    table.AddCell(cell1);


                    MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gespersonnel; password=");
                    con.Open();
                    string cmd = "SELECT change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole FROM change_classe,t_classe,section,ecole WHERE change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND change_classe.idEcole='" + txtIdEcole.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.anneeScolaire='" + txtIdAnnee.Text + "' AND etat='Actif' ORDER BY nom ASC";
                    MySqlCommand cmde = new MySqlCommand(cmd, con);
                    MySqlDataReader dr = cmde.ExecuteReader();
                    while (dr.Read())
                    {
                        //===========================================================================
                        PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["matricule"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                        cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell4.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["nom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                        cell5.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell5.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell7 = new PdfPCell(new Paragraph(@Convert.ToString(dr["prenom"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                        cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell7.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["sexe"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                        cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell6.BackgroundColor = BaseColor.WHITE;
                        //============================================================================

                        table.AddCell(cell4);
                        table.AddCell(cell5);
                        table.AddCell(cell7);
                        table.AddCell(cell6);

                    }
                    con.Close();

                    dc.Add(table);
                    dc.Add(new Paragraph("\n"));
                    dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                    dc.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ListesEleve.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
                dc.Close();
            }
            catch
            {

            }
        }
        public void ListeElev()
        {
            con.Open();
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles")
            {
                MySqlCommand cmdB1 = new MySqlCommand("SELECT change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,change_classe.classe as classe,change_classe.optionEtude as option,change_classe.idEcole as idEcole,change_classe.anneeScolaire as idAnnee FROM change_classe,t_classe,section,ecole WHERE change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND change_classe.idEcole='" + txtIdEcole.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.anneeScolaire='" + txtIdAnnee.Text + "' AND etat='Actif' ORDER BY nom", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                DataGrid.DataSource = dt;
                DataGrid.DataBind();
            }
            else
            {
                MySqlCommand cmdB1 = new MySqlCommand("SELECT change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,change_classe.classe as classe,change_classe.optionEtude as option,change_classe.idEcole as idEcole,change_classe.anneeScolaire as idAnnee FROM change_classe,t_classe,section,ecole WHERE change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND change_classe.idEcole='" + txtIdEcoleAffectationUser.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.anneeScolaire='" + txtIdAnnee.Text + "' AND etat='Actif' ORDER BY nom", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                DataGrid.DataSource = dt;
                DataGrid.DataBind();
            }
        }
        //protected void btnExporterCsv_Click(object sender, EventArgs e)
        //{
        //    AfficherInscription();
        //    DataTable dt = (DataTable)Data1.DataSource;

        //    StringBuilder sb = new StringBuilder();
        //    string[] columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();
        //    sb.AppendLine(string.Join(",", columnNames));

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        string[] fields = row.ItemArray.Select(field => field.ToString()).ToArray();
        //        sb.AppendLine(string.Join(",", fields));
        //    }

        //    // Enregistrer sur le serveur dans /Export
        //    string fileName = "ListeEleves.csv";
        //    string filePath = Server.MapPath("~/Export/" + fileName);

        //    File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

        //    // Proposer le téléchargement ensuite
        //    Response.Clear();
        //    Response.ContentType = "text/csv";
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        //    Response.WriteFile(filePath);
        //    Response.End();
        //}
        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=Feuil1.xls"); // ou .xlsx si tu veux
            Response.Charset = "";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                DataGrid.AllowPaging = false;//Désactiver la pagination si activée
                DataGrid.RenderControl(hw); // rend uniquement le gridview
                Response.Output.Write(sw.ToString());
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.End();
            }
        }

        // Pour éviter une erreur "Control 'DataGrid' of type 'GridView' must be placed inside a form tag"
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Nécessaire pour l'export
        }


    }
}