using Newtonsoft.Json;
using PASSWARE.Models;
using PASSWARE.Models.Entities;
using PASSWARE.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE.TabpageBase
{
    public class SqlTabpageList
    {
        private Dictionary<ComboBox, DataGridView> comboBoxDataGridViewPairs;
        private HttpClient client;
        private DataTable originalData; // Tüm verileri içeren orijinal DataTable

        public SqlTabpageList()
        {
            comboBoxDataGridViewPairs = new Dictionary<ComboBox, DataGridView>();
            client = new HttpClient();
        }

        public async Task<TabPage> CreateTabPage()
        {
            TabPage tabPage = new TabPage("TabPage");
            DataGridView dataGridView = CreateDataGridView();
            tabPage.Controls.Add(dataGridView);

            GroupBox groupBox = CreateGroupBox(new Size(348, 136), new Point(31, 43));
            tabPage.Controls.Add(groupBox);

            ComboBox comboBox = CreateComboBox(new Size(266, 24), new Point(35, 59));
            groupBox.Controls.Add(comboBox);


            // ComboBox ve DataGridView çiftini eşleştir
            comboBoxDataGridViewPairs.Add(comboBox, dataGridView);
            // API'den verileri ComboBox ve DataGridView'e yükle
            await LoadDataIntoComboBox(comboBox);
            await LoadDataIntoDataGridView(dataGridView);
            return tabPage;
        }

        private DataGridView CreateDataGridView()
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.Location = new Point(3, 192);
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Dock= DockStyle.None;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.Size = new Size(1500, 175);
            dataGridView.TabIndex = 1;

            dataGridView.MouseDoubleClick += DataGridView_MouseDoubleClick;
            return dataGridView;
        }

        private ComboBox CreateComboBox(Size size, Point location)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Size = size;
            comboBox.Location = location;
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            return comboBox;
        }
        private GroupBox CreateGroupBox(Size size, Point location)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Size = size;
            groupBox.Location = location;
            groupBox.Text = "Select Project";
            return groupBox;
        }
        private async Task LoadDataIntoComboBox(ComboBox comboBox)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/Projects/GetAll";
                ProjectController projectController = new ProjectController();
                var projectData = await projectController.GetProjectData(apiUrl);
                // API'den verileri al
                Project[] projects = projectData;

                // ComboBox'a projeleri ekle
                comboBox.DataSource = projects;
                comboBox.DisplayMember = "ProjectName";
                comboBox.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("API'den veri alınamadı. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadDataIntoDataGridView(DataGridView dataGridView)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/Sqls/GetAll";
               SqlController sqlController = new SqlController();
               var sqls= await sqlController.GetSqlData(apiUrl);
                // API'den verileri al
                Sql[] sqlArray = sqls;

                // Sql[] dizisini DataTable'a dönüştür
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID");
                dataTable.Columns.Add("ProjectId");
                dataTable.Columns.Add("SqlServerIp");
                dataTable.Columns.Add("SqlServerUserName");
                dataTable.Columns.Add("SqlServerPassword");

                foreach (Sql sql in sqlArray)
                {
                    dataTable.Rows.Add(sql.Id, sql.ProjectId, sql.SqlServerIp, sql.SqlServerUserName, sql.SqlServerPassword);
                }

                // Orijinal verileri sakla
                originalData = dataTable;

                // DataTable'ı DataGridView'e yükle
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("API'den veri alınamadı. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            DataGridView dataGridView = comboBoxDataGridViewPairs[comboBox];


            // Seçili projenin ID'sini al
            string selectedValue = comboBox.SelectedValue?.ToString();

            // DataGridView'i filtrele
            FilterDataGridView(dataGridView, selectedValue);
        }

        private void FilterDataGridView(DataGridView dataGridView, string selectedValue)
        {
            DataTable dataTable = originalData.Clone(); // Yeni bir DataTable oluştur

            if (!string.IsNullOrEmpty(selectedValue))
            {
                DataRow[] filteredRows = originalData.Select("ProjectId = '" + selectedValue + "'");
                foreach (DataRow row in filteredRows)
                {
                    dataTable.ImportRow(row);
                }
            }
            else
            {
                dataTable = originalData.Copy(); // Tüm verileri göster
            }

            dataGridView.DataSource = dataTable;
        }
        private void DataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (isAdminUser()) // isAdminUser() metodunu, admin kullanıcısının doğrulamasını yapacak şekilde güncelleyin.
            {
                // Çift tıklama işlemlerini buraya ekleyin
                // Örneğin, seçilen veriyi almak için aşağıdaki kodu kullanabilirsiniz:
                DataGridView dataGridView = (DataGridView)sender;
                if (dataGridView.SelectedRows.Count > 0)
                {
                    // Seçili satırı al
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    // Seçili satırın verilerini kullanarak istediğiniz işlemleri gerçekleştirin
                    string selectedId = selectedRow.Cells["ID"].Value.ToString();

                    HomePageControl homePageControl = new HomePageControl();
                    TabPage tabpage=homePageControl.CreateTabPage();

                    //TabControl.TabPages.Add(tabpage);
                    //TabControl.SelectedTab = tabpage;

                    // ... diğer işlemler
                }
            }
        }
        private bool isAdminUser()
        {
            var control = ActiveUser.Role;
            if (control.Contains("admin") && control.Contains("moderator"))
            {
                return true;
            }
            else if (control.Contains("Admin"))
            {
                return true;
            }
            return false; 
        }


    }
}
