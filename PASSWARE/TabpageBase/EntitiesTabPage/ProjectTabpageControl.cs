using PASSWARE.Models.Entities;
using PASSWARE.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE.TabpageBase.EntitiesTabPage
{
    public class ProjectTabpageControl
    {
        private int Id;
        private DataGridView dataGridView;
        public TabPage CreateTabPage(string companyID, string CompanyName, string selectedProjectId, string ProjectName, string ProjectServerIP, string ProjectServerUserName, string ProjectServerPassword, string colum1name, string colum2name, string colum3name, string colum4name, string colum5name, string colum6name, DataTable filterData)
        {
            TabPage tabPage = new TabPage("TabPage");
            Id = Convert.ToInt32(companyID);
            Panel panel = CreatePanel();
            tabPage.Controls.Add(panel);

            DataGridView dataGridView = CreateDataGridView();
            dataGridView.DataSource = filterData;
            dataGridView.Name = "dataGridView";
            tabPage.Controls.Add(dataGridView);

            Label label1 = CreateLabel(colum1name, "label1", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 20), 2);
            label1.Enabled = false;
            tabPage.Controls.Add(label1);

            Label label2 = CreateLabel(colum2name, "label2", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 66), 3);
            tabPage.Controls.Add(label2);

            Label label3 = CreateLabel(colum3name, "label3", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 124), 4);
            tabPage.Controls.Add(label3);

            Label label4 = CreateLabel(colum4name, "label4", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 176), 8);
            tabPage.Controls.Add(label4);

            Label label5 = CreateLabel(colum5name, "label5", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 226), 8);
            tabPage.Controls.Add(label5);
            Label label7 = CreateLabel(colum6name, "label7", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 260), 8); ;
            tabPage.Controls.Add(label7);


            Label label6 = CreateLabel(companyID, "label6", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 15), 8); ;
            label6.Visible = false;
            tabPage.Controls.Add(label6);


            TextBox textBox1 = CreateTextBox("txtProjct1", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 20), 5, selectedProjectId);
            textBox1.Enabled = false;
            tabPage.Controls.Add(textBox1);

            TextBox textBox2 = CreateTextBox("txtProjct2", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 66), 6, CompanyName);
            tabPage.Controls.Add(textBox2);

            TextBox textBox3 = CreateTextBox("txtProjct3", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 124), 7, ProjectName);
            tabPage.Controls.Add(textBox3);

            TextBox textBox4 = CreateTextBox("txtProjct4", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 176), 9, ProjectServerIP);
            tabPage.Controls.Add(textBox4);

            TextBox textBox5 = CreateTextBox("txtProjct5", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 226), 9, ProjectServerUserName);
            tabPage.Controls.Add(textBox5);

            TextBox textBox6 = CreateTextBox("txtProjct6", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 260), 9, ProjectServerPassword);
            tabPage.Controls.Add(textBox6);


            Button button1 = CreateButton("Add", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 40), 7);
            button1.Image = Properties.Resources.save;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Click += AddProject_Click;
            panel.Controls.Add(button1);

            Button button2 = CreateButton("Update", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 150), 8);
            button2.Image = Properties.Resources.update;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Click += UpdateProject_Click;
            panel.Controls.Add(button2);

            Button button3 = CreateButton("Delete ", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 250), 9);
            button3.Image = Properties.Resources.trash;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Click += DeleteProject_Click;
            panel.Controls.Add(button3);

            Button button4 = CreateButton("Files", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 360), 10);
            button4.Image = Properties.Resources._1_090;
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.Click += FilesProject_Click;
            panel.Controls.Add(button4);
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
            dataGridView.Location = new System.Drawing.Point(0, 300);
            dataGridView.RowHeadersWidth = 51;
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.Dock = DockStyle.None;
            dataGridView.Size = new System.Drawing.Size(1325, 370);
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
                // Seçili satırı al
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];

                string selectedId = string.Empty;
                if (selectedRow.Cells["ID"].Value != null)
                {
                    selectedId = selectedRow.Cells["ID"].Value.ToString();
                }
                else
                {
                    return;
                }
                string companyName = selectedRow.Cells["CompanyName"].Value.ToString();
                string projectName = selectedRow.Cells["ProjectName"].Value.ToString();
                string projectServerIP = selectedRow.Cells["ProjectServerIP"].Value.ToString();
                string projectServerUserName = selectedRow.Cells["ProjectServerUserName"].Value.ToString();
                string projectServerPassword = selectedRow.Cells["ProjectServerPassword"].Value.ToString();
                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct5");
                TextBox textBox6 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct6");

                textBox1.Text = selectedId; textBox2.Text = companyName; textBox3.Text = projectName; textBox4.Text = projectServerIP; textBox5.Text = projectServerUserName; textBox6.Text = projectServerPassword;
            }
        }

        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView.Columns.Count)
            {
                // Seçili hücrenin değerini al
                DataGridViewCell selectedCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string selectedId = string.Empty;
                if (dataGridView.Rows[e.RowIndex].Cells["ID"].Value != null)
                {
                    selectedId = dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                }
                else
                {
                    return;
                }
                string companyName = dataGridView.Rows[e.RowIndex].Cells["CompanyName"].Value.ToString();
                string projectName = dataGridView.Rows[e.RowIndex].Cells["ProjectName"].Value.ToString();
                string projectServerIP = dataGridView.Rows[e.RowIndex].Cells["ProjectServerIP"].Value.ToString();
                string projectServerUserName = dataGridView.Rows[e.RowIndex].Cells["ProjectServerUserName"].Value.ToString();
                string projectServerPassword = dataGridView.Rows[e.RowIndex].Cells["ProjectServerPassword"].Value.ToString();
                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct5");
                TextBox textBox6 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct6");
                textBox1.Text = selectedId; textBox2.Text = companyName; textBox3.Text = projectName; textBox4.Text = projectServerIP; textBox5.Text = projectServerUserName; textBox6.Text = projectServerPassword;
            }
        }
        private async Task<List<string>> GetExistingProjects(string name)
        {
            List<string> existingProjects = new List<string>();

            // Proje adına göre istek yaparak projeleri alın
            ProjectController projectController = new ProjectController();
            Project[] projects = await projectController.GetProjectName(name);

            // Projelerin adlarını existingProjects listesine ekleyin
            foreach (Project project in projects)
            {
                existingProjects.Add(project.ProjectName);
            }

            return existingProjects;
        }

        private async void AddProject_Click(object sender, EventArgs e)
        {
            try
            {
                ProjectController projectController = new ProjectController();

                Button button = (Button)sender;
                TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct5");
                TextBox textBox6 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct6");

                Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");

                DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                string projectName = textBox3.Text;
                if (string.IsNullOrEmpty(projectName))
                {
                    MessageBox.Show("Project name cannot be empty.");
                    return;
                }

                List<string> existingProjects = await GetExistingProjects(projectName);
                if (existingProjects.Contains(projectName))
                {
                    MessageBox.Show("A project with the same name already exists. Please enter a different project name.");
                    return;
                }
                string projectServerIP = textBox4.Text;
                string projectServerUserName = textBox5.Text;
                string projectServerPassword = textBox6.Text;

                string companyId = label1.Text;


                
                bool result = await projectController.AddProjectData(projectName, projectServerIP, projectServerUserName, projectServerPassword, companyId);
                if (result)
                {
                    MessageBox.Show("Project Added Successfully");

                    LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(companyId));
                }
                else
                {
                    MessageBox.Show("Project Failed to Added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }

        private async void UpdateProject_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct5");
                TextBox textBox6 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct6");

                Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");
                DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                int projectId = Convert.ToInt32(textBox1.Text);
                string projectName = textBox3.Text;
                string projectServerIP = textBox4.Text;
                string projectServerUserName = textBox5.Text;
                string projectServerPassword = textBox6.Text;


                string companyId = label1.Text;
                ProjectController projectController = new ProjectController();
                bool result = await projectController.UpdateProjectData(projectId, projectName, projectServerIP, projectServerUserName, projectServerPassword, companyId);
                if (result)
                {
                    MessageBox.Show("Project Updated Successfully");

                    LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(companyId));
                }
                else
                {
                    MessageBox.Show("Project failed to Update");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private async void DeleteProject_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult results = MessageBox.Show("Are you sure you want to delete this project?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (results == DialogResult.Yes)
                {
                    // Silme işlemine devam etmek
                    Button button = (Button)sender;
                    TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                    TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct1");
                    TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct2");
                    TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct3");
                    TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct4");
                    TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct5");
                    TextBox textBox6 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtProjct6");

                    Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");
                    DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                    int projectId = Convert.ToInt32(textBox1.Text);
                    string projectName = textBox3.Text;
                    string projectServerIP = textBox4.Text;
                    string projectServerUserName = textBox5.Text;
                    string projectServerPassword = textBox6.Text;
                    string companyId = label1.Text;
                    ProjectController projectController = new ProjectController();
                    bool result = await projectController.DeleteProjectData(projectId);
                    if (result)
                    {
                        MessageBox.Show("Project Deleted Successfully");
                        LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(companyId));
                        textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Project Failed to Update");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void FilesProject_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buton 4");
        }



        private async void LoadDataIntoDataGridView(DataGridView dataGridView, int id)
        {
            try
            {
                ProjectController projectController = new ProjectController();
                var project = await projectController.GetProject(id);
                Project[] projectArray = project;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID"); dataTable.Columns.Add("CompanyName"); dataTable.Columns.Add("ProjectName"); dataTable.Columns.Add("ProjectServerIP"); dataTable.Columns.Add("ProjectServerUserName"); dataTable.Columns.Add("ProjectServerPassword");

                Dictionary<int, string> companyNames = await GetCompanyNames();

                foreach (Project prjct in projectArray)
                {
                    string companyName = companyNames.ContainsKey(prjct.CompanyId) ? companyNames[prjct.CompanyId] : string.Empty;
                    dataTable.Rows.Add(prjct.Id, companyName, prjct.ProjectName, prjct.ProjectServerIP, prjct.ProjectServerUserName, prjct.ProjectServerPassword);
                }
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from API. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<Dictionary<int, string>> GetCompanyNames()
        {
            Dictionary<int, string> companyNames = new Dictionary<int, string>();
            try
            {
                string apiUrl = "https://localhost:44343/api/Companies/GetAll";
                CompanyController companyController = new CompanyController();
                var company = await companyController.GetCompanyData(apiUrl);

                foreach (Company compy in company)
                {
                    companyNames.Add(compy.Id, compy.CompanyName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("The names of the projects could not be retrieved. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return companyNames;
        }
    }

}
