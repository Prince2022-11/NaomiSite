using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using KimToo;
using System.Threading;

namespace NaomiSite
{
    public partial class AdminAgentChargeHoraireModifer : System.Web.UI.Page
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
                        TrouverPrestationAgent();
                        CalculerHeures();
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
            //if (txtmat.Text != "0" && txtHeure.Text != "0" && txtHeure.Text != "" && txtCours.Text != "0" && txtCours.Text != "" && (txtLundi.Text != "0" || txtMardi.Text != "0" || txtMercredi.Text != "0" || txtJeudi.Text != "0" || txtVendredi.Text != "0" || txtSamedi.Text != "0") && (txtLundi.Text != "" && txtMardi.Text != "" && txtMercredi.Text != "" && txtJeudi.Text != "" && txtVendredi.Text != "" && txtSamedi.Text != ""))
            //{
            //    con.Open();
            //    CalculerHeures();
            //    MySqlCommand cmd1a = con.CreateCommand();
            //    cmd1a.CommandType = CommandType.Text;
            //    cmd1a.CommandText = "insert into attribution_horaire values(default,'" + txtCours.Text + "','" + txtHeure.Text + "','" + txtmat.Text + "','" + lblLundi.Text + "','" + lblMardi.Text + "','" + lblMercredi.Text + "','" + lblJeudi.Text + "','" + lblVendredi.Text + "','" + lblSamedi.Text + "','" + txtLundi.Text + "','" + txtMardi.Text + "','" + txtMercredi.Text + "','" + txtJeudi.Text + "','" + txtVendredi.Text + "','" + txtSamedi.Text + "','" + txtIdAnnee.Text + "','3')";
            //    cmd1a.ExecuteNonQuery();
            //    con.Close();
            //    ViderChamps();
            //    txtSuccess.Visible = true;
            //}
            //else
            //{
            //    txtMessage.Visible = true;
            //}
        }
        protected void btnLundi_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RendreChampsVisible();
                RestreindreHeures();
                CalculerHeures();
            }
           
        }

        protected void txtLundi_TextChanged(object sender, EventArgs e)
        {
            CalculerHeures();
        }

        protected void txtmat_TextChanged(object sender, EventArgs e)
        {
            TrouverAgent();
        }
    }
}