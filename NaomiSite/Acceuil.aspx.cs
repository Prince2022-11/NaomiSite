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
    public partial class Acceuil : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
        protected void Page_Load(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            if (!Page.IsPostBack)
            {
                
                txtMessage.Visible = false;
                txtLogin.Text = "";
                txtPassword.Text = "";
            }
            
        }
        public void ConnexionAdmin()
        {
            Session["autorisation"] = true;
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            //con.Open();
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select *from utilisateur where service='Admin' and login='" + txtLogin.Text + "' and password='" + txtPassword.Text + "' ");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["login"].ToString() == txtLogin.Text && dr["password"].ToString() == txtPassword.Text)
                {
                    Session["login"] = dr["login"].ToString();
                    Response.Redirect("EspaceAdmin.aspx");
                }
                else
                {
                    txtMessage.Visible = true;
                    Session["autorisation"] = false;
                }
                txtMessage.Visible = true;
            }
        }
        public void ConnexionAgentTerrain()
        {
            Session["autorisation"] = true;
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            //con.Open();
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select *from agent where fonction='Agent terrain' AND login='" + txtLogin.Text + "' and password='" + txtPassword.Text + "' AND etat='Actif' ");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["login"].ToString() == txtLogin.Text && dr["password"].ToString() == txtPassword.Text)
                {
                    Session["login"] = dr["login"].ToString();
                    Response.Redirect("AgentTerrainEspace.aspx");
                }
                else
                {
                    txtMessage.Visible = true;
                    Session["autorisation"] = false;
                }
                txtMessage.Visible = true;
            }
        }
        public void ConnexionAgentComptable()
        {
            Session["autorisation"] = true;
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            //con.Open();
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select *from agent where fonction='Agent comptable' AND login='" + txtLogin.Text + "' and password='" + txtPassword.Text + "' AND etat='Actif'");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["login"].ToString() == txtLogin.Text && dr["password"].ToString() == txtPassword.Text)
                {
                    Session["login"] = dr["login"].ToString();
                    Response.Redirect("AgentComptableEspace.aspx");
                }
                else
                {
                    txtMessage.Visible = true;
                    Session["autorisation"] = false;
                }
                txtMessage.Visible = true;
            }
        }
        public void ConnexionAgentGestionnaire()
        {
            Session["autorisation"] = true;
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            //con.Open();
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select *from agent where fonction='Gestionnaire ETD' AND login='" + txtLogin.Text + "' and password='" + txtPassword.Text + "' AND etat='Actif'");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr["login"].ToString() == txtLogin.Text && dr["password"].ToString() == txtPassword.Text)
                {
                    Session["login"] = dr["login"].ToString();
                    Response.Redirect("AgentGestionnaireETD.aspx");
                }
                else
                {
                    txtMessage.Visible = true;
                    Session["autorisation"] = false;
                }
                txtMessage.Visible = true;
            }
        }

        protected void btnConnexion_Click(object sender, EventArgs e)
        {
            ConnexionAdmin();
            //ConnexionAgentTerrain();
            //ConnexionAgentComptable();
            //ConnexionAgentGestionnaire();
        }
    }
}