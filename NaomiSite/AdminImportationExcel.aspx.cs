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
using OfficeOpenXml;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Data.OleDb;

namespace NaomiSite
{
    public partial class AdminImportationExcel : System.Web.UI.Page
    {
        private static DataTable dtTempData = new DataTable();
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gestion_naomi");
        
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Vérification de la connexion de la varibale session
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
                    ImportationEleves();

                    if (!IsPostBack)
                    {
                        // Initialiser la DataTable
                        //dtTempData.Columns.Add("Matricule");
                        //dtTempData.Columns.Add("NomEleve");
                        //dtTempData.Columns.Add("PrenomEleve");
                        //dtTempData.Columns.Add("Sexe");
                        //dtTempData.Columns.Add("IdClasse");
                        //dtTempData.Columns.Add("IdOption");
                        //dtTempData.Columns.Add("IdAnnee");
                        //dtTempData.Columns.Add("IdEcole");
                    }
                }

            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void GuideClasseOption()
        {
            Random rnd = new Random();
            Document dc = new Document();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
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
                dc.Add(new Paragraph("                         GUIDE D'IMPORTATION DES FICHIERS ET LES DIFFERANTES ID " + "" + "\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                dc.Add(new Paragraph("                         1. Les annnées Scolaires" + "" + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                //Guide des années scolaires
                PdfPTable tableAnnee = new PdfPTable(2);
                PdfPCell cell0AN = new PdfPCell(new Paragraph("IdAnneeScolaire", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                cell0AN.HorizontalAlignment = Element.ALIGN_CENTER;
                cell0AN.BackgroundColor = BaseColor.BLACK;
                PdfPCell cellAN = new PdfPCell(new Paragraph("Explication", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                cellAN.HorizontalAlignment = Element.ALIGN_LEFT;
                cellAN.BackgroundColor = BaseColor.BLACK;

                tableAnnee.AddCell(cell0AN);
                tableAnnee.AddCell(cellAN);

                MySqlConnection conAN = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                conAN.Open();
                string cmdAN = "SELECT * FROM anneescol ORDER BY anneeScolaire ASC";
                MySqlCommand cmdeAN = new MySqlCommand(cmdAN, conAN);
                MySqlDataReader drAN = cmdeAN.ExecuteReader();
                while (drAN.Read())
                {
                    //===========================================================================
                    PdfPCell cell4AN = new PdfPCell(new Paragraph(@Convert.ToString(drAN["anneeScolaire"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    cell4AN.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell4AN.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell5AN = new PdfPCell(new Paragraph((string)drAN["designation"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    cell5AN.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell5AN.BackgroundColor = BaseColor.WHITE;
                    //============================================================================

                    tableAnnee.AddCell(cell4AN);
                    tableAnnee.AddCell(cell5AN);

                }
                conAN.Close();

                //Guide des Classes
                PdfPTable table = new PdfPTable(6);
                PdfPCell cell0 = new PdfPCell(new Paragraph("CLASSE", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                cell0.HorizontalAlignment = Element.ALIGN_CENTER;
                cell0.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell = new PdfPCell(new Paragraph("SECTION", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell2 = new PdfPCell(new Paragraph("ID CLASSE", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell1 = new PdfPCell(new Paragraph("ID SECTION", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell2N = new PdfPCell(new Paragraph("ECOLE", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                cell2N.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2N.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell1N = new PdfPCell(new Paragraph("ID ECOLE", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.WHITE)));
                cell1N.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1N.BackgroundColor = BaseColor.BLACK;

                table.AddCell(cell0);
                table.AddCell(cell);
                table.AddCell(cell2);
                table.AddCell(cell1);
                table.AddCell(cell2N);
                table.AddCell(cell1N);


                MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                con.Open();
                string cmd = "SELECT *FROM t_classe,section,ecole WHERE t_classe.idSection=section.idSection AND t_classe.idEcole=ecole.idEcole ORDER BY t_classe.id";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    //===========================================================================
                    PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["classe"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell4.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["nomSection"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    cell5.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell5.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7 = new PdfPCell(new Paragraph(@Convert.ToString(dr["id"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["idSection"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell6.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7N = new PdfPCell(new Paragraph(@Convert.ToString(dr["nomEcole"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    cell7N.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7N.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell6N = new PdfPCell(new Paragraph((string)dr["idEcole"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    cell6N.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell6N.BackgroundColor = BaseColor.WHITE;
                    //============================================================================

                    table.AddCell(cell4);
                    table.AddCell(cell5);
                    table.AddCell(cell7);
                    table.AddCell(cell6);
                    table.AddCell(cell7N);
                    table.AddCell(cell6N);

                }
                con.Close();

                dc.Add(tableAnnee);
                dc.Add(new Paragraph("                         2. Les classes, options et ecoles" + "" + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(table);
                dc.Add(new Paragraph("\n"));
                dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                dc.Close();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=GuideImportation.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }

        public void ImportationEleves()
        {
            //con.Open();
            //MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM t_agent,t_salaire_avance,ecole,utilisateur WHERE t_salaire_avance.matricule=t_agent.matricule AND t_salaire_avance.anneescol='" + txtIdAnnee.Text + "' AND t_salaire_avance.idEcole=ecole.idEcole AND t_salaire_avance.idOperateur=utilisateur.id ORDER BY dateAvance DESC", con);
            //cmdB1.ExecuteNonQuery();
            //DataTable dt = new DataTable();
            //MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
            //da.Fill(dt);
            //Data2.DataSource = dt;
            //Data2.DataBind();
            //con.Close();
            //con.Close();
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            txtMessage.Visible = false;
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string filePath = Server.MapPath("~/UploadedFiles/" + fileName);
                FileUpload1.SaveAs(filePath);
                btnFinaliserImportation.Visible = true;
                btnFinaliserImportation.Enabled = true;

                string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;'";

                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Feuil1$]", conn);
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Affiche dans le GridView
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Erreur : " + ex.Message);
                    }
                }
            }
        }

        protected void btnFinaliserImportation_Click(object sender, EventArgs e)
        {
            con.Close();
            foreach (GridViewRow row in GridView1.Rows)
            {
                string matricule = row.Cells[0].Text.ToString();
                string nom = row.Cells[1].Text.ToString();
                string prenom = row.Cells[2].Text.ToString();
                string sexe = row.Cells[3].Text.ToString();
                string idClasse = row.Cells[4].Text.ToString();
                string idOption = row.Cells[5].Text.ToString();
                string idAnnee = row.Cells[6].Text.ToString();
                string idEcole = row.Cells[7].Text.ToString();

                // 1. Mettre à jour tous les anciens à "Inactif"
                con.Open();
                MySqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "UPDATE change_classe SET etat='Inactif' WHERE matricule='" + matricule.ToString() + "'";
                cmd1.ExecuteNonQuery();
                con.Close();

                // 2. Insérer une nouvelle ligne avec état = "Actif"
                con.Open();
                MySqlCommand cmd1a = con.CreateCommand();
                cmd1a.CommandType = CommandType.Text;
                cmd1a.CommandText = "insert into change_classe values(default,'" + matricule.ToString() + "','" + nom.ToString() + "','" + prenom.ToString() + "','" + sexe.ToString() + "','" + idClasse.ToString() + "','" + idOption.ToString() + "','" + idAnnee.ToString() + "','" + idEcole.ToString() + "','Actif')";
                cmd1a.ExecuteNonQuery();
                con.Close();
            }
            txtMessage.Visible = true;
            btnFinaliserImportation.Enabled = false;

            //Response.Redirect("AdminImportationExcel.aspx");

        }

        protected void btnGuide_Click(object sender, EventArgs e)
        {
            GuideClasseOption();
        }
    }
}
