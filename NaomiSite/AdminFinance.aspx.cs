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
    public partial class AdminFinance : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
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