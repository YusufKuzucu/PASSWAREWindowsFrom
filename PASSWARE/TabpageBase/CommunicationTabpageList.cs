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
    public class CommunicationTabpageList
    {
        private readonly Dictionary<ComboBox, DataGridView> comboBoxDataGridViewPairs;
        private readonly HttpClient client;
        private DataTable originalData;
        private TabControl tabControl;
        private DataTable filterData;
        private string projectId;
        private string SqlprojeName;


        private Dictionary<int, string> projectNames;
        public CommunicationTabpageList(TabControl tabControl)
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
            dataGridView.ReadOnly = true;
            dataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            dataGridView.Location = new Point(0, 180);
            dataGridView.RowHeadersWidth = 51;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 9);
            dataGridView.Dock = DockStyle.None;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.Size = new Size(1527, 430);
            dataGridView.TabIndex = 1;
            dataGridView.CellMouseDoubleClick += DataGridView_CellMouseDoubleClick;
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
                string apiUrl = "https://localhost:44343/api/Communications/GetAll";
                CommunicationController communication = new CommunicationController();
                var comms = await communication.GetCommunicationData(apiUrl);
                Communication[] commArray = comms;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID"); dataTable.Columns.Add("ProjectName"); dataTable.Columns.Add("InternalEmail"); dataTable.Columns.Add("InternalNumber"); dataTable.Columns.Add("ExternalEmail"); dataTable.Columns.Add("ExternalNumber");

                Dictionary<int, string> projectNames = await GetProjectNames();

                foreach (Communication comm in commArray)
                {
                    string projectName = projectNames.ContainsKey(comm.ProjectId) ? projectNames[comm.ProjectId] : string.Empty;
                    dataTable.Rows.Add(comm.Id, projectName, comm.InternalEmail, comm.InternalNumber, comm.ExternalEmail,comm.ExternalNumber);
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
                    string selectedCommId = string.Empty;
                    if (selectedRow.Cells["ID"].Value !=null)
                    {
                        selectedCommId = selectedRow.Cells["ID"].Value.ToString();  
                    }
                    else
                    {
                        return;
                    }
                    string projectName = selectedRow.Cells["ProjectName"].Value.ToString();
                    string ınternalEmail = selectedRow.Cells["InternalEmail"].Value.ToString();
                    string ınternalNumber = selectedRow.Cells["InternalNumber"].Value.ToString();
                    string externalEmail = selectedRow.Cells["ExternalEmail"].Value.ToString();
                    string externalNumber = selectedRow.Cells["ExternalNumber"].Value.ToString();

                    var filterdata = filterData;
                    string projectID = projectId;
                    string colum1name = dataGridView.Columns[0].HeaderText; //column name
                    string colum2name = dataGridView.Columns[1].HeaderText;
                    string colum3name = dataGridView.Columns[2].HeaderText;
                    string colum4name = dataGridView.Columns[3].HeaderText;
                    string colum5name = dataGridView.Columns[4].HeaderText;
                    string colum6name = dataGridView.Columns[5].HeaderText;


                    TabPage newTabPage = new TabPage();
                    CommunicationTabpageControl commTabpageControl = new CommunicationTabpageControl();
                    TabPage tabPage = commTabpageControl.CreateTabPage(projectID, projectName, selectedCommId, ınternalEmail, ınternalNumber, externalEmail, externalNumber, colum1name, colum2name, colum3name, colum4name, colum5name,colum6name, filterdata);
                    tabPage.Text = "Communication";
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
                    string selectedCommId = string.Empty;
                    if (dataGridView.Rows[e.RowIndex].Cells["ID"].Value !=null)
                    {
                        selectedCommId = dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                    }
                    else
                    {
                        return;
                    }
                    string projectName = dataGridView.Rows[e.RowIndex].Cells["ProjectName"].Value.ToString();
                    string ınternalEmail = dataGridView.Rows[e.RowIndex].Cells["InternalEmail"].Value.ToString();
                    string ınternalNumber = dataGridView.Rows[e.RowIndex].Cells["InternalNumber"].Value.ToString();
                    string externalEmail = dataGridView.Rows[e.RowIndex].Cells["ExternalEmail"].Value.ToString();
                    string externalNumber = dataGridView.Rows[e.RowIndex].Cells["ExternalNumber"].Value.ToString();

                    var filterdata = filterData;
                    string projectID = projectId;
                    string colum1name = dataGridView.Columns[0].HeaderText;
                    string colum2name = dataGridView.Columns[1].HeaderText;
                    string colum3name = dataGridView.Columns[2].HeaderText;
                    string colum4name = dataGridView.Columns[3].HeaderText;
                    string colum5name = dataGridView.Columns[4].HeaderText;
                    string colum6name = dataGridView.Columns[5].HeaderText;


                    TabPage newTabPage = new TabPage();
                    CommunicationTabpageControl commTabpageControl = new CommunicationTabpageControl();
                    TabPage tabPage = commTabpageControl.CreateTabPage(projectID, projectName, selectedCommId, ınternalEmail, ınternalNumber, externalEmail, externalNumber, colum1name, colum2name, colum3name, colum4name, colum5name,colum6name, filterdata);
                    tabPage.Text = "Communication";
                    tabControl.TabPages.Add(tabPage);
                    tabControl.SelectedTab = tabPage;
                }
            }
        }
        private bool isAdminUser()
        {
            var control = ActiveUser.Role;
            if (control.Contains("Admin") && control.Contains("Moderator"))
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
