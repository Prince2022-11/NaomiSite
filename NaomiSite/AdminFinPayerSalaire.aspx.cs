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
    public partial class AdminFinPayerSalaire : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gestion_naomi");
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            //id = Convert.ToInt32(Request.QueryString["id"].ToString());
            //Vérification de la connexion de la varibale session
            if (Session["autorisation"] != null && (bool)Session["autorisation"] == true)
            {

                if (!IsPostBack) //Pour que la page ait le pouvoir de modifier le rendue
                {
                    txtLogin.Text = Session["login"].ToString();

                    // Vérifier l'admin connecté et l'agent à octroyer l'avance
                    con.Open();
                    id = Convert.ToString(Request.QueryString["id"].ToString());
                    txtMatricule.Text = id.ToString();

                    MySqlCommand cmd = new MySqlCommand("", con);
                    MySqlCommand cmde = con.CreateCommand();
                    cmde.CommandType = CommandType.Text;
                    cmd.CommandText = ("select * from utilisateur WHERE login='" + txtLogin.Text + "'");
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        txtRole.Text = dr["service"].ToString();
                        txtIdUser.Text = dr["id"].ToString();

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
                        TrouverAgent();
                        TrouverEcole();
                        //SituationCaisse();
                        //TrouverAvance();
                        //TrouverHeurePrestees();
                        //TrouverDerniereOperation();
                        //CalculerPayement();
                        //TrouverCompteAgent();

                        txtRembourse.Text = "0";
                        txtValeurHeure.Text = "3,5";
                        txtMontantPrimMat.Text = "0";
                        SituationCaisse();
                        TrouverDerniereOperation();
                        TrouverAvance();
                        TrouverHeurePrestees();
                        CalculerPayement();
                        TrouverCompteAgent();
                        success.Visible = false;
                        error.Visible = false;
                        double SalDejaRecu;

                        SalDejaRecu = Convert.ToDouble(txtSalDejaRecu.Text.Replace('.', '.'), CultureInfo.InvariantCulture);

                        btnAddStructure.Visible = true;
                        txtMessage.Visible = false;
                        txtMessage.Text = "Verifiez bien vos champs ou soit le disponible en caisse est insuffisant pour couvrir ce payement";
                        if (SalDejaRecu > 0)
                        {
                            btnAddStructure.Visible = false;
                            txtMessage.Visible = true;
                            txtMessage.Text = "Cet agent a déjà était payé au mois sélectionné, impossible de faire 2 payements pour un même mois, par contre vous pouvez l'octroyer une avance sur salaire si vous le souhaitez bien";
                        }
                        else
                        {
                            btnAddStructure.Visible = true;
                        }
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
        public void TrouverAgent()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_agent WHERE matricule='" + txtMatricule.Text + "' ");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdEcole.Text = dr["idEcole"].ToString();
                txtNomEleve.Text = dr["nom"].ToString() + " " + dr["prenom"].ToString();
            }
            con.Close();
        }
        public void TrouverHeurePrestees()
        {
            try
            {
                con.Open();
                MySqlCommand cmdB = con.CreateCommand();
                cmdB.CommandType = CommandType.Text;
                cmdB.CommandText = ("SELECT SUM(nbHenseigne) as nbHEnseignee from t_presence WHERE matricule='" + txtMatricule.Text + "' AND moisEnseigne='" + txtMois.SelectedValue + "' AND annee='" + txtIdAnnee.Text + "' AND nbHenseigne<>'-' AND nbHenseigne<>'En cours'");
                MySqlDataReader dr = cmdB.ExecuteReader();
                txtNbrHeure.Text = "0";
                txtNbrHeureSemaine.Text = "0";

                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                while (dr.Read())
                {
                    txtNbrHeure.Text = dr["nbHEnseignee"].ToString();
                    double HS = double.Parse(dr["nbHEnseignee"].ToString()) / 4;
                    txtNbrHeureSemaine.Text = HS.ToString();
                }
                con.Close();
            }
            catch { }
        }
        public void TrouverAvance()
        {
            con.Close();
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from compte_agent WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "' ");
            MySqlDataReader dr = cmdB.ExecuteReader();
            txtCompteExitant.Text = "Non";
            while (dr.Read())
            {
                txtAvance.Text = dr["avance"].ToString();
                txtRembourse.Text= dr["avance"].ToString();
                txtCompteExitant.Text = "Oui";
            }
            con.Close();
        }
        public void GererLeCas()
        {
            lblSalBase.Visible = false;
            txtMontantPrimMat.Visible = false;
            Label3.Visible = false;
            txtNbrHeure.Visible = false;
            Label5.Visible = false;
            txtNbrHeureSemaine.Visible = false;
            Label1.Visible = false;
            txtAvance.Visible = false;
            Label4.Visible = false;
            Label7.Visible = false;
            txtValeurHeure.Visible = false;
            lblRembourse.Visible = false;
            txtRembourse.Visible = false;
            lblResteAvance.Visible = false;
            txtResteAvance.Visible = false;
            lblUnite1.Visible = false;
            Label6.Visible = false;
            txtNetRecu.Visible = false;
            lblUnite2.Visible = false;
            btnConvertir.Visible = false;
            txtSalBase.Visible = false;
            Label10.Visible = false;
            Label8.Visible = false;
            ctrlValHeur.Visible = false;

            if (txtIdEcole.Text == "3")
            {
               
                Label3.Visible = true;
                txtNbrHeure.Visible = true;
                Label5.Visible = true;
                txtNbrHeureSemaine.Visible = true;
                Label1.Visible = true;
                txtAvance.Visible = true;
                Label4.Visible = true;
                Label7.Visible = true;
                txtValeurHeure.Visible = true;
                lblRembourse.Visible = true;
                txtRembourse.Visible = true;
                lblResteAvance.Visible = true;
                txtResteAvance.Visible = true;
                lblUnite1.Visible = true;
                Label6.Visible = true;
                txtNetRecu.Visible = true;
                lblUnite2.Visible = true;
                txtSalBase.Visible = true;
                Label10.Visible = true;
                Label8.Visible = true;
                btnConvertir.Visible = true;
                ctrlValHeur.Visible = true;
            }
            else
            {
                lblSalBase.Visible = true;
                txtMontantPrimMat.Visible = true;
                Label1.Visible = true;
                txtAvance.Visible = true;
                Label4.Visible = true;
                lblRembourse.Visible = true;
                txtRembourse.Visible = true;
                lblResteAvance.Visible = true;
                txtResteAvance.Visible = true;
                lblUnite1.Visible = true;
                Label6.Visible = true;
                txtNetRecu.Visible = true;
                lblUnite2.Visible = true;
            }
        }
        public void PrimareEtMaternelle()
        {
            //Actualiser la caisse
            //L'ajout de ces 2 lignes et de ces bibliothèques font l'utilisation du point au décimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            double avance, netRecu, remb, ResteAvance, salBasePrimaireMat;
            avance = Convert.ToDouble(txtAvance.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            remb = Convert.ToDouble(txtRembourse.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            salBasePrimaireMat = Convert.ToDouble(txtMontantPrimMat.Text.Replace(',', '.'), CultureInfo.InvariantCulture);

            ResteAvance = avance - remb;
            netRecu = salBasePrimaireMat - remb;
            txtNetRecu.Text = netRecu.ToString();
            txtResteAvance.Text = ResteAvance.ToString();
        }
        public void Secondaire()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            double avance,salBase, netRecu, valHeure, remb, HeurePreste, ResteAvance;
            valHeure = Convert.ToDouble(txtValeurHeure.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            avance = Convert.ToDouble(txtAvance.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            HeurePreste = Convert.ToDouble(txtNbrHeureSemaine.Text.Replace('.', '.'), CultureInfo.InvariantCulture);
            remb = Convert.ToDouble(txtRembourse.Text.Replace(',', '.'), CultureInfo.InvariantCulture);

            ResteAvance = avance - remb;
            salBase = (valHeure * HeurePreste);
            netRecu = (valHeure * HeurePreste) - remb;
            txtNetRecu.Text = netRecu.ToString();
            txtResteAvance.Text = ResteAvance.ToString();
            txtSalBase.Text = salBase.ToString();
        }
        public void CalculerPayement()
        {
            try
            {
                GererLeCas();
                TrouverCompteAgent();
                if (txtIdEcole.Text=="3")
                {
                    Secondaire();
                }
                else
                {
                    PrimareEtMaternelle();
                }

            }
            catch { }

        }

        public void TrouverDerniereOperation()
        {
            try
            {
                con.Close();
                con.Open();
                MySqlCommand cmdB = con.CreateCommand();
                cmdB.CommandType = CommandType.Text;
                cmdB.CommandText = ("SELECT MAX(id_salaire) as DOperation from t_salaire");
                MySqlDataReader dr = cmdB.ExecuteReader();
                txtDernierOperation.Text = "";
                while (dr.Read())
                {
                    txtDernierOperation.Text = dr["DOperation"].ToString();
                    int NouvelleOperation = int.Parse(dr["DOperation"].ToString()) + 1;
                    txtIdRecu.Text = NouvelleOperation.ToString() + "-" + txtMatricule.Text;
                }
                con.Close();
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
            cmd.CommandText = "select *from t_caisse WHERE libelle='USD' AND idEcole=+'" + txtIdEcole.Text + "'";
            MySqlDataReader dr = cmd.ExecuteReader();

            txtDispo.Text = "0";
            txtSortie.Text = "0";
            while (dr.Read())
            {
                txtDispo.Text = dr["Solde"].ToString();
                txtSortie.Text = dr["Sortie"].ToString();
            }

            con.Close();
        }
        public void TrouverCompteAgent()
        {
            con.Close();
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from compte_agent WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "' ");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtSept.Text = dr["septembre"].ToString();
                txtOcto.Text = dr["octobre"].ToString();
                txtNov.Text = dr["novembre"].ToString();
                txtDec.Text = dr["decembre"].ToString();
                txtJan.Text = dr["janvier"].ToString();
                txtFev.Text = dr["fevrier"].ToString();
                txtMar.Text = dr["mars"].ToString();
                txtAvr.Text = dr["avril"].ToString();
                txtMai.Text = dr["mai"].ToString();
                txtJuin.Text = dr["juin"].ToString();

                txtSalDejaRecu.Text = "0";
                if (txtMois.SelectedValue == "Septembre")
                {
                    txtSalDejaRecu.Text = dr["septembre"].ToString();
                }
                if (txtMois.SelectedValue == "Octobre")
                {
                    txtSalDejaRecu.Text = dr["octobre"].ToString() ;
                }
                if (txtMois.SelectedValue == "Novembre")
                {
                    txtSalDejaRecu.Text = dr["novembre"].ToString();
                }
                if (txtMois.SelectedValue == "Décembre")
                {
                    txtSalDejaRecu.Text = dr["decembre"].ToString();
                }
                if (txtMois.SelectedValue == "Janvier")
                {
                    txtSalDejaRecu.Text = dr["janvier"].ToString();
                }
                if (txtMois.SelectedValue == "Février")
                {
                    txtSalDejaRecu.Text = dr["fevrier"].ToString() ;
                }
                if (txtMois.SelectedValue == "Mars")
                {
                    txtSalDejaRecu.Text = dr["mars"].ToString();
                }
                if (txtMois.SelectedValue == "Avril")
                {
                    txtSalDejaRecu.Text = dr["avril"].ToString() ;
                }
                if (txtMois.SelectedValue == "Mai")
                {
                    txtSalDejaRecu.Text = dr["mai"].ToString() ;
                }
                if (txtMois.SelectedValue == "Juin")
                {
                    txtSalDejaRecu.Text = dr["juin"].ToString();
                }
            }
            con.Close();
        }
        public void ActualiserCompteAgent()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            con.Close();
            con.Open();

            double b,sept, oct,nov,dec,jan,fev,mars,avri,mai,juin,resteAvance ;
            b = Convert.ToDouble(txtNetRecu.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            resteAvance = Convert.ToDouble(txtResteAvance.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            sept = Convert.ToDouble(txtSept.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            oct = Convert.ToDouble(txtOcto.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            nov = Convert.ToDouble(txtNov.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            dec = Convert.ToDouble(txtDec.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            jan = Convert.ToDouble(txtJan.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            fev = Convert.ToDouble(txtFev.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            mars = Convert.ToDouble(txtMar.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            avri = Convert.ToDouble(txtAvr.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            mai = Convert.ToDouble(txtMai.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            juin = Convert.ToDouble(txtJuin.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;

            if (txtMois.SelectedValue == "Septembre")
            {
                string command = ("UPDATE compte_agent SET septembre ='" + sept.ToString(CultureInfo.InvariantCulture) + "',avance ='" + resteAvance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
            }
            if (txtMois.SelectedValue == "Octobre")
            {
                string command = ("UPDATE compte_agent SET octobre ='" + oct.ToString(CultureInfo.InvariantCulture) + "',avance ='" + resteAvance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
            }
            if (txtMois.SelectedValue == "Novembre")
            {
                string command = ("UPDATE compte_agent SET novembre ='" + nov.ToString(CultureInfo.InvariantCulture) + "',avance ='" + resteAvance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
            }
            if (txtMois.SelectedValue == "Décembre")
            {
                string command = ("UPDATE compte_agent SET decembre ='" + dec.ToString(CultureInfo.InvariantCulture) + "',avance ='" + resteAvance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
            }
            if (txtMois.SelectedValue == "Janvier")
            {
                string command = ("UPDATE compte_agent SET janvier ='" + jan.ToString(CultureInfo.InvariantCulture) + "',avance ='" + resteAvance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
            }
            if (txtMois.SelectedValue == "Février")
            {
                string command = ("UPDATE compte_agent SET fevrier ='" + fev.ToString(CultureInfo.InvariantCulture) + "',avance ='" + resteAvance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
            }
            if (txtMois.SelectedValue == "Mars")
            {
                string command = ("UPDATE compte_agent SET mars ='" + mars.ToString(CultureInfo.InvariantCulture) + "',avance ='" + resteAvance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
            }
            if (txtMois.SelectedValue == "Avril")
            {
                string command = ("UPDATE compte_agent SET avril ='" + avri.ToString(CultureInfo.InvariantCulture) + "',avance ='" + resteAvance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
            }
            if (txtMois.SelectedValue == "Mai")
            {
                string command = ("UPDATE compte_agent SET mai ='" + mai.ToString(CultureInfo.InvariantCulture) + "',avance ='" + resteAvance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
            }
            if (txtMois.SelectedValue == "Juin")
            {
                string command = ("UPDATE compte_agent SET juin ='" + juin.ToString(CultureInfo.InvariantCulture) + "',avance ='" + resteAvance.ToString(CultureInfo.InvariantCulture) + "' WHERE Matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "'");
                MySqlCommand cmde = new MySqlCommand(command, con);
                cmde.ExecuteNonQuery();
            }
            con.Close();
        }
        public void CreerCompteAnnuelSiManquantAgent()
        {
            con.Open();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into compte_agent values(default,'" + txtMatricule.Text + "','0','0','0','0','0','0','0','0','0','0','0','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
        }
        public void ActualiserLaCaisseEnSortie()
        {
            //Actualiser la caisse
            //L'ajout de ces 2 lignes et de ces bibliothèques font l'utilisation du point au décimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            double b, sort, Disp;
            MySqlConnection conx1 = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
            conx1.Open();
            //Actualisation de la caisse
            b = Convert.ToDouble(txtNetRecu.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            sort = Convert.ToDouble(txtSortie.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            Disp = Convert.ToDouble(txtDispo.Text.Replace(',', '.'), CultureInfo.InvariantCulture) - b;

            string command = ("UPDATE t_caisse SET Sortie ='" + sort.ToString(CultureInfo.InvariantCulture) + "', Solde ='" + Disp.ToString(CultureInfo.InvariantCulture) + "' WHERE libelle='USD' AND idEcole=+'" + txtIdEcole.Text + "'");
            MySqlCommand cmde = new MySqlCommand(command, conx1);
            cmde.ExecuteNonQuery();
        }
       
        public void ImprimerFacture()
        {
            if (txtIdEcole.Text == "3")
            {
                // Récupération des valeurs des TextBox
                string nom = txtNomEleve.Text;
                string matricule = txtDernierOperation.Text + "-" + txtMatricule.Text;
                string ecole = txtEcole.Text;
                string dateRecu = DateTime.Today.Date.ToShortDateString();
                string nbHeure = txtNbrHeure.Text;
                string Valheure = txtValeurHeure.Text;
                string SalBase = txtSalBase.Text;
                string Avance = txtAvance.Text;
                string Remb = txtRembourse.Text;
                string NetRecu = txtNetRecu.Text;
                string login = txtLogin.Text;
                string reste = txtResteAvance.Text;

                // Enregistrement du script pour définir les données de la facture
                ScriptManager.RegisterStartupScript(this, GetType(), "setData", $"setFactureData('{matricule}', '{nom}', '{ecole}', '{dateRecu}', '{nbHeure}', '{Valheure}', '{SalBase}', '{Avance}', '{Remb}', '{NetRecu}', '{reste}', '{login}'); imprimerFacture();", true);
            }
            else
            {
                // Récupération des valeurs des TextBox
                string nom = txtNomEleve.Text;
                string matricule = txtDernierOperation.Text + "-" + txtMatricule.Text;
                string ecole = txtEcole.Text;
                string dateRecu = DateTime.Today.Date.ToShortDateString();
                string nbHeure = "Tous les jours";
                string Valheure = "-";
                string SalBase = txtMontantPrimMat.Text;
                string Avance = txtAvance.Text;
                string Remb = txtRembourse.Text;
                string NetRecu = txtNetRecu.Text;
                string login = txtLogin.Text;
                string reste = txtResteAvance.Text;

                // Enregistrement du script pour définir les données de la facture
                ScriptManager.RegisterStartupScript(this, GetType(), "setData", $"setFactureData('{matricule}', '{nom}', '{ecole}', '{dateRecu}', '{nbHeure}', '{Valheure}', '{SalBase}', '{Avance}', '{Remb}', '{NetRecu}', '{reste}', '{login}'); imprimerFacture();", true);
            }
          

        }
        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            try
            {
                CalculerPayement();

                double Apayer, Disp, Apayer2,NbH,SalDejaRecu;
                Apayer = Convert.ToDouble(txtNetRecu.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                Apayer2 = Convert.ToDouble(txtMontantPrimMat.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                SalDejaRecu = Convert.ToDouble(txtSalDejaRecu.Text.Replace('.', '.'), CultureInfo.InvariantCulture);
                Disp = Convert.ToDouble(txtDispo.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                string date1, datePaye = DateTime.Today.Date.ToShortDateString();
                date1 = Convert.ToDateTime(datePaye.ToString()).ToString("yyyy-MM-dd");
                txtMessage.Visible = false;

                if (Apayer >= Disp || Apayer2 >= Disp)
                {
                    txtMessage.Visible = true;
                }
                else
                {
                    txtMessage.Visible = false;
                    txtMessage.Text = "Verifiez bien vos champs ou soit le disponible en caisse est insuffisant pour couvrir ce payement";
                    if (SalDejaRecu>0)
                    {
                        txtMessage.Visible = true;
                        txtMessage.Text = "Cet agent a déjà était payé au mois sélectionné, impossible de faire 2 payements pour un même mois, par contre vous pouvez l'octroyer une avance sur salaire si vous le souhaitez bien";
                    }
                    else
                    {
                        con.Open();
                        if (txtIdEcole.Text == "3")
                        {
                            NbH = Convert.ToDouble(txtNbrHeure.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                            if (txtValeurHeure.Text != "0" && txtValeurHeure.Text != "" && txtRembourse.Text != "" && Apayer > 0 && NbH > 0)
                            {
                                if (txtCompteExitant.Text == "Non")
                                {
                                    CreerCompteAnnuelSiManquantAgent();
                                }
                                MySqlCommand cmd1a = con.CreateCommand();
                                cmd1a.CommandType = CommandType.Text;
                                cmd1a.CommandText = "insert into t_salaire values(default,'" + txtMatricule.Text + "','" + txtSalBase.Text + "','" + txtNbrHeure.Text + "','" + txtAvance.Text + "','" + txtRembourse.Text + "','" + txtNetRecu.Text + "','" + txtMois.SelectedValue + "','" + txtIdAnnee.Text + "','" + date1 + "','" + txtIdUser.Text + "','" + txtIdEcole.Text + "')";
                                cmd1a.ExecuteNonQuery();
                                con.Close();
                                ActualiserCompteAgent();
                                ActualiserLaCaisseEnSortie();
                                ImprimerFacture();
                                lblSalBase.Visible = false;
                                txtMontantPrimMat.Visible = false;
                                Label3.Visible = false;
                                txtNbrHeure.Visible = false;
                                Label5.Visible = false;
                                txtNbrHeureSemaine.Visible = false;
                                Label1.Visible = false;
                                txtAvance.Visible = false;
                                Label4.Visible = false;
                                Label7.Visible = false;
                                txtValeurHeure.Visible = false;
                                lblRembourse.Visible = false;
                                txtRembourse.Visible = false;
                                lblResteAvance.Visible = false;
                                txtResteAvance.Visible = false;
                                lblUnite1.Visible = false;
                                Label6.Visible = false;
                                txtNetRecu.Visible = false;
                                lblUnite2.Visible = false;
                                btnConvertir.Visible = false;
                                txtSalBase.Visible = false;
                                Label10.Visible = false;
                                Label8.Visible = false;
                                //Response.Redirect("AdminFinPayerSalaire.aspx");
                            }
                            else
                            {
                                txtMessage.Visible = true;
                                txtMessage.Text = "Verifiez bien vos champs, le salaire de l'agent ne peut pas être inférieur 0";
                            }
                        }
                        else
                        {
                            if (txtMontantPrimMat.Text != "0" && txtMontantPrimMat.Text != "" && Apayer2 > 0)
                            {
                                if (txtCompteExitant.Text == "Non")
                                {
                                    CreerCompteAnnuelSiManquantAgent();
                                }
                                MySqlCommand cmd1a = con.CreateCommand();
                                cmd1a.CommandType = CommandType.Text;
                                cmd1a.CommandText = "insert into t_salaire values(default,'" + txtMatricule.Text + "','" + txtMontantPrimMat.Text + "','Tous les jours','" + txtAvance.Text + "','" + txtRembourse.Text + "','" + txtNetRecu.Text + "','" + txtMois.SelectedValue + "','" + txtIdAnnee.Text + "','" + date1 + "','" + txtIdUser.Text + "','" + txtIdEcole.Text + "')";
                                cmd1a.ExecuteNonQuery();
                                con.Close();
                                ActualiserCompteAgent();
                                ActualiserLaCaisseEnSortie();
                                ImprimerFacture();
                                lblSalBase.Visible = false;
                                txtMontantPrimMat.Visible = false;
                                Label3.Visible = false;
                                txtNbrHeure.Visible = false;
                                Label5.Visible = false;
                                txtNbrHeureSemaine.Visible = false;
                                Label1.Visible = false;
                                txtAvance.Visible = false;
                                Label4.Visible = false;
                                Label7.Visible = false;
                                txtValeurHeure.Visible = false;
                                lblRembourse.Visible = false;
                                txtRembourse.Visible = false;
                                lblResteAvance.Visible = false;
                                txtResteAvance.Visible = false;
                                lblUnite1.Visible = false;
                                Label6.Visible = false;
                                txtNetRecu.Visible = false;
                                lblUnite2.Visible = false;
                                btnConvertir.Visible = false;
                                txtSalBase.Visible = false;
                                Label10.Visible = false;
                                Label8.Visible = false;

                                ////Response.Redirect("AdminFinPayerSalaire.aspx");
                            }
                            else
                            {
                                txtMessage.Visible = true;
                                txtMessage.Text = "Verifiez bien vos champs, le salaire de l'agent ne peut pas être inférieur ou égal à 0";
                            }

                        }
                    }
                   
                }
            }
            catch
            {
                txtMessage.Visible = true;
                txtMessage.Text = "Quelque chose a mal tourné, vérifiez si vous avez saisi le montant valide inférieur au disponible en caisse USD, si c'est un décimal,utilisez un point-virgule (,)";
            }
        }

        protected void txtMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SituationCaisse();
                TrouverDerniereOperation();
                TrouverAvance();
                TrouverHeurePrestees();
                CalculerPayement();
                TrouverCompteAgent();
                success.Visible = false;
                error.Visible = false;
                double SalDejaRecu;

                SalDejaRecu = Convert.ToDouble(txtSalDejaRecu.Text.Replace('.', '.'), CultureInfo.InvariantCulture);

                btnAddStructure.Visible = true;
                txtMessage.Visible = false;
                txtMessage.Text = "Verifiez bien vos champs ou soit le disponible en caisse est insuffisant pour couvrir ce payement";
                if (SalDejaRecu > 0)
                {
                    btnAddStructure.Visible = false;
                    txtMessage.Visible = true;
                    txtMessage.Text = "Cet agent a déjà était payé au mois sélectionné, impossible de faire 2 payements pour un même mois, par contre vous pouvez l'octroyer une avance sur salaire si vous le souhaitez bien";
                }
                else
                {
                    btnAddStructure.Visible = true;
                }
            }
            catch
            {

            }

        }

        protected void btnConvertir_Click(object sender, EventArgs e)
        {
            CalculerPayement();
        }

        protected void txtValeurHeure_TextChanged(object sender, EventArgs e)
        {
            CalculerPayement();
        }

        protected void txtRembourse_TextChanged(object sender, EventArgs e)
        {
            CalculerPayement();
        }
    }
}