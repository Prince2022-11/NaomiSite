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
    public partial class AdminAgentChargeHoraireModifer : System.Web.UI.Page
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
                        TrouverPrestationAgent();
                        txtSuccess.Visible = false;
                        txtMessage.Visible = false;
                        btnLundi.Checked = false;
                        btnMardi.Checked = false;
                        btnMercredi.Checked = false;
                        btnJeudi.Checked = false;
                        btnVendredi.Checked = false;
                        btnSamedi.Checked = false;
                        txtLundi.Enabled = false;
                        txtMardi.Enabled = false;
                        txtMercredi.Enabled = false;
                        txtJeudi.Enabled = false;
                        txtVendredi.Enabled = false;
                        txtSamedi.Enabled = false;

                        CalculerHeures();
                        RendreChampsVisible();
                        RestreindreHeures();
                    }
                    catch { }
                }
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void TrouverPrestationAgent()
        {
            id = Convert.ToInt32(Request.QueryString["id"].ToString());
            con.Close();
            con.Open();
            txtIdAttribution.Text = id.ToString();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select *from attribution_horaire WHERE idAttribution=" + id + "");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtmat.Text = dr["Matricule"].ToString();
                txtHeure.Text = dr["totalHeure"].ToString();
                txtCours.Text = dr["coursAttribue"].ToString();
                lblLundi.Text = dr["Lundi"].ToString();
                lblMardi.Text = dr["Mardi"].ToString();
                lblMercredi.Text = dr["Mercredi"].ToString();
                lblJeudi.Text = dr["Jeudi"].ToString();
                lblVendredi.Text = dr["Vendredi"].ToString();
                lblSamedi.Text = dr["Samedi"].ToString();
                txtLundi.Text = dr["nbHlundi"].ToString();
                txtMardi.Text = dr["nbHmardi"].ToString();
                txtMercredi.Text = dr["nbHmercredi"].ToString();
                txtJeudi.Text = dr["nbHjeudi"].ToString();
                txtVendredi.Text = dr["nbHvendredi"].ToString();
                txtSamedi.Text = dr["nbHsamedi"].ToString();
            }
            TrouverAgent();
            con.Close();
        }
        public void TrouverAgent()
        {
            con.Close();
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_agent WHERE matricule='" + txtmat.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtNomAgent.Text = dr["nom"].ToString() + "-" + dr["prenom"].ToString();
            }
            con.Close();
        }
        public void RendreChampsVisible()
        {
            if (lblLundi.Text == "Oui")
            {
                txtLundi.Enabled = true;
                btnLundi.Checked = true;
            }
            else
            {
                txtLundi.Enabled = false;
                btnLundi.Checked = false;
            }
            if (lblMardi.Text == "Oui")
            {
                txtMardi.Enabled = true;
                btnMardi.Checked = true;
            }
            else
            {
                txtMardi.Enabled = false;
                btnMardi.Checked = false;
            }
            if (lblMercredi.Text == "Oui")
            {
                txtMercredi.Enabled = true;
                btnMercredi.Checked = true;
            }
            else
            {
                txtMercredi.Enabled = false;
                btnMercredi.Checked = false;
            }
            if (lblJeudi.Text == "Oui")
            {
                txtJeudi.Enabled = true;
                btnJeudi.Checked = true;
            }
            else
            {
                txtJeudi.Enabled = false;
                btnJeudi.Checked = false;
            }
            if (lblVendredi.Text == "Oui")
            {
                txtVendredi.Enabled = true;
                btnVendredi.Checked = true;
            }
            else
            {
                txtVendredi.Enabled = false;
                btnVendredi.Checked = false;
            }
            if (lblSamedi.Text == "Oui")
            {
                txtSamedi.Enabled = true;
                btnSamedi.Checked = true;
            }
            else
            {
                txtSamedi.Enabled = false;
                btnSamedi.Checked = false;
            }
            CalculerHeures();
        }
        public void RestreindreHeures()
        {
            if (btnLundi.Checked == true)
            {
                txtLundi.Enabled = true;
                lblLundi.Text = "Oui";
            }
            else
            {
                txtLundi.Enabled = false;
                lblLundi.Text = "Non";
                txtLundi.Text = "0";
            }
            if (btnMardi.Checked == true)
            {
                txtMardi.Enabled = true;
                lblMardi.Text = "Oui";
            }
            else
            {
                txtMardi.Enabled = false;
                lblMardi.Text = "Non";
                txtMardi.Text = "0";
            }
            if (btnMercredi.Checked == true)
            {
                txtMercredi.Enabled = true;
                lblMercredi.Text = "Oui";
            }
            else
            {
                txtMercredi.Enabled = false;
                lblMercredi.Text = "Non";
                txtMercredi.Text = "0";
            }
            if (btnJeudi.Checked == true)
            {
                txtJeudi.Enabled = true;
                lblJeudi.Text = "Oui";
            }
            else
            {
                txtJeudi.Enabled = false;
                lblJeudi.Text = "Non";
                txtJeudi.Text = "0";
            }
            if (btnVendredi.Checked == true)
            {
                txtVendredi.Enabled = true;
                lblVendredi.Text = "Oui";
            }
            else
            {
                txtVendredi.Enabled = false;
                lblVendredi.Text = "Non";
                txtVendredi.Text = "0";
            }
            if (btnSamedi.Checked == true)
            {
                txtSamedi.Enabled = true;
                lblSamedi.Text = "Oui";
            }
            else
            {
                txtSamedi.Enabled = false;
                lblSamedi.Text = "Non";
                txtSamedi.Text = "0";
            }
            CalculerHeures();
        }
        public void CalculerHeures()
        {
            //L'ajout de ces 2 lignes et de ces bibliothèques font l'utilisation du point au décimal
            try
            {
                txtHeure.Text = "0";
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

                double l, ma, me, j, v, s, totHeure;
                l = Convert.ToDouble(txtLundi.Text, CultureInfo.InvariantCulture);
                ma = Convert.ToDouble(txtMardi.Text, CultureInfo.InvariantCulture);
                me = Convert.ToDouble(txtMercredi.Text, CultureInfo.InvariantCulture);
                j = Convert.ToDouble(txtJeudi.Text, CultureInfo.InvariantCulture);
                v = Convert.ToDouble(txtVendredi.Text, CultureInfo.InvariantCulture);
                s = Convert.ToDouble(txtSamedi.Text, CultureInfo.InvariantCulture);

                totHeure = l + ma + me + j + v + s;
                txtHeure.Text = totHeure.ToString();
            }
            catch { }

        }
        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            if (txtmat.Text != "0" && txtHeure.Text != "0" && txtHeure.Text != "" && txtCours.Text != "0" && txtCours.Text != "" && (txtLundi.Text != "0" || txtMardi.Text != "0" || txtMercredi.Text != "0" || txtJeudi.Text != "0" || txtVendredi.Text != "0" || txtSamedi.Text != "0") && (txtLundi.Text != "" && txtMardi.Text != "" && txtMercredi.Text != "" && txtJeudi.Text != "" && txtVendredi.Text != "" && txtSamedi.Text != ""))
            {
                con.Open();
                CalculerHeures();
                MySqlCommand cmd1a = con.CreateCommand();
                cmd1a.CommandType = CommandType.Text;
                cmd1a.CommandText = "UPDATE attribution_horaire SET coursAttribue='"+ txtCours.Text + "',totalHeure='" + txtHeure.Text + "',Lundi='" + lblLundi.Text + "',Mardi='" + lblMardi.Text + "',Mercredi='" + lblMercredi.Text + "',Jeudi='" + lblJeudi.Text + "',Vendredi='" + lblVendredi.Text + "',Samedi='" + lblSamedi.Text + "',nbHlundi='" + txtLundi.Text + "',nbHmardi='" + txtMardi.Text + "',nbHmercredi='" + txtMercredi.Text + "',nbHjeudi='" + txtJeudi.Text + "',nbHvendredi='" + txtVendredi.Text + "',nbHsamedi='" + txtSamedi.Text + "' WHERE Matricule='" + txtmat.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "'";
                cmd1a.ExecuteNonQuery();
                con.Close();
                txtSuccess.Visible = true;
                Response.Redirect("AdminAgentChargeHoraire.aspx");
            }
            else
            {
                txtMessage.Visible = true;
            }
        }
        protected void btnLundi_CheckedChanged(object sender, EventArgs e)
        {
            RestreindreHeures();
            RendreChampsVisible();
            CalculerHeures();
        }

        protected void txtLundi_TextChanged(object sender, EventArgs e)
        {
            CalculerHeures();
        }
    }
}