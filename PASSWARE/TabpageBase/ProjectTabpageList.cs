using PASSWARE.Models.Entities;
using PASSWARE.Models;
using PASSWARE.Request;
using PASSWARE.TabpageBase.EntitiesTabPage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace PASSWARE.TabpageBase
{
    public class ProjectTabpageList
    {
        private readonly Dictionary<ComboBox, DataGridView> comboBoxDataGridViewPairs;
        private readonly HttpClient client;
        private DataTable originalData;
        private TabControl tabControl;
        private DataTable filterData;
        private string projectId;
        private string SqlprojeName;


        private Dictionary<int, string> projectNames;
        public ProjectTabpageList(TabControl tabControl)
        {
            comboBoxDataGridViewPairs = new Dictionary<ComboBox, DataGridView>();
            client = new HttpClient();
            this.tabControl = tabControl;
        }
        public async Task<TabPage> CreateTabPage(TabControl tabControl)
        {
            TabPage tabPage = new TabPage("TabPage");
            DataGridView dataGridView = CreateDataGridView();
            tabPage.Controls.Add(dataGridView);

            GroupBox groupBox = CreateGroupBox(new Size(1527, 190), new Point(0, 0));
            tabPage.Controls.Add(groupBox);

            ComboBox comboBox = CreateComboBox(new Size(300, 24), new Point(35, 80));
            groupBox.Controls.Add(comboBox);

            Label label1 = CreateLabel("Select Project", new System.Drawing.Size(44, 16), new System.Drawing.Point(33, 60));
            groupBox.Controls.Add(label1);

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
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.ReadOnly = true;
            dataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.Location = new Point(0, 192);
            dataGridView.RowHeadersWidth = 51;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
            dataGridView.Dock = DockStyle.None;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.Size = new Size(1527, 460);
            dataGridView.TabIndex = 1;
            dataGridView.CellMouseClick += DataGridView_CellMouseDoubleClick;
            dataGridView.MouseDoubleClick += DataGridView_MouseDoubleClick;
            return dataGridView;
        }
        private ComboBox CreateComboBox(Size size, Point location)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.Size = size;
            comboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            comboBox.Location = location;
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            return comboBox;
        }
        private GroupBox CreateGroupBox(Size size, Point location)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Size = size;
            groupBox.Location = location;
            groupBox.Text = "Filter Panel";
            return groupBox;
        }
        private Label CreateLabel(string text, Size size, Point location)
        {
            Label label = new Label();
            label.Text = text;
            label.Size = size;
            label.ForeColor = Color.Black;
            label.Location = location;
            label.AutoSize = true;
            return label;
        }
        private async Task LoadDataIntoComboBox(ComboBox comboBox)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/Companies/GetAll";
                CompanyController companyController = new CompanyController();
                var companyData = await companyController.GetCompanyData(apiUrl);

                Company[] Company = companyData;
                foreach (var item in Company)
                {
                    comboBox.Items.Add(item);
                }
                comboBox.DisplayMember = "CompanyName";
                comboBox.ValueMember = "ID";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from API. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task LoadDataIntoDataGridView(DataGridView dataGridView)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/Projects/GetAll";
                ProjectController projectController = new ProjectController();
                var projects = await projectController.GetProjectData(apiUrl);
                Project[] projectArray = projects;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID"); 
                dataTable.Columns.Add("CompanyName"); 
                dataTable.Columns.Add("ProjectName"); 
                dataTable.Columns.Add("ProjectServerIP");
                dataTable.Columns.Add("ProjectServerUserName"); 
                dataTable.Columns.Add("ProjectServerPassword");

                Dictionary<int, string> companyNames = await GetCompanyNames();


                foreach (Project project in projectArray)
                {
                    try
                    {
                        string companyName = companyNames.ContainsKey(project.CompanyId) ? companyNames[project.CompanyId] : string.Empty;

                        dataTable.Rows.Add(project.Id, companyName, project.ProjectName, project.ProjectServerIP, project.ProjectServerUserName, project.ProjectServerPassword);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding project to table. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                originalData = dataTable;
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from API.  Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async Task<Dictionary<int, string>> GetCompanyNames()
        {
            Dictionary<int, string> companyNames = new Dictionary<int, string>();
            try
            {
                string apiUrl = "https://localhost:44343/api/Companies/GetAll";
                CompanyController companyController = new CompanyController();
                var company = await companyController.GetCompanyData(apiUrl);

                foreach (Company companies in company)
                {
                    companyNames.Add(companies.Id, companies.CompanyName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The names of the projects could not be retrieved. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return companyNames;
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            DataGridView dataGridView = comboBoxDataGridViewPairs[comboBox];

            if (comboBox.SelectedItem is Company selectedProject)
            {
                int selectedValue = selectedProject.Id;
                projectId = selectedValue.ToString();
            }

            string selectedText = comboBox.Text;
            // DataGridView'i filtrele
            FilterDataGridView(dataGridView, selectedText);
        }
        private void FilterDataGridView(DataGridView dataGridView, string selectedText)
        {
            DataTable dataTable = originalData.Clone(); // Yeni bir DataTable oluştur

            if (!string.IsNullOrEmpty(selectedText))
            {
                DataRow[] filteredRows = originalData.Select("ProjectName  = '" + selectedText + "'");

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

            filterData = dataTable;
        }
        private void DataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (isAdminUser())
            {
                DataGridView dataGridView = (DataGridView)sender;
                if (dataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                    string selectedProjectId = selectedRow.Cells["ID"].Value.ToString();
                    string CompanyName = selectedRow.Cells["CompanyName"].Value.ToString();
                    string ProjectName = selectedRow.Cells["ProjectName"].Value.ToString();
                    string ProjectServerIP = selectedRow.Cells["ProjectServerIP"].Value.ToString();
                    string ProjectServerUserName = selectedRow.Cells["ProjectServerUserName"].Value.ToString();
                    string ProjectServerPassword = selectedRow.Cells["ProjectServerPassword"].Value.ToString();

                    var filterdata = filterData;
                    string companyID = projectId;
                    string colum1name = dataGridView.Columns[0].HeaderText; //column name
                    string colum2name = dataGridView.Columns[1].HeaderText;
                    string colum3name = dataGridView.Columns[2].HeaderText;
                    string colum4name = dataGridView.Columns[3].HeaderText;
                    string colum5name = dataGridView.Columns[4].HeaderText;
                    string colum6name = dataGridView.Columns[5].HeaderText;

                    TabPage newTabPage = new TabPage();
                    ProjectTabpageControl projectTabpageControl = new ProjectTabpageControl();
                    TabPage tabPage = projectTabpageControl.CreateTabPage(companyID, CompanyName, selectedProjectId, ProjectName, ProjectServerIP,ProjectServerUserName, ProjectServerPassword, colum1name, colum2name, colum3name, colum4name, colum5name, colum6name, filterdata);
                    tabPage.Text = "Project";
                    tabControl.TabPages.Add(tabPage);
                    tabControl.SelectedTab = tabPage;
                }
            }
        }

        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (isAdminUser())
            {
                DataGridView dataGridView = (DataGridView)sender;
                if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView.Columns.Count)
                {
                    // Seçili hücrenin değerini al
                    DataGridViewCell selectedCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string selectedProjectId = selectedCell.Value.ToString();
                    string CompanyName = dataGridView.Rows[e.RowIndex].Cells["CompanyName"].Value.ToString();
                    string ProjectName = dataGridView.Rows[e.RowIndex].Cells["ProjectName"].Value.ToString();
                    string ProjectServerIP = dataGridView.Rows[e.RowIndex].Cells["ProjectServerIP"].Value.ToString();
                    string ProjectServerUserName = dataGridView.Rows[e.RowIndex].Cells["ProjectServerUserName"].Value.ToString();
                    string ProjectServerPassword = dataGridView.Rows[e.RowIndex].Cells["ProjectServerPassword"].Value.ToString();
                    var filterdata = filterData;
                    string companyID = projectId;
                    string colum1name = dataGridView.Columns[0].HeaderText;
                    string colum2name = dataGridView.Columns[1].HeaderText;
                    string colum3name = dataGridView.Columns[2].HeaderText;
                    string colum4name = dataGridView.Columns[3].HeaderText;
                    string colum5name = dataGridView.Columns[4].HeaderText;
                    string colum6name = dataGridView.Columns[5].HeaderText;

                    TabPage newTabPage = new TabPage();
                    ProjectTabpageControl projectTabpageControl = new ProjectTabpageControl();
                    TabPage tabPage = projectTabpageControl.CreateTabPage(companyID, CompanyName, selectedProjectId, ProjectName, ProjectServerIP, ProjectServerUserName, ProjectServerPassword,colum1name, colum2name, colum3name, colum4name, colum5name, colum6name, filterdata);
                    tabPage.Text = "Project";
                    tabControl.TabPages.Add(tabPage);
                    tabControl.SelectedTab = tabPage;
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
            else if (control.Contains("admin"))
            {
                return true;
            }
            return false;
        }
    }

    
}
