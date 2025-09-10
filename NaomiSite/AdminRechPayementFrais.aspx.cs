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
    public partial class AdminRechPayementFrais : System.Web.UI.Page
    {
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gestion_naomi");
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
                    try
                    {
                        //AfficherToutPayement();
                        //TrouverIdEcole();
                        //TrouverSection();
                        //TrouverClasser();
                        //TrouverFrais();
                        //TrouverIdFrais();

                        TrouverIdEcole();
                        TrouverSection();
                        TrouverIdSection();
                        TrouverClasser();
                        TrouverIdClasse();
                        AfficherToutPayement();
                        TrouverFrais();
                        TrouverIdFrais();
                    }
                    catch { }
                   
                    btnOrdre.Checked = false;
                    btnPasenOrdre.Checked = false;
                }
                else
                {
                    Response.Redirect("Acceuil.aspx");
                }
            }
        }
        public void AfficherToutPayement()
        {
            con.Open();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT t_payement_frais.id_payement as id_Payement,t_payement_frais.idRecu as idRecu,t_payement_frais.date_payement as date_payement,frais_scolaire.designation as DFrais,t_payement_frais.montant_payer as montantPaye,t_payement_frais.unite as unite,change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole FROM t_payement_frais,frais_scolaire,change_classe,t_classe,section,ecole WHERE t_payement_frais.motif=frais_scolaire.idfrais AND t_payement_frais.matricule=change_classe.matricule AND t_payement_frais.anneescolaire=change_classe.anneeScolaire AND t_payement_frais.idEcole=change_classe.idEcole AND change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND etat='Actif' AND t_payement_frais.anneeScolaire='"+txtIdAnnee.Text+"' ORDER BY id_payement DESC", con);
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
                cmdD.CommandText = ("SELECT t_payement_frais.id_payement as id_Payement,t_payement_frais.idRecu as idRecu,t_payement_frais.date_payement as date_payement,frais_scolaire.designation as DFrais,t_payement_frais.montant_payer as montantPaye,t_payement_frais.unite as unite,change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole FROM t_payement_frais,frais_scolaire,change_classe,t_classe,section,ecole WHERE t_payement_frais.motif=frais_scolaire.idfrais AND t_payement_frais.matricule=change_classe.matricule AND t_payement_frais.anneescolaire=change_classe.anneeScolaire AND t_payement_frais.idEcole=change_classe.idEcole AND change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND etat='Actif' AND t_payement_frais.anneeScolaire='" + txtIdAnnee.Text + "' AND CONCAT(change_classe.matricule,change_classe.nomEleve,change_classe.prenom,t_classe.classe,section.nomSection,ecole.nomEcole,t_payement_frais.date_payement,frais_scolaire.designation) LIKE '%" + recherche + "%' ORDER date_payement DESC,id_payement DESC ");
                cmdD.ExecuteNonQuery();
                txtMessage.Text = "Les payements effectués incluants le critère '"+txtRecherche.Text+"'";
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
        public void AfficherParIntervalleDate()
        {
            con.Open();
            string d1, d2;
            d1 = DateTime.Parse(txtDate1.Text).ToShortDateString();
            d2 = DateTime.Parse(txtDate2.Text).ToShortDateString();
            MySqlCommand cmdB1 = new MySqlCommand("SELECT t_payement_frais.id_payement as id_Payement,t_payement_frais.idRecu as idRecu,t_payement_frais.date_payement as date_payement,frais_scolaire.designation as DFrais,t_payement_frais.montant_payer as montantPaye,t_payement_frais.unite as unite,change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole FROM t_payement_frais,frais_scolaire,change_classe,t_classe,section,ecole WHERE t_payement_frais.motif=frais_scolaire.idfrais AND t_payement_frais.matricule=change_classe.matricule AND t_payement_frais.anneescolaire=change_classe.anneeScolaire AND t_payement_frais.idEcole=change_classe.idEcole AND change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND etat='Actif' AND t_payement_frais.anneeScolaire='" + txtIdAnnee.Text + "' AND t_payement_frais.date_payement BETWEEN '" + d1+"' AND '"+d2 + "' ORDER BY date_payement DESC,id_payement DESC", con);
            cmdB1.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdB1);
            da.Fill(dt);
            Data1.DataSource = dt;
            Data1.DataBind();
            con.Close();
            con.Close();
            txtMessage.Text = "Les payements effectués entre le " +d1+ " et "+d2;
        }
        public void SontEnOrdre()
        {
            if (txtTranche.SelectedValue=="Tranche1")
            {
                //en ordre avec la 1ere tranche
                Random rnd = new Random();
                Document dc = new Document();
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    PdfWriter.GetInstance(dc, ms);
                    dc.Open();
                    //Le code
                    dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Sont en ordres avec les payements en  : " + (txtFrais.SelectedValue).ToUpper() + ", " + (txtTranche.SelectedValue).ToUpper() + "/ " + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Classe de               : " + (txtClasse.SelectedValue) + " " + (txtOption.SelectedValue) + " Au Niveau :" + (txtEcole.SelectedValue) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Prévue à payer en (" + (txtUnite.Text) + ") : Tranche1=" + (txtT1.Text) + " ,Tranche2= " + (txtT2.Text) + " ,Tranche1= " + (txtT3.Text) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                    PdfPTable table = new PdfPTable(4);
                    PdfPCell cell = new PdfPCell(new Paragraph("Matricule", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell1 = new PdfPCell(new Paragraph("Nom & PostNom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell2 = new PdfPCell(new Paragraph("Prénom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell3 = new PdfPCell(new Paragraph("Montant payé", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.BackgroundColor = BaseColor.BLACK;

                    table.AddCell(cell);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);

                    MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                    con.Open();
                    string cmd = "SELECT DISTINCT situation_paye.Matricule,change_classe.nomEleve, change_classe.prenom,situation_paye.tranche1,situation_paye.tranche2,situation_paye.tranche3 FROM change_classe,situation_paye WHERE situation_paye.Matricule=change_classe.matricule AND situation_paye.idfrais='" + txtIdFrais.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND situation_paye.anneescolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "' AND situation_paye.tranche1='" + txtT1.Text + "'";
                    MySqlCommand cmde = new MySqlCommand(cmd, con);
                    MySqlDataReader dr = cmde.ExecuteReader();
                    while (dr.Read())
                    {
                        //===========================================================================
                        PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["Matricule"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell4.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["nomEleve"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell5.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell6.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell7 = new PdfPCell(new Paragraph(@Convert.ToString(dr["tranche1"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell7.BackgroundColor = BaseColor.WHITE;

                        //============================================================================

                        table.AddCell(cell4);
                        table.AddCell(cell5);
                        table.AddCell(cell6);
                        table.AddCell(cell7);

                    }
                    con.Close();

                    //Prélever le total payé en Unité
                    MySqlConnection conN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    conN.Open();
                    MySqlCommand cmdN = new MySqlCommand("SELECT SUM(situation_paye.tranche1) as Total FROM situation_paye,frais_scolaire WHERE situation_paye.idFrais=frais_scolaire.idfrais AND situation_paye.tranche1=frais_scolaire.tranche1 AND situation_paye.idFrais='" + txtIdFrais.Text + "' AND frais_scolaire.classe='" + txtIdClasse.Text + "' AND frais_scolaire.optionConcerne='" + txtIdOption.Text + "' AND situation_paye.anneeScolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "'", conN);
                    MySqlDataReader drN = cmdN.ExecuteReader();
                    Chunk ck;
                    Paragraph pr = new Paragraph();
                    while (drN.Read())
                    {
                        ck = new Chunk("                                                                                  Total payé =  " + @Convert.ToString(drN["Total"]) + "" + txtUnite.Text);
                        pr = new Paragraph(ck);

                    }
                    conN.Close();

                    dc.Add(table);
                    dc.Add(pr);
                    dc.Add(new Paragraph("\n"));
                    dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                    dc.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EnOrdre1ereTranche.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
                dc.Close();
            }
            if (txtTranche.SelectedValue == "Tranche2")
            {
                //en ordre avec la 2e tranche
                Random rnd = new Random();
                Document dc = new Document();
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    PdfWriter.GetInstance(dc, ms);
                    dc.Open();
                    //Le code
                    dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Sont en ordres avec les payements en  : " + (txtFrais.SelectedValue).ToUpper() + ", " + (txtTranche.SelectedValue).ToUpper() + " /" + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Classe de                 : " + (txtClasse.SelectedValue) + " " + (txtOption.SelectedValue) + " Au Niveau :" + (txtEcole.SelectedValue) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Prévue à payer en (" + (txtUnite.Text) + ") : Tranche1=" + (txtT1.Text) + " ,Tranche2= " + (txtT2.Text) + " ,Tranche1= " + (txtT3.Text) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                    PdfPTable table = new PdfPTable(4);
                    PdfPCell cell = new PdfPCell(new Paragraph("Matricule", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell1 = new PdfPCell(new Paragraph("Nom & PostNom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell2 = new PdfPCell(new Paragraph("Prénom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell3 = new PdfPCell(new Paragraph("Montant payé", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.BackgroundColor = BaseColor.BLACK;

                    table.AddCell(cell);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);

                    MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                    con.Open();
                    string cmd = "SELECT DISTINCT situation_paye.Matricule,change_classe.nomEleve, change_classe.prenom,situation_paye.tranche1,situation_paye.tranche2,situation_paye.tranche3 FROM change_classe,situation_paye WHERE situation_paye.Matricule=change_classe.matricule AND situation_paye.idfrais='" + txtIdFrais.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND situation_paye.anneescolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "' AND situation_paye.tranche2='" + txtT2.Text + "'";
                    MySqlCommand cmde = new MySqlCommand(cmd, con);
                    MySqlDataReader dr = cmde.ExecuteReader();
                    while (dr.Read())
                    {
                        //===========================================================================
                        PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["Matricule"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell4.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["nomEleve"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell5.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell6.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell7 = new PdfPCell(new Paragraph(@Convert.ToString(dr["tranche2"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell7.BackgroundColor = BaseColor.WHITE;

                        //============================================================================

                        table.AddCell(cell4);
                        table.AddCell(cell5);
                        table.AddCell(cell6);
                        table.AddCell(cell7);

                    }
                    con.Close();

                    //Prélever le total payé en Unité
                    MySqlConnection conN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    conN.Open();
                    MySqlCommand cmdN = new MySqlCommand("SELECT SUM(situation_paye.tranche2) as Total FROM situation_paye,frais_scolaire WHERE situation_paye.idFrais=frais_scolaire.idfrais AND situation_paye.tranche2=frais_scolaire.tranche2 AND situation_paye.idFrais='" + txtIdFrais.Text + "' AND frais_scolaire.classe='" + txtIdClasse.Text + "' AND frais_scolaire.optionConcerne='" + txtIdOption.Text + "' AND situation_paye.anneeScolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "'", conN);
                    MySqlDataReader drN = cmdN.ExecuteReader();
                    Chunk ck;
                    Paragraph pr = new Paragraph();
                    while (drN.Read())
                    {
                        ck = new Chunk("                                                                                  Total payé =  " + @Convert.ToString(drN["Total"]) + "" + txtUnite.Text);
                        pr = new Paragraph(ck);

                    }
                    conN.Close();

                    dc.Add(table);
                    dc.Add(pr);
                    dc.Add(new Paragraph("\n"));
                    dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                    dc.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EnOrdre2eTranche.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
                dc.Close();
            }
            if (txtTranche.SelectedValue == "Tranche3")
            {
                //en ordre avec la 3e tranche
                Random rnd = new Random();
                Document dc = new Document();
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    PdfWriter.GetInstance(dc, ms);
                    dc.Open();
                    //Le code
                    dc.Open();
                    dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Sont en ordres avec les payements en  : " + (txtFrais.SelectedValue).ToUpper() + ", " + (txtTranche.SelectedValue).ToUpper() + " /" + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Classe de               : " + (txtClasse.SelectedValue) + " " + (txtOption.SelectedValue) + " Au Niveau :" + (txtEcole.SelectedValue) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Prévue à payer en (" + (txtUnite.Text) + ") : Tranche1=" + (txtT1.Text) + " ,Tranche2= " + (txtT2.Text) + " ,Tranche1= " + (txtT3.Text) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                    PdfPTable table = new PdfPTable(4);
                    PdfPCell cell = new PdfPCell(new Paragraph("Matricule", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell1 = new PdfPCell(new Paragraph("Nom & PostNom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell2 = new PdfPCell(new Paragraph("Prénom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell3 = new PdfPCell(new Paragraph("Montant payé", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.BackgroundColor = BaseColor.BLACK;

                    table.AddCell(cell);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);

                    MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                    con.Open();
                    string cmd = "SELECT DISTINCT situation_paye.Matricule,change_classe.nomEleve, change_classe.prenom,situation_paye.tranche1,situation_paye.tranche2,situation_paye.tranche3 FROM change_classe,situation_paye WHERE situation_paye.Matricule=change_classe.matricule AND situation_paye.idfrais='" + txtIdFrais.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND situation_paye.anneescolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "' AND situation_paye.tranche3='" + txtT3.Text + "'";
                    MySqlCommand cmde = new MySqlCommand(cmd, con);
                    MySqlDataReader dr = cmde.ExecuteReader();
                    while (dr.Read())
                    {
                        //===========================================================================
                        PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["Matricule"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell4.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["nomEleve"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell5.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell6.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell7 = new PdfPCell(new Paragraph(@Convert.ToString(dr["tranche3"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell7.BackgroundColor = BaseColor.WHITE;

                        //============================================================================

                        table.AddCell(cell4);
                        table.AddCell(cell5);
                        table.AddCell(cell6);
                        table.AddCell(cell7);

                    }
                    con.Close();

                    //Prélever le total payé en Unité
                    MySqlConnection conN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    conN.Open();
                    MySqlCommand cmdN = new MySqlCommand("SELECT SUM(situation_paye.tranche3) as Total FROM situation_paye,frais_scolaire WHERE situation_paye.idFrais=frais_scolaire.idfrais AND situation_paye.tranche3=frais_scolaire.tranche3 AND situation_paye.idFrais='" + txtIdFrais.Text + "' AND frais_scolaire.classe='" + txtIdClasse.Text + "' AND frais_scolaire.optionConcerne='" + txtIdOption.Text + "' AND situation_paye.anneeScolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "'", conN);
                    MySqlDataReader drN = cmdN.ExecuteReader();
                    Chunk ck;
                    Paragraph pr = new Paragraph();
                    while (drN.Read())
                    {
                        ck = new Chunk("                                                                                  Total payé =  " + @Convert.ToString(drN["Total"]) + "" + txtUnite.Text);
                        pr = new Paragraph(ck);

                    }
                    conN.Close();

                    dc.Add(table);
                    dc.Add(pr);
                    dc.Add(new Paragraph("\n"));
                    dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                    dc.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EnOrdre3emeTranche.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
                dc.Close();
            }

            txtMessage.Text = "Les payements effectués au " + txtEcole.SelectedValue + " / " + txtClasse.SelectedValue + " - " + txtOption.SelectedValue + " / " + txtFrais.SelectedValue + "-" + txtTranche.SelectedValue;
        }
        public void NeSontPasEnOrdre()
        {
            if (txtTranche.SelectedValue == "Tranche1")
            {
                //Pas en ordre avec la 1ere tranche
                Random rnd = new Random();
                Document dc = new Document();
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    PdfWriter.GetInstance(dc, ms);
                    dc.Open();
                    //Le code
                    dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Ne sont pas en ordres avec les payements en  : " + (txtFrais.SelectedValue).ToUpper() + ", " + (txtTranche.SelectedValue).ToUpper() + "/ " + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Classe de               : " + (txtClasse.SelectedValue) + " " + (txtOption.SelectedValue) + " Au Niveau :" + (txtEcole.SelectedValue) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Prévue à payer en (" + (txtUnite.Text) + ") : Tranche1=" + (txtT1.Text) + " ,Tranche2= " + (txtT2.Text) + " ,Tranche1= " + (txtT3.Text) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                    PdfPTable table = new PdfPTable(4);
                    PdfPCell cell = new PdfPCell(new Paragraph("Matricule", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell1 = new PdfPCell(new Paragraph("Nom & PostNom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell2 = new PdfPCell(new Paragraph("Prénom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell3 = new PdfPCell(new Paragraph("Montant payé", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.BackgroundColor = BaseColor.BLACK;

                    table.AddCell(cell);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);

                    MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                    con.Open();
                    string cmd = "SELECT DISTINCT situation_paye.Matricule,change_classe.nomEleve, change_classe.prenom,situation_paye.tranche1,situation_paye.tranche2,situation_paye.tranche3 FROM change_classe,situation_paye WHERE situation_paye.Matricule=change_classe.matricule AND situation_paye.idfrais='" + txtIdFrais.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND situation_paye.anneescolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "' AND situation_paye.tranche1<'" + txtT1.Text + "'";
                    MySqlCommand cmde = new MySqlCommand(cmd, con);
                    MySqlDataReader dr = cmde.ExecuteReader();
                    while (dr.Read())
                    {
                        //===========================================================================
                        PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["Matricule"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell4.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["nomEleve"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell5.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell6.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell7 = new PdfPCell(new Paragraph(@Convert.ToString(dr["tranche1"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell7.BackgroundColor = BaseColor.WHITE;

                        //============================================================================

                        table.AddCell(cell4);
                        table.AddCell(cell5);
                        table.AddCell(cell6);
                        table.AddCell(cell7);

                    }
                    con.Close();

                    //Prélever le total payé en Unité
                    MySqlConnection conN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    conN.Open();
                    MySqlCommand cmdN = new MySqlCommand("SELECT SUM(situation_paye.tranche1) as Total FROM situation_paye,frais_scolaire WHERE situation_paye.idFrais=frais_scolaire.idfrais AND situation_paye.tranche1<>frais_scolaire.tranche1 AND situation_paye.idFrais='" + txtIdFrais.Text + "' AND frais_scolaire.classe='" + txtIdClasse.Text + "' AND frais_scolaire.optionConcerne='" + txtIdOption.Text + "' AND situation_paye.anneeScolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "'", conN);
                    MySqlDataReader drN = cmdN.ExecuteReader();
                    Chunk ck;
                    Paragraph pr = new Paragraph();
                    while (drN.Read())
                    {
                        ck = new Chunk("                                                                                  Total payé =  " + @Convert.ToString(drN["Total"]) + "" + txtUnite.Text);
                        pr = new Paragraph(ck);

                    }
                    conN.Close();

                    dc.Add(table);
                    dc.Add(pr);
                    dc.Add(new Paragraph("\n"));
                    dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                    dc.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PasEnOrdre1ereTranche.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
                dc.Close();
            }
            if (txtTranche.SelectedValue == "Tranche2")
            {
                //Pas en ordre avec la 2e tranche
                Random rnd = new Random();
                Document dc = new Document();
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    PdfWriter.GetInstance(dc, ms);
                    dc.Open();
                    //Le code
                    dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Ne sont poas en ordres avec les payements en  : " + (txtFrais.SelectedValue).ToUpper() + ", " + (txtTranche.SelectedValue).ToUpper() + " /" + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Classe de                 : " + (txtClasse.SelectedValue) + " " + (txtOption.SelectedValue) + " Au Niveau :" + (txtEcole.SelectedValue) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Prévue à payer en (" + (txtUnite.Text) + ") : Tranche1=" + (txtT1.Text) + " ,Tranche2= " + (txtT2.Text) + " ,Tranche1= " + (txtT3.Text) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                    PdfPTable table = new PdfPTable(4);
                    PdfPCell cell = new PdfPCell(new Paragraph("Matricule", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell1 = new PdfPCell(new Paragraph("Nom & PostNom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell2 = new PdfPCell(new Paragraph("Prénom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell3 = new PdfPCell(new Paragraph("Montant payé", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.BackgroundColor = BaseColor.BLACK;

                    table.AddCell(cell);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);

                    MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                    con.Open();
                    string cmd = "SELECT DISTINCT situation_paye.Matricule,change_classe.nomEleve, change_classe.prenom,situation_paye.tranche1,situation_paye.tranche2,situation_paye.tranche3 FROM change_classe,situation_paye WHERE situation_paye.Matricule=change_classe.matricule AND situation_paye.idfrais='" + txtIdFrais.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND situation_paye.anneescolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "' AND situation_paye.tranche2<'" + txtT2.Text + "'";
                    MySqlCommand cmde = new MySqlCommand(cmd, con);
                    MySqlDataReader dr = cmde.ExecuteReader();
                    while (dr.Read())
                    {
                        //===========================================================================
                        PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["Matricule"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell4.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["nomEleve"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell5.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell6.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell7 = new PdfPCell(new Paragraph(@Convert.ToString(dr["tranche2"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell7.BackgroundColor = BaseColor.WHITE;

                        //============================================================================

                        table.AddCell(cell4);
                        table.AddCell(cell5);
                        table.AddCell(cell6);
                        table.AddCell(cell7);

                    }
                    con.Close();

                    //Prélever le total payé en Unité
                    MySqlConnection conN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    conN.Open();
                    MySqlCommand cmdN = new MySqlCommand("SELECT SUM(situation_paye.tranche2) as Total FROM situation_paye,frais_scolaire WHERE situation_paye.idFrais=frais_scolaire.idfrais AND situation_paye.tranche2<>frais_scolaire.tranche2 AND situation_paye.idFrais='" + txtIdFrais.Text + "' AND frais_scolaire.classe='" + txtIdClasse.Text + "' AND frais_scolaire.optionConcerne='" + txtIdOption.Text + "' AND situation_paye.anneeScolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "'", conN);
                    MySqlDataReader drN = cmdN.ExecuteReader();
                    Chunk ck;
                    Paragraph pr = new Paragraph();
                    while (drN.Read())
                    {
                        ck = new Chunk("                                                                                  Total payé =  " + @Convert.ToString(drN["Total"]) + "" + txtUnite.Text);
                        pr = new Paragraph(ck);

                    }
                    conN.Close();

                    dc.Add(table);
                    dc.Add(pr);
                    dc.Add(new Paragraph("\n"));
                    dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                    dc.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PasEnOrdre2emeTranche.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
                dc.Close();
            }
            if (txtTranche.SelectedValue == "Tranche3")
            {
                //Pas en ordre avec la 3e tranche
                Random rnd = new Random();
                Document dc = new Document();
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    PdfWriter.GetInstance(dc, ms);
                    dc.Open();
                    //Le code
                    dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Ne sont pas en ordres avec les payements en  : " + (txtFrais.SelectedValue).ToUpper() + ", " + (txtTranche.SelectedValue).ToUpper() + " /" + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Classe de               : " + (txtClasse.SelectedValue) + " " + (txtOption.SelectedValue) + " Au Niveau :" + (txtEcole.SelectedValue) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                    dc.Add(new Paragraph("              Prévue à payer en (" + (txtUnite.Text) + ") : Tranche1=" + (txtT1.Text) + " ,Tranche2= " + (txtT2.Text) + " ,Tranche1= " + (txtT3.Text) + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                    PdfPTable table = new PdfPTable(4);
                    PdfPCell cell = new PdfPCell(new Paragraph("Matricule", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell1 = new PdfPCell(new Paragraph("Nom & PostNom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell1.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell2 = new PdfPCell(new Paragraph("Prénom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell2.BackgroundColor = BaseColor.BLACK;
                    PdfPCell cell3 = new PdfPCell(new Paragraph("Montant payé", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell3.BackgroundColor = BaseColor.BLACK;

                    table.AddCell(cell);
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);

                    MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                    con.Open();
                    string cmd = "SELECT DISTINCT situation_paye.Matricule,change_classe.nomEleve, change_classe.prenom,situation_paye.tranche1,situation_paye.tranche2,situation_paye.tranche3 FROM change_classe,situation_paye WHERE situation_paye.Matricule=change_classe.matricule AND situation_paye.idfrais='" + txtIdFrais.Text + "' AND change_classe.classe='" + txtIdClasse.Text + "' AND change_classe.optionEtude='" + txtIdOption.Text + "' AND situation_paye.anneescolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "' AND situation_paye.tranche3<'" + txtT3.Text + "'";
                    MySqlCommand cmde = new MySqlCommand(cmd, con);
                    MySqlDataReader dr = cmde.ExecuteReader();
                    while (dr.Read())
                    {
                        //===========================================================================
                        PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["Matricule"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell4.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["nomEleve"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell5.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell6.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell7 = new PdfPCell(new Paragraph(@Convert.ToString(dr["tranche3"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                        cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell7.BackgroundColor = BaseColor.WHITE;

                        //============================================================================

                        table.AddCell(cell4);
                        table.AddCell(cell5);
                        table.AddCell(cell6);
                        table.AddCell(cell7);

                    }
                    con.Close();

                    //Prélever le total payé en Unité
                    MySqlConnection conN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    conN.Open();
                    MySqlCommand cmdN = new MySqlCommand("SELECT SUM(situation_paye.tranche3) as Total FROM situation_paye,frais_scolaire WHERE situation_paye.idFrais=frais_scolaire.idfrais AND situation_paye.tranche3<>frais_scolaire.tranche2 AND situation_paye.idFrais='" + txtIdFrais.Text + "' AND frais_scolaire.classe='" + txtIdClasse.Text + "' AND frais_scolaire.optionConcerne='" + txtIdOption.Text + "' AND situation_paye.anneeScolaire='" + txtIdAnnee.Text + "' AND situation_paye.idEcole='" + txtIdEcole.Text + "'", conN);
                    MySqlDataReader drN = cmdN.ExecuteReader();
                    Chunk ck;
                    Paragraph pr = new Paragraph();
                    while (drN.Read())
                    {
                        ck = new Chunk("                                                                                  Total payé =  " + @Convert.ToString(drN["Total"]) + "" + txtUnite.Text);
                        pr = new Paragraph(ck);

                    }
                    conN.Close();

                    dc.Add(table);
                    dc.Add(pr);
                    dc.Add(new Paragraph("\n"));
                    dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                    dc.Close();
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PasEnOrdre3emeTranche.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
                dc.Close();
            }
            txtMessage.Text = "Les payements effectués au " + txtEcole.SelectedValue + " / " + txtClasse.SelectedValue + " - " + txtOption.SelectedValue + " / " + txtFrais.SelectedValue + "-" + txtTranche.SelectedValue;
        }
        public void TrouverIdEcole()
        {
            con.Close();
            con.Open();
            if (txtIdEcoleAffectationUser.Text == "Toutes les écoles" || txtRole.Text == "Comptable")
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
        public void TrouverFrais()
        {
            con.Open();
            txtFrais.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from frais_scolaire WHERE classe='" + txtIdClasse.Text + "' AND optionConcerne='" + txtIdOption.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "' ORDER BY designation ASC");
            MySqlDataReader dr = cmdB.ExecuteReader();
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
            }
            con.Close();
        }
        public void TrouverEleve()
        {
            con.Open();
            txtNomELeve.Items.Clear();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from change_classe WHERE classe='" + txtIdClasse.Text + "' AND optionEtude='" + txtIdOption.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "' AND etat='Actif' ORDER BY nomEleve ASC");
            MySqlDataReader dr = cmdB.ExecuteReader();
            txtNomELeve.Items.Add("--Sélectionnez un élève pour lequel vous voulez visualiser le compte--");
            while (dr.Read())
            {
                txtNomELeve.Items.Add(dr["nomEleve"].ToString());
            }
            con.Close();
        }
        public void TrouverMatriculeEleve()
        {
            con.Open();
            MySqlCommand cmdB = con.CreateCommand();
            cmdB.CommandType = CommandType.Text;
            cmdB.CommandText = ("SELECT *from change_classe WHERE classe='" + txtIdClasse.Text + "' AND optionEtude='" + txtIdOption.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "' AND etat='Actif' AND nomEleve='" + txtNomELeve.SelectedValue + "'");
            MySqlDataReader dr = cmdB.ExecuteReader();
            while (dr.Read())
            {
                txtMatricule.Text = dr["matricule"].ToString();
                txtPrenomEleve.Text = dr["prenom"].ToString();
            }
            con.Close();
        }
        public void afficherParDate(string afficherParDate)
        {

            //Payement par date
            Random rnd = new Random();
            Document dc = new Document();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                PdfWriter.GetInstance(dc, ms);
                dc.Open();
                //Le code
                dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                           La paie des élèves contenant votre recherche de : " + (txtRecherche.Text).ToUpper() + " /Année :" + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.BLACK)));


                PdfPTable table = new PdfPTable(8);
                PdfPCell cell = new PdfPCell(new Paragraph("Date", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell1 = new PdfPCell(new Paragraph("Nom & postNom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell2 = new PdfPCell(new Paragraph("Prenom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3 = new PdfPCell(new Paragraph("Classe", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell4 = new PdfPCell(new Paragraph("Option", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                cell4.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell5 = new PdfPCell(new Paragraph("Frais Payé", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                cell5.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell6 = new PdfPCell(new Paragraph("Montant", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                cell6.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell7 = new PdfPCell(new Paragraph("Unité", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                cell7.BackgroundColor = BaseColor.BLACK;

                table.AddCell(cell);
                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
                table.AddCell(cell4);
                table.AddCell(cell5);
                table.AddCell(cell6);
                table.AddCell(cell7);

                if (txtRecherche.Text == "")
                {
                    //Message erreur
                }
                else
                {
                    MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                    con.Open();
                    string cmd = "SELECT t_payement_frais.date_payement as date_payement,frais_scolaire.designation as DFrais,t_payement_frais.montant_payer as montantPaye,t_payement_frais.unite as unite,change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole FROM t_payement_frais,frais_scolaire,change_classe,t_classe,section,ecole WHERE t_payement_frais.motif=frais_scolaire.idfrais AND t_payement_frais.matricule=change_classe.matricule AND t_payement_frais.anneescolaire=change_classe.anneeScolaire AND t_payement_frais.idEcole=change_classe.idEcole AND change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND etat='Actif' AND CONCAT(change_classe.matricule,change_classe.nomEleve,change_classe.prenom,t_classe.classe,section.nomSection,ecole.nomEcole,t_payement_frais.date_payement,frais_scolaire.designation) LIKE '%" + afficherParDate + "%' ORDER BY date_payement ASC";
                    MySqlCommand cmde = new MySqlCommand(cmd, con);
                    MySqlDataReader dr = cmde.ExecuteReader();
                    while (dr.Read())
                    {
                        //===========================================================================
                        PdfPCell cell8 = new PdfPCell(new Paragraph(@Convert.ToString(dr["date_payement"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                        cell8.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell8.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell9 = new PdfPCell(new Paragraph((string)dr["nom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                        cell9.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell9.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell10 = new PdfPCell(new Paragraph((string)dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                        cell10.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell10.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell11 = new PdfPCell(new Paragraph((string)dr["classe"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                        cell11.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell11.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell12 = new PdfPCell(new Paragraph(@Convert.ToString(dr["option"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                        cell12.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell12.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell13 = new PdfPCell(new Paragraph((string)dr["DFrais"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                        cell13.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell13.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell14 = new PdfPCell(new Paragraph((string)dr["montantPaye"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                        cell14.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell14.BackgroundColor = BaseColor.WHITE;
                        PdfPCell cell15 = new PdfPCell(new Paragraph((string)dr["unite"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                        cell15.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell15.BackgroundColor = BaseColor.WHITE;

                        //============================================================================

                        table.AddCell(cell8);
                        table.AddCell(cell9);
                        table.AddCell(cell10);
                        table.AddCell(cell11);
                        table.AddCell(cell12);
                        table.AddCell(cell13);
                        table.AddCell(cell14);
                        table.AddCell(cell15);

                    }

                    //Prélever le total payé en USD
                    MySqlConnection conN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    conN.Open();
                    MySqlCommand cmdN = new MySqlCommand("select sum(montant_payer) as Total FROM t_payement_frais,frais_scolaire,change_classe,t_classe,section,ecole WHERE t_payement_frais.motif=frais_scolaire.idfrais AND t_payement_frais.matricule=change_classe.matricule AND t_payement_frais.anneescolaire=change_classe.anneeScolaire AND t_payement_frais.idEcole=change_classe.idEcole AND change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND etat='Actif' AND CONCAT(change_classe.matricule,change_classe.nomEleve,change_classe.prenom,t_classe.classe,section.nomSection,ecole.nomEcole,t_payement_frais.date_payement,frais_scolaire.designation) LIKE '%" + afficherParDate + "%' AND t_payement_frais.unite='USD' ORDER BY date_payement ASC", conN);
                    MySqlDataReader drN = cmdN.ExecuteReader();
                    Chunk ck;
                    Paragraph pr = new Paragraph();
                    while (drN.Read())
                    {
                        ck = new Chunk("                                                                                  Total payé en Dollars=  " + @Convert.ToString(drN["Total"]) + "$");
                        pr = new Paragraph(ck);

                        table.AddCell(new Paragraph(@Convert.ToString(drN["Total"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLUE)));
                        PdfPCell cell16 = new PdfPCell(new Paragraph(@Convert.ToString(drN["Total"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLUE)));
                        cell16.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell16.BackgroundColor = BaseColor.GREEN;
                        table.AddCell(cell16);
                    }
                    conN.Close();

                    //Prélever le total payé en CDF
                    MySqlConnection conNN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                    conNN.Open();
                    MySqlCommand cmdNN = new MySqlCommand("select sum(montant_payer) as Total FROM t_payement_frais,frais_scolaire,change_classe,t_classe,section,ecole WHERE t_payement_frais.motif=frais_scolaire.idfrais AND t_payement_frais.matricule=change_classe.matricule AND t_payement_frais.anneescolaire=change_classe.anneeScolaire AND t_payement_frais.idEcole=change_classe.idEcole AND change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND etat='Actif' AND CONCAT(change_classe.matricule,change_classe.nomEleve,change_classe.prenom,t_classe.classe,section.nomSection,ecole.nomEcole,t_payement_frais.date_payement,frais_scolaire.designation) LIKE '%" + afficherParDate + "%' AND t_payement_frais.unite='CDF'", conNN);
                    MySqlDataReader drNN = cmdNN.ExecuteReader();
                    Chunk ckK;
                    Paragraph prR = new Paragraph();
                    while (drNN.Read())
                    {
                        ckK = new Chunk("                                                                                  Total payé en Francs =  " + @Convert.ToString(drNN["Total"]) + "CDF");
                        prR = new Paragraph(ckK);

                        table.AddCell(new Paragraph(@Convert.ToString(drNN["Total"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLUE)));
                        PdfPCell cell17 = new PdfPCell(new Paragraph(@Convert.ToString(drNN["Total"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLUE)));
                        cell17.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell17.BackgroundColor = BaseColor.GREEN;
                        table.AddCell(cell17);
                    }

                    dc.Add(table);
                    dc.Add(pr);
                    dc.Add(prR);
                    dc.Add(new Paragraph("\n"));
                    dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                    dc.Close();

                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PayementParDate.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
            }
            dc.Close();

        }
        public void IntervalleDatePDF()
        {
            //Payement par date
            AfficherParIntervalleDate();
            string d1, d2;
            d1 = DateTime.Parse(txtDate1.Text).ToShortDateString();
            d2 = DateTime.Parse(txtDate2.Text).ToShortDateString();
            Random rnd = new Random();
            Document dc = new Document();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                PdfWriter.GetInstance(dc, ms);
                dc.Open();
                //Le code
                dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                           Le payement par intervalle des dates : " + (txtDate1.Text).ToUpper() + " et " + txtDate2.Text + " /Année : " + (txtDesignationAnnee.Text).ToUpper() + " \n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.BLACK)));


                PdfPTable table = new PdfPTable(8);
                PdfPCell cell = new PdfPCell(new Paragraph("Date", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell1 = new PdfPCell(new Paragraph("Nom & postNom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell2 = new PdfPCell(new Paragraph("Prenom", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3 = new PdfPCell(new Paragraph("Classe", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell4 = new PdfPCell(new Paragraph("Option", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                cell4.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell5 = new PdfPCell(new Paragraph("Frais Payé", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                cell5.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell6 = new PdfPCell(new Paragraph("Montant", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                cell6.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell7 = new PdfPCell(new Paragraph("Unité", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.WHITE)));
                cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                cell7.BackgroundColor = BaseColor.BLACK;

                table.AddCell(cell);
                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
                table.AddCell(cell4);
                table.AddCell(cell5);
                table.AddCell(cell6);
                table.AddCell(cell7);

                MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                con.Open();
                string cmd = "SELECT t_payement_frais.date_payement as date_payement,frais_scolaire.designation as DFrais,t_payement_frais.montant_payer as montantPaye,t_payement_frais.unite as unite,change_classe.idClasse as idClasse,change_classe.matricule as matricule,change_classe.nomEleve as nom,change_classe.prenom as prenom,change_classe.sexe as sexe,t_classe.classe as classe,section.nomSection as option,ecole.nomEcole as idEcole FROM t_payement_frais,frais_scolaire,change_classe,t_classe,section,ecole WHERE t_payement_frais.motif=frais_scolaire.idfrais AND t_payement_frais.matricule=change_classe.matricule AND t_payement_frais.anneescolaire=change_classe.anneeScolaire AND t_payement_frais.anneescolaire='" + txtIdAnnee.Text + "' AND t_payement_frais.idEcole=change_classe.idEcole AND change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND t_payement_frais.date_payement BETWEEN '" + d1 + "' AND '" + d2 + "' ORDER BY date_payement ASC";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    //===========================================================================
                    PdfPCell cell8 = new PdfPCell(new Paragraph(@Convert.ToString(dr["date_payement"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                    cell8.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell8.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell9 = new PdfPCell(new Paragraph((string)dr["nom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                    cell9.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell9.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell10 = new PdfPCell(new Paragraph((string)dr["prenom"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                    cell10.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell10.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell11 = new PdfPCell(new Paragraph((string)dr["classe"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                    cell11.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell11.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell12 = new PdfPCell(new Paragraph(@Convert.ToString(dr["option"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                    cell12.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell12.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell13 = new PdfPCell(new Paragraph((string)dr["DFrais"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                    cell13.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell13.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell14 = new PdfPCell(new Paragraph((string)dr["montantPaye"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                    cell14.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell14.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell15 = new PdfPCell(new Paragraph((string)dr["unite"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK)));
                    cell15.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell15.BackgroundColor = BaseColor.WHITE;

                    //============================================================================

                    table.AddCell(cell8);
                    table.AddCell(cell9);
                    table.AddCell(cell10);
                    table.AddCell(cell11);
                    table.AddCell(cell12);
                    table.AddCell(cell13);
                    table.AddCell(cell14);
                    table.AddCell(cell15);

                }

                //Prélever le total payé en USD
                MySqlConnection conN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                conN.Open();
                MySqlCommand cmdN = new MySqlCommand("Select sum(montant_payer) as Total FROM t_payement_frais,frais_scolaire,change_classe,t_classe,section,ecole WHERE t_payement_frais.motif=frais_scolaire.idfrais AND t_payement_frais.matricule=change_classe.matricule AND t_payement_frais.anneescolaire=change_classe.anneeScolaire AND t_payement_frais.anneescolaire='" + txtIdAnnee.Text + "' AND t_payement_frais.idEcole=change_classe.idEcole AND change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND t_payement_frais.date_payement BETWEEN '" + d1 + "' AND '" + d2 + "' AND t_payement_frais.unite='USD' ORDER BY date_payement ASC", conN);
                MySqlDataReader drN = cmdN.ExecuteReader();
                Chunk ck;
                Paragraph pr = new Paragraph();
                while (drN.Read())
                {
                    ck = new Chunk("                                                                                  Total payé en Dollars=  " + @Convert.ToString(drN["Total"]) + "$");
                    pr = new Paragraph(ck);

                    table.AddCell(new Paragraph(@Convert.ToString(drN["Total"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLUE)));
                    PdfPCell cell16 = new PdfPCell(new Paragraph(@Convert.ToString(drN["Total"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLUE)));
                    cell16.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell16.BackgroundColor = BaseColor.GREEN;
                    table.AddCell(cell16);
                }
                conN.Close();

                //Prélever le total payé en CDF
                MySqlConnection conNN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                conNN.Open();
                MySqlCommand cmdNN = new MySqlCommand("Select sum(montant_payer) as Total FROM t_payement_frais,frais_scolaire,change_classe,t_classe,section,ecole WHERE t_payement_frais.motif=frais_scolaire.idfrais AND t_payement_frais.matricule=change_classe.matricule AND t_payement_frais.anneescolaire=change_classe.anneeScolaire AND t_payement_frais.anneescolaire='" + txtIdAnnee.Text + "' AND t_payement_frais.idEcole=change_classe.idEcole AND change_classe.classe=t_classe.id and change_classe.optionEtude=section.idSection AND change_classe.idEcole=ecole.idEcole AND etat='Actif' AND t_payement_frais.date_payement BETWEEN '" + d1 + "' AND '" + d2 + "' AND t_payement_frais.unite='CDF'", conNN);
                MySqlDataReader drNN = cmdNN.ExecuteReader();
                Chunk ckK;
                Paragraph prR = new Paragraph();
                while (drNN.Read())
                {
                    ckK = new Chunk("                                                                                  Total payé en Francs =  " + @Convert.ToString(drNN["Total"]) + "CDF");
                    prR = new Paragraph(ckK);

                    table.AddCell(new Paragraph(@Convert.ToString(drNN["Total"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLUE)));
                    PdfPCell cell17 = new PdfPCell(new Paragraph(@Convert.ToString(drNN["Total"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLUE)));
                    cell17.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell17.BackgroundColor = BaseColor.GREEN;
                    table.AddCell(cell17);
                }

                dc.Add(table);
                dc.Add(pr);
                dc.Add(prR);
                dc.Add(new Paragraph("\n"));
                dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));
                dc.Close();

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=PayementEleveParIntervalleDate.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
            dc.Close();

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
            AfficherToutPayement();
            TrouverFrais();
            TrouverIdFrais();
            TrouverEleve();
            TrouverMatriculeEleve();
        }
        protected void txtIdEcole_TextChanged(object sender, EventArgs e)
        {
            TrouverSection();
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
            AfficherToutPayement();
            TrouverFrais();
            TrouverIdFrais();
            TrouverEleve();
            TrouverMatriculeEleve();
        }

        protected void txtOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdSection();
            TrouverClasser();
            TrouverIdClasse();
            AfficherToutPayement();
            TrouverFrais();
            TrouverIdFrais();
            TrouverEleve();
            TrouverMatriculeEleve();
        }

        protected void txtClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdClasse();
            AfficherToutPayement();
            TrouverFrais();
            TrouverIdFrais();
            TrouverEleve();
            TrouverMatriculeEleve();
        }

        protected void DataGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void txtRecherche_TextChanged(object sender, EventArgs e)
        {
            recherche(txtRecherche.Text);
        }

        protected void btnOrdre_CheckedChanged(object sender, EventArgs e)
        {
            btnOrdre.Checked = true;
            btnPasenOrdre.Checked = false;

        }

        protected void btnPasenOrdre_CheckedChanged(object sender, EventArgs e)
        {
            btnOrdre.Checked = false;
            btnPasenOrdre.Checked = true;
        }
        protected void txtFrais_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverIdFrais();
        }

        protected void btnRechApproFondie_Click(object sender, EventArgs e)
        {
            try
            {
                recherche(txtRecherche.Text);
                afficherParDate(txtRecherche.Text);
            }
            catch { }
            

        }

        protected void btnRechIntervalle_Click(object sender, EventArgs e)
        {
            try
            {
                AfficherParIntervalleDate();
                IntervalleDatePDF();
            }
            catch { }
            
        }

        protected void btnRechEnOrdreEtPasEnOrdre_Click(object sender, EventArgs e)
        {
            if (btnOrdre.Checked == true)
            {
                SontEnOrdre();
            }
            if (btnPasenOrdre.Checked == true)
            {
                NeSontPasEnOrdre();
            }
        }
        protected void txtNomELeve_SelectedIndexChanged(object sender, EventArgs e)
        {
            TrouverMatriculeEleve();
        }
        protected void btnCompteELeve_Click(object sender, EventArgs e)
        {
            //tous les payements faits par un élève
            Random rnd = new Random();
            Document dc = new Document();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                PdfWriter.GetInstance(dc, ms);
                dc.Open();
                //Le code
                dc.Open();
                dc.Add(new Paragraph("                                                      REPUBLIQUE DEMOCRATIQUE DU CONGO \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                      MINISTERE DE L'ENSEIGNEMENT PRIMAIRE,SECONDAIRE ET TECHNIQUE \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                                           COMPLEXE SCOLAIRE NAOMI \n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                                                        Province du Sud-Kivu\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                               Arrêté MINISTERIEL No MINEPSP/CAB MIN/086/2006\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                                                                          Contacts :  0971368721\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph("                    ------------------------------------------------------------------------------------------------------------------\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph(" Rélevé des payements faits par l'élève pour l'année Scolaire : " + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph(" Nom & PostNom de l'élève : " + (txtNomELeve.SelectedValue).ToUpper() + "\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                dc.Add(new Paragraph(" Prénom de l'élève                : " + (txtPrenomEleve.Text).ToUpper() + "\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, BaseColor.BLACK)));
                dc.Add(new Paragraph(" Classe de l'élève                : " + (txtClasse.SelectedValue).ToUpper() + " " + (txtOption.SelectedValue).ToUpper() + " / Niveau : " + (txtEcole.SelectedValue).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                dc.Add(new Paragraph(" Structuration des frais Scolaires dans votre classe à l'année: " + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));

                PdfPTable tableF = new PdfPTable(5);
                PdfPCell cellF = new PdfPCell(new Paragraph("Désignation du frais", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cellF.HorizontalAlignment = Element.ALIGN_CENTER;
                cellF.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell1F = new PdfPCell(new Paragraph("Tranche1", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell1F.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1F.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell2F = new PdfPCell(new Paragraph("Tranche 2", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell2F.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2F.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3F = new PdfPCell(new Paragraph("Tranche3", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell3F.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3F.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell4F = new PdfPCell(new Paragraph("Unité Monétaire", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell4F.HorizontalAlignment = Element.ALIGN_CENTER;
                cell4F.BackgroundColor = BaseColor.BLACK;

                tableF.AddCell(cellF);
                tableF.AddCell(cell1F);
                tableF.AddCell(cell2F);
                tableF.AddCell(cell3F);
                tableF.AddCell(cell4F);

                MySqlConnection conF = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                conF.Open();
                string cmdF = "SELECT *from frais_scolaire WHERE classe='" + txtIdClasse.Text + "' AND optionConcerne='" + txtIdOption.Text + "' AND anneeScolaire='" + txtIdAnnee.Text + "' AND idEcole='" + txtIdEcole.Text + "' ORDER BY unite ASC";
                MySqlCommand cmdeF = new MySqlCommand(cmdF, conF);
                MySqlDataReader drF = cmdeF.ExecuteReader();
                while (drF.Read())
                {
                    //===========================================================================
                    PdfPCell cell41F = new PdfPCell(new Paragraph(@Convert.ToString(drF["designation"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell41F.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell41F.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell5F = new PdfPCell(new Paragraph((string)drF["tranche1"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell5F.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell5F.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell6F = new PdfPCell(new Paragraph((string)drF["tranche2"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell6F.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell6F.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7F = new PdfPCell(new Paragraph((string)drF["tranche3"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7F.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7F.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell8F = new PdfPCell(new Paragraph((string)drF["unite"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell8F.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell8F.BackgroundColor = BaseColor.WHITE;

                    //============================================================================

                    tableF.AddCell(cell41F);
                    tableF.AddCell(cell5F);
                    tableF.AddCell(cell6F);
                    tableF.AddCell(cell7F);
                    tableF.AddCell(cell8F);

                }
                conF.Close();
                //Prélever le total prevu en USD
                MySqlConnection conNF = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                conNF.Open();
                MySqlCommand cmdNF = new MySqlCommand("select sum(tranche1+tranche2+tranche3) as Total FROM frais_scolaire WHERE frais_scolaire.unite='USD' AND frais_scolaire.classe='" + txtIdClasse.Text + "' AND frais_scolaire.optionConcerne='" + txtIdOption.Text + "' AND frais_scolaire.anneescolaire='" + txtIdAnnee.Text + "'AND frais_scolaire.idEcole='" + txtIdEcole.Text + "'", conNF);
                MySqlDataReader drNF = cmdNF.ExecuteReader();
                Chunk ckF;
                Paragraph prF = new Paragraph();
                while (drNF.Read())
                {
                    ckF = new Chunk("                                                                                  Total Prévu en Dollars=  " + @Convert.ToString(drNF["Total"]) + "$");
                    prF = new Paragraph(ckF);

                }
                conNF.Close();

                //Prélever le total prevu en CDF
                MySqlConnection conNF1 = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                conNF1.Open();
                MySqlCommand cmdNF1 = new MySqlCommand("select sum(tranche1+tranche2+tranche3) as Total FROM frais_scolaire WHERE frais_scolaire.unite='CDF' AND frais_scolaire.classe='" + txtIdClasse.Text + "' AND frais_scolaire.optionConcerne='" + txtIdOption.Text + "' AND frais_scolaire.anneescolaire='" + txtIdAnnee.Text + "'AND frais_scolaire.idEcole='" + txtIdEcole.Text + "'", conNF1);
                MySqlDataReader drNF1 = cmdNF1.ExecuteReader();
                Chunk ckF1;
                Paragraph prF1 = new Paragraph();
                while (drNF1.Read())
                {
                    ckF1 = new Chunk("                                                                                  Total prévu en FC =  " + @Convert.ToString(drNF1["Total"]) + "CDF");
                    prF1 = new Paragraph(ckF1);

                }
                conNF1.Close();

                dc.Add(tableF);
                dc.Add(prF);
                dc.Add(prF1);

                ///Autre tableau
                dc.Add(new Paragraph("\n" + " Les payements faits par l'élève à l'année " + (txtDesignationAnnee.Text).ToUpper() + "\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, BaseColor.BLACK)));
                PdfPTable table = new PdfPTable(4);
                PdfPCell cell = new PdfPCell(new Paragraph("Date payement", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell1 = new PdfPCell(new Paragraph("Frais payé", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                cell1.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell2 = new PdfPCell(new Paragraph("Montant payé", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                cell2.BackgroundColor = BaseColor.BLACK;
                PdfPCell cell3 = new PdfPCell(new Paragraph("Unité Monétaire", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.WHITE)));
                cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                cell3.BackgroundColor = BaseColor.BLACK;

                table.AddCell(cell);
                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);

                MySqlConnection con = new MySqlConnection("server=localhost; uid=root; database= gestion_naomi; password=");
                con.Open();
                string cmd = "SELECT t_payement_frais.date_payement,frais_scolaire.designation,t_payement_frais.montant_payer,frais_scolaire.unite FROM t_payement_frais,change_classe,frais_scolaire WHERE t_payement_frais.matricule='" + txtMatricule.Text + "' AND t_payement_frais.motif=frais_scolaire.idfrais AND t_payement_frais.matricule=change_classe.matricule AND t_payement_frais.anneescolaire=change_classe.anneeScolaire AND t_payement_frais.anneescolaire='" + txtIdAnnee.Text + "' AND t_payement_frais.idEcole='" + txtIdEcole.Text + "' ";
                MySqlCommand cmde = new MySqlCommand(cmd, con);
                MySqlDataReader dr = cmde.ExecuteReader();
                while (dr.Read())
                {
                    //===========================================================================
                    PdfPCell cell4 = new PdfPCell(new Paragraph(@Convert.ToString(dr["date_payement"]), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell4.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell5 = new PdfPCell(new Paragraph((string)dr["designation"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell5.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell5.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell6 = new PdfPCell(new Paragraph((string)dr["montant_payer"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell6.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell6.BackgroundColor = BaseColor.WHITE;
                    PdfPCell cell7 = new PdfPCell(new Paragraph((string)dr["unite"], FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK)));
                    cell7.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell7.BackgroundColor = BaseColor.WHITE;

                    //============================================================================

                    table.AddCell(cell4);
                    table.AddCell(cell5);
                    table.AddCell(cell6);
                    table.AddCell(cell7);

                }
                con.Close();

                //Prélever le total payé en USD
                MySqlConnection conN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                conN.Open();
                MySqlCommand cmdN = new MySqlCommand("select sum(montant_payer) as Total from t_payement_frais WHERE matricule='" + txtMatricule.Text + "' AND t_payement_frais.unite='USD' AND t_payement_frais.anneescolaire='" + txtIdAnnee.Text + "'AND t_payement_frais.idEcole='" + txtIdEcole.Text + "'", conN);
                MySqlDataReader drN = cmdN.ExecuteReader();
                Chunk ck;
                Paragraph pr = new Paragraph();
                while (drN.Read())
                {
                    ck = new Chunk("                                                                                  Total payé en Dollars=  " + @Convert.ToString(drN["Total"]) + "$");
                    pr = new Paragraph(ck);

                }
                conN.Close();

                //Prélever le total payé en CDF
                MySqlConnection conNN = new MySqlConnection("server=localhost;uid=root;database=gestion_naomi;password=");
                conNN.Open();
                MySqlCommand cmdNN = new MySqlCommand("select sum(montant_payer) as Total from t_payement_frais WHERE matricule='" + txtMatricule.Text + "' AND t_payement_frais.unite='CDF' AND t_payement_frais.anneescolaire='" + txtIdAnnee.Text + "'AND t_payement_frais.idEcole='" + txtIdEcole.Text + "'", conNN);
                MySqlDataReader drNN = cmdNN.ExecuteReader();
                Chunk ckK;
                Paragraph prR = new Paragraph();
                while (drNN.Read())
                {
                    ckK = new Chunk("                                                                                  Total payé en Francs =  " + @Convert.ToString(drNN["Total"]) + " CDF");
                    prR = new Paragraph(ckK);

                }

                dc.Add(table);
                dc.Add(pr);
                dc.Add(prR);
                dc.Add(new Paragraph("\n"));
                dc.Add(new Paragraph("                                                                                   Fait à Bukavu le: " + System.DateTime.Now));

                conNN.Close();
                dc.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=CompteEleve.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
            dc.Close();
        }
        
    }
}