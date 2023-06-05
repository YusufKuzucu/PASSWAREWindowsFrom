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
    public class SqlTabpageControl
    {
        private int Id;
        private DataGridView dataGridView;
        public TabPage CreateTabPage(string projectId, string projectName, string selectedId, string selectSqlServerIp, string selectSqlServerUserName, string selectSqlServerPassword, string colum1name, string colum2name, string colum3name, string colum4name, string colum5name, DataTable filterData)
        {
            TabPage tabPage = new TabPage("TabPage");
            Id = Convert.ToInt32(projectId);
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

            Label label6 = CreateLabel(projectId, "label6", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 15), 8); ;
            label6.Enabled = false;
            tabPage.Controls.Add(label6);


            TextBox textBox1 = CreateTextBox("txtSql1", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 34), 5, selectedId);
            textBox1.Enabled = false;
            tabPage.Controls.Add(textBox1);

            TextBox textBox2 = CreateTextBox("txtSql2", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 80), 6, projectName);
            tabPage.Controls.Add(textBox2);

            TextBox textBox3 = CreateTextBox("txtSql3", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 138), 7, selectSqlServerIp);
            tabPage.Controls.Add(textBox3);

            TextBox textBox4 = CreateTextBox("txtSql4", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 190), 9, selectSqlServerUserName);
            tabPage.Controls.Add(textBox4);

            TextBox textBox5 = CreateTextBox("txtSql5", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 240), 9, selectSqlServerPassword);
            tabPage.Controls.Add(textBox5);


            Button button1 = CreateButton("Add", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 55), 7);
            button1.Click += AddSql_Click;
            button1.Image = Properties.Resources.save;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            panel.Controls.Add(button1);

            Button button2 = CreateButton("Update", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 171), 8);
            button2.Image = Properties.Resources.update;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Click += UpdateSql_Click;
            panel.Controls.Add(button2);

            Button button3 = CreateButton("Delete ", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 290), 9);
            button3.Image = Properties.Resources.trash;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Click += DeleteSql_Click;
            panel.Controls.Add(button3);

            Button button4 = CreateButton("Pdf", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 410), 10);
            button4.Image = Properties.Resources.pdf;
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.Click += PdfSql_Click;
            panel.Controls.Add(button4);
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

                string selectedId = selectedRow.Cells["ID"].Value.ToString();
                string projectName = selectedRow.Cells["ProjectName"].Value.ToString();
                string selectSqlServerIp = selectedRow.Cells["SqlServerIp"].Value.ToString();
                string selectSqlServerUserName = selectedRow.Cells["SqlServerUserName"].Value.ToString();
                string selectSqlServerPassword = selectedRow.Cells["SqlServerPassword"].Value.ToString();
                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql5");
                textBox1.Text = selectedId; textBox2.Text = projectName; textBox3.Text = selectSqlServerIp; textBox4.Text = selectSqlServerUserName; textBox5.Text = selectSqlServerPassword;
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
                string selectedId = dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                string projectName = dataGridView.Rows[e.RowIndex].Cells["ProjectName"].Value.ToString();
                string selectSqlServerIp = dataGridView.Rows[e.RowIndex].Cells["SqlServerIp"].Value.ToString();
                string selectSqlServerUserName = dataGridView.Rows[e.RowIndex].Cells["SqlServerUserName"].Value.ToString();
                string selectSqlServerPassword = dataGridView.Rows[e.RowIndex].Cells["SqlServerPassword"].Value.ToString();
                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql5");
                textBox1.Text = selectedId; textBox2.Text = projectName; textBox3.Text = selectSqlServerIp; textBox4.Text = selectSqlServerUserName; textBox5.Text = selectSqlServerPassword;
            }
        }

        private async void AddSql_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
            TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql1");
            TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql2");
            TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql3");
            TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql4");
            TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql5");

            Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");

            DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
            string sqlServerIP = textBox3.Text;
            string sqlServerUserName = textBox4.Text;
            string sqlServerPassword = textBox5.Text;
            string projectId = label1.Text;


            SqlController sqlController = new SqlController();
            bool result = await sqlController.AddSqlData(sqlServerIP, sqlServerUserName, sqlServerPassword, projectId);
            if (result)
            {
                MessageBox.Show("sql added successfully");

                LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(projectId));
            }
            else
            {
                MessageBox.Show("sql failed added");
            }
        }
        private async void UpdateSql_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
            TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql1");
            TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql2");
            TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql3");
            TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql4");
            TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql5");

            Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");
            DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
            int sqlId = Convert.ToInt32(textBox1.Text);
            string sqlServerIP = textBox3.Text;
            string sqlServerUserName = textBox4.Text;
            string sqlServerPassword = textBox5.Text;
            string projectId = label1.Text;
            SqlController sqlController = new SqlController();
            bool result = await sqlController.UpdateSqlData(sqlId, sqlServerIP, sqlServerUserName, sqlServerPassword, projectId);
            if (result)
            {
                MessageBox.Show("sql updated successfully");

                LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(projectId));
            }
            else
            {
                MessageBox.Show("sql failed  update");
            }
        }
        private async void DeleteSql_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql4");
                TextBox textBox5 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtSql5");

                Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");
                DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                int sqlId = Convert.ToInt32(textBox1.Text);
                string sqlServerIP = textBox3.Text;
                string sqlServerUserName = textBox4.Text;
                string sqlServerPassword = textBox5.Text;
                string projectId = label1.Text;
                SqlController sqlController = new SqlController();
                bool result = await sqlController.DeleteSqlData(sqlId);
                if (result)
                {
                    MessageBox.Show("Sql deleted successfully");
                    LoadDataIntoDataGridView(dataGridView, Convert.ToInt32(projectId));
                    textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear();
                }
                else
                {
                    MessageBox.Show("SQL failed to update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void PdfSql_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buton 4");
        }



        private async void LoadDataIntoDataGridView(DataGridView dataGridView, int id)
        {
            try
            {
                SqlController sqlController = new SqlController();
                var sqls = await sqlController.GetSql(id);
                Sql[] sqlArray = sqls;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID"); dataTable.Columns.Add("ProjectName"); dataTable.Columns.Add("SqlServerIp"); dataTable.Columns.Add("SqlServerUserName"); dataTable.Columns.Add("SqlServerPassword");

                Dictionary<int, string> projectNames = await GetProjectNames();

                foreach (Sql sql in sqlArray)
                {
                    string projectName = projectNames.ContainsKey(sql.ProjectId) ? projectNames[sql.ProjectId] : string.Empty;
                    dataTable.Rows.Add(sql.Id, projectName, sql.SqlServerIp, sql.SqlServerUserName, sql.SqlServerPassword);
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
