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
	public partial class AdminChangeClasseExecute : System.Web.UI.Page
	{
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=; database=gestion_naomi");
        string id;
        protected void Page_Load(object sender, EventArgs e)
		{
            id = Request.QueryString["id"].ToString();
            txtnumMat.Text = id;
        }
	}
}