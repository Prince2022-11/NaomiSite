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
    public partial class AdminAgentPresenceNonCloturee : System.Web.UI.Page
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
            MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM t_agent,t_presence WHERE t_presence.matricule=t_agent.matricule AND t_presence.annee='" + txtIdAnnee.Text + "' AND heureDepart='En cours' ORDER BY datep DESC", con);
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
        protected void btnCloture_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idPresence = btn.CommandArgument;//Recupérer le l'id de la présence

            //Vérification si la présence n'est pas encore pointé
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select * from t_agent,t_presence WHERE t_presence.matricule=t_agent.matricule AND t_presence.id_presence='" + idPresence.ToString() + "' AND heureDepart='En cours'");
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
                txtIdPresence.Text = dr["id_presence"].ToString();
                txtNomEnseignant.Text = "Cloturer pour ' " + dr["nom"].ToString() + " '";
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
                cmd1a.CommandText = "UPDATE t_presence SET heureDepart='" + Heure.ToString() + "',nbHenseigne='" + txtHeurePrestee.Text + "' WHERE id_presence='" + txtIdPresence.Text + "' AND nbHenseigne='En cours' AND annee='" + txtIdAnnee.Text + "'";
                cmd1a.ExecuteNonQuery();
                Response.Redirect("AdminAgentPresenceNonCloturee.aspx");
            }
            con.Close();
        }

    }
}