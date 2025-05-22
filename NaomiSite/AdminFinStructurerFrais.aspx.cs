using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using KimToo;
using System.Threading;

namespace NaomiSite
{
    public partial class AdminFinStructurerFrais : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
        MySqlConnection con2 = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
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
                if (!IsPostBack)
                {
                    AfficherFraisScolaire();
                    TrouverIdEcole();
                    txtLibelle.Text = "Frais scolaires";
                }
                
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void AfficherFraisScolaire()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT DISTINCT frais_scolaire.idfrais as idfrais,ecole.nomEcole as nomEcole,section.nomSection as nomSection,t_classe.classe as nomClasse,frais_scolaire.designation as designation,frais_scolaire.tranche1 as tranche1,tranche2 as tranche2,tranche3 as tranche3,unite as unite from frais_scolaire,t_classe,section,ecole WHERE frais_scolaire.classe=t_classe.id AND frais_scolaire.idEcole=ecole.idEcole AND frais_scolaire.optionConcerne=section.idSection AND t_classe.idSection=section.idSection AND frais_scolaire.anneeScolaire='" + txtIdAnnee.Text + "' ORDER BY nomEcole ASC,nomSection ASC,nomClasse ASC", con);
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
                cmdD.CommandText = ("SELECT DISTINCT frais_scolaire.idfrais as idfrais,ecole.nomEcole as nomEcole,section.nomSection as nomSection,t_classe.classe as nomClasse,frais_scolaire.designation as designation,frais_scolaire.tranche1 as tranche1,tranche2 as tranche2,tranche3 as tranche3,unite as unite from frais_scolaire, t_classe, section, ecole WHERE frais_scolaire.classe = t_classe.id AND frais_scolaire.idEcole = ecole.idEcole AND frais_scolaire.optionConcerne = section.idSection AND t_classe.idSection = section.idSection AND frais_scolaire.anneeScolaire = '" + txtIdAnnee.Text + "' AND CONCAT(ecole.nomEcole,section.nomSection,t_classe.classe,frais_scolaire.designation ) LIKE '%" + recherche + "%' ORDER BY nomEcole ASC,nomSection ASC,nomClasse ASC");
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
            txtOption.Items.Add("Toutes les options de l'ecole".ToString());
            con.Close();
        }
        public void TrouverIdSection()
        {
            con.Open();
            txtIdOption.Text = "0";
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from section WHERE nomSection='" + txtOption.SelectedValue + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdOption.Text = "";
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
            txtClasse.Items.Add("Toutes les classes de la section".ToString());
            con.Close();
        }
        public void TrouverIdClasse()
        {
            con.Open();
            txtIdClasse.Text = "0";
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_classe WHERE classe='" + txtClasse.SelectedValue + "' AND idSection='" + txtIdOption.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdClasse.Text = "";
                txtIdClasse.Text = dr["id"].ToString();
            }
            con.Close();
        }

        protected void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            recherche(txtRecherche.Text);
        }
        protected void txtEcole_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdEcole();
            TrouverSection();
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
        }
        protected void txtIdEcole_TextChanged(object sender, EventArgs e)
        {
            TrouverSection();
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
        }

        protected void txtOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtOption.SelectedValue== "Toutes les options de l'ecole")
            {
                TrouverClasser();
                txtIdClasse.Text = "0";
                txtIdOption.Text = "0";
            }
            else
            {
                TrouverIdSection();
                TrouverClasser();
                TrouverIdClasse();
            }
            
        }

        protected void txtClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtClasse.SelectedValue== "Toutes les classes de la section")
            {
                txtIdClasse.Text = "0";
            }
            else
            {
                TrouverIdClasse();
            }
           
        }
        public void EnregFraisClasseSpécifiqueEtSectionSpecifique()
        {
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into frais_scolaire values(default,'" + txtLibelle.Text + "','" + txtTranche1.Text.Replace(',', '.') + "','" + txtTranche2.Text.Replace(',', '.') + "','" + txtTranche3.Text.Replace(',', '.') + "','" + txtUnite.SelectedValue + "','" + txtIdClasse.Text + "','" + txtIdOption.Text + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
            Response.Redirect("AdminFinStructurerFrais.aspx");
        }
        public void EnregFraisClasseGeneraleEtSectionGenerale()
        {
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into frais_scolaire values(default,'" + txtLibelle.Text + "','" + txtTranche1.Text.Replace(',', '.') + "','" + txtTranche2.Text.Replace(',', '.') + "','" + txtTranche3.Text.Replace(',', '.') + "','" + txtUnite.SelectedValue + "','Toutes les classes','Toutes les options','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
            Response.Redirect("AdminFinStructurerFrais.aspx");
        }
        public void EnregFraisClasseGeneraleEtSectionSpecifique()
        {
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into frais_scolaire values(default,'" + txtLibelle.Text + "','" + txtTranche1.Text.Replace(',', '.') + "','" + txtTranche2.Text.Replace(',', '.') + "','" + txtTranche3.Text.Replace(',', '.') + "','" + txtUnite.SelectedValue + "','Toutes les classes','" + txtIdOption.Text + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
            Response.Redirect("AdminFinStructurerFrais.aspx");
        }
        public void EnregFraisClasseReduiteEtSectionSpecifique()
        {
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into frais_scolaire values(default,'" + txtLibelle.Text + "','" + txtTranche1.Text.Replace(',', '.') + "','" + txtTranche2.Text.Replace(',', '.') + "','" + txtTranche3.Text.Replace(',', '.') + "','" + txtUnite.SelectedValue + "','"+txtClasse.SelectedValue+"','Toutes les options','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
            Response.Redirect("AdminFinStructurerFrais.aspx");
        }
        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
                con.Close();
                con2.Close();
                con.Open();
                con2.Open();

                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

                double t1 = double.Parse(txtTranche1.Text.Replace(',', '.'));
                double t2 = double.Parse(txtTranche1.Text.Replace(',', '.'));
                double t3 = double.Parse(txtTranche1.Text.Replace(',', '.'));
                double somme = t1 + t2 + t3;

                if (somme > 0)
                {
                    if (txtOption.SelectedValue == "Toutes les options de l'ecole" && txtClasse.SelectedValue == "Toutes les classes de la section")
                    {
                        //Entregistrement d'un frais dans toutes les classes
                        //Et dans toutes les options de l'école sélectionnée
                        MySqlCommand cmdB = con.CreateCommand();
                        cmdB.CommandType = CommandType.Text;
                        cmdB.CommandText = ("SELECT *from t_classe WHERE idEcole='" + txtIdEcole.Text + "'");
                        MySqlDataReader dr = cmdB.ExecuteReader();
                        while (dr.Read())
                        {
                            MySqlCommand cmd1a = con2.CreateCommand();
                            cmd1a.CommandType = CommandType.Text;
                            cmd1a.CommandText = "insert into frais_scolaire values(default,'" + txtLibelle.Text + "','" + txtTranche1.Text.Replace(',', '.') + "','" + txtTranche2.Text.Replace(',', '.') + "','" + txtTranche3.Text.Replace(',', '.') + "','" + txtUnite.SelectedValue + "','" + dr["id"].ToString() + "','" + dr["idSection"].ToString() + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
                            cmd1a.ExecuteNonQuery();
                        }
                        con2.Close();
                        con.Close();
                        Response.Redirect("AdminFinStructurerFrais.aspx");
                    }
                    if (txtOption.SelectedValue != "Toutes les options de l'ecole" && txtClasse.SelectedValue == "Toutes les classes de la section")
                    {
                        //Entregistrement d'un frais dans toutes les classes
                        //de la section sélectionnée de cette école spécifiée
                        MySqlCommand cmdB = con.CreateCommand();
                        cmdB.CommandType = CommandType.Text;
                        cmdB.CommandText = ("SELECT *from t_classe WHERE idSection='" + txtIdOption.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                        MySqlDataReader dr = cmdB.ExecuteReader();
                        while (dr.Read())
                        {
                            MySqlCommand cmd1a = con2.CreateCommand();
                            cmd1a.CommandType = CommandType.Text;
                            cmd1a.CommandText = "insert into frais_scolaire values(default,'" + txtLibelle.Text + "','" + txtTranche1.Text.Replace(',', '.') + "','" + txtTranche2.Text.Replace(',', '.') + "','" + txtTranche3.Text.Replace(',', '.') + "','" + txtUnite.SelectedValue + "','" + dr["id"].ToString() + "','" + txtIdOption.Text + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
                            cmd1a.ExecuteNonQuery();
                        }
                        con2.Close();
                        con.Close();
                        Response.Redirect("AdminFinStructurerFrais.aspx");
                    }
                    if (txtClasse.SelectedValue != "Toutes les classes de la section" && txtOption.SelectedValue == "Toutes les options de l'ecole")
                    {
                        //Entregistrement d'un frais dans la classe spécifiée
                        //de toutes les sections sélectionnées de cette école spécifiée
                        MySqlCommand cmdB = con.CreateCommand();
                        cmdB.CommandType = CommandType.Text;
                        cmdB.CommandText = ("SELECT *from t_classe WHERE classe='" + txtClasse.SelectedValue + "' AND idEcole='" + txtIdEcole.Text + "'");
                        MySqlDataReader dr = cmdB.ExecuteReader();
                        while (dr.Read())
                        {
                            MySqlCommand cmd1a = con2.CreateCommand();
                            cmd1a.CommandType = CommandType.Text;
                            cmd1a.CommandText = "insert into frais_scolaire values(default,'" + txtLibelle.Text + "','" + txtTranche1.Text.Replace(',', '.') + "','" + txtTranche2.Text.Replace(',', '.') + "','" + txtTranche3.Text.Replace(',', '.') + "','" + txtUnite.SelectedValue + "','" + dr["id"].ToString() + "','" + dr["idSection"].ToString() + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
                            cmd1a.ExecuteNonQuery();
                        }
                        con2.Close();
                        con.Close();
                        Response.Redirect("AdminFinStructurerFrais.aspx");
                    }
                    if (txtOption.SelectedValue != "Toutes les options de l'ecole" && txtClasse.SelectedValue != "Toutes les classes de la section")
                    {
                        //Entregistrement d'un frais dans la classe spécifiée
                        //de la section spécifiée de cette école spécifiée
                        MySqlCommand cmd1a = con.CreateCommand();
                        cmd1a.CommandType = CommandType.Text;
                        cmd1a.CommandText = "insert into frais_scolaire values(default,'" + txtLibelle.Text + "','" + txtTranche1.Text.Replace(',', '.') + "','" + txtTranche2.Text.Replace(',', '.') + "','" + txtTranche3.Text.Replace(',', '.') + "','" + txtUnite.SelectedValue + "','" + txtIdClasse.Text + "','" + txtIdOption.Text + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
                        cmd1a.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("AdminFinStructurerFrais.aspx");
                    }
                }
                else
                {
                    txtMessage.Visible = true;
                }
                
        }
    }
}