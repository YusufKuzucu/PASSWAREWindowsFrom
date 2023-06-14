using PASSWARE.Models;
using PASSWARE.Models.Entities;
using PASSWARE.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE.TabpageBase.EntitiesTabPage
{
    public class FilesTabpageControl
    {
        private int Id;
        private DataGridView dataGridView;
        public TabPage CreateTabPage(string projectID,string selectedfilesId,DataTable filterdata)
        {
            TabPage tabPage = new TabPage("TabPage");
            tabPage.BackColor = Color.White;
            Id = Convert.ToInt32(projectID);
            Panel panel = CreatePanel();
            tabPage.Controls.Add(panel);

            DataGridView dataGridView = CreateDataGridView();
            dataGridView.DataSource = filterdata;
            dataGridView.Name = "dataGridView";
            tabPage.Controls.Add(dataGridView);

            Label label1 = CreateLabel(selectedfilesId, "label1", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 34), 2);
            label1.Enabled = false;
            tabPage.Controls.Add(label1);

            Label label6 = CreateLabel(projectID, "label6", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 15), 8); ;
            label6.Enabled = false;
            tabPage.Controls.Add(label6);

            Button button1 = CreateButton("Add", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 40), 7);
            button1.Click += AddSql_Click;
            button1.Image = Properties.Resources.save;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            panel.Controls.Add(button1);

            Button button3 = CreateButton("Delete ", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 150), 9);
            button3.Image = Properties.Resources.trash;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Click += DeleteSql_Click;
            panel.Controls.Add(button3);
            return tabPage;
        }
        private Panel CreatePanel()
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Right;
            panel.BackColor = Color.FromKnownColor(KnownColor.Control);
            panel.Location = new System.Drawing.Point(1331, 3);
            panel.Size = new System.Drawing.Size(200, 477);
            panel.TabIndex = 0;
            panel.BorderStyle = BorderStyle.FixedSingle;
            return panel;
        }
        private Button CreateButton(string text, Size size, Point location, int tabındex)
        {
            Button button = new Button();
            button.Text = text;
            button.BackColor = Color.FromKnownColor(KnownColor.Silver);
            button.Size = size;
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            button.ForeColor = Color.Black;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Location = location;
            button.TabIndex = tabındex;
            button.FlatStyle = FlatStyle.Flat;
            return button;
        }
        private Label CreateLabel(string text, string name, Size size, Point location, int tabındex)
        {
            Label label = new Label();
            label.Text = text;
            label.Name = name;
            label.Size = size;
            label.ForeColor = Color.Black;
            label.Location = location;
            label.AutoSize = true;
            label.TabIndex = tabındex;
            return label;
        }
        private TextBox CreateTextBox(string text, Size size, Point location, int tabındex, string text2)
        {
            TextBox textBox = new TextBox();
            textBox.Name = text;
            textBox.Text = text2;
            textBox.ForeColor = Color.Black;
            textBox.Size = size;
            textBox.Location = location;
            textBox.TabIndex = tabındex;
            return textBox;
        }
        private DataGridView CreateDataGridView()
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.Location = new System.Drawing.Point(0, 5);
            dataGridView.RowHeadersWidth = 51;
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.Dock = DockStyle.None;
            dataGridView.Size = new System.Drawing.Size(1325, 600);
            dataGridView.TabIndex = 1;
            LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(Id));
            dataGridView.CellMouseClick += DataGridView_CellMouseDoubleClick;
            dataGridView.MouseDoubleClick += DataGridView_MouseDoubleClick;
            return dataGridView;
        }
        private void DataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                string selectedId = selectedRow.Cells["ID"].Value.ToString();
                TabPage tabPage = (TabPage)dataGridView.Parent;
                Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label1");
                label1.Text=selectedId;
            }
        }
        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView.Columns.Count)
            {
                DataGridViewCell selectedCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string selectedId = dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString();

                TabPage tabPage = (TabPage)dataGridView.Parent;
                Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label1");
                label1.Text=selectedId;
            }
        }
        private async void AddSql_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
            DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = "ALL Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string dosyaYolu = openFileDialog.FileName;
                    Yükle(dosyaYolu);
                    LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(Id));
                }
            }
        }
        private async void Yükle(string dosyaYolu)
        {

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
                using (var formContent = new MultipartFormDataContent())
                {
                    byte[] dosyaVerisi = File.ReadAllBytes(dosyaYolu);
                    Files files = new Files()
                    {
                        ConnectionInfo = dosyaVerisi,
                        ConnectExplanation = dosyaYolu,
                        ProjectId = Id   //TODO : projectId eklenicek
                    };

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44343/api/Files/Post");

                    request.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(files));
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Dosya başarıyla yüklendi.");
                    }
                    else
                    {
                        MessageBox.Show("Dosya yükleme hatası: " + response.StatusCode);
                    }
                }
            }

        }
        private async void DeleteSql_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult results = MessageBox.Show("Are you sure you want to delete this SQL?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (results == DialogResult.Yes)
                {
                    Button button = (Button)sender;
                    TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                    Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label1");
                    Label label6 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");

                    DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                    int filesId = Convert.ToInt32(label1.Text);
                    
                    string projectId = label6.Text;
                    FilesController filesController = new FilesController();
                    bool result = await filesController.DeleteFilesData(filesId);
                    if (result)
                    {
                        MessageBox.Show("Files Deleted Successfully");
                        LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(projectId));
                    }
                    else
                    {
                        MessageBox.Show("Files Failed to Update");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private async void LoadDataIntoDataGridView(DataGridView dataGridView, int id)
        {
            try
            {
                FilesController filesController = new FilesController();
                var Files = await filesController.GetFile(id);
                Files[] filesArray = Files;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID"); dataTable.Columns.Add("ProjectName"); dataTable.Columns.Add("ConnectExplanation"); dataTable.Columns.Add("ConnectionInfo");
                Dictionary<int, string> projectNames = await GetProjectNames();
                foreach (Files files in filesArray)
                {
                    string projectName = projectNames.ContainsKey(files.ProjectId) ? projectNames[files.ProjectId] : string.Empty;
                    dataTable.Rows.Add(files.Id, projectName, files.ConnectExplanation, files.ConnectionInfo);
                }
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from API. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<Dictionary<int, string>> GetProjectNames()
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
    }


}
