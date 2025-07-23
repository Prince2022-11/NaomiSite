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
    public partial class AdminAnneeScolaire : System.Web.UI.Page
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
                AfficherAnneeScolaire();

            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void AfficherAnneeScolaire()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT anneeScolaire as id, designation as designation, etat as etat from anneescol ORDER BY designation ASC", con);
            cmdB1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
            da.Fill(dt);
            Data1.DataSource = dt;
            Data1.DataBind();
            con.Close();
            con.Close();
        }
        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into anneescol values(default,'" + txtNewAnnee.Text + "','Inactif')";
            cmd1a.ExecuteNonQuery();
            con.Close();
            Response.Redirect("AdminAnneeScolaire.aspx");
        }
        protected void Data1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}