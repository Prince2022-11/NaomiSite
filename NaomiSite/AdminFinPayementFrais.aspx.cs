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
    public partial class AdminFinPayementFrais : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

                        if (dr["service"].ToString() == "Admin" && dr["idEcole"].ToString() == "Toutes les écoles")
                        {
                            ctrlAnnee.Visible = true;
                            ctrlAgent.Visible = true;
                            ctrlFinance.Visible = true;
                            ctrlInscription.Visible = true;
                            ctrlUtilisateur.Visible = true;
                            txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                        }
                        if (dr["service"].ToString() == "Préfet Secondaire" && dr["idEcole"].ToString() == "3")
                        {
                            ctrlAnnee.Visible = false;
                            ctrlAgent.Visible = true;
                            ctrlFinance.Visible = true;
                            ctrlInscription.Visible = true;
                            ctrlUtilisateur.Visible = false;
                            txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                        }
                        if (dr["service"].ToString() == "Directeur" && (dr["idEcole"].ToString() == "2" || dr["idEcole"].ToString() == "1"))
                        {
                            ctrlAnnee.Visible = false;
                            ctrlAgent.Visible = true;
                            ctrlFinance.Visible = true;
                            ctrlInscription.Visible = true;
                            ctrlUtilisateur.Visible = false;
                            txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
                        }
                        if (dr["service"].ToString() == "Comptable" && (dr["idEcole"].ToString() == "3" || dr["idEcole"].ToString() == "2" || dr["idEcole"].ToString() == "1"))
                        {
                            ctrlAnnee.Visible = false;
                            ctrlAgent.Visible = true;
                            ctrlFinance.Visible = true;
                            ctrlInscription.Visible = true;
                            ctrlUtilisateur.Visible = false;
                            txtIdEcoleAffectationUser.Text = dr["idEcole"].ToString();
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
                    txtRecherche.Text = "";
                    TrouverIdEcole();
                    TrouverSection();
                    TrouverIdSection();
                    TrouverClasser();
                    TrouverIdClasse();
                    AfficherInscription();
                }
                else
                {
                    Response.Redirect("Acceuil.aspx");
                }
            }
        }
        public void AfficherInscription()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole FROM change_classe,t_classe,section,ecole WHERE change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND change_classe.idEcole='" + txtIdEcole.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.anneeScolaire='" + txtIdAnnee.Text + "' AND etat='Actif' ORDER BY nom", con);
            cmdB1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
            da.Fill(dt);
            Data1.DataSource = dt;
            Data1.DataBind();
            con.Close();
        }
        public void recherche(string recherche)
        {
            try
            {
                con.Close();
                con.Open();
                MySqlCommand cmdD = con.CreateCommand();
                cmdD.CommandType = CommandType.Text;
                cmdD.CommandText = ("SELECT change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole FROM change_classe, t_classe, section, ecole WHERE change_classe.classe = t_classe.id and change_classe.optionEtude = section.idSection AND change_classe.idEcole = ecole.idEcole AND change_classe.anneeScolaire = '" + txtIdAnnee.Text + "' AND etat = 'Actif' AND CONCAT(change_classe.matricule,change_classe.nomEleve,change_classe.prenom,t_classe.classe,section.nomSection,ecole.nomEcole) LIKE '%" + recherche + "%' ORDER BY nom ASC ");
                cmdD.ExecuteNonQuery();
                DataTable dtD = new DataTable();
                MySqlDataAdapter daD = new MySqlDataAdapter(cmdD);
                daD.Fill(dtD);
                Data1.DataSource = dtD;
                Data1.DataBind();
                con.Close();
            }
            catch
            {

            }
        }
        public void TrouverIdEcole()
        {
            con.Close();
            con.Open();
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles"|| txtRole.Text == "Comptable")
            {
                lblEcole.Visible = true;
                txtEcole.Visible = true;
                MySqlCommand cmdB = con.CreateCommand();
                cmdB.CommandType = CommandType.Text;
                cmdB.CommandText = ("SELECT *from ecole WHERE nomEcole='" + txtEcole.SelectedValue + "'");
                MySqlDataReader dr = cmdB.ExecuteReader();
                while (dr.Read())
                {
                    txtIdEcole.Text = dr["idEcole"].ToString();
                }
            }
            else
            {
                txtIdEcole.Text = txtIdEcoleAffectationUser.Text;
                TrouverEcole();
            }
            con.Close();
        }
        public void TrouverEcole()
        {
            con.Close();
            con.Open();
            txtEcole.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT nomEcole from ecole WHERE idEcole='" + txtIdEcoleAffectationUser.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtEcole.Items.Add(dr["nomEcole"].ToString());
            }
            con.Close();
        }
        public void TrouverSection()
        {
            con.Open();
            txtOption.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT nomSection from section WHERE idEcole='" + txtIdEcole.Text + "' ORDER BY nomSection ASC");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtOption.Items.Add(dr["nomSection"].ToString());
            }
            con.Close();
        }
        public void TrouverIdSection()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from section WHERE nomSection='" + txtOption.SelectedValue + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdOption.Text = dr["idSection"].ToString();
            }
            con.Close();
        }
        public void TrouverClasser()
        {
            con.Open();
            txtClasse.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT classe from t_classe WHERE idSection='" + txtIdOption.Text + "' ORDER BY classe ASC");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtClasse.Items.Add(dr["classe"].ToString());
            }
            con.Close();
        }
        public void TrouverIdClasse()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_classe WHERE classe='" + txtClasse.SelectedValue + "' AND idSection='" + txtIdOption.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdClasse.Text = dr["id"].ToString();
            }
            con.Close();
        }
        protected void Data1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void txtEcole_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdEcole();
            TrouverSection();
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
            AfficherInscription();
        }
        protected void txtIdEcole_TextChanged(object sender, EventArgs e)
        {
            TrouverSection();
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
            AfficherInscription();
        }

        protected void txtOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
            AfficherInscription();
        }

        protected void txtClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdClasse();
            AfficherInscription();
        }

        protected void DataGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            recherche(txtRecherche.Text);
        }
    }
}