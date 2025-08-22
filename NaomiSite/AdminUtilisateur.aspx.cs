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
    public partial class AdminUtilisateur : System.Web.UI.Page
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
                AfficherUser();
                TrouverIdEcole();

            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void AfficherUser()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT * from utilisateur,ecole WHERE utilisateur.service='Admin' UNION SELECT * from utilisateur,ecole WHERE utilisateur.idEcole=ecole.idEcole ORDER BY login ASC", con);
            cmdB1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
            da.Fill(dt);
            Data1.DataSource = dt;
            Data1.DataBind();
            con.Close();
            con.Close();
        }
        public void TrouverIdEcole()
        {
            con.Close();
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from ecole WHERE nomEcole='" + txtEcole.SelectedValue + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdEcole.Text = dr["idEcole"].ToString();
            }
            con.Close();
        }
        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            con.Open();
            if (txtFonction.SelectedValue == "Admin")
            {
                MySqlCommand cmd1a = con.CreateCommand();
                cmd1a.CommandType = CommandType.Text;
                cmd1a.CommandText = "insert into utilisateur values(default,'" + txtLoginUser.Text + "','" + txtPassword.Text + "','" + txtFonction.SelectedValue + "','Toutes les écoles','Actif')";
                cmd1a.ExecuteNonQuery();
                con.Close();
                Response.Redirect("AdminUtilisateur.aspx");
            }
            else
            {
                MySqlCommand cmd1a = con.CreateCommand();
                cmd1a.CommandType = CommandType.Text;
                cmd1a.CommandText = "insert into utilisateur values(default,'" + txtLoginUser.Text + "','" + txtPassword.Text + "','" + txtFonction.SelectedValue + "','" + txtIdEcole.Text + "','Actif')";
                cmd1a.ExecuteNonQuery();
                con.Close();
                Response.Redirect("AdminUtilisateur.aspx");
            }
        }
        protected void txtEcole_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdEcole();
        }
        protected void Data1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}