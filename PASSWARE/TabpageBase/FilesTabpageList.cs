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
using System.IO;
using System.Diagnostics;

namespace PASSWARE.TabpageBase
{
    public class FilesTabpageList
    {
        private readonly Dictionary<ComboBox, DataGridView> comboBoxDataGridViewPairs;
        private readonly HttpClient client;
        private DataTable originalData;
        private TabControl tabControl;
        private DataTable filterData;
        private string projectId;

        private Dictionary<int, string> projectNames;
        public FilesTabpageList(TabControl tabControl)
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

            comboBoxDataGridViewPairs.Add(comboBox, dataGridView);
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
            dataGridView.Location = new Point(0, 180);
            dataGridView.ReadOnly = true;
            dataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
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
                string apiUrl = "https://localhost:44343/api/Files/GetAll";
                FilesController filesController = new FilesController();
                var files = await filesController.GetFilesData(apiUrl);
                Files[] filesArray = files;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID");
                dataTable.Columns.Add("ProjectName");
                dataTable.Columns.Add("ConnectExplanation");
                dataTable.Columns.Add("ConnectionInfo");

                Dictionary<int, string> projectNames = await GetProjectNames();

                foreach (Files file in filesArray)
                {
                    string projectName = projectNames.ContainsKey(file.ProjectId) ? projectNames[file.ProjectId] : string.Empty;
                    dataTable.Rows.Add(file.Id, projectName, file.ConnectExplanation, file.ConnectionInfo);
                }
                originalData = dataTable;
                dataGridView.DataSource = dataTable;

                // Çift tıklama olayını DataGridView'e ekleyin
                //dataGridView.CellClick += DataGridView_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from API.  Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        //private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataGridView dataGridView = (DataGridView)sender;

        //    if (e.ColumnIndex == dataGridView.Columns["ConnectionInfo"].Index && e.RowIndex >= 0)
        //    {
        //        var selectedFile = dataGridView.Rows[e.RowIndex].DataBoundItem as Files;
        //        if (selectedFile != null)
        //        {
        //            string fileExtension = Path.GetExtension(selectedFile.ConnectExplanation);
        //            OpenFile(selectedFile.ConnectionInfo, fileExtension);
        //        }
        //    }
        //}

        //private void OpenFile(byte[] fileData, string fileExtension)
        //{
        //    // Dosya türüne göre işlem yapın
        //    if (fileExtension == ".pdf")
        //    {
        //        // PDF dosyasını açmak için uygun işlemi gerçekleştirin
        //        string tempFilePath = Path.GetTempFileName(); // Geçici bir dosya oluşturun
        //        File.WriteAllBytes(tempFilePath, fileData); // Veriyi geçici dosyaya yazın

        //        // PDF dosyasını varsayılan PDF okuyucuyla açın
        //        Process.Start(tempFilePath);
        //    }
        //    else if (fileExtension == ".png" || fileExtension == ".jpg" || fileExtension == ".jpeg")
        //    {
        //        // Resmi açmak için uygun işlemi gerçekleştirin
        //        string tempFilePath = Path.GetTempFileName(); // Geçici bir dosya oluşturun
        //        File.WriteAllBytes(tempFilePath, fileData); // Veriyi geçici dosyaya yazın

        //        // Resmi varsayılan görüntüleyiciyle açın
        //        Process.Start(tempFilePath);
        //    }
        //    else if (fileExtension == ".doc" || fileExtension == ".docx")
        //    {
        //        // Word belgesini açmak için uygun işlemi gerçekleştirin
        //        string tempFilePath = Path.GetTempFileName(); // Geçici bir dosya oluşturun
        //        File.WriteAllBytes(tempFilePath, fileData); // Veriyi geçici dosyaya yazın

        //        // Word belgesini varsayılan Word uygulamasıyla açın
        //        Process.Start(tempFilePath);
        //    }
        //    else if (fileExtension == ".xls" || fileExtension == ".xlsx")
        //    {
        //        // Excel dosyasını açmak için uygun işlemi gerçekleştirin
        //        string tempFilePath = Path.GetTempFileName(); // Geçici bir dosya oluşturun
        //        File.WriteAllBytes(tempFilePath, fileData); // Veriyi geçici dosyaya yazın

        //        // Excel dosyasını varsayılan Excel uygulamasıyla açın
        //        Process.Start(tempFilePath);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Unsupported file type.");
        //    }
        //}


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
                    string selectedfilesId = string.Empty;
                    if (selectedRow.Cells["ID"].Value !=null)
                    {
                        selectedfilesId = selectedRow.Cells["ID"].Value.ToString(); 
                    }
                    else
                    {
                        return;
                    }
                    var filterdata = filterData;
                    string projectID = projectId;
                    string colum1name = dataGridView.Columns[0].HeaderText; //column name
                    

                    TabPage newTabPage = new TabPage();
                    FilesTabpageControl filesTabpageControl = new FilesTabpageControl();
                    TabPage tabPage = filesTabpageControl.CreateTabPage(projectID, selectedfilesId, filterdata);
                    tabPage.Text = "Files";
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
                    DataGridViewCell selectedCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    string selectedFileId = string.Empty;
                    if (dataGridView.Rows[e.RowIndex].Cells["ID"].Value !=null)
                    {
                        selectedFileId = dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                    }
                    else
                    {

                        return;
                    }
                    string projectName = dataGridView.Rows[e.RowIndex].Cells["ProjectName"].Value.ToString();
                    var filterdata = filterData;
                    string projectID = projectId;
                    string colum1name = dataGridView.Columns[0].HeaderText; //column name
                    TabPage newTabPage = new TabPage();
                    FilesTabpageControl filesTabpageControl = new FilesTabpageControl();
                    TabPage tabPage = filesTabpageControl.CreateTabPage(projectID,selectedFileId, filterdata);
                    tabPage.Text = "Files";
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
