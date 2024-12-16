using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

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
                MySqlCommand cmd1 = new MySqlCommand("", con);
                MySqlCommand cmde = con.CreateCommand();
                cmde.CommandType = CommandType.Text;
                cmd.CommandText = ("select * from utilisateur WHERE login='" + txtLogin.Text + "'");
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtRole.Text = dr["service"].ToString();
                }
                con.Close();
                //TotalAgentTerrain();
                //TotalComptableETD();
                //TotalGestionnaireETD();
                //TotalETD();
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void TotalETD()
        {
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from etd", con);
            MySqlDataReader drA = cmdA.ExecuteReader();

            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtTotalETD.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalGestionnaireETD()
        {
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from agent WHERE fonction='Gestionnaire ETD'", con);
            MySqlDataReader drA = cmdA.ExecuteReader();

            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtTotalGestionnaireETD.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalAgentTerrain()
        {
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from agent WHERE fonction='Agent terrain'", con);
            MySqlDataReader drA = cmdA.ExecuteReader();

            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtTotalAgentTerrain.Text = nombre.ToString();
            }
            con.Close();
        }
        public void TotalComptableETD()
        {
            con.Open();
            MySqlCommand cmdA = new MySqlCommand("select count(*) from agent WHERE fonction='Agent comptable'", con);
            MySqlDataReader drA = cmdA.ExecuteReader();

            if (drA.Read())
            {
                int nombre = drA.GetInt32(0);
                txtTotalComptable.Text = nombre.ToString();
            }
            con.Close();
        }
    }
}