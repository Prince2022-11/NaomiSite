﻿using System;
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
    public partial class AdminAgentFinCours : System.Web.UI.Page
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
                        AgentDejaPointe();
                        txtNomEnseignant.Visible = false;
                        txtHeurePrestee.Visible = false;
                        btnCloturerCours.Visible = false;
                        Label10.Visible = false;
                    }
                    catch { }
                }
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        
        public void AgentDejaPointe()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM t_agent,t_presence WHERE t_presence.matricule=t_agent.matricule AND t_presence.annee='" + txtIdAnnee.Text + "' AND t_presence.datep='" + txtDate.Text + "' AND heureDepart='En cours' ORDER BY nom ASC", con);
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
                txtJour.Text = "Samadi";
            }
        }
        protected void PointerPresence_Click(object sender, EventArgs e)
        {

        }
        protected void Data1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void txtMatricule_TextChanged(object sender, EventArgs e)
        {

        }
        protected void btnSituationPresence_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Document dc = new Document();
            String chemin = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ " + rnd.Next() * 1000 + "EleveEnOrdreTranche1.pdf";
            FileStream fs = File.Create(chemin);
            PdfWriter.GetInstance(dc, fs);
            dc.Open();
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
            string cmd = "SELECT * FROM t_agent,t_presence WHERE t_presence.matricule=t_agent.matricule AND t_presence.annee='" + txtIdAnnee.Text + "' ORDER BY datep,nom ASC";
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
            System.Diagnostics.Process.Start(chemin);
        }
        protected void btnCloture_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string matricule = btn.CommandArgument;//Recupérer le matricule

            //Vérification si la présence n'est pas encore pointé
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select * from t_agent,t_presence WHERE t_presence.matricule=t_agent.matricule AND t_presence.matricule='" + matricule.ToString() + "' AND t_presence.datep='" + txtDate.Text + "'");
            MySqlDataReader dr = cmd.ExecuteReader();

            txtMatricule.Text = "";
            txtMessage.Text = "Assurez-vous que l'heure et la date de votre ordinateur sont à jours avant de pointer";
            while (dr.Read())
            {
                txtNomEnseignant.Visible = true;
                txtHeurePrestee.Visible = true;
                btnCloturerCours.Visible = true;
                Label10.Visible = true;
                txtMatricule.Text = dr["matricule"].ToString();
                txtNomEnseignant.Text ="Cloturer pour ' "+dr["nom"].ToString()+" '";
            }
        }

        protected void btnCloturerCours_Click(object sender, EventArgs e)
        {
            if (txtMatricule.Text == "" || txtHeurePrestee.Text == "")
            {
                txtMessage.Text = "Sélectionnez d'abord un agent pour lequel vous voulez cloturer et précisez le nombre d'heures prestées...";
               
            }
            else
            {
                //Commencer à pointer la présence si l'on rencontre qu'elle n'était pas encore pointée
                con.Close();
                con.Open();
                string Heure = DateTime.Now.ToShortTimeString();
                MySqlCommand cmd1a = con.CreateCommand();
                cmd1a.CommandType = CommandType.Text;
                cmd1a.CommandText = "UPDATE t_presence SET heureDepart='" + Heure.ToString() + "',nbHenseigne='" + txtHeurePrestee.Text + "' WHERE matricule='" + txtMatricule.Text + "' AND datep='" + txtDate.Text + "' AND annee='" + txtIdAnnee.Text + "'";
                cmd1a.ExecuteNonQuery();
                Response.Redirect("AdminAgentFinCours.aspx");
            }
            con.Close();
        }

    }
}