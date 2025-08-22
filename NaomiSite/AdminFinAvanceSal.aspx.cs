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
    public partial class AdminFinAvanceSal : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            //id = Convert.ToInt32(Request.QueryString["id"].ToString());
            //Vérification de la connexion de la varibale session
            if (Session["autorisation"] != null && (bool)Session["autorisation"] == true)
            {

                if (!IsPostBack) //Pour que la page ait le pouvoir de modifier le rendue
                {
                    txtLogin.Text = Session["login"].ToString();

                    // Vérifier l'admin connecté et l'agent à octroyer l'avance
                    con.Open();
                    id = Convert.ToString(Request.QueryString["id"].ToString());
                    txtMatricule.Text = id.ToString();

                    MySqlCommand cmd = new MySqlCommand("", con);
                    MySqlCommand cmde = con.CreateCommand();
                    cmde.CommandType = CommandType.Text;
                    cmd.CommandText = ("select * from utilisateur WHERE login='" + txtLogin.Text + "'");
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        txtRole.Text = dr["service"].ToString();
                        txtIdUser.Text = dr["id"].ToString();

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
                    success.Visible = false;
                    error.Visible = false;
                    con.Close();
                    try
                    {
                        TrouverAgent();
                        TrouverEcole();
                        TrouverAvance();
                        SituationCaisse();
                        TrouverDerniereOperation();
                    }
                    catch { }
                }
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void TrouverEcole()
        {
            con.Close();
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from ecole WHERE idEcole='" + txtIdEcole.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtEcole.Text = " / " + dr["nomEcole"].ToString();
            }
            con.Close();
        }
        public void TrouverAgent()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_agent WHERE matricule='" + txtMatricule.Text + "' ");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdEcole.Text = dr["idEcole"].ToString();
                txtNomEleve.Text = dr["nom"].ToString() + " " + dr["prenom"].ToString();
            }
            con.Close();
        }

        public void TrouverAvance()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from compte_agent WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "' ");
            MySqlDataReader dr = cmdB.ExecuteReader();
            txtCompteExitant.Text = "Non";
            while (dr.Read())
            {
                txtAvance.Text = dr["avance"].ToString();
                txtCompteExitant.Text = "Oui";
            }
            con.Close();
        }
        public void CreerCompteAnnuelSiManquantAgent()
        {
            con.Open();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into compte_agent values(default,'" + txtMatricule.Text + "','0','0','0','0','0','0','0','0','0','0','0','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
        }

        public void TrouverDerniereOperation()
        {
            try
            {
                con.Close();
                con.Open();
                MySqlCommand cmdB = con.CreateCommand();
                cmdB.CommandType = CommandType.Text;
                cmdB.CommandText = ("SELECT MAX(IdAvance) as DOperation from t_salaire_avance");
                MySqlDataReader dr = cmdB.ExecuteReader();
                txtDernierOperation.Text = "";
                while (dr.Read())
                {
                    txtDernierOperation.Text = dr["DOperation"].ToString();
                    int NouvelleOperation = int.Parse(dr["DOperation"].ToString()) + 1;
                    txtIdRecu.Text = NouvelleOperation.ToString() + "-" + txtMatricule.Text;
                }
                con.Close();
            }
            catch
            {

            }
        }
       public void SituationCaisse()
        {
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = "select *from t_caisse WHERE libelle='USD' AND idEcole=+'" + txtIdEcole.Text + "'";
            MySqlDataReader dr = cmd.ExecuteReader();

            txtDispo.Text = "0";
            txtSortie.Text = "0";
            while (dr.Read())
            {
                txtDispo.Text = dr["Solde"].ToString();
                txtSortie.Text = dr["Sortie"].ToString();
            }

            con.Close();
        }
        public void ActualiserLaCaisseEnEntree()
        {
            //Actualiser la caisse
            //L'ajout de ces 2 lignes et de ces bibliothèques font l'utilisation du point au décimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            double b, sort, Disp;
            MySqlConnection conx1 = new MySqlConnection("server=localhost;uid=root;database=gespersonnel;password=");
            conx1.Open();
            //Actualisation de la caisse s'il n'y a pas eu de conversion de l'argent de l'élève
            b = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            sort = Convert.ToDouble(txtSortie.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            Disp = Convert.ToDouble(txtDispo.Text.Replace(',', '.'), CultureInfo.InvariantCulture) - b;

            string command = ("UPDATE t_caisse SET Sortie ='" + sort.ToString(CultureInfo.InvariantCulture) + "', Solde ='" + Disp.ToString(CultureInfo.InvariantCulture) + "' WHERE libelle='USD' AND idEcole=+'" + txtIdEcole.Text + "'");
            MySqlCommand cmde = new MySqlCommand(command, conx1);
            cmde.ExecuteNonQuery();

        }
        public void ActualiserCompteAgentAvecAvance()
        {
            //Actualiser la caisse
            //L'ajout de ces 2 lignes et de ces bibliothèques font l'utilisation du point au décimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            double avance,b;
            con.Close();
            con.Open();
            //Actualisation de la caisse s'il n'y a pas eu de conversion de l'argent de l'élève
            b = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            avance = Convert.ToDouble(txtAvance.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;

            string command = ("UPDATE compte_agent SET avance ='" + avance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
            MySqlCommand cmde = new MySqlCommand(command, con);
            cmde.ExecuteNonQuery();
        }

        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            try
            {
                double MontantDepense, Disp;
                MontantDepense = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                Disp = Convert.ToDouble(txtDispo.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                txtMessage.Visible = false;
                if (MontantDepense >= Disp)
                {
                    txtMessage.Visible = true;
                }
                else
                {
                    if (txtCompteExitant.Text=="Non")
                    {
                        CreerCompteAnnuelSiManquantAgent();
                    }
                    con.Open();
                    string dateInscription = DateTime.Today.Date.ToShortDateString();
                    MySqlCommand cmd1a = con.CreateCommand();
                    cmd1a.CommandType = CommandType.Text;
                    cmd1a.CommandText = "insert into t_salaire_avance values(default,'" + txtMatricule.Text + "','" + txtmontant.Text + "','" + txtMois.Text + "','" + txtIdAnnee.Text + "','" + dateInscription.ToString() + "','" + txtIdUser.Text + "','" + txtIdEcole.Text + "')";
                    cmd1a.ExecuteNonQuery();
                    con.Close();
                    ActualiserCompteAgentAvecAvance();
                    ActualiserLaCaisseEnEntree();
                    Response.Redirect("AdminFinAvanceOct.aspx");
                }
            }
            catch
            {
                txtMessage.Visible = true;
                txtMessage.Text = "Quelque chose a mal tourné, vérifiez si vous avez saisi nombre valide, si c'est un décimal, n'oubliez pas d'utiliser un point-virgule (,)";
            }
        }

        protected void txtFrais_SelectedIndexChanged(object sender, EventArgs e)
        {
            SituationCaisse();
            TrouverDerniereOperation();
            success.Visible = false;
            error.Visible = false;
        }
    }
}