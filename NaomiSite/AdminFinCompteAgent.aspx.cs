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
    public partial class AdminFinCompteAgent : System.Web.UI.Page
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
                    TrouverIdEcole();
                    AfficherCompteAgent();
                }
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }

        public void AfficherCompteAgent()
        {
            con.Open();
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles")
            {
                MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM t_agent,compte_agent WHERE compte_agent.matricule=t_agent.matricule AND compte_agent.anneeScolaire='" + txtIdAnnee.Text + "' AND compte_agent.idEcole='" + txtIdEcole.Text + "' ORDER BY nom ASC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data2.DataSource = dt;
                Data2.DataBind();
            }
            else
            {
                MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM t_agent,compte_agent WHERE compte_agent.matricule=t_agent.matricule AND compte_agent.anneeScolaire='" + txtIdAnnee.Text + "' AND t_agent.idEcole='" + txtIdEcoleAffectationUser.Text + "' AND compte_agent.idEcole='" + txtIdEcole.Text + "' ORDER BY nom ASC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data2.DataSource = dt;
                Data2.DataBind();
            }
            con.Close();
        }
        public void TrouverIdEcole()
        {
            con.Close();
            con.Open();
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles")
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
        protected void btnSituationPresence_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminFinRechPayement.aspx");
        }
        protected void txtEcole_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdEcole();
            AfficherCompteAgent();
        }
        protected void txtIdEcole_TextChanged(object sender, EventArgs e)
        {
            AfficherCompteAgent();
        }

        protected void btnDetailPayement_Click(object sender, EventArgs e)
        {
            try
            {
                //Pointer sur l'agent sélectionné
                Button btn = (Button)sender;
                string[] args = btn.CommandArgument.Split(',');//Séparer les valeurs venues dans la commande
                string matricule = args[0];

                MySqlConnection con9 = new MySqlConnection("server=localhost; uid=root; password=; database=gestion_naomi");
                con9.Open();
                MySqlCommand cmd9 = con9.CreateCommand();
                cmd9.CommandType = CommandType.Text;
                cmd9.CommandText = ("SELECT *from t_agent,ecole WHERE t_agent.idEcole=ecole.idEcole AND t_agent.matricule='" + matricule.ToString() + "'");
                MySqlDataReader dr9 = cmd9.ExecuteReader();
                while (dr9.Read())
                {
                    txtNomAgent.Text = dr9["nom"].ToString() + " " + dr9["prenom"].ToString() + " / " + dr9["nomEcole"].ToString();
                }
                con9.Close();

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

                    dc.Add(new Paragraph("                      BULLETIN DE PAIE DE L'AGENT, ANNEE SCOLAIRE " + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                      Nom de l'agent :" + (txtNomAgent.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.BLACK)));


                    PdfPTable table = new PdfPTable(9);
                    PdfPCell cell1a = new PdfPCell(new Paragraph("Mois", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.WHITE)));
                    cell1a.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1a.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell1 = new PdfPCell(new Paragraph("Date", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.WHITE)));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell2 = new PdfPCell(new Paragraph("Salaire /Heure", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.WHITE)));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell3 = new PdfPCell(new Paragraph("Moyenne des H/Mois", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.WHITE)));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell3b = new PdfPCell(new Paragraph("Sal.Base", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.WHITE)));
                    cell3b.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3b.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell5 = new PdfPCell(new Paragraph("Avance Sur Sal.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.WHITE)));
                    cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell5.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell2a = new PdfPCell(new Paragraph("Remboursement", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.WHITE)));
                    cell2a.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2a.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell2b = new PdfPCell(new Paragraph("Reste/Avance", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.WHITE)));
                    cell2b.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2b.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell3a = new PdfPCell(new Paragraph("Net reçu", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.WHITE)));
                    cell3a.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3a.BackgroundColor = BaseColor.BLACK;
                    table.AddCell(cell1a);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);
                    table.AddCell(cell3b);
                    table.AddCell(cell5);
                    table.AddCell(cell2a);
                    table.AddCell(cell2b);
                    table.AddCell(cell3a);

                    MySqlConnection con = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("select t_salaire.id_salaire,t_salaire.mois_payer,t_agent.matricule,t_salaire.salBase,((t_salaire.salBase)/(t_salaire.nombreHeure/4)) as SalHoraire,t_salaire.salbase, t_salaire.nombreHeure/4 as NbrHeure,t_salaire.avance,t_salaire.rembourser,t_salaire.avance-t_salaire.rembourser as Reste_Emp,t_salaire.net_payer,t_salaire.mois_payer, t_salaire.datepaye FROM t_agent,t_salaire WHERE t_salaire.matricule=t_agent.matricule AND t_salaire.matricule='" + matricule.ToString() + "' AND t_salaire.annee='" + txtIdAnnee.Text + "'", con);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        //===========================================================================
                        PdfPCell cell7a = new PdfPCell(new Paragraph((string)dr["mois_payer"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK)));
                        cell7a.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell7a.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell7 = new PdfPCell(new Paragraph((string)dr["datepaye"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK)));
                        cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell7.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell8 = new PdfPCell(new Paragraph(@Convert.ToString(dr["SalHoraire"] + "$"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK)));
                        cell8.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell8.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell9 = new PdfPCell(new Paragraph(@Convert.ToString(dr["NbrHeure"] + "H"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK)));
                        cell9.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell9.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell8a = new PdfPCell(new Paragraph(@Convert.ToString(dr["salBase"] + "$"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK)));
                        cell8a.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell8a.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell10 = new PdfPCell(new Paragraph(@Convert.ToString(dr["avance"] + "$"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK)));
                        cell10.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell10.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell11 = new PdfPCell(new Paragraph(@Convert.ToString(dr["rembourser"] + "$"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK)));
                        cell11.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell11.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell11a = new PdfPCell(new Paragraph(@Convert.ToString(dr["Reste_Emp"] + "$"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK)));
                        cell11a.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell11a.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell9a = new PdfPCell(new Paragraph(@Convert.ToString(dr["net_payer"] + "$"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK)));
                        cell9a.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell9a.BackgroundColor = BaseColor.WHITE;



                        //============================================================================

                        table.AddCell(cell7a);
                        table.AddCell(cell7);
                        table.AddCell(cell8);
                        table.AddCell(cell9);
                        table.AddCell(cell8a);
                        table.AddCell(cell10);
                        table.AddCell(cell11);
                        table.AddCell(cell11a);
                        table.AddCell(cell9a);

                    }
                    MySqlConnection conN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    conN.Open();
                    MySqlCommand cmdN = new MySqlCommand("select sum(salbase) as Total FROM t_agent,t_salaire WHERE t_salaire.matricule=t_agent.matricule AND t_salaire.matricule='" + matricule.ToString() + "' AND t_salaire.annee='" + txtIdAnnee.Text + "'", conN);
                    MySqlDataReader drN = cmdN.ExecuteReader();
                    Chunk ck;
                    Paragraph pr = new Paragraph();
                    while (drN.Read())
                    {
                        ck = new Chunk("                                                                                            Total Payé :  " + @Convert.ToString(drN["Total"]) + "$");
                        pr = new Paragraph(ck);

                        table.AddCell(new Paragraph(@Convert.ToString(drN["Total"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLUE)));
                        PdfPCell cell8b = new PdfPCell(new Paragraph(@Convert.ToString(drN["Total"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLUE)));
                        cell8b.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell8b.BackgroundColor = BaseColor.GREEN;
                        table.AddCell(cell8b);
                    }

                    dc.Add(table);
                    dc.Add(pr);
                    dc.Add(new Paragraph("\n"));
                    dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                    dc.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=BulletinPayeAgent.pdf");
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
    }
}