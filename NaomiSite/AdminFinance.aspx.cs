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
    public partial class AdminFinance : System.Web.UI.Page
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

                    FinAvancSal.Visible = false;
                    FinCaisse.Visible = false;
                    FinDepense.Visible = false;
                    FinNivPay.Visible = false;
                    FinPayFr.Visible = false;
                    FinPaySal.Visible = false;
                    FinRechPay.Visible = false;
                    FinRechPaySal.Visible = false;
                    FinStrucFrais.Visible = false;

                    if (dr["service"].ToString() == "Admin" && dr["idEcole"].ToString() == "Toutes les écoles")
                    {
                        ctrlAnnee.Visible = true;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = true;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                        FinAvancSal.Visible = true;
                        FinCaisse.Visible = true;
                        FinDepense.Visible = true;
                        FinNivPay.Visible = true;
                        FinPayFr.Visible = true;
                        FinPaySal.Visible = true;
                        FinRechPay.Visible = true;
                        FinRechPaySal.Visible = true;
                        FinStrucFrais.Visible = true;
                    }
                    if (dr["service"].ToString() == "Préfet Secondaire" && dr["idEcole"].ToString() == "3")
                    {
                        ctrlAnnee.Visible = false;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = false;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                        FinAvancSal.Visible = true;
                        FinCaisse.Visible = true;
                        FinDepense.Visible = true;
                        FinNivPay.Visible = true;
                        FinPayFr.Visible = true;
                        FinPaySal.Visible = true;
                        FinRechPay.Visible = true;
                        FinRechPaySal.Visible = true;
                        FinStrucFrais.Visible = true;
                    }
                    if (dr["service"].ToString() == "Directeur" && (dr["idEcole"].ToString() == "2" || dr["idEcole"].ToString() == "1"))
                    {
                        ctrlAnnee.Visible = false;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = false;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                        FinAvancSal.Visible = true;
                        FinCaisse.Visible = true;
                        FinDepense.Visible = true;
                        FinNivPay.Visible = true;
                        FinPayFr.Visible = true;
                        FinPaySal.Visible = true;
                        FinRechPay.Visible = true;
                        FinRechPaySal.Visible = true;
                        FinStrucFrais.Visible = true;
                    }
                    if (dr["service"].ToString() == "Comptable" && (dr["idEcole"].ToString() == "3" || dr["idEcole"].ToString() == "2" || dr["idEcole"].ToString() == "1"))
                    {
                        ctrlAnnee.Visible = false;
                        ctrlAgent.Visible = true;
                        ctrlFinance.Visible = true;
                        ctrlInscription.Visible = true;
                        ctrlUtilisateur.Visible = false;
                        txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                        FinAvancSal.Visible = true;
                        FinCaisse.Visible = true;
                        FinDepense.Visible = true;
                        FinNivPay.Visible = false;
                        FinPayFr.Visible = true;
                        FinPaySal.Visible = false;
                        FinRechPay.Visible = true;
                        FinRechPaySal.Visible = false;
                        FinStrucFrais.Visible = false;
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