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
    public partial class AdminAgentPresence : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
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
                    try
                    {
                        txtJourAnglais.Text = DateTime.Today.DayOfWeek.ToString();
                        txtDate.Text = DateTime.Today.ToShortDateString();
                        jour();
                        MOIS();
                        AfficherAgentConcernes();
                        AgentDejaPointe();
                    }
                    catch { }
                }
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void AfficherAgentConcernes()
        {
            con.Open();
            if (txtJour.Text == "Lundi")
            {

                MySqlCommand cmdB1 = new MySqlCommand("SELECT attribution_horaire.Matricule as Matricule, nom as nom,prenom as prenom, coursAttribue as coursAttribue, nbHlundi as nombreHeure FROM t_agent,attribution_horaire WHERE attribution_horaire.Matricule=t_agent.matricule AND attribution_horaire.idEcole='3' AND Lundi='Oui' AND attribution_horaire.anneeScolaire='" + txtIdAnnee.Text +"' ORDER BY nom ASC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data1.DataSource = dt;
                Data1.DataBind();
                
            }
            if (txtJour.Text == "Mardi")
            {

                MySqlCommand cmdB1 = new MySqlCommand("SELECT attribution_horaire.Matricule as Matricule, nom as nom,prenom as prenom, coursAttribue as coursAttribue, nbHmardi as nombreHeure FROM t_agent,attribution_horaire WHERE attribution_horaire.Matricule=t_agent.matricule AND attribution_horaire.idEcole='3' AND Mardi='Oui' AND attribution_horaire.anneeScolaire='" + txtIdAnnee.Text + "' ORDER BY nom ASC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data1.DataSource = dt;
                Data1.DataBind();

            }
            if (txtJour.Text == "Mercredi")
            {

                MySqlCommand cmdB1 = new MySqlCommand("SELECT attribution_horaire.Matricule as Matricule, nom as nom,prenom as prenom, coursAttribue as coursAttribue, nbHmercredi as nombreHeure FROM t_agent,attribution_horaire WHERE attribution_horaire.Matricule=t_agent.matricule AND attribution_horaire.idEcole='3' AND Mercredi='Oui' AND attribution_horaire.anneeScolaire='" + txtIdAnnee.Text + "' ORDER BY nom ASC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data1.DataSource = dt;
                Data1.DataBind();

            }
            if (txtJour.Text == "Jeudi")
            {

                MySqlCommand cmdB1 = new MySqlCommand("SELECT attribution_horaire.Matricule as Matricule, nom as nom,prenom as prenom, coursAttribue as coursAttribue, nbHjeudi as nombreHeure FROM t_agent,attribution_horaire WHERE attribution_horaire.Matricule=t_agent.matricule AND attribution_horaire.idEcole='3' AND Jeudi='Oui' AND attribution_horaire.anneeScolaire='" + txtIdAnnee.Text + "' ORDER BY nom ASC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data1.DataSource = dt;
                Data1.DataBind();

            }
            if (txtJour.Text == "Vendredi")
            {

                MySqlCommand cmdB1 = new MySqlCommand("SELECT attribution_horaire.Matricule as Matricule, nom as nom,prenom as prenom, coursAttribue as coursAttribue, nbHvendredi as nombreHeure FROM t_agent,attribution_horaire WHERE attribution_horaire.Matricule=t_agent.matricule AND attribution_horaire.idEcole='3' AND Vendredi='Oui' AND attribution_horaire.anneeScolaire='" + txtIdAnnee.Text + "' ORDER BY nom ASC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data1.DataSource = dt;
                Data1.DataBind();

            }
            if (txtJour.Text == "Samedi")
            {

                MySqlCommand cmdB1 = new MySqlCommand("SELECT attribution_horaire.Matricule as Matricule, nom as nom,prenom as prenom, coursAttribue as coursAttribue, nbHsamedi as nombreHeure FROM t_agent,attribution_horaire WHERE attribution_horaire.Matricule=t_agent.matricule AND attribution_horaire.idEcole='3' AND Samedi='Oui' AND attribution_horaire.anneeScolaire='" + txtIdAnnee.Text + "' ORDER BY nom ASC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data1.DataSource = dt;
                Data1.DataBind();

            }
            con.Close();

        }
        public void AgentDejaPointe()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM t_agent,t_presence WHERE t_presence.matricule=t_agent.matricule AND t_presence.annee='"+txtIdAnnee.Text+"' AND t_presence.datep='"+txtDate.Text+"' ORDER BY nom ASC", con);
            cmdB1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
            da.Fill(dt);
            Data2.DataSource = dt;
            Data2.DataBind();
            con.Close();
            con.Close();
        }
        public void jour()
        {
            if (txtJourAnglais.Text == "Monday")
            {
                txtJour.Text = "Lundi";
            }
            if (txtJourAnglais.Text == "Tuesday")
            {
                txtJour.Text = "Mardi";
            }
            if (txtJourAnglais.Text == "Wednesday")
            {
                txtJour.Text = "Mercredi";
            }
            if (txtJourAnglais.Text == "Thursday")
            {
                txtJour.Text = "Jeudi";
            }
            if (txtJourAnglais.Text == "Friday")
            {
                txtJour.Text = "Vendredi";
            }
            if (txtJourAnglais.Text == "Saturday")
            {
                txtJour.Text = "Samedi";
            }
        }
        public void MOIS()
        {
            string moisAnglais = DateTime.Today.Month.ToString();
            //txtMois.Text= DateTime.Today.Month.ToString();
            if (moisAnglais.ToString() == "1")
            {
                txtMois.Text = "Janvier";
            }
            if (moisAnglais.ToString()== "2")
            {
                txtMois.Text = "Février";
            }
            if (moisAnglais.ToString() == "3")
            {
                txtMois.Text = "Mars";
            }
            if (moisAnglais.ToString() == "4")
            {
                txtMois.Text = "Avril";
            }
            if (moisAnglais.ToString() == "5")
            {
                txtMois.Text = "Mai";
            }
            if (moisAnglais.ToString() == "6")
            {
                txtMois.Text = "Juin";
            }
            if (moisAnglais== "7")
            {
                txtMois.Text = "Juillet";
            }
            if (moisAnglais.ToString() == "8")
            {
                txtMois.Text = "Août";
            }
            if (moisAnglais.ToString() == "9")
            {
                txtMois.Text = "Septembre";
            }
            if (moisAnglais.ToString() == "10")
            {
                txtMois.Text = "Octobre";
            }
            if (moisAnglais.ToString() == "11")
            {
                txtMois.Text = "Novembre";
            }
            if (moisAnglais.ToString() == "12")
            {
                txtMois.Text = "Décembre";
            }
        }
        protected void PointerPresence_Click(object sender, EventArgs e)
        {
            //Pointer sur l'agent sélectionné
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');//Séparer les valeurs venues dans la commande

            string matricule = args[0];//Recupérer le matricule
            string HeurPrevue = args[1];//Recupérer l'heure prévue

            //Vérification si la présence n'est pas encore pointé
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select * from t_presence WHERE matricule='" + matricule.ToString() + "' AND datep='" + txtDate.Text + "'");
            MySqlDataReader dr = cmd.ExecuteReader();

            txtMatricule.Text = "";
            txtMessage.Text = "Assurez-vous que l'heure et la date de votre ordinateur sont à jours avant de pointer";
            while (dr.Read())
            {
                txtMatricule.Text = dr["matricule"].ToString();
            }
            if (txtMatricule.Text == "")
            {
                //Commencer à pointer la présence si l'on rencontre qu'elle n'était pas encore pointée
                con.Close();
                con.Open();
                string Heure = DateTime.Now.ToShortTimeString();
                MySqlCommand cmd1a = con.CreateCommand();
                cmd1a.CommandType = CommandType.Text;
                cmd1a.CommandText = "insert into t_presence values(default,'" + matricule.ToString() + "','" + Heure.ToString() + "','Présent','" + txtDate.Text + "','" + HeurPrevue.ToString() + "','En cours','En cours','" + txtMois.Text + "','" + txtIdAnnee.Text + "')";
                cmd1a.ExecuteNonQuery();
                Response.Redirect("AdminAgentPresence.aspx");
            }
            else
            {
                txtMessage.Text = "Vous avez déjà pointé pour cet agent, vous ne pouvez pas le faire 2 fois pour un même jour";
            }
            con.Close();
        }
        protected void PointerAbsence_Click(object sender, EventArgs e)
        {
            //Pointer sur l'agent sélectionné
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');//Séparer les valeurs venues dans la commande

            string matricule = args[0];//Recupérer le matricule
            string HeurPrevue = args[1];//Recupérer l'heure

            //Vérification si l'absence n'est pas encore pointé
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select * from t_presence WHERE matricule='" + matricule.ToString() + "' AND datep='" + txtDate.Text + "'");
            MySqlDataReader dr = cmd.ExecuteReader();

            txtMatricule.Text = "";
            txtMessage.Text = "Assurez-vous que l'heure et la date de votre ordinateur sont à jours avant de pointer";
            while (dr.Read())
            {
                txtMatricule.Text = dr["matricule"].ToString();
            }
            if (txtMatricule.Text == "")
            {
                //Commencer à pointer l'absence si l'on rencontre qu'elle n'était pas encore pointée
                con.Close();
                con.Open();
                string Heure = DateTime.Now.ToShortTimeString();
                MySqlCommand cmd1a = con.CreateCommand();
                cmd1a.CommandType = CommandType.Text;
                cmd1a.CommandText = "insert into t_presence values(default,'" + matricule.ToString() + "','-','Absent','" + txtDate.Text + "','" + HeurPrevue.ToString() + "','-','-','" + txtMois.Text + "','" + txtIdAnnee.Text + "')";
                cmd1a.ExecuteNonQuery();
                Response.Redirect("AdminAgentPresence.aspx");
            }
            else
            {
                txtMessage.Text = "Vous avez déjà pointé pour cet agent, vous ne pouvez pas le faire 2 fois pour un même jour";
            }
            con.Close();
        }
        protected void Data1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void txtMatricule_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void SupprimerPresenceAbsence_Click(object sender, EventArgs e)
        {
            //Pointer sur l'agent sélectionné
            Button btn = (Button)sender;
            string matricule = btn.CommandArgument;//Recupérer le matricule

            //Vérification si l'absence n'est pas encore pointé
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select * from t_presence WHERE matricule='" + matricule.ToString() + "' AND datep='" + txtDate.Text + "'");
            MySqlDataReader dr = cmd.ExecuteReader();

            txtMatricule.Text = "";
            txtMessage.Text = "Assurez-vous que l'heure et la date de votre ordinateur sont à jours avant de pointer";
            while (dr.Read())
            {
                txtMatricule.Text = dr["matricule"].ToString();
            }
            if (txtMatricule.Text =="")
            {
                
            }
            else
            {
                //Commencer à supprimer l'absence ou la présence si l'on rencontre qu'elle n'était pas encore pointée
                con.Close();
                con.Open();
                string Heure = DateTime.Now.ToShortTimeString();
                MySqlCommand cmd1a = con.CreateCommand();
                cmd1a.CommandType = CommandType.Text;
                cmd1a.CommandText = "DELETE FROM t_presence WHERE matricule='" + matricule.ToString() + "' AND datep='" + txtDate.Text + "'";
                cmd1a.ExecuteNonQuery();
                Response.Redirect("AdminAgentPresence.aspx");
            }
            con.Close();
        }

        protected void btnSituationPresence_Click(object sender, EventArgs e)
        {
            new Random();
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
                dc.Add(new Paragraph("              Situation des présences et absences en  : " + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                PdfPTable table = new PdfPTable(8);
                PdfPCell cell0 = new PdfPCell(new Paragraph("Date", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell0.HorizontalAlignment = Element.ALIGN_CENTER;
                cell0.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell = new PdfPCell(new Paragraph("Matricule", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell1 = new PdfPCell(new Paragraph("Nom,PostNom & Prénom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell2 = new PdfPCell(new Paragraph("Motif", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3 = new PdfPCell(new Paragraph("Heures Prévues", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3a = new PdfPCell(new Paragraph("Arrivée à", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell3a.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3a.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3b = new PdfPCell(new Paragraph("Cloturé à", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell3b.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3b.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3c = new PdfPCell(new Paragraph("Heure Enseignées", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell3c.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3c.BackgroundColor = BaseColor.BLACK;

                table.AddCell(cell0);
                table.AddCell(cell);
                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
                table.AddCell(cell3a);
                table.AddCell(cell3b);
                table.AddCell(cell3c);

                MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gespersonnel; password=");
                con.Open();
                string cmd = "SELECT * FROM t_agent,t_presence WHERE t_presence.matricule=t_agent.matricule AND t_presence.annee='" + txtIdAnnee.Text + "' ORDER BY t_presence.id_presence ASC";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    //===========================================================================
                    PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["datep"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell4.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["matricule"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell5.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["nom"] + "-" + dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell6.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7 = new PdfPCell(new Paragraph(@Convert.ToString(dr["motif"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7a = new PdfPCell(new Paragraph(@Convert.ToString(dr["nbHeure"] + "H"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7a.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7a.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7b = new PdfPCell(new Paragraph(@Convert.ToString(dr["heure_arriver"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7b.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7b.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7c = new PdfPCell(new Paragraph(@Convert.ToString(dr["heureDepart"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7c.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7c.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7d = new PdfPCell(new Paragraph(@Convert.ToString(dr["nbHenseigne"] + "H"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7d.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7d.BackgroundColor = BaseColor.WHITE;
                    //============================================================================

                    table.AddCell(cell4);
                    table.AddCell(cell5);
                    table.AddCell(cell6);
                    table.AddCell(cell7);
                    table.AddCell(cell7a);
                    table.AddCell(cell7b);
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
                Response.AddHeader("content-disposition", "attachment;filename=RegistrePresence.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
            dc.Close();
        }
    }
}