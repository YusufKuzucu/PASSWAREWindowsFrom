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

namespace PASSWARE.TabpageBase
{
    public class UITabpageList
    {
        private readonly Dictionary<ComboBox, DataGridView> comboBoxDataGridViewPairs;
        private readonly HttpClient client;
        private DataTable originalData;
        private TabControl tabControl;
        private DataTable filterData;
        private string projectId;
        private string SqlprojeName;


        private Dictionary<int, string> projectNames;
        public UITabpageList(TabControl tabControl)
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
                string apiUrl = "https://localhost:44343/api/Projects/GetAll";
                ProjectController projectController = new ProjectController();
                var projectData = await projectController.GetProjectData(apiUrl);

                Project[] projects = projectData;
                foreach (var item in projects)
                {
                    comboBox.Items.Add(item);
                }
                comboBox.DisplayMember = "ProjectName";
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
                string apiUrl = "https://localhost:44343/api/UIs/GetAll";
                UIController uıController = new UIController();
                var uıs = await uıController.GetUIData(apiUrl);
                UI[] uıArray = uıs;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID"); dataTable.Columns.Add("ProjectName"); dataTable.Columns.Add("UIServerIP"); dataTable.Columns.Add("UIServerUserName"); dataTable.Columns.Add("UIServerPassword");

                Dictionary<int, string> projectNames = await GetProjectNames();

                foreach (UI uı in uıArray)
                {
                    string projectName = projectNames.ContainsKey(uı.ProjectId) ? projectNames[uı.ProjectId] : string.Empty;
                    dataTable.Rows.Add(uı.Id, projectName, uı.UIServerIP, uı.UIServerUserName, uı.UIServerPassword);
                }
                originalData = dataTable;
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from API.  Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public async Task<Dictionary<int, string>> GetProjectNames()
        {
            Dictionary<int, string> projectNames = new Dictionary<int, string>();
            try
            {
                string apiUrl = "https://localhost:44343/api/Projects/GetAll";
                ProjectController projectController = new ProjectController();
                var projects = await projectController.GetProjectData(apiUrl);

                foreach (Project project in projects)
                {
                    projectNames.Add(project.Id, project.ProjectName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The names of the projects could not be retrieved. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return projectNames;
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            DataGridView dataGridView = comboBoxDataGridViewPairs[comboBox];

            if (comboBox.SelectedItem is Project selectedProject)
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
                    string selectedUIId = selectedRow.Cells["ID"].Value.ToString();
                    string projectName = selectedRow.Cells["ProjectName"].Value.ToString();
                    string uIServerIP = selectedRow.Cells["UIServerIP"].Value.ToString();
                    string uIServerUserName = selectedRow.Cells["UIServerUserName"].Value.ToString();
                    string uIServerPassword = selectedRow.Cells["UIServerPassword"].Value.ToString();
                    var filterdata = filterData;
                    string projectID = projectId;
                    string colum1name = dataGridView.Columns[0].HeaderText; //column name
                    string colum2name = dataGridView.Columns[1].HeaderText;
                    string colum3name = dataGridView.Columns[2].HeaderText;
                    string colum4name = dataGridView.Columns[3].HeaderText;
                    string colum5name = dataGridView.Columns[4].HeaderText;

                    TabPage newTabPage = new TabPage();
                    UITabpageControl uıTabpageControl = new UITabpageControl();
                    TabPage tabPage = uıTabpageControl.CreateTabPage(projectID, projectName, selectedUIId, uIServerIP, uIServerUserName, uIServerPassword, colum1name, colum2name, colum3name, colum4name, colum5name, filterdata);
                    tabPage.Text = "UI";
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
                    string selectedUIId = selectedCell.Value.ToString();
                    string projectName = dataGridView.Rows[e.RowIndex].Cells["ProjectName"].Value.ToString();
                    string UIServerIP = dataGridView.Rows[e.RowIndex].Cells["UIServerIP"].Value.ToString();
                    string UIServerUserName = dataGridView.Rows[e.RowIndex].Cells["UIServerUserName"].Value.ToString();
                    string UIServerPassword = dataGridView.Rows[e.RowIndex].Cells["UIServerPassword"].Value.ToString();
                    var filterdata = filterData;
                    string projectID = projectId;
                    string colum1name = dataGridView.Columns[0].HeaderText;
                    string colum2name = dataGridView.Columns[1].HeaderText;
                    string colum3name = dataGridView.Columns[2].HeaderText;
                    string colum4name = dataGridView.Columns[3].HeaderText;
                    string colum5name = dataGridView.Columns[4].HeaderText;

                    TabPage newTabPage = new TabPage();
                    UITabpageControl uıTabpageControl = new UITabpageControl();
                    TabPage tabPage = uıTabpageControl.CreateTabPage(projectID, projectName, selectedUIId, UIServerIP, UIServerUserName, UIServerPassword, colum1name, colum2name, colum3name, colum4name, colum5name, filterdata);
                    tabPage.Text = "UI";
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
