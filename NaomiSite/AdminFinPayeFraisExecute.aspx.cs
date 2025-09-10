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
    public partial class AdminFinPayeFraisExecute : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gestion_naomi");
        int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            //id = Convert.ToInt32(Request.QueryString["id"].ToString());
            //Vérification de la connexion de la varibale session
            if (Session["autorisation"] != null && (bool)Session["autorisation"] == true)
            {

                if (!IsPostBack) //Pour que la page ait le pouvoir de modifier le rendue
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
                    success.Visible = false;
                    error.Visible = false;
                    con.Close();
                    try
                    {
                        ViderChamps();
                        TrouverEleve();
                        TrouverEcole();
                        TrouverSection();
                        TrouverClasser();
                        TrouverFrais();
                        TrouverIdFrais();
                        SituationCaisse();
                        TrouverDerniereOperation();
                    }
                    catch { }
                }
            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void TrouverEcole()
        {
            con.Close();
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from ecole WHERE idEcole='" + txtIdEcole.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtEcole.Text = " / " + dr["nomEcole"].ToString();
            }
            con.Close();
        }
        public void TrouverEleve()
        {
            id = Convert.ToInt32(Request.QueryString["id"].ToString());
            con.Close();
            con.Open();
            txtIdFrais.Text = id.ToString();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmd1 = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = ("select *from change_classe WHERE idClasse=" + id + " AND anneeScolaire='"+txtIdAnnee.Text+"' AND etat='Actif'");
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtIdEcole.Text = dr["idEcole"].ToString();
                txtIdOption.Text = dr["optionEtude"].ToString();
                txtIdClasse.Text = dr["classe"].ToString();
                txtMatricule.Text = dr["matricule"].ToString();
                txtNomEleve.Text = dr["nomEleve"].ToString()+" "+dr["prenom"].ToString()+"--";
                //txtTranche2.Text = dr["tranche2"].ToString();
                //txtTranche3.Text = dr["tranche3"].ToString();
                //txtUnite.Text = dr["unite"].ToString();
            }
            con.Close();
        }
        public void TrouverSection()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT nomSection from section WHERE idSection='" + txtIdOption.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtOption.Text = dr["nomSection"].ToString();
            }
            con.Close();
        }
        public void TrouverClasser()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_classe WHERE id='" + txtIdClasse.Text + "' AND idSection='" + txtIdOption.Text + "' AND idEcole='" + txtIdEcole.Text + "' ");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtClasse.Text = dr["classe"].ToString();
            }
            con.Close();
        }
        public void TrouverFrais()
        {
            con.Open();
            txtFrais.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from frais_scolaire WHERE classe='" + txtIdClasse.Text + "' AND optionConcerne='" + txtIdOption.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "' ORDER BY designation ASC");
            MySqlDataReader dr = cmdB.ExecuteReader();
            txtFrais.Items.Add("--Sélectionnez un frais que l'élève veut payer--");
            while (dr.Read())
            {
                txtFrais.Items.Add(dr["designation"].ToString());
            }
            con.Close();
        }
        public void TrouverIdFrais()
        {
            try
            {
                con.Open();
                MySqlCommand cmdB = con.CreateCommand();
                cmdB.CommandType = CommandType.Text;
                cmdB.CommandText = ("SELECT *from frais_scolaire WHERE classe='" + txtIdClasse.Text + "' AND optionConcerne='" + txtIdOption.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "' AND designation='" + txtFrais.SelectedValue + "' ORDER BY designation ASC");
                MySqlDataReader dr = cmdB.ExecuteReader();
                while (dr.Read())
                {
                    txtIdFrais.Text = dr["idfrais"].ToString();
                    txtT1.Text = dr["tranche1"].ToString();
                    txtT2.Text = dr["tranche2"].ToString();
                    txtT3.Text = dr["tranche3"].ToString();
                    txtUnite.Text = dr["unite"].ToString();
                    txtUnite1.Text = "Le montant prévu en " + dr["unite"].ToString();
                    txtUnite2.Text = "Evolution en payement en " + dr["unite"].ToString();
                }
                con.Close();
            }
            catch
            {

            }
        }
        public void TrouverDerniereOperation()
        {
            try
            {
                con.Close();
                con.Open();
                MySqlCommand cmdB = con.CreateCommand();
                cmdB.CommandType = CommandType.Text;
                cmdB.CommandText = ("SELECT MAX(id_payement) as DOperation from t_payement_frais");
                MySqlDataReader dr = cmdB.ExecuteReader();
                txtDernierOperation.Text = "";
                while (dr.Read())
                {
                    txtDernierOperation.Text = dr["DOperation"].ToString();
                    int NouvelleOperation = int.Parse(dr["DOperation"].ToString()) + 1;
                    txtIdRecu.Text= NouvelleOperation.ToString() + "-" + txtMatricule.Text;
                }
                con.Close();
            }
            catch
            {

            }
        }
        public void TrouverFraisDejaPaye()
        {
            try
            {
                //using System.Globalization;using System.Threading;
                //L'ajout de ces 2 lignes et de ces bibliothèques font l'utilisation du point au décimal
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

                con.Open();
                MySqlCommand cmd = new MySqlCommand("", con);
                MySqlCommand cmde = con.CreateCommand();
                cmde.CommandType = CommandType.Text;
                cmd.CommandText = "select *from situation_paye where idFrais='" + txtIdFrais.Text + "' and matricule='" + txtMatricule.Text + "' and anneeScolaire='" + txtIdAnnee.Text + "'";
                MySqlDataReader dr = cmd.ExecuteReader();
                txtT11.Text = "0";
                txtT22.Text = "0";
                txtT33.Text = "0";
                txtReste.Text = "0";
                while (dr.Read())
                {
                    
                    txtT11.Text = dr["tranche1"].ToString();
                    txtT22.Text = dr["tranche2"].ToString();
                    txtT33.Text = dr["tranche3"].ToString();
                }
                //Trouver le reste d'un frais
                double Apayer,Reste, Paye;
                Apayer = double.Parse(txtT1.Text.Replace(',', '.').ToString()) + double.Parse(txtT2.Text.Replace(',', '.').ToString()) + double.Parse(txtT3.Text.Replace(',', '.').ToString());
                Paye = double.Parse(txtT11.Text.Replace(',', '.').ToString()) + double.Parse(txtT22.Text.Replace(',', '.').ToString()) + double.Parse(txtT33.Text.Replace(',', '.').ToString());
                Reste = Apayer - Paye;
                txtReste.Text = Reste.ToString()+" "+txtUnite.Text;
                con.Close();
            }
            catch
            {

            }
        }
        public void ViderChamps()
        {
            txtT11.Text = "0";
            txtT22.Text = "0";
            txtT33.Text = "0";
            txtMontantVenuAvec.Text = "0";
            txtmontant.Text = "";
            txtIdFrais.Text = "";
            txtT1.Text = "0";
            txtT2.Text = "0";
            txtT3.Text = "0";
            txtTaux.Text = "";
            txtEquivalenceMontant.Text = "";
            txtReste.Text = "0";
            if (!IsPostBack)
            {
                txtFrais.SelectedValue = "--Sélectionnez un frais que l'élève veut payer--";
            }

        }
        public void EnregistrerDansSituationEleve()
        {
            try
            {
                //L'ajout de ces 2 lignes et de ces bibliothèques font l'utilisation du point au décimal
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

                //declaration des variables pour prendre ce qui est prévue dans un frais
                double t1, t2, t3;
                t1 = Convert.ToDouble(txtT1.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                t2 = Convert.ToDouble(txtT2.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                t3 = Convert.ToDouble(txtT3.Text.Replace(',', '.'), CultureInfo.InvariantCulture);

                //Déclaration des variables pour stocker ce que l'élève a déjà payé
                double t11, t22, t33;
                t11 = Convert.ToDouble(txtT11.Text, CultureInfo.InvariantCulture);
                t22 = Convert.ToDouble(txtT22.Text, CultureInfo.InvariantCulture);
                t33 = Convert.ToDouble(txtT33.Text, CultureInfo.InvariantCulture);

                //Déclaration des variables pour le calcul (Resultat)
                double ResT1, ResT2, ResT3, somme, diff, diff2, remb, b;
                b = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                somme = t11 + t22 + t33 + b;

                if (somme > t1)
                {
                    diff = somme - t1;
                    ResT1 = t1;
                    ResT2 = 0;
                    ResT3 = 0;
                    txtT11.Text = ResT1.ToString(CultureInfo.InvariantCulture);
                    txtT22.Text = ResT2.ToString(CultureInfo.InvariantCulture);
                    txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);

                    if (diff > t2)
                    {
                        diff2 = diff - t2;
                        ResT2 = t2;
                        ResT3 = 0;
                        txtT22.Text = ResT2.ToString(CultureInfo.InvariantCulture);
                        txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);
                        if (diff2 > t3)
                        {
                            remb = diff2 - t3;
                            ResT3 = t3;
                            txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);

                            //L'élève fini à payer la 3e Tranche
                            MySqlConnection con = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                            con.Open(); string cmd = "insert into situation_paye values(default,'" + txtIdFrais.Text + "','" + ResT1.ToString(CultureInfo.InvariantCulture) + "','" + ResT2.ToString(CultureInfo.InvariantCulture) + "','" + ResT3.ToString(CultureInfo.InvariantCulture) + "','" + txtMatricule.Text + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
                            MySqlCommand commande = new MySqlCommand(cmd, con);
                            commande.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            ResT3 = diff2;
                            txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);

                            MySqlConnection con = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                            con.Open(); string cmd = "insert into situation_paye values(default,'" + txtIdFrais.Text + "','" + ResT1.ToString(CultureInfo.InvariantCulture) + "','" + ResT2.ToString(CultureInfo.InvariantCulture) + "','" + ResT3.ToString(CultureInfo.InvariantCulture) + "','" + txtMatricule.Text + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
                            MySqlCommand commande = new MySqlCommand(cmd, con);
                            commande.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    else
                    {
                        ResT2 = diff;
                        txtT22.Text = ResT2.ToString(CultureInfo.InvariantCulture);

                        MySqlConnection con = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                        con.Open(); string cmd = "insert into situation_paye values(default,'" + txtIdFrais.Text + "','" + ResT1.ToString(CultureInfo.InvariantCulture) + "','" + ResT2.ToString(CultureInfo.InvariantCulture) + "','" + ResT3.ToString(CultureInfo.InvariantCulture) + "','" + txtMatricule.Text + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
                        MySqlCommand commande = new MySqlCommand(cmd, con);
                        commande.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else
                {
                    ResT1 = somme;
                    ResT2 = 0;
                    ResT3 = 0;
                    txtT11.Text = ResT1.ToString(CultureInfo.InvariantCulture);
                    txtT22.Text = ResT2.ToString(CultureInfo.InvariantCulture);
                    txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);

                    MySqlConnection con = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    con.Open(); string cmd = "insert into situation_paye values(default,'" + txtIdFrais.Text + "','" + ResT1.ToString(CultureInfo.InvariantCulture) + "','" + ResT2.ToString(CultureInfo.InvariantCulture) + "','" + ResT3.ToString(CultureInfo.InvariantCulture) + "','" + txtMatricule.Text + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
                    MySqlCommand commande = new MySqlCommand(cmd, con);
                    commande.ExecuteNonQuery();
                    con.Close();
                }
                
            }
            catch
            {

            }
        }
        public void MettreAJoursSituation()
        {
            try
            {
                con.Open();
                //L'ajout de ces 2 lignes et de ces bibliothèques font l'utilisation du point au décimal
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

                //declaration des variables pour prendre ce qui est prévue dans un frais
                double t1, t2, t3;
                t1 = Convert.ToDouble(txtT1.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                t2 = Convert.ToDouble(txtT2.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                t3 = Convert.ToDouble(txtT3.Text.Replace(',', '.'), CultureInfo.InvariantCulture);

                //Déclaration des variables pour stocker ce que l'élève a déjà payé
                double t11, t22, t33;
                t11 = Convert.ToDouble(txtT11.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                t22 = Convert.ToDouble(txtT22.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                t33 = Convert.ToDouble(txtT33.Text.Replace(',', '.'), CultureInfo.InvariantCulture);

                //Déclaration des variables pour le calcul (Resultat)
                double ResT1, ResT2, ResT3, somme, diff, diff2, remb, b;
                b = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                somme = t11 + t22 + t33 + b;

                if (somme > t1)
                {
                    diff = somme - t1;
                    ResT1 = t1;
                    ResT2 = 0;
                    ResT3 = 0;
                    txtT11.Text = ResT1.ToString(CultureInfo.InvariantCulture);
                    txtT22.Text = ResT2.ToString(CultureInfo.InvariantCulture);
                    txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);

                    if (diff > t2)
                    {
                        diff2 = diff - t2;
                        ResT2 = t2;
                        ResT3 = 0;
                        txtT22.Text = ResT2.ToString(CultureInfo.InvariantCulture);
                        txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);
                        if (diff2 > t3)
                        {
                            remb = diff2 - t3;
                            ResT3 = t3;
                            txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);
                            string command = ("UPDATE situation_paye SET tranche1='" + ResT1.ToString(CultureInfo.InvariantCulture) + "',tranche2='" + ResT2.ToString(CultureInfo.InvariantCulture) + "',tranche3='" + ResT3.ToString(CultureInfo.InvariantCulture) + "' WHERE idFrais='" + txtIdFrais.Text + "' and matricule='" + txtMatricule.Text + "' and anneeScolaire='" + txtIdAnnee.Text + "' and idEcole='" + txtIdEcole.Text + "'");
                            MySqlCommand cmde = new MySqlCommand(command, con);
                            cmde.ExecuteNonQuery();
                        }
                        else
                        {
                            ResT3 = diff2;
                            txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);
                            string command = ("UPDATE situation_paye SET tranche1='" + ResT1.ToString(CultureInfo.InvariantCulture) + "',tranche2='" + ResT2.ToString(CultureInfo.InvariantCulture) + "',tranche3='" + ResT3.ToString(CultureInfo.InvariantCulture) + "' WHERE idFrais='" + txtIdFrais.Text + "' and matricule='" + txtMatricule.Text + "' and anneeScolaire='" + txtIdAnnee.Text + "' and idEcole='" + txtIdEcole.Text + "'");
                            MySqlCommand cmde = new MySqlCommand(command, con);
                            cmde.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        ResT2 = diff;
                        txtT22.Text = ResT2.ToString(CultureInfo.InvariantCulture);
                        string command = ("UPDATE situation_paye SET tranche1='" + ResT1.ToString(CultureInfo.InvariantCulture) + "',tranche2='" + ResT2.ToString(CultureInfo.InvariantCulture) + "',tranche3='" + ResT3.ToString(CultureInfo.InvariantCulture) + "' WHERE idFrais='" + txtIdFrais.Text + "' and matricule='" + txtMatricule.Text + "' and anneeScolaire='" + txtIdAnnee.Text + "' and idEcole='" + txtIdEcole.Text + "'");
                        MySqlCommand cmde = new MySqlCommand(command, con);
                        cmde.ExecuteNonQuery();
                    }
                }
                else
                {
                    ResT1 = somme;
                    ResT2 = 0;
                    ResT3 = 0;
                    txtT11.Text = ResT1.ToString(CultureInfo.InvariantCulture);
                    txtT22.Text = ResT2.ToString(CultureInfo.InvariantCulture);
                    txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);
                    string command = ("UPDATE situation_paye SET tranche1='" + ResT1.ToString(CultureInfo.InvariantCulture) + "',tranche2='" + ResT2.ToString(CultureInfo.InvariantCulture) + "',tranche3='" + ResT3.ToString(CultureInfo.InvariantCulture) + "' WHERE idFrais='" + txtIdFrais.Text + "' and matricule='" + txtMatricule.Text + "' and anneeScolaire='" + txtIdAnnee.Text + "' and idEcole='" + txtIdEcole.Text + "'");
                    MySqlCommand cmde = new MySqlCommand(command, con);
                    cmde.ExecuteNonQuery();
                }
                
            }
            catch
            {

            }

        }
        public void ExecuterSituation()
        {
            if (txtT11.Text == "0")
            {
                EnregistrerDansSituationEleve();
            }
            else
            {
                MettreAJoursSituation();
            }
        }
        public void MettreAjourComptePayementEleve()
        {
            try
            {
                MySqlConnection con = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                con.Open();

                //Mettre à jour le compte eleve, ce code permettra au préfet d'avoir une idée sur l'évolution des payements des élève dune maniere mensuelle
                if (txtFrais.SelectedValue == "Frais scolaires")
                {
                    double sept, octo, nov, dec, jan, fev, mars, avr, mai, juin, diff, mont;
                    double t11, t22, t33, Apayer;
                    t11 = Convert.ToDouble(txtT1.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    t22 = Convert.ToDouble(txtT2.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    t33 = Convert.ToDouble(txtT3.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    Apayer = t11 + t22 + t33;

                    double t1, t2, t3, Paye,a;
                    a = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    t1 = Convert.ToDouble(txtT11.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    t2 = Convert.ToDouble(txtT22.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    t3 = Convert.ToDouble(txtT33.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    Paye = t1 + t2 + t3 + a;

                    if (Paye > Apayer)
                    {
                        //Affichage du message d'erreur
                        success.Visible = false;
                        error.Visible = true;
                        error.Style.Add("display", "block");
                    }
                    else
                    {
                        //initialisation du compte de l'élève
                        string cmd8AA = ("UPDATE compte_eleve SET septembre= '0',octobre= '0',novembre= '0',decembre= '0',janvier= '0',fevrier= '0',mars= '0',avril= '0',mai= '0',juin= '0' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                        MySqlCommand cmde18AAA = new MySqlCommand(cmd8AA, con);
                        cmde18AAA.ExecuteNonQuery();

                        //Repartition encore
                        mont = Apayer / 10;
                        if (Paye > mont || Paye == mont)
                        {
                            sept = mont;
                            diff = Paye - mont;
                            if (diff > mont || diff == mont)
                            {
                                octo = mont;
                                diff = diff - mont;
                                if (diff > mont || diff == mont)
                                {
                                    nov = mont;
                                    diff = diff - mont;
                                    if (diff > mont || diff == mont)
                                    {
                                        dec = mont;
                                        diff = diff - mont;
                                        if (diff > mont || diff == mont)
                                        {
                                            jan = mont;
                                            diff = diff - mont;
                                            if (diff > mont || diff == mont)
                                            {
                                                fev = mont;
                                                diff = diff - mont;
                                                if (diff > mont || diff == mont)
                                                {
                                                    mars = mont;
                                                    diff = diff - mont;
                                                    if (diff > mont || diff == mont)
                                                    {
                                                        avr = mont;
                                                        diff = diff - mont;
                                                        if (diff > mont || diff == mont)
                                                        {
                                                            mai = mont;
                                                            diff = diff - mont;
                                                            if (diff > mont || diff == mont)
                                                            {
                                                                juin = mont;
                                                                diff = diff - mont;
                                                                string cmd9 = ("UPDATE compte_eleve SET septembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',octobre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',novembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',decembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',janvier= '" + mont.ToString(CultureInfo.InvariantCulture) + "',fevrier= '" + mont.ToString(CultureInfo.InvariantCulture) + "',mars= '" + mont.ToString(CultureInfo.InvariantCulture) + "',avril= '" + mont.ToString(CultureInfo.InvariantCulture) + "',mai= '" + mont.ToString(CultureInfo.InvariantCulture) + "',juin= '" + mont.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                                                                MySqlCommand cmde19 = new MySqlCommand(cmd9, con);
                                                                cmde19.ExecuteNonQuery();

                                                                if (diff > 0)
                                                                {
                                                                    //MessageBox.Show("Vous allez rembourser à l'élève une somme de " + diff.ToString() + " " + txtUnite.Text);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                mai = mont;
                                                                juin = diff;
                                                                string cmd8 = ("UPDATE compte_eleve SET septembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',octobre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',novembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',decembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',janvier= '" + mont.ToString(CultureInfo.InvariantCulture) + "',fevrier= '" + mont.ToString(CultureInfo.InvariantCulture) + "',mars= '" + mont.ToString(CultureInfo.InvariantCulture) + "',avril= '" + mont.ToString(CultureInfo.InvariantCulture) + "',mai= '" + mont.ToString(CultureInfo.InvariantCulture) + "',juin= '" + juin.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                                                                MySqlCommand cmde18 = new MySqlCommand(cmd8, con);
                                                                cmde18.ExecuteNonQuery();
                                                            }

                                                        }
                                                        else
                                                        {
                                                            avr = mont;
                                                            mai = diff;
                                                            string cmd7 = ("UPDATE compte_eleve SET septembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',octobre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',novembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',decembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',janvier= '" + mont.ToString(CultureInfo.InvariantCulture) + "',fevrier= '" + mont.ToString(CultureInfo.InvariantCulture) + "',mars= '" + mont.ToString(CultureInfo.InvariantCulture) + "',avril= '" + avr.ToString(CultureInfo.InvariantCulture) + "',mai= '" + mai.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                                                            MySqlCommand cmde17 = new MySqlCommand(cmd7, con);
                                                            cmde17.ExecuteNonQuery();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mars = mont;
                                                        avr = diff;
                                                        string cmd6 = ("UPDATE compte_eleve SET septembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',octobre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',novembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',decembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',janvier= '" + mont.ToString(CultureInfo.InvariantCulture) + "',fevrier= '" + mont.ToString(CultureInfo.InvariantCulture) + "',mars= '" + mars.ToString(CultureInfo.InvariantCulture) + "',avril= '" + avr.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                                                        MySqlCommand cmde16 = new MySqlCommand(cmd6, con);
                                                        cmde16.ExecuteNonQuery();
                                                    }
                                                }
                                                else
                                                {
                                                    fev = mont;
                                                    mars = diff;
                                                    string cmd5 = ("UPDATE compte_eleve SET septembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',octobre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',novembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',decembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',janvier= '" + mont.ToString(CultureInfo.InvariantCulture) + "',fevrier= '" + fev.ToString(CultureInfo.InvariantCulture) + "',mars= '" + mars.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                                                    MySqlCommand cmde15 = new MySqlCommand(cmd5, con);
                                                    cmde15.ExecuteNonQuery();
                                                }

                                            }
                                            else
                                            {
                                                jan = mont;
                                                fev = diff;
                                                string cmd4 = ("UPDATE compte_eleve SET septembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',octobre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',novembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',decembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',janvier= '" + jan.ToString(CultureInfo.InvariantCulture) + "',fevrier= '" + fev.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                                                MySqlCommand cmde14 = new MySqlCommand(cmd4, con);
                                                cmde14.ExecuteNonQuery();
                                            }
                                        }
                                        else
                                        {
                                            dec = mont;
                                            jan = diff;
                                            string cmd3 = ("UPDATE compte_eleve SET septembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',octobre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',novembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',decembre= '" + dec.ToString(CultureInfo.InvariantCulture) + "',janvier= '" + jan.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                                            MySqlCommand cmde13 = new MySqlCommand(cmd3, con);
                                            cmde13.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        nov = mont;
                                        dec = diff;
                                        string cmd2 = ("UPDATE compte_eleve SET septembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',octobre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',novembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',decembre= '" + dec.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                                        MySqlCommand cmde12 = new MySqlCommand(cmd2, con);
                                        cmde12.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    octo = mont;
                                    nov = diff;
                                    string cmd1 = ("UPDATE compte_eleve SET septembre= '" + mont.ToString(CultureInfo.InvariantCulture) + "',octobre= '" + octo.ToString(CultureInfo.InvariantCulture) + "',novembre= '" + nov.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                                    MySqlCommand cmde11 = new MySqlCommand(cmd1, con);
                                    cmde11.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                sept = mont;
                                octo = diff;
                                string cmdA = ("UPDATE compte_eleve SET septembre= '" + sept.ToString(CultureInfo.InvariantCulture) + "',octobre='" + octo.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                                MySqlCommand cmde1A = new MySqlCommand(cmdA, con);
                                cmde1A.ExecuteNonQuery();
                            }

                        }
                        else
                        {
                            sept = Paye;
                            string cmdB = ("UPDATE compte_eleve SET septembre='" + sept.ToString(CultureInfo.InvariantCulture) + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                            MySqlCommand cmde1 = new MySqlCommand(cmdB, con);
                            cmde1.ExecuteNonQuery();
                        }
                    }
                    con.Close();
                }
                else
                {

                }
            }
            catch
            {

            }
        }
        public void SituationCaisse()
        {
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("", con);
            MySqlCommand cmde = con.CreateCommand();
            cmde.CommandType = CommandType.Text;
            cmd.CommandText = "select *from t_caisse WHERE libelle='" + txtUnite.Text + "' AND idEcole=+'" + txtIdEcole.Text + "'";
            MySqlDataReader dr = cmd.ExecuteReader();

            txtDispo.Text = "0";
            txtEntree.Text = "0";
            while (dr.Read())
            {
                txtDispo.Text = dr["Solde"].ToString();
                txtEntree.Text = dr["Entree"].ToString();
                txtSortie.Text = dr["Sortie"].ToString();
            }

            con.Close();
        }
        public void SauvegarderRecuPayement()
        {
            string dateRecu = DateTime.Today.Date.ToShortDateString();
            con.Open();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into recu_payement values('" + txtIdRecu.Text + "','" + dateRecu.ToString() + "','" + txtMatricule.Text + "','" + txtIdEcole.Text + "','" + txtFrais.SelectedValue + "','" + txtUnite.Text + "','" + txtIdAnnee.Text + "','" + txtT1.Text + "','" + txtT2.Text + "','" + txtT3.Text + "','" + txtT11.Text + "','" + txtT22.Text + "','" + txtT33.Text + "','" + txtReste.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
        }
        public void ImprimerFactureEleve()
        {
            // Récupération des valeurs des TextBox
            string nom = txtNomEleve.Text;
            string matricule= txtDernierOperation.Text+"-"+txtMatricule.Text;
            string classe = txtClasse.Text +"-"+ txtOption.Text;
            string dateRecu = DateTime.Today.Date.ToShortDateString();
            string frais = txtFrais.SelectedValue;
            string unite1 = txtUnite.Text;
            string unite2 = txtUnite.Text;
            string prevu = txtT1.Text + ", Tr2=" + txtT2.Text + ", Tr3=" + txtT3.Text;
            string apayer = txtT11.Text + ", Tr2=" + txtT22.Text + ", Tr3=" + txtT33.Text;
            string reste = txtReste.Text;
            string login = txtLogin.Text;
            if (txtMontantVenuAvec.Text == "0")
            {
                string montant = txtmontant.Text + " " + txtUnite.Text;
                // Enregistrement du script pour définir les données de la facture
                ScriptManager.RegisterStartupScript(this, GetType(), "setData", $"setFactureData('{matricule}', '{nom}','{classe}', '{dateRecu}', '{montant}', '{frais}', '{unite1}', '{prevu}', '{unite2}', '{apayer}', '{reste}', '{login}'); imprimerFacture();", true);
            }
            else
            {
                if (txtUnite.Text == "USD")
                {
                    string montant = txtMontantVenuAvec.Text + " CDF";
                    // Enregistrement du script pour définir les données de la facture
                    ScriptManager.RegisterStartupScript(this, GetType(), "setData", $"setFactureData('{matricule}', '{nom}','{classe}', '{dateRecu}', '{montant}', '{frais}', '{unite1}', '{prevu}', '{unite2}', '{apayer}', '{reste}', '{login}'); imprimerFacture();", true);
                }
                if (txtUnite.Text == "CDF")
                {
                    string montant = txtMontantVenuAvec.Text + " USD";
                    // Enregistrement du script pour définir les données de la facture
                    ScriptManager.RegisterStartupScript(this, GetType(), "setData", $"setFactureData('{matricule}', '{nom}','{classe}', '{dateRecu}', '{montant}', '{frais}', '{unite1}', '{prevu}', '{unite2}', '{apayer}', '{reste}', '{login}'); imprimerFacture();", true);
                }
            }
            
        }
        public void ActualiserLaCaisseEnEntree()
        {
            //Actualiser la caisse
            //L'ajout de ces 2 lignes et de ces bibliothèques font l'utilisation du point au décimal
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            double b, sort, Disp, Entr;
            MySqlConnection conx1 = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
            conx1.Open();
            if (txtMontantVenuAvec.Text == "0")
            {

                //Actualisation de la caisse s'il n'y a pas eu de conversion de l'argent de l'élève
                b = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                sort = Convert.ToDouble(txtSortie.Text.Replace(',', '.'), CultureInfo.InvariantCulture) - b;
                Entr = Convert.ToDouble(txtEntree.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
                Disp = Convert.ToDouble(txtDispo.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;

                string command = ("UPDATE t_caisse SET Entree ='" + Entr + "', Solde ='" + Disp + "' WHERE libelle='" + txtUnite.Text + "' AND idEcole=+'" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, conx1);
                cmde.ExecuteNonQuery();
            }
            else
            {
                //Actualisation de la caisse s'il y a eu de conversion de l'argent de l'élève
                b = Convert.ToDouble(txtMontantVenuAvec.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                sort = Convert.ToDouble(txtSortie.Text.Replace(',', '.'), CultureInfo.InvariantCulture) - b;
                Entr = Convert.ToDouble(txtEntree.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
                Disp = Convert.ToDouble(txtDispo.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
                if (txtUnite.Text == "USD")
                {
                    string command = ("UPDATE t_caisse SET Entree ='" + Entr + "', Solde ='" + Disp + "' WHERE libelle='CDF' AND idEcole=+'" + txtIdEcole.Text + "'");
                    MySqlCommand cmde = new MySqlCommand(command, conx1);
                    cmde.ExecuteNonQuery();
                }
                if (txtUnite.Text == "CDF")
                {
                    string command = ("UPDATE t_caisse SET Entree ='" + Entr + "', Solde ='" + Disp + "' WHERE libelle='USD' AND idEcole=+'" + txtIdEcole.Text + "'");
                    MySqlCommand cmde = new MySqlCommand(command, conx1);
                    cmde.ExecuteNonQuery();
                }
            }
            
        }
        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conx = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                conx.Open();
                string TakeDate = DateTime.Today.ToShortDateString();
                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                if (txtMontantVenuAvec.Text == "0")
                {
                    //Enregistrement si on n'a pas converti le montant que l'élève est venu avec à la caisse
                    string cmd = "insert into t_payement_frais values(default,'" + txtMatricule.Text + "','" + txtIdFrais.Text + "','" + txtmontant.Text.Replace(',', '.') + "','" + txtUnite.Text + "','" + TakeDate.ToString() + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "','" + txtLogin.Text + "','" + txtIdRecu.Text + "')";
                    MySqlCommand commande = new MySqlCommand(cmd, conx);

                    double a = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    double t11, t22, t33, APayer;
                    t11 = Convert.ToDouble(txtT1.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    t22 = Convert.ToDouble(txtT2.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    t33 = Convert.ToDouble(txtT3.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    APayer = t11 + t22 + t33;

                    double t1, t2, t3, Payer;
                    t1 = Convert.ToDouble(txtT11.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    t2 = Convert.ToDouble(txtT22.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    t3 = Convert.ToDouble(txtT33.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    Payer = t1 + t2 + t3 + a;

                    if (a > 0 && a <= APayer)
                    {

                        if (Payer <= APayer)
                        {
                            //Pour mettre à jour le compte de l'élève 
                            MettreAjourComptePayementEleve();

                            commande.ExecuteNonQuery();
                            //====Actualiser la situation de l'élève ou en créer une pour un frais======
                            ExecuterSituation();
                            SituationCaisse();
                            ActualiserLaCaisseEnEntree();

                            TrouverFraisDejaPaye();
                            SauvegarderRecuPayement();
                            TrouverDerniereOperation();
                            ImprimerFactureEleve();

                            ViderChamps();

                            SituationCaisse();
                            btnAddStructure.Visible = false;

                            //Affichage du message succès
                            success.Visible = true;
                            error.Visible = false;
                            success.Style.Add("display", "block");
                            conx.Close();
                            //Response.Redirect("AdminFinPayeFraisExecute.aspx");


                        }
                        else
                        {
                            //Affichage du message d'erreur
                            success.Visible = false;
                            error.Visible = true;
                            error.Style.Add("display", "block");
                        }

                    }
                }
                else
                {
                    //Enregistrement si on a converti le montant que l'élève est venu avec à la caisse
                    if (txtUnite.Text == "USD")
                    {
                        string cmd = "insert into t_payement_frais values(default,'" + txtMatricule.Text + "','" + txtIdFrais.Text + "','" + txtMontantVenuAvec.Text.Replace(',', '.') + "','CDF','" + TakeDate.ToString() + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "','" + txtLogin.Text + "','" + txtIdRecu.Text + "')";
                        MySqlCommand commande = new MySqlCommand(cmd, conx);

                        double a = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                        double t11, t22, t33, APayer;
                        t11 = Convert.ToDouble(txtT1.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                        t22 = Convert.ToDouble(txtT2.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                        t33 = Convert.ToDouble(txtT3.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                        APayer = t11 + t22 + t33;

                        double t1, t2, t3, Payer;
                        t1 = Convert.ToDouble(txtT11.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                        t2 = Convert.ToDouble(txtT22.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                        t3 = Convert.ToDouble(txtT33.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                        Payer = t1 + t2 + t3 + a;

                        if (a > 0 && a <= APayer)
                        {

                            if (Payer <= APayer)
                            {
                                //Pour mettre à jour le compte de l'élève 
                                MettreAjourComptePayementEleve();

                                commande.ExecuteNonQuery();
                                //====Actualiser la situation de l'élève ou en créer une pour un frais======
                                ExecuterSituation();
                                SituationCaisse();
                                ActualiserLaCaisseEnEntree();

                                TrouverFraisDejaPaye();
                                SauvegarderRecuPayement();
                                TrouverDerniereOperation();
                                ImprimerFactureEleve();
                                ViderChamps();

                                SituationCaisse();
                                btnAddStructure.Visible = false;

                                //Affichage du message succès
                                success.Visible = true;
                                error.Visible = false;
                                success.Style.Add("display", "block");
                                conx.Close();
                                //Response.Redirect("AdminFinPayeFraisExecute.aspx");


                            }
                            else
                            {
                                //Affichage du message d'erreur
                                success.Visible = false;
                                error.Visible = true;
                                error.Style.Add("display", "block");
                            }

                        }
                    }
                    if (txtUnite.Text == "CDF")
                    {
                        string cmd = "insert into t_payement_frais values(default,'" + txtMatricule.Text + "','" + txtIdFrais.Text + "','" + txtMontantVenuAvec.Text.Replace(',', '.') + "','USD','" + TakeDate.ToString() + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "','" + txtLogin.Text + "','" + txtIdRecu.Text + "')";
                        MySqlCommand commande = new MySqlCommand(cmd, conx);

                        double a = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                        double t11, t22, t33, APayer;
                        t11 = Convert.ToDouble(txtT1.Text, CultureInfo.InvariantCulture);
                        t22 = Convert.ToDouble(txtT2.Text, CultureInfo.InvariantCulture);
                        t33 = Convert.ToDouble(txtT3.Text, CultureInfo.InvariantCulture);
                        APayer = t11 + t22 + t33;

                        double t1, t2, t3, Payer;
                        t1 = Convert.ToDouble(txtT11.Text, CultureInfo.InvariantCulture);
                        t2 = Convert.ToDouble(txtT22.Text, CultureInfo.InvariantCulture);
                        t3 = Convert.ToDouble(txtT33.Text, CultureInfo.InvariantCulture);
                        Payer = t1 + t2 + t3 + a;

                        if (a > 0 && a <= APayer)
                        {

                            if (Payer <= APayer)
                            {
                                //Pour mettre à jour le compte de l'élève 
                                MettreAjourComptePayementEleve();

                                commande.ExecuteNonQuery();
                                //====Actualiser la situation de l'élève ou en créer une pour un frais======
                                ExecuterSituation();
                                SituationCaisse();
                                ActualiserLaCaisseEnEntree();

                                TrouverFraisDejaPaye();
                                SauvegarderRecuPayement();
                                TrouverDerniereOperation();
                                ImprimerFactureEleve();

                                ViderChamps();

                                SituationCaisse();
                                btnAddStructure.Visible = false;

                                //Affichage du message succès
                                success.Visible = true;
                                error.Visible = false;
                                success.Style.Add("display", "block");
                                conx.Close();
                                //Response.Redirect("AdminFinPayeFraisExecute.aspx");


                            }
                            else
                            {
                                //Affichage du message d'erreur
                                success.Visible = false;
                                error.Visible = true;
                                error.Style.Add("display", "block");
                            }

                        }
                    }

                }

            }
            catch
            {
                txtMessage.Visible = true;
                txtMessage.Text = "Quelque chose a mal tourné, vérifiez si vous avez saisi nombre valide, si c'est un décimal, n'oubliez pas d'utiliser un point-virgule (,)";
            }

        }

        protected void txtFrais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViderChamps();
            TrouverIdFrais();
            TrouverFraisDejaPaye();
            SituationCaisse();
            TrouverDerniereOperation();
            success.Visible = false;
            error.Visible = false;
        }

        protected void txtmontant_TextChanged(object sender, EventArgs e)
        {
            success.Visible = false;
            error.Visible = false;
            txtConvertir.Visible = false;
        }

        protected void btnConvertir_Click(object sender, EventArgs e)
        {
            if (txtmontant.Text=="")
            {
                txtConvertir.Visible = true;
            }
            else
            {
                lblEquivalence.Visible = true;
                lblTaux.Visible = true;
                txtEquivalenceMontant.Visible = true;
                txtTaux.Visible = true;
                btnAddStructure.Visible = false;
                btnValiderAvecConversion.Visible = true;

                txtMontantVenuAvec.Text = txtmontant.Text;
                lblEquivalence.Text = txtMontantVenuAvec.Text + " Equivalent en " +txtUnite.Text + " à ";
                if (txtUnite.Text=="USD")
                {
                    txtTaux.Text = "3200";
                    double a, b;
                    a= Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    b = a/ 3200;
                    txtEquivalenceMontant.Text =b.ToString();

                }
                if (txtUnite.Text == "CDF")
                {
                    txtTaux.Text = "3200";
                    double a, b;
                    a = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                    b = a*3200;
                    txtEquivalenceMontant.Text = b.ToString();
                }

            }
        }

        protected void btnValiderAvecConversion_Click(object sender, EventArgs e)
        {
            btnAddStructure.Visible = false;
            if (txtUnite.Text == "USD")
            {
                double a, montaConverti, taux;
                taux = Convert.ToDouble(txtTaux.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                a = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                montaConverti = a / taux;
                txtEquivalenceMontant.Text = montaConverti.ToString();
                txtmontant.Text = montaConverti.ToString();

                //Afficher les cashés
                lblEquivalence.Visible = false;
                lblTaux.Visible = false;
                txtEquivalenceMontant.Visible = false;
                txtTaux.Visible = false;
                btnAddStructure.Visible = true;
                btnValiderAvecConversion.Visible = false;

            }
            if (txtUnite.Text == "CDF")
            {
                double a, montaConverti, taux;
                taux = Convert.ToDouble(txtTaux.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                a = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                montaConverti = a * taux;
                txtEquivalenceMontant.Text = montaConverti.ToString();
                txtmontant.Text = montaConverti.ToString();

                //Afficher les cashés
                lblEquivalence.Visible = false;
                lblTaux.Visible = false;
                txtEquivalenceMontant.Visible = false;
                txtTaux.Visible = false;
                btnAddStructure.Visible = true;
                btnValiderAvecConversion.Visible = false;
            }
        }

        protected void txtTaux_TextChanged(object sender, EventArgs e)
        {
            if (txtUnite.Text == "USD")
            {
                double a, b,taux;
                taux= Convert.ToDouble(txtTaux.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                a = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                b = a / taux;
                txtEquivalenceMontant.Text = b.ToString();

            }
            if (txtUnite.Text == "CDF")
            {
                double a, b, taux;
                taux = Convert.ToDouble(txtTaux.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                a = Convert.ToDouble(txtmontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                b = a * taux;
                txtEquivalenceMontant.Text = b.ToString();
            }
        }
    }
}