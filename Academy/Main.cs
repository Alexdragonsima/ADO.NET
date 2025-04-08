using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Management;
using System.Configuration;

namespace Academy
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();

			Connector connector = new Connector
				(
					ConfigurationManager.ConnectionStrings["DBMS_DDL"].ConnectionString
				);
			// dgv - dataGridview
			dgvStudents.DataSource = connector.Select("*", "Students");
		}
	}
}
