using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http.Headers;

namespace AcademyDataSet
{
	public partial class MainForm : Form
	{
		Cashe GroupsRelatedData;

		public MainForm()
		{
			InitializeComponent();
			AllocConsole();

			GroupsRelatedData= new Cashe();
			GroupsRelatedData.AddTable("Directions", "direction_id,direction_name");
			GroupsRelatedData.AddTable("Groups", "group_id,group_name,direction");
			GroupsRelatedData.AddRelation("GroupsDirections", "Groups,direction", "Directions,direction_id");
			GroupsRelatedData.Load();
			GroupsRelatedData.Print("Directions");
			GroupsRelatedData.Print("Groups");

			//Загружаем направление из базы в combobox
			//направления обучения уже загружены в таблицу в dataset и эту таблицу мы указываем как источник данных
			cbDirections.DataSource = GroupsRelatedData.Set.Tables["Directions"];
			// Из множества полей таблицы нужно указать какое поле будет отображаться в выпадающем списке
			cbDirections.DisplayMember = "direction_name";
			//и какое поле будет возвращаться при выборе элемента combobox
			cbDirections.ValueMember = "direction_id";

			cbGroups.DataSource = GroupsRelatedData.Set.Tables["Groups"];
			cbGroups.DisplayMember = "group_name";
			cbGroups.ValueMember = "group_id";
		}
		
		
		
		
		
		[DllImport("kernel32.dll")]
		public static extern bool AllocConsole();
		[DllImport("kernel32.dll")]
		public static extern bool FreeConsole();

		private void cbDirections_SelectedIndexChanged(object sender, EventArgs e)
		{
			//Console.WriteLine(Set.Tables["Directions"].ChildRelations);
			//DataRow row = Set.Tables["Directions"].Rows.Find(cbDirections.SelectedValue);
			//cbGroups.DataSource = row.GetChildRows("GroupsDirections");
			//cbGroups.DisplayMember = "group_name";
			//cbGroups.ValueMember = "group_id";
			GroupsRelatedData.Set.Tables["Groups"].DefaultView.RowFilter = $"direction={cbDirections.SelectedValue}";
		}
	}
}
