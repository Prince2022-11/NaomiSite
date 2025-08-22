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
    public partial class AdminNiveauPaye : System.Web.UI.Page
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
                            txtAnneeEncours.Text = dr1["designation"].ToString();
                        }
                        TrouverIdEcole();
                        con.Close();
                    }
                }
                else
                {
                    Response.Redirect("Acceuil.aspx");
                }
            }
            catch { }
        }
        public void Effacer()
        {
            txtEnseignant.Text = "0";
            txtFF.Text = "0";
            TxtPromo.Text = "0";
            txtTotMois.Text = "0";
            txtTotPaye.Text = "0";
            txtPourc.Text = "0";

        }
        public void TrouverIdEcole()
        {
            con.Close();
            con.Open();
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles")
            {
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
                txtIdEcole.Text = txtIdEcoleAffectationUser.Text;
                TrouverEcole();
            }
            con.Close();
        }
        public void TrouverEcole()
        {
            con.Close();
            con.Open();
            txtEcole.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT nomEcole from ecole WHERE idEcole='" + txtIdEcoleAffectationUser.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtEcole.Items.Add(dr["nomEcole"].ToString());
            }
            con.Close();
        }
        public void CompterEleveAnnee()
        {
            try
            {
                MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gespersonnel; password=");
                con.Open();
                string cmd = "SELECT count(*) from change_classe WHERE anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "' AND etat='Actif'";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                Effacer();
                txtTotEleve.Text = "0";
                if (dr.Read())
                {
                    int i = dr.GetInt32(0);
                    txtTotEleve.Text = i.ToString();
                }
                con.Close();
            }
            catch
            {

            }

        }
        public void ElevePaye()
        {
            con.Open();
            Effacer();
            if (txtmois.SelectedValue == "Septembre")
            {
                MySqlCommand cmdB = con.CreateCommand();
                cmdB.CommandType = CommandType.Text;
                cmdB.CommandText = ("SELECT count(*),sum(septembre) as Somme from compte_eleve WHERE septembre>0 AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlDataReader dr = cmdB.ExecuteReader();
                while (dr.Read())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    txtTotMois.Text = dr["Somme"].ToString();
                    int a = dr.GetInt32(0);
                    double b = (a / double.Parse(txtTotEleve.Text.Replace(',', '.'))) * 100;
                    txtPourc.Text = b.ToString("#.00") + "%";
                    txtTotPaye.Text = a.ToString();
                    double promo, ff, enseignant, totMois;
                    totMois = Convert.ToDouble(txtTotMois.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    promo = totMois * 0.20;
                    ff = totMois * 0.10;
                    enseignant = totMois * 0.70;

                    TxtPromo.Text = promo.ToString("#.00");
                    txtFF.Text = ff.ToString("#.00");
                    txtEnseignant.Text = enseignant.ToString("#.00");
                }
                con.Close();
            }
            if (txtmois.SelectedValue == "Octobre")
            {
                string cmd = "SELECT count(*),sum(octobre) as Somme from compte_eleve WHERE octobre>0 AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    txtTotMois.Text = dr["Somme"].ToString();
                    int a = dr.GetInt32(0);
                    double b = (a / double.Parse(txtTotEleve.Text.Replace(',', '.'))) * 100;
                    txtPourc.Text = b.ToString("#.00") + "%";
                    txtTotPaye.Text = a.ToString();
                    double promo, ff, enseignant, totMois;
                    totMois = Convert.ToDouble(txtTotMois.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    promo = totMois * 0.20;
                    ff = totMois * 0.10;
                    enseignant = totMois * 0.70;

                    TxtPromo.Text = promo.ToString("#.00");
                    txtFF.Text = ff.ToString("#.00");
                    txtEnseignant.Text = enseignant.ToString("#.00");
                }
            }
            if (txtmois.SelectedValue == "Novembre")
            {
                string cmd = "SELECT count(*),sum(novembre) as Somme from compte_eleve WHERE novembre>0 AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    txtTotMois.Text = dr["Somme"].ToString();
                    int a = dr.GetInt32(0);
                    double b = (a / double.Parse(txtTotEleve.Text.Replace(',', '.'))) * 100;
                    txtPourc.Text = b.ToString("#.00") + "%";
                    txtTotPaye.Text = a.ToString();
                    double promo, ff, enseignant, totMois;
                    totMois = Convert.ToDouble(txtTotMois.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    promo = totMois * 0.20;
                    ff = totMois * 0.10;
                    enseignant = totMois * 0.70;

                    TxtPromo.Text = promo.ToString("#.00");
                    txtFF.Text = ff.ToString("#.00");
                    txtEnseignant.Text = enseignant.ToString("#.00");
                }
            }
            if (txtmois.SelectedValue == "Décembre")
            {
                string cmd = "SELECT count(*),sum(decembre) as Somme from compte_eleve WHERE decembre>0 AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    txtTotMois.Text = dr["Somme"].ToString();
                    int a = dr.GetInt32(0);
                    double b = (a / double.Parse(txtTotEleve.Text.Replace(',', '.'))) * 100;
                    txtPourc.Text = b.ToString("#.00") + "%";
                    txtTotPaye.Text = a.ToString();
                    double promo, ff, enseignant, totMois;
                    totMois = Convert.ToDouble(txtTotMois.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    promo = totMois * 0.20;
                    ff = totMois * 0.10;
                    enseignant = totMois * 0.70;

                    TxtPromo.Text = promo.ToString("#.00");
                    txtFF.Text = ff.ToString("#.00");
                    txtEnseignant.Text = enseignant.ToString("#.00");
                }
            }
            if (txtmois.SelectedValue == "Janvier")
            {
                string cmd = "SELECT count(*),sum(janvier) as Somme from compte_eleve WHERE janvier>0 AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    txtTotMois.Text = dr["Somme"].ToString();
                    int a = dr.GetInt32(0);
                    double b = (a / double.Parse(txtTotEleve.Text.Replace(',', '.'))) * 100;
                    txtPourc.Text = b.ToString("#.00") + "%";
                    txtTotPaye.Text = a.ToString();
                    double promo, ff, enseignant, totMois;
                    totMois = Convert.ToDouble(txtTotMois.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    promo = totMois * 0.20;
                    ff = totMois * 0.10;
                    enseignant = totMois * 0.70;

                    TxtPromo.Text = promo.ToString("#.00");
                    txtFF.Text = ff.ToString("#.00");
                    txtEnseignant.Text = enseignant.ToString("#.00");
                }
            }
            if (txtmois.SelectedValue == "Février")
            {
                string cmd = "SELECT count(*),sum(fevrier) as Somme from compte_eleve WHERE fevrier>0 AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    txtTotMois.Text = dr["Somme"].ToString();
                    int a = dr.GetInt32(0);
                    double b = (a / double.Parse(txtTotEleve.Text.Replace(',', '.'))) * 100;
                    txtPourc.Text = b.ToString("#.00") + "%";
                    txtTotPaye.Text = a.ToString();
                    double promo, ff, enseignant, totMois;
                    totMois = Convert.ToDouble(txtTotMois.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    promo = totMois * 0.20;
                    ff = totMois * 0.10;
                    enseignant = totMois * 0.70;

                    TxtPromo.Text = promo.ToString("#.00");
                    txtFF.Text = ff.ToString("#.00");
                    txtEnseignant.Text = enseignant.ToString("#.00");
                }
            }
            if (txtmois.SelectedValue == "Mars")
            {
                string cmd = "SELECT count(*),sum(mars) as Somme from compte_eleve WHERE mars>0 AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    txtTotMois.Text = dr["Somme"].ToString();
                    int a = dr.GetInt32(0);
                    double b = (a / double.Parse(txtTotEleve.Text.Replace(',', '.'))) * 100;
                    txtPourc.Text = b.ToString("#.00") + "%";
                    txtTotPaye.Text = a.ToString();
                    double promo, ff, enseignant, totMois;
                    totMois = Convert.ToDouble(txtTotMois.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    promo = totMois * 0.20;
                    ff = totMois * 0.10;
                    enseignant = totMois * 0.70;

                    TxtPromo.Text = promo.ToString("#.00");
                    txtFF.Text = ff.ToString("#.00");
                    txtEnseignant.Text = enseignant.ToString("#.00");
                }
            }
            if (txtmois.SelectedValue == "Avril")
            {
                string cmd = "SELECT count(*),sum(avril) as Somme from compte_eleve WHERE avril>0 AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    txtTotMois.Text = dr["Somme"].ToString();
                    int a = dr.GetInt32(0);
                    double b = (a / double.Parse(txtTotEleve.Text.Replace(',', '.'))) * 100;
                    txtPourc.Text = b.ToString("#.00") + "%";
                    txtTotPaye.Text = a.ToString();
                    double promo, ff, enseignant, totMois;
                    totMois = Convert.ToDouble(txtTotMois.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    promo = totMois * 0.20;
                    ff = totMois * 0.10;
                    enseignant = totMois * 0.70;

                    TxtPromo.Text = promo.ToString("#.00");
                    txtFF.Text = ff.ToString("#.00");
                    txtEnseignant.Text = enseignant.ToString("#.00");
                }
            }
            if (txtmois.SelectedValue == "Mai")
            {
                string cmd = "SELECT count(*),sum(mai) as Somme from compte_eleve WHERE mai>0 AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    txtTotMois.Text = dr["Somme"].ToString();
                    int a = dr.GetInt32(0);
                    double b = (a / double.Parse(txtTotEleve.Text.Replace(',', '.'))) * 100;
                    txtPourc.Text = b.ToString("#.00") + "%";
                    txtTotPaye.Text = a.ToString();
                    double promo, ff, enseignant, totMois;
                    totMois = Convert.ToDouble(txtTotMois.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    promo = totMois * 0.20;
                    ff = totMois * 0.10;
                    enseignant = totMois * 0.70;

                    TxtPromo.Text = promo.ToString("#.00");
                    txtFF.Text = ff.ToString("#.00");
                    txtEnseignant.Text = enseignant.ToString("#.00");
                }
            }
            if (txtmois.SelectedValue == "Juin")
            {
                string cmd = "SELECT count(*),sum(juin) as Somme from compte_eleve WHERE juin>0 AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                    txtTotMois.Text = dr["Somme"].ToString();
                    int a = dr.GetInt32(0);
                    double b = (a / double.Parse(txtTotEleve.Text.Replace(',', '.'))) * 100;
                    txtPourc.Text = b.ToString("#.00") + "%";
                    txtTotPaye.Text = a.ToString();
                    double promo, ff, enseignant, totMois;
                    totMois = Convert.ToDouble(txtTotMois.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    promo = totMois * 0.20;
                    ff = totMois * 0.10;
                    enseignant = totMois * 0.70;

                    TxtPromo.Text = promo.ToString("#.00");
                    txtFF.Text = ff.ToString("#.00");
                    txtEnseignant.Text = enseignant.ToString("#.00");
                }
            }

        }
        protected void txtEcole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Effacer();
                TrouverIdEcole();
                CompterEleveAnnee();
            }
            catch
            {

            }
           
        }
        protected void txtmois_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Effacer();
                CompterEleveAnnee();
                ElevePaye();
            }
            catch
            {

            }
            
        }

        protected void txtmois_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
    }
}