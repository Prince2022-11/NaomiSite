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
    public partial class EspaceAdmin : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gestion_naomi");
        protected void Page_Load(object sender, EventArgs e)
        {
            //Vérification de la connexion de la varibale session
            if (Session["autorisation"] != null && (bool)Session["autorisation"] == true)
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

                    ctrlMat1.Visible = false;
                    ctrlMat2.Visible = false;
                    ctrlPrim1.Visible = false;
                    ctrlPrim2.Visible = false;
                    ctrlSecond1.Visible = false;
                    ctrlSecond2.Visible = false;
                    ctrlTotAgent.Visible = false;
                    ctrlTotEnseignant.Visible = false;

                    if (dr["service"].ToString() == "Admin" && dr["idEcole"].ToString() == "Toutes les écoles")
                    {
                        ctrlAnnee.Visible = true;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = true;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();

                        ctrlMat1.Visible = true;
                        ctrlMat2.Visible = true;
                        ctrlPrim1.Visible = true;
                        ctrlPrim2.Visible = true;
                        ctrlSecond1.Visible = true;
                        ctrlSecond2.Visible = true;
                        ctrlTotAgent.Visible = true;
                        ctrlTotEnseignant.Visible = true;
                    }
                    if (dr["service"].ToString() == "Préfet Secondaire" && dr["idEcole"].ToString() == "3")
                    {
                        ctrlAnnee.Visible = false;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = false;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();

                        ctrlMat1.Visible = false;
                        ctrlMat2.Visible = false;
                        ctrlPrim1.Visible = false;
                        ctrlPrim2.Visible = false;
                        ctrlSecond1.Visible = true;
                        ctrlSecond2.Visible = true;
                        ctrlTotAgent.Visible = true;
                        ctrlTotEnseignant.Visible = true;
                    }
                    if (dr["service"].ToString() == "Directeur" && (dr["idEcole"].ToString() == "2"|| dr["idEcole"].ToString() == "1"))
                    {
                        ctrlAnnee.Visible = false;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = false;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();

                        if(dr["idEcole"].ToString() == "2")
                        {
                            ctrlMat1.Visible = false;
                            ctrlMat2.Visible = false;
                            ctrlPrim1.Visible = true;
                            ctrlPrim2.Visible = true;
                            ctrlSecond1.Visible = false;
                            ctrlSecond2.Visible = false;
                            ctrlTotAgent.Visible = true;
                            ctrlTotEnseignant.Visible = false;
                        }
                        if (dr["idEcole"].ToString() == "1")
                        {
                            ctrlMat1.Visible = true;
                            ctrlMat2.Visible = true;
                            ctrlPrim1.Visible = false;
                            ctrlPrim2.Visible = false;
                            ctrlSecond1.Visible = false;
                            ctrlSecond2.Visible = false;
                            ctrlTotAgent.Visible = true;
                            ctrlTotEnseignant.Visible = false;
                        }

                    }
                    if (dr["service"].ToString() == "Comptable" && (dr["idEcole"].ToString() == "3" || dr["idEcole"].ToString() == "2" || dr["idEcole"].ToString() == "1"))
                    {
                        ctrlAnnee.Visible = false;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = false;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();

                        if (dr["idEcole"].ToString() == "3")
                        {
                            ctrlMat1.Visible = false;
                            ctrlMat2.Visible = false;
                            ctrlPrim1.Visible = false;
                            ctrlPrim2.Visible = false;
                            ctrlSecond1.Visible = true;
                            ctrlSecond2.Visible = true;
                            ctrlTotAgent.Visible = true;
                            ctrlTotEnseignant.Visible = true;
                        }
                        if (dr["idEcole"].ToString() == "2")
                        {
                            ctrlMat1.Visible = false;
                            ctrlMat2.Visible = false;
                            ctrlPrim1.Visible = true;
                            ctrlPrim2.Visible = true;
                            ctrlSecond1.Visible = false;
                            ctrlSecond2.Visible = false;
                            ctrlTotAgent.Visible = true;
                            ctrlTotEnseignant.Visible = false;
                        }
                        if (dr["idEcole"].ToString() == "1")
                        {
                            ctrlMat1.Visible = true;
                            ctrlMat2.Visible = true;
                            ctrlPrim1.Visible = false;
                            ctrlPrim2.Visible = false;
                            ctrlSecond1.Visible = false;
                            ctrlSecond2.Visible = false;
                            ctrlTotAgent.Visible = true;
                            ctrlTotEnseignant.Visible = false;
                        }
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
                    TotalInscrit();
                    TotalMaternelleAnnee();
                    TotalInscritPrimaire();
                    TotalPrimaireAnnee();
                    TotalInscritSecondaire();
                    TotalSecondaireAnnee();
                    TotalAgent();
                    EnseignantPresent();
                }
                catch
                {

                }
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void TotalInscrit()
        {
            //Les inscrits sur base de l'année actif
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from t_eleve,anneescol WHERE t_eleve.anneescol=anneescol.anneeScolaire and t_eleve.anneescol='" + txtIdAnnee.Text + "' AND t_eleve.idEcole='1' ", con);
            MySqlDataReader drA = cmdA.ExecuteReader();
            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtTotalInscrit.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalMaternelleAnnee()
        {
            //Les inscrits sur base de l'année actif
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from change_classe WHERE anneeScolaire='" + txtIdAnnee.Text + "' AND etat='Actif' AND idEcole='1'", con);
            MySqlDataReader drA = cmdA.ExecuteReader();
            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtTotalUSD.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalInscritPrimaire()
        {
            //Les inscrits sur base de l'année actif
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from t_eleve,anneescol WHERE t_eleve.anneescol=anneescol.anneeScolaire and t_eleve.anneescol='" + txtIdAnnee.Text + "' AND t_eleve.idEcole='2' ", con);
            MySqlDataReader drA = cmdA.ExecuteReader();
            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtInscritPrimaire.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalPrimaireAnnee()
        {
            //Les inscrits sur base de l'année actif
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from change_classe WHERE anneeScolaire='" + txtIdAnnee.Text + "' AND etat='Actif' AND idEcole='2'", con);
            MySqlDataReader drA = cmdA.ExecuteReader();
            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtTotAnneePrimaire.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalInscritSecondaire()
        {
            //Les inscrits sur base de l'année actif
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from t_eleve,anneescol WHERE t_eleve.anneescol=anneescol.anneeScolaire and t_eleve.anneescol='" + txtIdAnnee.Text + "' AND t_eleve.idEcole='3' ", con);
            MySqlDataReader drA = cmdA.ExecuteReader();
            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtInscritSecond.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalSecondaireAnnee()
        {
            //Les inscrits sur base de l'année actif
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from change_classe WHERE anneeScolaire='" + txtIdAnnee.Text + "' AND etat='Actif' AND idEcole='3'", con);
            MySqlDataReader drA = cmdA.ExecuteReader();
            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtTotAnneeSecond.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalAgent()
        {
            con.Open();
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles")
            {
                MySqlCommand cmdA = new MySqlCommand("select count(*) from t_agent", con);
                MySqlDataReader drA = cmdA.ExecuteReader();

                if (drA.Read())
                {
                    int nombre = drA.GetInt32(0);
                    txtTotalAgent.Text = nombre.ToString();
                }
            }
            else
            {
                MySqlCommand cmdA = new MySqlCommand("select count(*) from t_agent WHERE idEcole='" + txtIdEcoleAffectationUser.Text + "'", con);
                MySqlDataReader drA = cmdA.ExecuteReader();

                if (drA.Read())
                {
                    int nombre = drA.GetInt32(0);
                    txtTotalAgent.Text = nombre.ToString();
                }
            }
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
        public void EnseignantPresent()
        {
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("SELECT count(*)FROM t_agent,t_presence WHERE t_presence.matricule=t_agent.matricule AND t_presence.annee='" + txtIdAnnee.Text + "' AND t_presence.datep='" + txtDate.Text + "' AND motif='Présent'", con);
            MySqlDataReader drA = cmdA.ExecuteReader();
            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtEnseignantPresent.Text = nombre.ToString();
            }
            con.Close();

        }
    }
}