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
    public class JumpTabpageControl
    {
        private int Id;
        private DataGridView dataGridView;
        public TabPage CreateTabPage(string projectID,string  projectName,string  selectedVpnId, string jumpServerIP, string jumpServerUserName, string jumpServerPassword, string colum1name, string colum2name, string colum3name, string colum4name, string colum5name, DataTable filterData)
        {
            TabPage tabPage = new TabPage("TabPage");
            Id = Convert.ToInt32(projectID);
            Panel panel = CreatePanel();
            tabPage.Controls.Add(panel);

            DataGridView dataGridView = CreateDataGridView();
            dataGridView.DataSource = filterData;
            dataGridView.Name = "dataGridView";
            tabPage.Controls.Add(dataGridView);

            Label label1 = CreateLabel(colum1name, "label1", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 34), 2);
            label1.Enabled = false;
            tabPage.Controls.Add(label1);

            Label label2 = CreateLabel(colum2name, "label2", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 80), 3);
            tabPage.Controls.Add(label2);

            Label label3 = CreateLabel(colum3name, "label3", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 138), 4);
            tabPage.Controls.Add(label3);

            Label label4 = CreateLabel(colum4name, "label4", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 190), 8);
            tabPage.Controls.Add(label4);

            Label label5 = CreateLabel(colum5name, "label5", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 240), 8);
            tabPage.Controls.Add(label5);

            Label label6 = CreateLabel(projectID, "label6", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 15), 8); ;
            label6.Enabled = false;
            tabPage.Controls.Add(label6);


            TextBox textBox1 = CreateTextBox("txtjmp1", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 34), 5, selectedVpnId);
            textBox1.Enabled = false;
            tabPage.Controls.Add(textBox1);

            TextBox textBox2 = CreateTextBox("txtjmp2", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 80), 6, projectName);
            tabPage.Controls.Add(textBox2);

            TextBox textBox3 = CreateTextBox("txtjmp3", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 138), 7, jumpServerIP);
            tabPage.Controls.Add(textBox3);

            TextBox textBox4 = CreateTextBox("txtjmp4", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 190), 9, jumpServerUserName);
            tabPage.Controls.Add(textBox4);

            TextBox textBox5 = CreateTextBox("txtjmp5", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 240), 9, jumpServerPassword);
            tabPage.Controls.Add(textBox5);


            Button button1 = CreateButton("Add", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 40), 7);
            button1.Image = Properties.Resources.save;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.Click += AddJump_Click;
            panel.Controls.Add(button1);

            Button button2 = CreateButton("Update", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 150), 8);
            button2.Image = Properties.Resources.update;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Click += UpdateJump_Click;
            panel.Controls.Add(button2);

            Button button3 = CreateButton("Delete ", new System.Drawing.Size(199, 50), new System.Drawing.Point(1, 250), 9);
            button3.Image = Properties.Resources.trash;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Click += DeleteJump_Click;
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
            dataGridView.Location = new System.Drawing.Point(0, 280);
            dataGridView.RowHeadersWidth = 51;
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.Dock = DockStyle.None;
            dataGridView.Size = new System.Drawing.Size(1325, 330);
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

                string selectedJumpId = string.Empty;
                if (selectedRow.Cells["ID"].Value !=null)
                {
                    selectedJumpId = selectedRow.Cells["ID"].Value.ToString();
                }
                else
                {
                    return;
                }
                string projectName = selectedRow.Cells["ProjectName"].Value.ToString();
                string jumpServerIP = selectedRow.Cells["JumpServerIP"].Value.ToString();
                string jumpServerUserName = selectedRow.Cells["JumpServerUserName"].Value.ToString();
                string jumpServerPassword = selectedRow.Cells["JumpServerPassword"].Value.ToString();
                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp5");
                textBox1.Text = selectedJumpId; textBox2.Text = projectName; textBox3.Text = jumpServerIP; textBox4.Text = jumpServerUserName; textBox5.Text = jumpServerPassword;
            }
        }

        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView.Columns.Count)
            {
                // Seçili hücrenin değerini al
                DataGridViewCell selectedCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string selectedJumpId = string.Empty;
                if (dataGridView.Rows[e.RowIndex].Cells["ID"].Value !=null)
                {
                    selectedJumpId = dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                }
                else
                {
                    return;
                }
                string projectName = dataGridView.Rows[e.RowIndex].Cells["ProjectName"].Value.ToString();
                string jumpServerIP = dataGridView.Rows[e.RowIndex].Cells["JumpServerIP"].Value.ToString();
                string jumpServerUserName = dataGridView.Rows[e.RowIndex].Cells["JumpServerUserName"].Value.ToString();
                string jumpServerPassword = dataGridView.Rows[e.RowIndex].Cells["JumpServerPassword"].Value.ToString();
                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp5");
                textBox1.Text = selectedJumpId; textBox2.Text = projectName; textBox3.Text = jumpServerIP; textBox4.Text = jumpServerUserName; textBox5.Text = jumpServerPassword;
            }
        }

        private async void AddJump_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp5");

                Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");

                DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                string jumpServerIP = textBox3.Text;
                string jumpServerUserName = textBox4.Text;
                string jumpServerPassword = textBox5.Text;
                string projectId = label1.Text;


                JumpController jumpController = new JumpController();
                bool result = await jumpController.AddJumpData(jumpServerIP, jumpServerUserName, jumpServerPassword, projectId);
                if (result)
                {
                    MessageBox.Show("Jump Added Succesfully");

                    LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(projectId));
                }
                else
                {
                    MessageBox.Show("Jump Failed to Added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private async void UpdateJump_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp5");

                Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");
                DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                int VpnId = Convert.ToInt32(textBox1.Text);
                string jumpServerIP = textBox3.Text;
                string jumpServerUserName = textBox4.Text;
                string jumpServerPassword = textBox5.Text;
                string projectId = label1.Text;

                JumpController jumpController = new JumpController();
                bool result = await jumpController.UpdateJumpData(VpnId, jumpServerIP, jumpServerUserName, jumpServerPassword, projectId);
                if (result)
                {
                    MessageBox.Show("Jump Updated Succesfully");

                    LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(projectId));
                }
                else
                {
                    MessageBox.Show("Jump Failed to Update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private async void DeleteJump_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult results = MessageBox.Show("Are you sure you want to delete this Jump?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (results==DialogResult.Yes)
                {
                    Button button = (Button)sender;
                    TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                    TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp1");
                    TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp2");
                    TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp3");
                    TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp4");
                    TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtjmp5");

                    Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");
                    DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                    int vpnId = Convert.ToInt32(textBox1.Text);
                    string jumpServerIP = textBox3.Text;
                    string jumpServerUserName = textBox4.Text;
                    string jumpServerPassword = textBox5.Text;
                    string projectId = label1.Text;
                    JumpController jumpController = new JumpController();
                    bool result = await jumpController.DeleteJumpData(vpnId);
                    if (result)
                    {
                        MessageBox.Show("Jump Deleted Succesfully");
                        LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(projectId));
                        textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Jump Failed to Delete");
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
                JumpController jumpController = new JumpController();
                var jumps = await jumpController.GetJump(id);
                Jump[] jumpArray = jumps;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID"); dataTable.Columns.Add("ProjectName"); dataTable.Columns.Add("JumpServerIP"); dataTable.Columns.Add("JumpServerUserName"); dataTable.Columns.Add("JumpServerPassword");

                Dictionary<int, string> projectNames = await GetProjectNames();

                foreach (Jump jump in jumpArray)
                {
                    string projectName = projectNames.ContainsKey(jump.ProjectId) ? projectNames[jump.ProjectId] : string.Empty;
                    dataTable.Rows.Add(jump.Id, projectName, jump.JumpServerIP, jump.JumpServerUserName, jump.JumpServerPassword);
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
