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
    public partial class AdminInscription : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
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
                    numMat();
                    TrouverIdEcole();

                    if (txtMatricule.Text != "")
                    {
                        TrouverEleve();
                        btnAddStructure.Visible = false;
                        txtmat.Visible = false;
                        btnModification.Visible = true;
                        TrouverEcoleModification();
                        TrouverSectionModification();
                        TrouverClasserModification();
                    }
                    else
                    {
                        btnAddStructure.Visible = true;
                        txtmat.Visible = true;
                        btnModification.Visible = false;
                    }
                }

            }
            else
            {
                Response.Redirect("Acceuil.aspx");
            }
        }
        public void AfficherInscription()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT t_eleve.dateInscription as dateInscription,t_eleve.matricule as matricule,t_eleve.nom as nom,t_eleve.prenom as prenom,t_eleve.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole,t_eleve.nom_du_pere as nom_du_pere,t_eleve.nom_de_la_mere as nom_de_la_mere,t_eleve.lieuNaiss as lieuNaiss,t_eleve.dateNaiss as dateNaiss,t_eleve.adresse as adresse FROM t_eleve,t_classe,section,ecole WHERE t_eleve.classe=t_classe.id and t_eleve.option=section.idSection AND t_eleve.idEcole=ecole.idEcole AND t_eleve.anneescol='"+txtIdAnnee.Text+"' ORDER BY dateInscription ASC", con);
            cmdB1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
            da.Fill(dt);
            Data1.DataSource = dt;
            Data1.DataBind();
            con.Close();
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
                cmdD.CommandText = ("SELECT t_eleve.dateInscription as dateInscription,t_eleve.matricule as matricule,t_eleve.nom as nom,t_eleve.prenom as prenom,t_eleve.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole,t_eleve.nom_du_pere as nom_du_pere,t_eleve.nom_de_la_mere as nom_de_la_mere,t_eleve.lieuNaiss as lieuNaiss,t_eleve.dateNaiss as dateNaiss,t_eleve.adresse as adresse FROM t_eleve,t_classe,section,ecole WHERE t_eleve.classe=t_classe.id and t_eleve.option=section.idSection AND t_eleve.idEcole=ecole.idEcole AND CONCAT(t_eleve.dateInscription,t_eleve.matricule,t_eleve.nom,t_eleve.prenom,t_eleve.sexe,t_classe.classe,section.nomSection,ecole.nomEcole,t_eleve.nom_du_pere,t_eleve.nom_de_la_mere,t_eleve.adresse) LIKE '%" + recherche + "%' ORDER BY dateInscription ASC ");
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
        public void numMat()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select MAX(idClasse) as Ordre from change_classe", con);
                MySqlDataReader dr = cmd.ExecuteReader();

                //Attribution du NumMat par défaut
                int val2, val3;
                val2 = 1;
                int a, b, c;// g;
                string d;
                a = int.Parse(DateTime.Today.Year.ToString());
                b = a - 2000;
                c = b - 1;
                d = c.ToString() + "-" + b;
                string result = string.Format("{0:D4}", val2);
                string dec = (result + "/" + d);
                txtmat.Text = dec;

                while (dr.Read())
                {
                    //Vérifier le grand NumMat pour l'incrémenter et avoir un nouveau numMat
                    txtDernierMat.Text = dr["Ordre"].ToString();
                    val2 = int.Parse(txtDernierMat.Text) + 1;
                    txtDernierMat.Text = val2.ToString();
                    val3 = int.Parse(txtDernierMat.Text);
                    string result2 = string.Format("{0:D4}", val3);
                    string dec2 = (result2 + "/" + d);
                    txtmat.Text = dec2;
                }
                con.Close();

            }
            catch
            {
                //MessageBox.Show("Erreur dans l'attribution du numéro matricule","Erreur survenue",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
            con.Close();
        }
        public void TrouverEcoleModification()
        {
            con.Open();
            txtOption.Items.Clear();
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
        public void TrouverSectionModification()
        {
            con.Open();
            txtOption.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT nomSection from section WHERE idSection='" + txtIdOption.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtOption.Items.Add(dr["nomSection"].ToString());
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
        public void TrouverClasserModification()
        {
            con.Open();
            txtClasse.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT classe from t_classe WHERE id='" + txtIdClasse.Text + "'");
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
            cmdB.CommandText = ("SELECT *from t_classe WHERE classe='" + txtClasse.SelectedValue + "' AND idSection='"+txtIdOption.Text+"'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtIdClasse.Text = dr["id"].ToString();
            }
            con.Close();
        }
        public void EnregistrerClasseActive()
        {
            con.Open();
            string dateInscription = DateTime.Today.Date.ToShortDateString();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into change_classe values(default,'" + txtmat.Text + "','" + txtNom.Text + "','" + txtPrenom.Text + "','" + txtSexe.SelectedValue + "','" + txtIdClasse.Text + "','" + txtIdOption.Text + "','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "','Actif')";
            cmd1a.ExecuteNonQuery();
            con.Close();
        }
        public void ModifierNomClasseActive()
        {
            con.Open();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "UPDATE change_classe SET nomEleve='" + txtNom.Text + "',prenom='" + txtPrenom.Text + "',sexe='" + txtSexe.SelectedValue + "' WHERE matricule='" + txtMatricule.Text + "'";
            cmd1a.ExecuteNonQuery();
            con.Close();
        }
        public void ModifierClasseActive()
        {
            con.Open();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "UPDATE change_classe SET classe='" + txtIdClasse.Text + "',optionEtude='" + txtIdOption.Text + "',idEcole='" + txtIdEcole.Text + "' WHERE matricule='" + txtMatricule.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "'";
            cmd1a.ExecuteNonQuery();
            con.Close();
        }
        public void CreerCompteEleve()
        {
            con.Open();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into compte_eleve values(default,'" + txtmat.Text + "','0','0','0','0','0','0','0','0','0','0','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
        }

        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            con.Open();
            string dateInscription = DateTime.Today.Date.ToShortDateString();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into t_eleve values('" + txtmat.Text + "','" + txtNom.Text + "','" + txtPrenom.Text + "','" + txtSexe.SelectedValue + "','" + txtIdClasse.Text + "','" + txtnationalité.Text + "','" + txtNomPere.Text + "','" + txtNomMere.Text + "','" + txtAdresse.Text + "','" + txtIdAnnee.Text + "','" + dateInscription.ToString() + "','" + txtLieuNaiss.Text + "','" + txtdate.Text + "','" + txtEcoleProv.Text + "','" + txtIdOption.Text + "','" + txtPourc.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
            EnregistrerClasseActive();
            CreerCompteEleve();
            Response.Redirect("AdminInscription.aspx");
        }
        protected void btnModification_Click(object sender, EventArgs e)
        {
            con.Open();
            string dateInscription = DateTime.Today.Date.ToShortDateString();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "UPDATE t_eleve SET nom='" + txtNom.Text + "',prenom='" + txtPrenom.Text + "',sexe='" + txtSexe.SelectedValue + "',classe='" + txtIdClasse.Text + "',nationalite='" + txtnationalité.Text + "',nom_du_pere='" + txtNomPere.Text + "',nom_de_la_mere='" + txtNomMere.Text + "',adresse='" + txtAdresse.Text + "',lieuNaiss='" + txtLieuNaiss.Text + "',dateNaiss='" + txtdate.Text + "',ecoleProv='" + txtEcoleProv.Text + "',option='" + txtIdOption.Text + "',pourcReussite='" + txtPourc.Text + "',idEcole='" + txtIdEcole.Text + "' WHERE matricule='" + txtMatricule.Text + "'";
            cmd1a.ExecuteNonQuery();
            con.Close();
            ModifierNomClasseActive();
            ModifierClasseActive();
            Response.Redirect("AdminInscription.aspx");
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
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
        }

        protected void txtClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdClasse();
        }
        public void TrouverEleve()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_eleve WHERE matricule='" + txtMatricule.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtNom.Text = dr["nom"].ToString();
                txtPrenom.Text = dr["prenom"].ToString();
                txtSexe.Text = dr["sexe"].ToString();
                txtIdEcole.Text = dr["idEcole"].ToString();
                txtIdOption.Text = dr["option"].ToString();
                txtIdClasse.Text = dr["classe"].ToString();
                txtnationalité.Text = dr["nationalite"].ToString();
                txtNomPere.Text = dr["nom_du_pere"].ToString();
                txtNomMere.Text = dr["nom_de_la_mere"].ToString();
                txtLieuNaiss.Text = dr["lieuNaiss"].ToString();
                txtAdresse.Text = dr["adresse"].ToString();
                txtdate.Text = dr["dateNaiss"].ToString();
                txtEcoleProv.Text = dr["ecoleProv"].ToString();
                txtPourc.Text = dr["pourcReussite"].ToString();
            }
            con.Close();
        }

        protected void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            recherche(txtRecherche.Text);
        }
        public void ImprimerRecherche(string recherche)
        {
            
        }

        protected void btnImprim_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Document dc = new Document();
            String chemin = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ " + rnd.Next() * 1000 + "RapportInscritpionCritère.pdf";
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

            dc.Add(new Paragraph("                                                     LISTE DES ELEVES INSCRITS TRIES SUR CRITERE :  " + (txtRecherche.Text).ToUpper() + ", Année Scolaire: " + txtDesignationAnnee.Text + "\n \n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.BLACK)));

            PdfPTable table = new PdfPTable(5);
            PdfPCell cell = new PdfPCell(new Paragraph("Nom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = BaseColor.BLACK;
            PdfPCell cell1 = new PdfPCell(new Paragraph("Prénom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell1.BackgroundColor = BaseColor.BLACK;
            PdfPCell cell2 = new PdfPCell(new Paragraph("Sexe", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.BackgroundColor = BaseColor.BLACK;
            PdfPCell cell3 = new PdfPCell(new Paragraph("Classe", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            cell3.BackgroundColor = BaseColor.BLACK;
            PdfPCell cell4 = new PdfPCell(new Paragraph("Option", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
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
            MySqlCommand cmd = new MySqlCommand("SELECT t_eleve.dateInscription as dateInscription,t_eleve.matricule as matricule,t_eleve.nom as nom,t_eleve.prenom as prenom,t_eleve.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole,t_eleve.nom_du_pere as nom_du_pere,t_eleve.nom_de_la_mere as nom_de_la_mere,t_eleve.lieuNaiss as lieuNaiss,t_eleve.dateNaiss as dateNaiss,t_eleve.adresse as adresse FROM t_eleve,t_classe,section,ecole WHERE t_eleve.classe=t_classe.id and t_eleve.option=section.idSection AND t_eleve.idEcole=ecole.idEcole ORDER BY dateInscription ASC", con);
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //===========================================================================
                PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["nom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                cell5.HorizontalAlignment = Element.ALIGN_LEFT;
                cell5.BackgroundColor = BaseColor.WHITE;
                PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                cell6.HorizontalAlignment = Element.ALIGN_LEFT;
                cell6.BackgroundColor = BaseColor.WHITE;
                PdfPCell cell7 = new PdfPCell(new Paragraph((string)dr["sexe"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                cell7.BackgroundColor = BaseColor.WHITE;
                PdfPCell cell8 = new PdfPCell(new Paragraph((string)dr["classe"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                cell8.HorizontalAlignment = Element.ALIGN_LEFT;
                cell8.BackgroundColor = BaseColor.WHITE;
                PdfPCell cell9 = new PdfPCell(new Paragraph((string)dr["option"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
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
            dc.Add(new Paragraph("                              Fait à Bukavu le: " + System.DateTime.Now + "\n\n"));
            dc.Add(new Paragraph("                              Imprimé par " + (txtLogin.Text), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, BaseColor.BLACK)));
            dc.Close();
            System.Diagnostics.Process.Start(chemin);
        }

        protected void txtMatricule_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricule.Text != "")
            {
                TrouverEleve();
                btnAddStructure.Visible = false;
                txtmat.Visible = false;
                btnModification.Visible = true;
                TrouverEcoleModification();
                TrouverSectionModification();
                TrouverClasserModification();
            }
            else
            {
                btnAddStructure.Visible = true;
                txtmat.Visible = true;
                btnModification.Visible = false;
            }
        }
    }
}