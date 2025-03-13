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
	public partial class AdminChangeClasseExecute : System.Web.UI.Page
	{
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gespersonnel");
        string id;
        protected void Page_Load(object sender, EventArgs e)
		{
            id = Request.QueryString["id"].ToString();
            txtnumMat.Text = id;
        }
	}
}