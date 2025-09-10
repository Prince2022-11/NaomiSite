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
using System.Globalization;
using System.Threading;

namespace NaomiSite
{
    public partial class AdminFinAvanceOct : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gestion_naomi");
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            //id = Convert.ToInt32(Request.QueryString["id"].ToString());
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
                    AgentAvecAvance();
                }
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }

        public void AgentAvecAvance()
        {
            con.Open();
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles" || txtLogin.Text == "Comptable")
            {
                MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM t_agent,t_salaire_avance,ecole,utilisateur WHERE t_salaire_avance.matricule=t_agent.matricule AND t_salaire_avance.anneescol='" + txtIdAnnee.Text + "' AND t_salaire_avance.idEcole=ecole.idEcole AND t_salaire_avance.idOperateur=utilisateur.id ORDER BY dateAvance DESC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data2.DataSource = dt;
                Data2.DataBind();
            }
            else
            {
                MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM t_agent,t_salaire_avance,ecole,utilisateur WHERE t_salaire_avance.matricule=t_agent.matricule AND t_salaire_avance.anneescol='" + txtIdAnnee.Text + "' AND  t_salaire_avance.idEcole='" + txtIdEcoleAffectationUser.Text + "' AND t_salaire_avance.idEcole=ecole.idEcole AND t_salaire_avance.idOperateur=utilisateur.id ORDER BY dateAvance DESC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data2.DataSource = dt;
                Data2.DataBind();
            }
            con.Close();
        }
        protected void btnSituationPresence_Click(object sender, EventArgs e)
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
                dc.Add(new Paragraph("                              Situation des avances sur salaire en : " + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                PdfPTable table = new PdfPTable(7);
                PdfPCell cell0 = new PdfPCell(new Paragraph("Date", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell0.HorizontalAlignment = Element.ALIGN_CENTER;
                cell0.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell = new PdfPCell(new Paragraph("Ecole", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell2 = new PdfPCell(new Paragraph("Matricule", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell1 = new PdfPCell(new Paragraph("Nom,PostNom & Prénom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3 = new PdfPCell(new Paragraph("Montant avance", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3a = new PdfPCell(new Paragraph("Mois", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell3a.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3a.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3c = new PdfPCell(new Paragraph("Opérateur", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell3c.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3c.BackgroundColor = BaseColor.BLACK;

                table.AddCell(cell0);
                table.AddCell(cell);
                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
                table.AddCell(cell3a);
                table.AddCell(cell3c);

                MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                con.Open();
                string cmd = "SELECT * FROM t_agent,t_salaire_avance,ecole,utilisateur WHERE t_salaire_avance.matricule=t_agent.matricule AND t_salaire_avance.anneescol='" + txtIdAnnee.Text + "' AND t_salaire_avance.idEcole=ecole.idEcole AND t_salaire_avance.idOperateur=utilisateur.id ORDER BY dateAvance DESC";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    //===========================================================================
                    PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["dateAvance"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell4.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["nomEcole"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell5.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7 = new PdfPCell(new Paragraph(@Convert.ToString(dr["matricule"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["nom"] + "-" + dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell6.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7a = new PdfPCell(new Paragraph(@Convert.ToString(dr["montantAvance"] + "USD"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7a.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7a.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7c = new PdfPCell(new Paragraph(@Convert.ToString(dr["mois"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7c.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7c.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7d = new PdfPCell(new Paragraph(@Convert.ToString(dr["login"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7d.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7d.BackgroundColor = BaseColor.WHITE;
                    //============================================================================

                    table.AddCell(cell4);
                    table.AddCell(cell5);
                    table.AddCell(cell6);
                    table.AddCell(cell7);
                    table.AddCell(cell7a);
                    table.AddCell(cell7c);
                    table.AddCell(cell7d);

                }
                con.Close();

                dc.Add(table);
                dc.Add(new Paragraph("\n"));
                dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                dc.Close();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=AvanceSalaire.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
            dc.Close();
        }

    }
}