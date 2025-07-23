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
                try
                {
                    TotalInscrit();
                    TotalAgent();
                    TotalUSD();
                    TotalCDF();
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
            MySqlCommand cmdA = new MySqlCommand("select count(*) from t_eleve,anneescol WHERE t_eleve.anneescol=anneescol.anneeScolaire and t_eleve.anneescol='" + txtIdAnnee.Text + "'", con);
            MySqlDataReader drA = cmdA.ExecuteReader();
            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtTotalInscrit.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalAgent()
        {
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from t_agent", con);
            MySqlDataReader drA = cmdA.ExecuteReader();

            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtTotalAgent.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalUSD()
        {
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select SUM(Solde) as soldeUSD from t_caisse WHERE libelle='USD'", con);
            MySqlDataReader drA = cmdA.ExecuteReader();

            if (drA.Read())
            {
                txtTotalUSD.Text = drA["soldeUSD"].ToString();
            }
            con.Close();
        }
        public void TotalCDF()
        {
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select SUM(Solde) as soldeCDF from t_caisse WHERE libelle='CDF'", con);
            MySqlDataReader drA = cmdA.ExecuteReader();

            if (drA.Read())
            {
                txtTotalCDF.Text = drA["soldeCDF"].ToString();
            }
            con.Close();
        }
    }
}