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
    public partial class AdminFinDepense : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gestion_naomi");
        protected void Page_Load(object sender, EventArgs e)
        {
            //Vérification de la connexion de la varibale session
            if (Session["autorisation"] != null && (bool)Session["autorisation"] == true)
            {
                if (!IsPostBack)
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
                    con.Close();
                    AfficherDepense();
                    TrouverIdEcole();
                    SituationCaisse();
                }

            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void AfficherDepense()
        {
            con.Open();
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles")
            {
                MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM depense,utilisateur WHERE depense.idOperateur=utilisateur.id AND depense.anneeScolaire='" + txtIdAnnee.Text + "' ORDER BY idDepense DESC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data1.DataSource = dt;
                Data1.DataBind();
            }
            else
            {
                MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM depense,utilisateur WHERE depense.idOperateur=utilisateur.id AND depense.anneeScolaire='" + txtIdAnnee.Text + "' AND depense.idEcole='" + txtIdEcoleAffectationUser.Text + "' ORDER BY idDepense DESC", con);
                cmdB1.ExecuteNonQuery();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
                da.Fill(dt);
                Data1.DataSource = dt;
                Data1.DataBind();
            }
            con.Close();
        }
        public void recherche(string recherche)
        {
            try
            {
                con.Close();
                con.Open();
                if (txtIdEcoleAffectationUser.Text == "Toutes les écoles")
                {
                    MySqlCommand cmdD = con.CreateCommand();
                    cmdD.CommandType = CommandType.Text;
                    cmdD.CommandText = ("SELECT * FROM depense,utilisateur WHERE depense.idOperateur=utilisateur.id AND depense.anneeScolaire='" + txtIdAnnee.Text + "' AND CONCAT(depense.designation,depense.dateDepense,depense.montant,utilisateur.login) LIKE '%" + recherche + "%' ORDER BY idDepense DESC ");
                    cmdD.ExecuteNonQuery();
                    DataTable dtD = new DataTable();
                    MySqlDataAdapter daD = new MySqlDataAdapter(cmdD);
                    daD.Fill(dtD);
                    Data1.DataSource = dtD;
                    Data1.DataBind();
                }
                else
                {
                    MySqlCommand cmdD = con.CreateCommand();
                    cmdD.CommandType = CommandType.Text;
                    cmdD.CommandText = ("SELECT * FROM depense,utilisateur WHERE depense.idOperateur=utilisateur.id AND depense.anneeScolaire='" + txtIdAnnee.Text + "' AND depense.idEcole='" + txtIdEcoleAffectationUser.Text + "' AND CONCAT(depense.designation,depense.dateDepense,depense.montant,utilisateur.login) LIKE '%" + recherche + "%' ORDER BY idDepense DESC ");
                    cmdD.ExecuteNonQuery();
                    DataTable dtD = new DataTable();
                    MySqlDataAdapter daD = new MySqlDataAdapter(cmdD);
                    daD.Fill(dtD);
                    Data1.DataSource = dtD;
                    Data1.DataBind();
                }
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
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles")
            {
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
        public void TrouverEcoleModification()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT nomEcole from ecole WHERE idEcole='" + txtIdEcole.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtEcole.Items.Add(dr["nomEcole"].ToString());
            }
            con.Close();
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
            txtSortie.Text = "0";
            while (dr.Read())
            {
                txtDispo.Text = dr["Solde"].ToString();
                txtSortie.Text = dr["Sortie"].ToString();
            }

            con.Close();
        }
        public void ActualiserLaCaisseEnEntree()
        {
            //Actualiser la caisse
            //L'ajout de ces 2 lignes et de ces bibliothèques font l'utilisation du point au décimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            double b, sort, Disp;
            MySqlConnection conx1 = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
            conx1.Open();
            //Actualisation de la caisse s'il n'y a pas eu de conversion de l'argent de l'élève
            b = Convert.ToDouble(txtMontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
            sort = Convert.ToDouble(txtSortie.Text.Replace(',', '.'), CultureInfo.InvariantCulture) + b;
            Disp = Convert.ToDouble(txtDispo.Text.Replace(',', '.'), CultureInfo.InvariantCulture) - b;

            string command = ("UPDATE t_caisse SET Sortie ='" + sort.ToString(CultureInfo.InvariantCulture) + "', Solde ='" + Disp.ToString(CultureInfo.InvariantCulture) + "' WHERE libelle='" + txtUnite.Text + "' AND idEcole=+'" + txtIdEcole.Text + "'");
            MySqlCommand cmde = new MySqlCommand(command, conx1);
            cmde.ExecuteNonQuery();

        }
        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            try
            {
                double MontantDepense, Disp;
                MontantDepense = Convert.ToDouble(txtMontant.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                Disp = Convert.ToDouble(txtDispo.Text.Replace(',', '.'), CultureInfo.InvariantCulture);
                txtMessage.Visible = false;
                if (MontantDepense >= Disp)
                {
                    txtMessage.Visible = true;
                }
                else
                {
                    con.Open();
                    string dateInscription = DateTime.Today.Date.ToShortDateString();
                    MySqlCommand cmd1a = con.CreateCommand();
                    cmd1a.CommandType = CommandType.Text;
                    cmd1a.CommandText = "insert into depense values(default,'" + dateInscription.ToString() + "','" + txtMotif.Text + "','" + txtMontant.Text + "','" + txtUnite.SelectedValue + "','" + txtIdAnnee.Text + "','" + txtIdUser.Text + "','" + txtIdEcole.Text + "')";
                    cmd1a.ExecuteNonQuery();
                    con.Close();
                    ActualiserLaCaisseEnEntree();
                    Response.Redirect("AdminFinDepense.aspx");
                }
            }
            catch
            {
                txtMessage.Visible = true;
                txtMessage.Text = "Quelque chose a mal tourné, vérifiez si vous avez saisi nombre valide, si c'est un décimal, n'oubliez pas d'utiliser un point-virgule (,)";
            }
           
        }
        protected void btnModification_Click(object sender, EventArgs e)
        {
            //con.Open();
            //string dateInscription = DateTime.Today.Date.ToShortDateString();
            //MySqlCommand cmd1a = con.CreateCommand();
            //cmd1a.CommandType = CommandType.Text;
            //cmd1a.CommandText = "UPDATE t_agent SET nom='" + txtNom.Text + "',prenom='" + txtPrenom.Text + "',sexe='" + txtSexe.SelectedValue + "',niveau='" + txtNiveau.SelectedValue + "',domaine='" + txtDomaine.Text + "',fonction='" + txtFonction.SelectedValue + "',etat_civil='" + txtEtatCivil.Text + "',adresse='" + txtAdresse.Text + "',idEcole='" + txtIdEcole.Text + "' WHERE matricule='" + txtMatricule.Text + "'";
            //cmd1a.ExecuteNonQuery();
            //con.Close();
            //Response.Redirect("AdminAgentAjout.aspx");
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        protected void Data1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void txtEcole_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdEcole();
            SituationCaisse();
        }
        public void TrouverAgent()
        {
        }

        protected void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            recherche(txtRecherche.Text);
        }
        public void ImprimerRecherche1(string ImprimerRecherche1)
        {
            Random rnd = new Random();
            Document dc = new Document();
            String chemin = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ " + rnd.Next() * 1000 + "DépenseEcole.pdf";
            FileStream fs = File.Create(chemin);
            PdfWriter.GetInstance(dc, fs);
            dc.Open();
            dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
            dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
            dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
            dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
            dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
            dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
            dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

            dc.Add(new Paragraph("                                                                              LES DEPENSES DE L'ECOLE EFFECTUEES, Année Scolaire: " + txtDesignationAnnee.Text + "\n \n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.BLACK)));

            PdfPTable table = new PdfPTable(5);
            PdfPCell cell = new PdfPCell(new Paragraph("Date Dépense", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.BLACK;
            PdfPCell cell1 = new PdfPCell(new Paragraph("Désignation", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.BackgroundColor = BaseColor.BLACK;
            PdfPCell cell2 = new PdfPCell(new Paragraph("Montant", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.BackgroundColor = BaseColor.BLACK;
            PdfPCell cell3 = new PdfPCell(new Paragraph("Unité", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            cell3.BackgroundColor = BaseColor.BLACK;
            PdfPCell cell4 = new PdfPCell(new Paragraph("Opérateur", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            cell4.BackgroundColor = BaseColor.BLACK;

            table.AddCell(cell);
            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);
            table.AddCell(cell4);

            //Recherche des élèves
            con.Close();
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM depense,utilisateur WHERE depense.idOperateur=utilisateur.id AND depense.anneeScolaire='" + txtIdAnnee.Text + "' AND CONCAT(depense.designation,depense.dateDepense,depense.montant,utilisateur.login) LIKE '%" + ImprimerRecherche1 + "%' ORDER BY idDepense DESC ", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //===========================================================================
                PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["dateDepense"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                cell5.HorizontalAlignment = Element.ALIGN_LEFT;
                cell5.BackgroundColor = BaseColor.WHITE;
                PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["designation"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                cell6.HorizontalAlignment = Element.ALIGN_LEFT;
                cell6.BackgroundColor = BaseColor.WHITE;
                PdfPCell cell7 = new PdfPCell(new Paragraph((string)dr["montant"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                cell7.BackgroundColor = BaseColor.WHITE;
                PdfPCell cell8 = new PdfPCell(new Paragraph((string)dr["unite"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                cell8.HorizontalAlignment = Element.ALIGN_LEFT;
                cell8.BackgroundColor = BaseColor.WHITE;
                PdfPCell cell9 = new PdfPCell(new Paragraph((string)dr["login"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                cell9.HorizontalAlignment = Element.ALIGN_CENTER;
                cell9.BackgroundColor = BaseColor.WHITE;
                //============================================================================
                table.AddCell(cell5);
                table.AddCell(cell6);
                table.AddCell(cell7);
                table.AddCell(cell8);
                table.AddCell(cell9);
            }

            dc.Add(table);
            dc.Add(new Paragraph("\n"));
            dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
            dc.Close();
            System.Diagnostics.Process.Start(chemin);
        }

        protected void btnRechApproFondie_Click(object sender, EventArgs e)
        {
            ImprimerRecherche1(txtRecherche.Text);
        }

        protected void txtUnite_SelectedIndexChanged(object sender, EventArgs e)
        {
            SituationCaisse();
        }
    }
}