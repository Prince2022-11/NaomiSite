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
    public partial class AdminChangeClasse : System.Web.UI.Page
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
                AfficherInscription();
                TrouverIdEcole();
                txtMessage.Visible = false;
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void AfficherInscription()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole FROM change_classe,t_classe,section,ecole WHERE change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND change_classe.idEcole='" + txtIdEcole.Text+ "' AND change_classe.optionEtude='"+txtIdOption.Text+ "' AND change_classe.classe='"+txtIdClasse.Text+ "' AND change_classe.anneeScolaire='"+txtIdAnnee.Text+"' AND etat='Actif' ORDER BY nom", con);
            cmdB1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
            da.Fill(dt);
            DataGrid.DataSource = dt;
            DataGrid.DataBind();
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
        public void TrouverSectionMontante()
        {
            con.Open();
            txtOptionMontante.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT nomSection from section WHERE idEcole='" + txtIdEcole.Text + "' ORDER BY nomSection ASC");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtOptionMontante.Items.Add(dr["nomSection"].ToString());
            }
            con.Close();
        }
        public void TrouverAnneScolaireMontante()
        {
            con.Open();
            int a1 = int.Parse(txtIdAnnee.Text);
            txtAnneeMontante.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT designation from anneescol WHERE etat='Inactif' AND anneeScolaire>"+a1+" ORDER BY designation ASC");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtAnneeMontante.Items.Add(dr["designation"].ToString());
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
        public void TrouverIdSectionMontante()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from section WHERE nomSection='" + txtOptionMontante.SelectedValue + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdOptionMontante.Text = dr["idSection"].ToString();
            }
            con.Close();
        }
        public void TrouverIdAnneeScolaireMontante()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from anneescol WHERE designation='" + txtAnneeMontante.SelectedValue + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdAnneeMontante.Text = dr["anneeScolaire"].ToString();
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
        public void TrouverClasseMontante()
        {
            con.Open();
            txtClasseMontante.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT classe from t_classe WHERE idSection='" + txtIdOptionMontante.Text + "' ORDER BY classe ASC");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtClasseMontante.Items.Add(dr["classe"].ToString());
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
        public void TrouverIdClasseMontante()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_classe WHERE classe='" + txtClasseMontante.SelectedValue + "' AND idSection='" + txtIdOptionMontante.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdClasseMontante.Text = dr["id"].ToString();
            }
            con.Close();
        }
        public void TrouverElementsChangerClasse()
        {
            //Trouver les informations de l'élève à qui la classe sera changée
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("SELECT *FROM change_classe WHERE idClasse='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "'");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtMatricule.Text = dr["matricule"].ToString();
                txtNom.Text = dr["nomEleve"].ToString();
                txtPrenom.Text = dr["prenom"].ToString();
                txtSexe.Text = dr["sexe"].ToString();
            }
            con.Close();
        }
        public void CreerCompteEleve()
        {
            con.Open();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into compte_eleve values(default,'" + txtMatricule.Text + "','0','0','0','0','0','0','0','0','0','0','" + txtIdAnneeMontante.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
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
            TrouverAnneScolaireMontante();
            TrouverIdAnneeScolaireMontante();
            TrouverSectionMontante();
            TrouverIdSectionMontante();
            TrouverClasseMontante();
            TrouverIdClasseMontante();
            AfficherInscription();
            txtMatricule.Text = "Aucun...";
        }
        protected void txtIdEcole_TextChanged(object sender, EventArgs e)
        {
            TrouverSection();
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
            AfficherInscription();
            txtMatricule.Text = "Aucun...";
        }

        protected void txtOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
            AfficherInscription();
            txtMatricule.Text = "Aucun...";
        }

        protected void txtClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdClasse();
            AfficherInscription();
            txtMatricule.Text = "Aucun...";
        }
        protected void txtAnneeMontante_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdAnneeScolaireMontante();
        }
        protected void txtOptionMontante_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdSectionMontante();
            TrouverClasseMontante();
            TrouverIdClasseMontante();
        }
        protected void txtClasseMontante_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdClasseMontante();
        }

        protected void DataGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }
        protected void btnChangerClasse_Click(object sender, EventArgs e)
        {
            txtMessage.Visible = false;
            TrouverElementsChangerClasse();
            if (txtMatricule.Text != "" && txtIdAnneeMontante.Text != "" && txtIdOptionMontante.Text != "" && txtIdClasseMontante.Text != "")
            {
                //Enregistrement de l'élève dans la nouvelle classe
                con.Close();
                con.Open();
                MySqlCommand cmd1a = con.CreateCommand();
                cmd1a.CommandType = CommandType.Text;
                cmd1a.CommandText = "insert into change_classe values(default,'" + txtMatricule.Text + "','" + txtNom.Text + "', '" + txtPrenom.Text + "', '" + txtSexe.Text + "', '" + txtIdClasseMontante.Text + "', '" + txtIdOptionMontante.Text + "', '" + txtIdAnneeMontante.Text + "', '" + txtIdEcole.Text + "','Actif')";
                cmd1a.ExecuteNonQuery();

                //Mettre à jour la situation de l'élève pour qu'il ne soit plus visible dans cette 
                //Promotion dans la même année
                MySqlCommand cmd1aa = con.CreateCommand();
                cmd1aa.CommandType = CommandType.Text;
                cmd1aa.CommandText = "UPDATE change_classe SET etat='Inactif' WHERE matricule='" + txtMatricule.Text + "' AND classe='" + txtIdClasse.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "'";
                cmd1aa.ExecuteNonQuery();
                con.Close();
                //Créer son compte pour la nouvelle année et actualiser les restants dans la classe
                CreerCompteEleve();
                txtMatricule.Text = "Succès... Sélectionnez un autre";
                AfficherInscription();
            }
            else
            {
                txtMessage.Visible = true;
            }
        }
    }
}