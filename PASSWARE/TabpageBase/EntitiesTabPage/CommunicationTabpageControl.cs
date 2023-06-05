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
    public class CommunicationTabpageControl
    {
        private int Id;
        private DataGridView dataGridView;
        public TabPage CreateTabPage(string projectID, string projectName, string selectedVpnId, string ınternalEmail, string ınternalNumber, string externalEmail, string externalNumber, string colum1name, string colum2name, string colum3name, string colum4name, string colum5name, string colum6name, DataTable filterData)
        {
            TabPage tabPage = new TabPage("TabPage");
            Id = Convert.ToInt32(projectID);
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

            Label label7 = CreateLabel(colum6name, "label7", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 250), 8); ;
            tabPage.Controls.Add(label7);

            Label label6 = CreateLabel(projectID, "label6", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 10), 8); ;
            label6.Enabled = false;
            tabPage.Controls.Add(label6);


            TextBox textBox1 = CreateTextBox("txtcomm1", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 20), 5, selectedVpnId);
            textBox1.Enabled = false;
            tabPage.Controls.Add(textBox1);

            TextBox textBox2 = CreateTextBox("txtcomm2", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 66), 6, projectName);
            tabPage.Controls.Add(textBox2);

            TextBox textBox3 = CreateTextBox("txtcomm3", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 124), 7, ınternalEmail);
            tabPage.Controls.Add(textBox3);

            TextBox textBox4 = CreateTextBox("txtcomm4", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 176), 9, ınternalNumber);
            tabPage.Controls.Add(textBox4);

            TextBox textBox5 = CreateTextBox("txtcomm5", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 226), 9, externalEmail);
            tabPage.Controls.Add(textBox5);

            TextBox textBox6 = CreateTextBox("txtcomm6", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 250), 9, externalNumber);
            tabPage.Controls.Add(textBox6);


            Button button1 = CreateButton("Add", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 40), 7);
            button1.Image = Properties.Resources.save;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Click += AddCommunication_Click;
            panel.Controls.Add(button1);

            Button button2 = CreateButton("Update", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 150), 8);
            button2.Image = Properties.Resources.update;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Click += UpdateCommunication_Click;
            panel.Controls.Add(button2);

            Button button3 = CreateButton("Delete ", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 250), 9);
            button3.Image = Properties.Resources.trash;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Click += DeleteCommunication_Click;
            panel.Controls.Add(button3);

            return tabPage;
        }
        private Panel CreatePanel()
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Right;
            panel.BackColor = System.Drawing.Color.Gray;
            panel.Location = new System.Drawing.Point(1331, 3);
            panel.Size = new System.Drawing.Size(200, 477);
            panel.TabIndex = 0;
            return panel;
        }
        private Button CreateButton(string text, Size size, Point location, int tabındex)
        {
            Button button = new Button();
            button.Text = text;
            button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(157)))), ((int)(((byte)(88)))));
            button.Size = size;
            button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            button.ForeColor = Color.White;
            button.UseVisualStyleBackColor = false;
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

                string selectedVpnId = selectedRow.Cells["ID"].Value.ToString();
                string projectName = selectedRow.Cells["ProjectName"].Value.ToString();
                string ınternalEmail = selectedRow.Cells["InternalEmail"].Value.ToString();
                string ınternalNumber = selectedRow.Cells["InternalNumber"].Value.ToString();
                string externalEmail = selectedRow.Cells["ExternalEmail"].Value.ToString();
                string externalNumber = selectedRow.Cells["ExternalNumber"].Value.ToString();

                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm5");
                TextBox textBox6 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm6");

                textBox1.Text = selectedVpnId; textBox2.Text = projectName; textBox3.Text = ınternalEmail; textBox4.Text = ınternalNumber; textBox5.Text = externalEmail;textBox6.Text = externalNumber;
            }
        }

        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView.Columns.Count)
            {
                // Seçili hücrenin değerini al
                DataGridViewCell selectedCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //string selectedId = selectedCell.Value.ToString();,
                string selectedVpnId = dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                string projectName = dataGridView.Rows[e.RowIndex].Cells["ProjectName"].Value.ToString();
                string ınternalEmail = dataGridView.Rows[e.RowIndex].Cells["InternalEmail"].Value.ToString();
                string ınternalNumber = dataGridView.Rows[e.RowIndex].Cells["InternalNumber"].Value.ToString();
                string externalEmail = dataGridView.Rows[e.RowIndex].Cells["ExternalEmail"].Value.ToString();
                string externalNumber = dataGridView.Rows[e.RowIndex].Cells["ExternalNumber"].Value.ToString();

                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm5");
                TextBox textBox6 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm6");
                textBox1.Text = selectedVpnId; textBox2.Text = projectName; textBox3.Text = ınternalEmail; textBox4.Text = ınternalNumber; textBox5.Text = externalEmail; textBox6.Text = externalNumber;
            }
        }

        private async void AddCommunication_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult results = MessageBox.Show("Are you sure you want to added this Communication?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (results==DialogResult.Yes)
                {
                    Button button = (Button)sender;
                    TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                    TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm1");
                    TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm2");
                    TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm3");
                    TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm4");
                    TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm5");
                    TextBox textBox6 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm6");

                    Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");

                    DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                    string ınternalEmail = textBox3.Text;
                    string ınternalNumber = textBox4.Text;
                    string externalEmail = textBox5.Text;
                    string externalNumber = textBox6.Text;

                    string projectId = label1.Text;


                    CommunicationController commController = new CommunicationController();
                    bool result = await commController.AddCommunicationData(ınternalEmail, ınternalNumber, externalEmail, externalNumber, projectId);
                    if (result)
                    {
                        MessageBox.Show("Communication Added Succesfully");

                        LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(projectId));
                    }
                    else
                    {
                        MessageBox.Show("Communication Failed to Added");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private async void UpdateCommunication_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult results = MessageBox.Show("Are you sure you want to update this Communication?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (results==DialogResult.Yes)
                {
                    Button button = (Button)sender;
                    TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                    TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm1");
                    TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm2");
                    TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm3");
                    TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm4");
                    TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm5");
                    TextBox textBox6 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm6");

                    Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");
                    DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                    int commId = Convert.ToInt32(textBox1.Text);
                    string ınternalEmail = textBox3.Text;
                    string ınternalNumber = textBox4.Text;
                    string externalEmail = textBox5.Text;
                    string externalNumber = textBox6.Text;
                    string projectId = label1.Text;

                    CommunicationController commController = new CommunicationController();
                    bool result = await commController.UpdateCommunicationData(commId, ınternalEmail, ınternalNumber, externalEmail, externalNumber, projectId);
                    if (result)
                    {
                        MessageBox.Show("Communication Updated Succesfully");

                        LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(projectId));
                    }
                    else
                    {
                        MessageBox.Show("Communication Failed to Update");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private async void DeleteCommunication_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult results = MessageBox.Show("Are you sure you want to delete this Communication?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (results==DialogResult.Yes)
                {
                    Button button = (Button)sender;
                    TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                    TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm1");
                    TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm2");
                    TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm3");
                    TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm4");
                    TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm5");
                    TextBox textBox6 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcomm6");

                    Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");
                    DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                    int commId = Convert.ToInt32(textBox1.Text);
                    string ınternalEmail = textBox3.Text;
                    string ınternalNumber = textBox4.Text;
                    string externalEmail = textBox5.Text;
                    string externalNumber = textBox6.Text;
                    string projectId = label1.Text;
                    CommunicationController commController = new CommunicationController();

                    bool result = await commController.DeleteCommunicaitonData(commId);
                    if (result)
                    {
                        MessageBox.Show("Communication Deleted Succesfully");
                        LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(projectId));
                        textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Communication Failed to Delete");
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
                CommunicationController communication = new CommunicationController();
                var comms = await communication.GetCommunication(id);
                Communication[] commArray = comms;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID"); dataTable.Columns.Add("ProjectName"); dataTable.Columns.Add("InternalEmail"); dataTable.Columns.Add("InternalNumber"); dataTable.Columns.Add("ExternalEmail"); dataTable.Columns.Add("ExternalNumber");

                Dictionary<int, string> projectNames = await GetProjectNames();

                foreach (Communication comm in commArray)
                {
                    string projectName = projectNames.ContainsKey(comm.ProjectId) ? projectNames[comm.ProjectId] : string.Empty;
                    dataTable.Rows.Add(comm.Id, projectName, comm.InternalEmail, comm.InternalNumber, comm.ExternalEmail, comm.ExternalNumber);
                }
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from API.  Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
