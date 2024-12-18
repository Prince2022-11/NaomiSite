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
    public partial class AdminUpdateAnnee : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Convert.ToInt32(Request.QueryString["id"].ToString());
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmd1 = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select etat from anneescol WHERE anneeScolaire=" + id + "");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["etat"].ToString() == "Actif")
                {
                    con.Close();
                    con.Open();
                    cmd1.CommandText = "UPDATE anneescol SET etat='Inactif' WHERE anneeScolaire=" + id + "";
                    cmd1.ExecuteNonQuery();
                    Response.Redirect("AdminAnneeScolaire.aspx");
                }
                else
                {
                    con.Close();
                    con.Open();
                    cmd1.CommandText = "UPDATE anneescol SET etat='Actif' WHERE anneeScolaire=" + id + "";
                    cmd1.ExecuteNonQuery();
                    Response.Redirect("AdminAnneeScolaire.aspx");
                }
            }
            con.Close();
        }
    }
}