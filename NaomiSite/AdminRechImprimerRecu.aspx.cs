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
    public partial class AdminRechImprimerRecu : System.Web.UI.Page
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
                    success.Visible = false;
                    error.Visible = false;
                    con.Close();
                    try
                    {
                        TrouverRecu();
                        TrouverEleve();
                        TrouverPayementFait();
                        TrouverEcole();
                        TrouverSection();
                        TrouverClasser();
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
        public void TrouverRecu()
        {
            //id = Request.QueryString["id"].ToString();
            id = Convert.ToString(Request.QueryString["id"].ToString());
            con.Close();
            con.Open();
            txtIdRecu.Text = id.ToString();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select *from recu_payement WHERE idRecu='"+ id.ToString() +"' AND idAnnee='" + txtIdAnnee.Text + "'");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtDatePayement.Text = dr["datePayement"].ToString();
                txtMatricule.Text = dr["matricule"].ToString();
                txtIdEcole.Text = dr["idEcole"].ToString();
                txtFrais.Text = dr["fraisPaye"].ToString();
                txtT1.Text = dr["prevuT1"].ToString();
                txtUnite1.Text= "Le montant prévu en " + dr["unite"].ToString();
                txtUnite2.Text = "Evolution en payement en " + dr["unite"].ToString();
                txtUnite.Text = dr["unite"].ToString();
                txtT2.Text = dr["prevuT2"].ToString();
                txtT3.Text = dr["prevuT3"].ToString();
                txtT11.Text = dr["payeT1"].ToString();
                txtT22.Text = dr["payeT2"].ToString();
                txtT33.Text = dr["payeT3"].ToString();
                txtReste.Text = dr["reste"].ToString();
            }
            con.Close();
        }
        public void TrouverPayementFait()
        {
            id = Convert.ToString(Request.QueryString["id"].ToString());
            con.Close();
            con.Open();
            txtIdRecu.Text = id.ToString();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select *from t_payement_frais WHERE idRecu='" + id.ToString() + "' AND matricule='" + txtMatricule.Text + "'");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtmontant.Text = dr["montant_payer"].ToString()+" "+dr["unite"].ToString();
                txtOperateur.Text = dr["idOperateur"].ToString();
                txtDatePayement.Text = dr["date_payement"].ToString();
            }
            con.Close();
        }
        public void TrouverEleve()
        {
            con.Open();
            txtIdRecu.Text = id.ToString();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select *from change_classe WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' ");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtIdEcole.Text = dr["idEcole"].ToString();
                txtIdOption.Text = dr["optionEtude"].ToString();
                txtIdClasse.Text = dr["classe"].ToString();
                txtNomEleve.Text = dr["nomEleve"].ToString() + " " + dr["prenom"].ToString() + "--";
            }
            con.Close();
        }
        public void TrouverSection()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT nomSection from section WHERE idSection='" + txtIdOption.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtOption.Text = dr["nomSection"].ToString();
            }
            con.Close();
        }
        public void TrouverClasser()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_classe WHERE id='" + txtIdClasse.Text + "' AND idSection='" + txtIdOption.Text + "' AND idEcole='" + txtIdEcole.Text + "' ");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtClasse.Text = dr["classe"].ToString();
            }
            con.Close();
        }
        public void SituationCaisse()
        {
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = "select *from t_caisse WHERE libelle='" + txtUnite.Text + "' AND idEcole=+'" + txtIdEcole.Text + "'";
            MySqlDataReader dr = cmd.ExecuteReader();

            txtDispo.Text = "0";
            txtDispo.Text = "0";
            txtDispo.Text = "0";
            while (dr.Read())
            {
                txtDispo.Text = dr["Solde"].ToString();
                txtEntree.Text = dr["Entree"].ToString();
                txtSortie.Text = dr["Sortie"].ToString();
            }

            con.Close();
        }
        public void ImprimerFactureEleve()
        {
            // Récupération des valeurs des TextBox
            string nom = txtNomEleve.Text;
            string matricule = txtIdRecu.Text;
            string classe = txtClasse.Text + "-" + txtOption.Text;
            string dateRecu = txtDatePayement.Text;
            string frais = txtFrais.Text;
            string unite1 = txtUnite.Text;
            string unite2 = txtUnite.Text;
            string prevu = txtT1.Text + ", Tr2=" + txtT2.Text + ", Tr3=" + txtT3.Text;
            string apayer = txtT11.Text + ", Tr2=" + txtT22.Text + ", Tr3=" + txtT33.Text;
            string reste = txtReste.Text;
            string login = txtOperateur.Text;

            string montant = txtmontant.Text;
            // Enregistrement du script pour définir les données de la facture
            ScriptManager.RegisterStartupScript(this, GetType(), "setData", $"setFactureData('{matricule}', '{nom}','{classe}', '{dateRecu}', '{montant}', '{frais}', '{unite1}', '{prevu}', '{unite2}', '{apayer}', '{reste}', '{login}'); imprimerFacture();", true);
          

        }

        protected void btnImprimer_Click(object sender, EventArgs e)
        {
            ImprimerFactureEleve();
            //Affichage du message succès

            success.Visible = true;
            error.Visible = false;
            success.Style.Add("display", "block");
        }
    }
}