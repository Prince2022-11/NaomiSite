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

namespace NaomiSite
{
    public partial class AdminAgentAjout : System.Web.UI.Page
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
                    AfficherAgent();
                    numMat();
                    TrouverIdEcole();

                    if (txtMatricule.Text != "")
                    {
                        TrouverAgent();
                        btnAddStructure.Visible = false;
                        txtmat.Visible = false;
                        btnModification.Visible = true;
                        TrouverEcoleModification();
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
        public void AfficherAgent()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT * FROM t_agent,ecole WHERE t_agent.idEcole=ecole.idEcole ORDER BY nom ASC", con);
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
                cmdD.CommandText = ("SELECT * FROM t_agent,ecole WHERE t_agent.idEcole=ecole.idEcole AND CONCAT(t_agent.matricule,t_agent.nom,t_agent.prenom,t_agent.sexe,t_agent.domaine,t_agent.fonction,t_agent.niveau,ecole.nomEcole) LIKE '%" + recherche + "%' ORDER BY nom ASC ");
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
                MySqlCommand cmd = new MySqlCommand("select count(matricule) as Ordre from t_agent", con);
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
        public void CreerCompteAgent()
        {
            con.Open();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into compte_agent values(default,'" + txtmat.Text + "','0','0','0','0','0','0','0','0','0','0','" + txtIdAnnee.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
        }

        protected void btnAddStructure_Click(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "insert into t_agent values('" + txtmat.Text + "','" + txtNom.Text + "','" + txtPrenom.Text + "','" + txtSexe.SelectedValue + "','" + txtNiveau.SelectedValue + "','" + txtDomaine.Text + "','" + txtFonction.SelectedValue + "','" + txtEtatCivil.SelectedValue + "','" + txtPhone.Text + "','" + txtAdresse.Text + "','" + txtIdEcole.Text + "')";
            cmd1a.ExecuteNonQuery();
            con.Close();
            CreerCompteAgent();
            Response.Redirect("AdminAgentAjout.aspx");
        }
        protected void btnModification_Click(object sender, EventArgs e)
        {
            con.Open();
            string dateInscription = DateTime.Today.Date.ToShortDateString();
            MySqlCommand cmd1a = con.CreateCommand();
            cmd1a.CommandType = CommandType.Text;
            cmd1a.CommandText = "UPDATE t_agent SET nom='" + txtNom.Text + "',prenom='" + txtPrenom.Text + "',sexe='" + txtSexe.SelectedValue + "',niveau='" + txtNiveau.SelectedValue + "',domaine='" + txtDomaine.Text + "',fonction='" + txtFonction.SelectedValue + "',etat_civil='" + txtEtatCivil.Text + "',adresse='" + txtAdresse.Text + "',idEcole='" + txtIdEcole.Text + "' WHERE matricule='" + txtMatricule.Text + "'";
            cmd1a.ExecuteNonQuery();
            con.Close();
            Response.Redirect("AdminAgentAjout.aspx");
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
        }
        public void TrouverAgent()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from t_agent WHERE matricule='" + txtMatricule.Text + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtNom.Text = dr["nom"].ToString();
                txtPrenom.Text = dr["prenom"].ToString();
                txtSexe.Text = dr["sexe"].ToString();
                txtIdEcole.Text = dr["idEcole"].ToString();
                txtNiveau.Text = dr["niveau"].ToString();
                txtDomaine.Text = dr["domaine"].ToString();
                txtFonction.Text = dr["fonction"].ToString();
                txtEtatCivil.Text = dr["etat_civil"].ToString();
                txtPhone.Text = dr["telephone"].ToString();
                txtAdresse.Text = dr["adresse"].ToString();
            }
            con.Close();
        }

        protected void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            recherche(txtRecherche.Text);
        }
        public void ImprimerRecherche1(string ImprimerRecherche1)
        {
            Random rnd = new Random();
            Document dc = new Document();
            String chemin = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ " + rnd.Next() * 1000 + "ListeAgent.pdf";
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

            dc.Add(new Paragraph("                                                                              LISTE DES AGENTS DE L'ECOLE, Année Scolaire: " + txtDesignationAnnee.Text + "\n \n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.BLACK)));

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
            PdfPCell cell3 = new PdfPCell(new Paragraph("Niveau", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            cell3.BackgroundColor = BaseColor.BLACK;
            PdfPCell cell4 = new PdfPCell(new Paragraph("Ecole", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.WHITE)));
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
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM t_agent,ecole WHERE t_agent.idEcole=ecole.idEcole AND CONCAT(t_agent.matricule,t_agent.nom,t_agent.prenom,t_agent.sexe,t_agent.domaine,t_agent.fonction,t_agent.niveau,ecole.nomEcole) LIKE '%" + ImprimerRecherche1 + "%' ORDER BY nomEcole,nom ASC ", con);
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
                PdfPCell cell8 = new PdfPCell(new Paragraph((string)dr["niveau"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                cell8.HorizontalAlignment = Element.ALIGN_LEFT;
                cell8.BackgroundColor = BaseColor.WHITE;
                PdfPCell cell9 = new PdfPCell(new Paragraph((string)dr["nomEcole"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
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
        protected void txtMatricule_TextChanged(object sender, EventArgs e)
        {
            if (txtMatricule.Text != "")
            {
                TrouverAgent();
                btnAddStructure.Visible = false;
                txtmat.Visible = false;
                btnModification.Visible = true;
                TrouverEcoleModification();
            }
            else
            {
                btnAddStructure.Visible = true;
                txtmat.Visible = true;
                btnModification.Visible = false;
            }
        }

        protected void btnRechApproFondie_Click(object sender, EventArgs e)
        {
            ImprimerRecherche1(txtRecherche.Text);
        }
    }
}