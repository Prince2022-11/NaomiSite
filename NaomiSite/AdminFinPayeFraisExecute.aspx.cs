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

namespace NaomiSite
{
    public partial class AdminFinPayeFraisExecute : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
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
                        TrouverEleve();
                        TrouverEcole();
                        TrouverSection();
                        TrouverClasser();
                        TrouverFrais();
                        TrouverIdFrais();
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
                txtEcole.Text = dr["nomEcole"].ToString();
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
                txtIdOption.Text = dr["optionEtude"].ToString();
                txtIdEcole.Text = dr["idEcole"].ToString();
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
            cmdB.CommandText = ("SELECT nomSection from section WHERE idEcole='" + txtIdEcole.Text + "'");
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
            cmdB.CommandText = ("SELECT *from t_classe WHERE id='" + txtIdClasse.Text + "'");
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
        public void TrouverFraisDejaPaye()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("", con);
                MySqlCommand cmde = con.CreateCommand();
                cmde.CommandType = CommandType.Text;
                cmd.CommandText = "select *from situation_paye where idFrais='" + txtIdFrais.Text + "' and matricule='" + txtMatricule.Text + "' and anneeScolaire='" + txtIdAnnee.Text + "'";
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtT11.Text = "0";
                    txtT22.Text = "0";
                    txtT33.Text = "0";
                    txtT11.Text = dr["tranche1"].ToString();
                    txtT22.Text = dr["tranche2"].ToString();
                    txtT33.Text = dr["tranche3"].ToString();
                }

                con.Close();
            }
            catch
            {

            }
        }
        public void EnregistrerDansSituationEleve()
        {
            try
            {
                //declaration des variables pour prendre ce qui est prévue dans un frais
                double t1, t2, t3;
                t1 = Convert.ToDouble(txtT1.Text, CultureInfo.InvariantCulture);
                t2 = Convert.ToDouble(txtT2.Text, CultureInfo.InvariantCulture);
                t3 = Convert.ToDouble(txtT3.Text, CultureInfo.InvariantCulture);

                //Déclaration des variables pour stocker ce que l'élève a déjà payé
                double t11, t22, t33;
                t11 = Convert.ToDouble(txtT11.Text, CultureInfo.InvariantCulture);
                t22 = Convert.ToDouble(txtT22.Text, CultureInfo.InvariantCulture);
                t33 = Convert.ToDouble(txtT33.Text, CultureInfo.InvariantCulture);

                //Déclaration des variables pour le calcul (Resultat)
                double ResT1, ResT2, ResT3, somme, diff, diff2, remb, b;
                b = Convert.ToDouble(txtmontant.Text, CultureInfo.InvariantCulture);
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
                        }
                        else
                        {
                            ResT3 = diff2;
                            txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);
                        }
                    }
                    else
                    {
                        ResT2 = diff;
                        txtT22.Text = ResT2.ToString(CultureInfo.InvariantCulture);
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
                }
                MySqlConnection con = new MySqlConnection("server=localhost;uid=root;database=gespersonnel;password=");
                con.Open(); string cmd = "insert into situation_paye values(default,'" + txtIdFrais.Text + "','" + ResT1.ToString(CultureInfo.InvariantCulture) + "','" + ResT2.ToString(CultureInfo.InvariantCulture) + "','" + ResT3.ToString(CultureInfo.InvariantCulture) + "','" + txtMatricule.Text + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
                MySqlCommand commande = new MySqlCommand(cmd, con);
                commande.ExecuteNonQuery();
                con.Close();
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

                //declaration des variables pour prendre ce qui est prévue dans un frais
                double t1, t2, t3;
                t1 = Convert.ToDouble(txtT1.Text, CultureInfo.InvariantCulture);
                t2 = Convert.ToDouble(txtT2.Text, CultureInfo.InvariantCulture);
                t3 = Convert.ToDouble(txtT3.Text, CultureInfo.InvariantCulture);

                //Déclaration des variables pour stocker ce que l'élève a déjà payé
                double t11, t22, t33;
                t11 = Convert.ToDouble(txtT11.Text, CultureInfo.InvariantCulture);
                t22 = Convert.ToDouble(txtT22.Text, CultureInfo.InvariantCulture);
                t33 = Convert.ToDouble(txtT33.Text, CultureInfo.InvariantCulture);

                //Déclaration des variables pour le calcul (Resultat)
                double ResT1, ResT2, ResT3, somme, diff, diff2, remb, b;
                b = Convert.ToDouble(txtmontant.Text, CultureInfo.InvariantCulture);
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
                        }
                        else
                        {
                            ResT3 = diff2;
                            txtT33.Text = ResT3.ToString(CultureInfo.InvariantCulture);
                        }
                    }
                    else
                    {
                        ResT2 = diff;
                        txtT22.Text = ResT2.ToString(CultureInfo.InvariantCulture);
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
                }

                string command = ("UPDATE situation_paye SET tranche1='" + ResT1.ToString(CultureInfo.InvariantCulture) + "',tranche2='" + ResT2.ToString(CultureInfo.InvariantCulture) + "',tranche3='" + ResT3.ToString(CultureInfo.InvariantCulture) + "' WHERE idFrais='" + txtIdFrais.Text + "' and matricule='" + txtMatricule.Text + "' and anneeScolaire='" + txtIdAnnee.Text + "' and idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
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
                MySqlConnection con = new MySqlConnection("server=localhost;uid=root;database=gespersonnel;password=");
                con.Open();

                //Mettre à jour le compte eleve, ce code permettra au préfet d'avoir une idée sur l'évolution des payements des élève dune maniere mensuelle
                if (txtFrais.SelectedValue == "Frais scolaires")
                {
                    double sept, octo, nov, dec, jan, fev, mars, avr, mai, juin, diff, mont;
                    double Paye, Apayer;

                    Apayer = Convert.ToDouble(txtT1.Text, CultureInfo.InvariantCulture) + Convert.ToDouble(txtT2.Text, CultureInfo.InvariantCulture) + Convert.ToDouble(txtT3.Text, CultureInfo.InvariantCulture);
                    Paye = Convert.ToDouble(txtT11.Text, CultureInfo.InvariantCulture) + Convert.ToDouble(txtT22.Text, CultureInfo.InvariantCulture) + Convert.ToDouble(txtT33.Text, CultureInfo.InvariantCulture) + Convert.ToDouble(txtmontant.Text, CultureInfo.InvariantCulture);

                    if (Paye > Apayer)
                    {
                        //MessageBox.Show("Vérifier le montant saisi","",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conx = new MySqlConnection("server=localhost;uid=root;database=gespersonnel;password=");
                conx.Open();
                string TakeDate = DateTime.Today.ToShortDateString();
                string cmd = "insert into t_payement_frais values(default,'" + txtMatricule.Text + "','" + txtIdFrais.Text + "','" + txtmontant.Text + "','" + txtUnite.Text + "','" + TakeDate.ToString() + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "','" + txtLogin.Text + "')";
                MySqlCommand commande = new MySqlCommand(cmd, conx);

                double a = Convert.ToDouble(txtmontant.Text, CultureInfo.InvariantCulture);
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

                bool canPay = false;
                if (a > 0 && a <= APayer)
                {
                    
                    if (Payer <= APayer)
                    {
                        //Pour mettre à jour le compte de l'élève 
                        MettreAjourComptePayementEleve();

                        commande.ExecuteNonQuery();
                        //====Actualiser la situation de l'élève ou en créer une pour un frais======
                        ExecuterSituation();

                        //Actualiser la caisse
                        double b = Convert.ToDouble(txtmontant.Text, CultureInfo.InvariantCulture);

                        MySqlConnection conx1 = new MySqlConnection("server=localhost;uid=root;database=gespersonnel;password=");
                        conx1.Open();
                        string command = ("UPDATE t_caisse SET Entree =Entree+'" + b + "', Solde =Solde+'" + b + "'' WHERE libelle='" + txtUnite.Text + "' AND idEcole=+'" + txtIdEcole.Text + "'");
                        MySqlCommand cmde = new MySqlCommand(command, conx1);
                        cmde.ExecuteNonQuery();
                        //EnregistrementRecu();

                        //Succès opération
                        bool isSaved = true;
                        if (isSaved)
                        {
                            string message = "Opération Sauvegardée";
                            string script = $"showSuccess('{message}');";
                            ClientScript.RegisterStartupScript(this.GetType(), "showSuccess", script, true);
                        }
                        conx1.Close();
                        conx.Close();
                    }
                    else
                    {
                        if (!canPay)
                        {
                            string message = "Cet(te) élève ne peut pas payer tout ce montant ou soit il (elle) est déjà en ordre avec ce frais pour l'année indiquée.";
                            string script = $"showError('{message}');";
                            ClientScript.RegisterStartupScript(this.GetType(), "showError", script, true);
                        }
                    }

                }
            }
            catch
            {

            }

        }

        protected void txtFrais_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdFrais();
            TrouverFraisDejaPaye();
        }
    }
}