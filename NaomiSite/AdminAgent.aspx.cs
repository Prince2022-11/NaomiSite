using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace NaomiSite
{
    public partial class AdminAgent : System.Web.UI.Page
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

                    ctrlAjoutAgent.Visible = false;
                    ctrlChargeHor.Visible = false;
                    ctrlPresence.Visible = false;
                    ctrlFinCours.Visible = false;
                    ctrlPayeAgent.Visible = false;
                    ctrAjoutAgent2.Visible = false;
                    ctrlCompte.Visible = false;
                    ctrlRechPayement.Visible = false;

                    if (dr["service"].ToString() == "Admin" && dr["idEcole"].ToString() == "Toutes les écoles")
                    {
                        ctrlAnnee.Visible = true;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = true;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                        ctrlAjoutAgent.Visible = true;
                        ctrlChargeHor.Visible = true;
                        ctrlPresence.Visible = true;
                        ctrlFinCours.Visible = true;
                        ctrlPayeAgent.Visible = true;
                        ctrAjoutAgent2.Visible = true;
                        ctrlCompte.Visible = true;
                        ctrlRechPayement.Visible = true;
                    }
                    if (dr["service"].ToString() == "Préfet Secondaire" && dr["idEcole"].ToString() == "3")
                    {
                        ctrlAnnee.Visible = false;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = false;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                        ctrlAjoutAgent.Visible = true;
                        ctrlChargeHor.Visible = true;
                        ctrlPresence.Visible = true;
                        ctrlFinCours.Visible = true;
                        ctrlPayeAgent.Visible = true;
                        ctrAjoutAgent2.Visible = true;
                        ctrlCompte.Visible = true;
                        ctrlRechPayement.Visible = true;
                    }
                    if (dr["service"].ToString() == "Directeur" && (dr["idEcole"].ToString() == "2" || dr["idEcole"].ToString() == "1"))
                    {
                        ctrlAnnee.Visible = false;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = false;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                        ctrlAjoutAgent.Visible = true;
                        ctrlChargeHor.Visible = false;
                        ctrlPresence.Visible = false;
                        ctrlFinCours.Visible = false;
                        ctrlPayeAgent.Visible = true;
                        ctrAjoutAgent2.Visible = true;
                        ctrlCompte.Visible = true;
                        ctrlRechPayement.Visible = true;
                    }
                    if (dr["service"].ToString() == "Comptable" && (dr["idEcole"].ToString() == "3" || dr["idEcole"].ToString() == "2" || dr["idEcole"].ToString() == "1"))
                    {
                        ctrlAnnee.Visible = false;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = false;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();

                        if(dr["idEcole"].ToString() == "3")
                        {
                            ctrlAjoutAgent.Visible = false;
                            ctrlChargeHor.Visible = false;
                            ctrlPresence.Visible = true;
                            ctrlFinCours.Visible = true;
                            ctrlPayeAgent.Visible = true;
                            ctrAjoutAgent2.Visible = false;
                            ctrlCompte.Visible = false;
                            ctrlRechPayement.Visible = false;
                        }
                        if (dr["idEcole"].ToString() == "1"|| dr["idEcole"].ToString() == "2")
                        {
                            ctrlAjoutAgent.Visible = true;
                            ctrlChargeHor.Visible = false;
                            ctrlPresence.Visible = false;
                            ctrlFinCours.Visible = false;
                            ctrlPayeAgent.Visible = true;
                            ctrAjoutAgent2.Visible = false;
                            ctrlCompte.Visible = false;
                            ctrlRechPayement.Visible = false;
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

            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
    }
}