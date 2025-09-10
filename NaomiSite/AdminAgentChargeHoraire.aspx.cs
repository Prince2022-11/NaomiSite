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
    public partial class AdminAgentChargeHoraire : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gestion_naomi");
        protected void Page_Load(object sender, EventArgs e)
        {
            //Vérification de la connexion de la varibale session
            if (Session["autorisation"] != null && (bool)Session["autorisation"] == true)
            {
                if (!IsPostBack)
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
                    AfficherAgent();
                    ViderChamps();
                    TrouverAgents();
                    TrouverMatAgent();
                }

            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void AfficherAgent()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM t_agent,attribution_horaire WHERE attribution_horaire.Matricule=t_agent.matricule AND attribution_horaire.idEcole='3' AND attribution_horaire.anneeScolaire='"+txtIdAnnee.Text+"' ORDER BY nom ASC", con);
            cmdB1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
            da.Fill(dt);
            Data1.DataSource = dt;
            Data1.DataBind();
            con.Close();
            con.Close();
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
        public void ViderChamps()
        {
            txtmat.Text = "0";
            txtCours.Text = "";
            txtHeure.Text = "0";
            txtLundi.Text = "0";
            txtMardi.Text = "0";
            txtMercredi.Text = "0";
            txtJeudi.Text = "0";
            txtVendredi.Text = "0";
            txtSamedi.Text = "0";
            lblLundi.Text = "Non";
            lblMardi.Text = "Non";
            lblMercredi.Text = "Non";
            lblJeudi.Text = "Non";
            lblVendredi.Text = "Non";
            lblSamedi.Text = "Non";
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
        }
        public void RendreChampsVisible()
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
        public void recherche(string recherche)
        {
            try
            {
                con.Close();
                con.Open();
                MySqlCommand cmdD = con.CreateCommand();
                cmdD.CommandType = CommandType.Text;
                cmdD.CommandText = ("SELECT * FROM t_agent,attribution_horaire WHERE attribution_horaire.Matricule=t_agent.matricule AND attribution_horaire.idEcole='3' AND CONCAT(t_agent.matricule,t_agent.nom,t_agent.prenom,t_agent.domaine) LIKE '%" + recherche + "%' ORDER BY nom ASC ");
                cmdD.ExecuteNonQuery();
                DataTable dtD = new DataTable();
                MySqlDataAdapter daD = new MySqlDataAdapter(cmdD);
                daD.Fill(dtD);
                Data1.DataSource = dtD;
                Data1.DataBind();
                con.Close();
            }
            catch
            {

            }
        }
        public void TrouverAgents()
        {
            txtAgent.Items.Clear();
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT nom from t_agent WHERE idEcole='3'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtAgent.Items.Add(dr["nom"].ToString());
            }
            con.Close();
        }
        public void TrouverMatAgent()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT * from t_agent WHERE nom='"+txtAgent.SelectedValue+"' AND idEcole='3'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtmat.Text=dr["matricule"].ToString();
            }
            con.Close();
            TrouverSiAgentAuneChargeHoraire();
        }
        public void TrouverSiAgentAuneChargeHoraire()
        {
            con.Open();
            txtMessage.Visible = false;
            btnAddStructure.Visible = true;
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT * from attribution_horaire WHERE Matricule='" + txtmat.Text + "' AND anneeScolaire='"+txtIdAnnee.Text+"'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtMessage.Visible = true;
                txtMessage.Text = "L'agent sélectionné a déjà une charge horaire encours de cette année";
                btnAddStructure.Visible = false;
            }
            con.Close();
        }

        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtmat.Text != "0" && txtHeure.Text != "0" && txtHeure.Text != "" && txtCours.Text != "0" && txtCours.Text != "" && (txtLundi.Text != "0" || txtMardi.Text != "0" || txtMercredi.Text != "0" || txtJeudi.Text != "0" || txtVendredi.Text != "0" || txtSamedi.Text != "0") && (txtLundi.Text != "" && txtMardi.Text != "" && txtMercredi.Text != "" && txtJeudi.Text != "" && txtVendredi.Text != "" && txtSamedi.Text != ""))
                {
                    con.Open();
                    CalculerHeures();
                    MySqlCommand cmd1a = con.CreateCommand();
                    cmd1a.CommandType = CommandType.Text;
                    cmd1a.CommandText = "insert into attribution_horaire values(default,'" + txtCours.Text + "','" + txtHeure.Text + "','" + txtmat.Text + "','" + lblLundi.Text + "','" + lblMardi.Text + "','" + lblMercredi.Text + "','" + lblJeudi.Text + "','" + lblVendredi.Text + "','" + lblSamedi.Text + "','" + txtLundi.Text + "','" + txtMardi.Text + "','" + txtMercredi.Text + "','" + txtJeudi.Text + "','" + txtVendredi.Text + "','" + txtSamedi.Text + "','" + txtIdAnnee.Text + "','3')";
                    cmd1a.ExecuteNonQuery();
                    con.Close();
                    ViderChamps();
                    txtSuccess.Visible = true;
                    Response.Redirect("AdminAgentChargeHoraire.aspx");
                }
                else
                {
                    txtMessage.Visible = true;
                }
            }
            catch
            {
                txtMessage.Visible = true;
                txtMessage.Text = "Quelque chose a mal tourné, n'utilisez pas des caractères spéciaux comme : la virgule, l'apostrophe, le guillemets, les points, ...";
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        protected void Data1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            recherche(txtRecherche.Text);
        }

        protected void txtAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViderChamps();
            TrouverMatAgent();
            TrouverSiAgentAuneChargeHoraire();
        }
        protected void btnRechApproFondie_Click(object sender, EventArgs e)
        {
            //ImprimerRecherche1(txtRecherche.Text);
        }

        protected void btnLundi_CheckedChanged(object sender, EventArgs e)
        {
            RendreChampsVisible();
            CalculerHeures();
        }

        protected void txtLundi_TextChanged(object sender, EventArgs e)
        {
            CalculerHeures();
        }
    }
}