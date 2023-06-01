using PASSWARE.Models;
using PASSWARE.Models.Entities;
using PASSWARE.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //TabControl tabControl = HomePageControl.CreateTabControlWithTabPage();
            //this.tabControl1.TabPages.AddRange(tabControl.TabPages.Cast<TabPage>().ToArray());
            HomePageControl homePageControl=new HomePageControl();
            TabPage tabPage = homePageControl.CreateTabPage();
            tabControl1.TabPages.Add(tabPage);

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView1 = new DataGridView();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Dock = DockStyle.None;
            dataGridView1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top ; 
            dataGridView1.Location = new Point(0, 190);
            //dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;

            var apiUrl = "https://localhost:44343/api/Sqls/GetAll";
            SqlController sqlController = new SqlController();
            Sql[] sqlData = await sqlController.GetSqlData(apiUrl);
            if (sqlData != null)
            {
                dataGridView1.DataSource = sqlData;
            }
            else
            {
                MessageBox.Show("Veriler alınamadı.");
            }

            // TabPage oluşturun ve DataGridView kontrolünü içine yerleştirin
            TabPage tabPage1 = new TabPage();
            tabPage1.Text = "SQl";
            tabPage1.Controls.Add(dataGridView1);
            tabControl1.TabPages.Add(tabPage1);
        }

    }
}
