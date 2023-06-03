using Newtonsoft.Json;
using PASSWARE.Models;
using PASSWARE.Models.Entities;
using PASSWARE.Request;
using PASSWARE.TabpageBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE
{
    public class HomePageControl
    {
        private int Id;
        private DataGridView dataGridView;
        public TabPage CreateTabPage(string projectId, string selectedId,string selectSqlServerIp, string selectSqlServerUserName, string selectSqlServerPassword, string colum1name, string colum2name, string colum3name, string colum4name, string colum5name, DataTable filterData)
        {
            TabPage tabPage = new TabPage("TabPage");
            Id=Convert.ToInt32(projectId);
            Panel panel = CreatePanel();
            tabPage.Controls.Add(panel);

            DataGridView dataGridView = CreateDataGridView();
            dataGridView.DataSource = filterData;
            dataGridView.Name = "dataGridView";
            tabPage.Controls.Add(dataGridView);
          
            Label label1 = CreateLabel(colum2name,"label1", new System.Drawing.Size(44, 16), new System.Drawing.Point(57, 65), 2);
            tabPage.Controls.Add(label1);

            Label label2 = CreateLabel(colum3name, "label2", new System.Drawing.Size(44, 16), new System.Drawing.Point(57, 130), 3);
            tabPage.Controls.Add(label2);

            Label label3 = CreateLabel(colum4name, "label3", new System.Drawing.Size(44, 16), new System.Drawing.Point(57, 201), 4);
            tabPage.Controls.Add(label3);

            Label label4 = CreateLabel(colum5name, "label4", new System.Drawing.Size(44, 16), new System.Drawing.Point(57, 250), 8);
            tabPage.Controls.Add(label4);

            Label label5 = CreateLabel(selectedId, "label5", new System.Drawing.Size(44, 16), new System.Drawing.Point(57, 50), 8);
            label5.Enabled = false;
            tabPage.Controls.Add(label5);

            Label label6 = CreateLabel(projectId, "label6", new System.Drawing.Size(44, 16), new System.Drawing.Point(57, 35), 8); ;
            label6.Enabled = false;
            tabPage.Controls.Add(label6);


            TextBox textBox1 = CreateTextBox("textbox1" ,new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 58), 5, selectedId);
            tabPage.Controls.Add(textBox1);

            TextBox textBox2 = CreateTextBox("textbox2", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 124), 6, selectSqlServerIp);
            tabPage.Controls.Add(textBox2);

            TextBox textBox3 = CreateTextBox("textbox3", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 195), 7, selectSqlServerUserName);
            tabPage.Controls.Add(textBox3);

            TextBox textBox4 = CreateTextBox("textbox4", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 250), 9, selectSqlServerPassword);
            tabPage.Controls.Add(textBox4);

            Button button1 = CreateButton("Add", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 55), 7);
            button1.Click += Button1_Click;
            panel.Controls.Add(button1);

            Button button2 = CreateButton("Update", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 171), 8);
            button2.Click += Button2_Click;
            panel.Controls.Add(button2);

            Button button3 = CreateButton("Delete ", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 290), 9);
            button3.Click += Button3_Click;
            panel.Controls.Add(button3);

            Button button4 = CreateButton("Pdf", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 410), 10);
            button4.Click += Button4_Click;
            panel.Controls.Add(button4);
            return tabPage;
        }
        private  Panel CreatePanel()
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
        private Label CreateLabel(string text,string name, Size size, Point location, int tabındex)
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
        private TextBox CreateTextBox(string text, Size size, Point location, int tabındex,string text2)
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
            dataGridView.Location = new System.Drawing.Point(0, 275);
            dataGridView.RowHeadersWidth = 51;
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.ScrollBars=ScrollBars.Vertical;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.Dock = DockStyle.None;
            dataGridView.Size = new System.Drawing.Size(1325, 390);
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
                //string selectProjectId = selectedRow.Cells["ProjectId"].Value.ToString();
                string selectSqlServerIp = selectedRow.Cells["SqlServerIp"].Value.ToString();
                string selectSqlServerUserName = selectedRow.Cells["SqlServerUserName"].Value.ToString();
                string selectSqlServerPassword = selectedRow.Cells["SqlServerPassword"].Value.ToString();
                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox4");
                textBox1.Text = selectedId;
                textBox2.Text = selectSqlServerIp;  
                textBox3.Text = selectSqlServerUserName;
                textBox4.Text = selectSqlServerPassword;
            }
        }
        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView.Columns.Count)
            {
                // Seçili hücrenin değerini al
                DataGridViewCell selectedCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string selectedId = selectedCell.Value.ToString();
                string selectSqlServerIp = dataGridView.Rows[e.RowIndex].Cells["SqlServerIp"].Value.ToString();
                string selectSqlServerUserName = dataGridView.Rows[e.RowIndex].Cells["SqlServerUserName"].Value.ToString();
                string selectSqlServerPassword = dataGridView.Rows[e.RowIndex].Cells["SqlServerPassword"].Value.ToString();
                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox2");
                TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox3");
                TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox4");
                textBox1.Text = selectedId;
                textBox2.Text = selectSqlServerIp;
                textBox3.Text = selectSqlServerUserName;
                textBox4.Text = selectSqlServerPassword;
            }
        }
        private async void Button1_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
            TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox1");
            TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox2");
            TextBox textBox3 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox3");
            TextBox textBox4 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "textbox4");
            Label label1=tabPage.Controls.OfType<Label>().FirstOrDefault(x=>x.Name=="label6");
            DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
            string sqlServerIP = textBox1.Text;
            string sqlServerUserName = textBox2.Text;
            string sqlServerPassword = textBox3.Text;
            string projectId = label1.Text;
         
            SqlController sqlController = new SqlController();
            bool result = await sqlController.AddSqlData(sqlServerIP, sqlServerUserName, sqlServerPassword,projectId);
            if (result)
            {
                MessageBox.Show("sql eklendi");
              
                LoadDataIntoDataGridView( dataGridView,Convert.ToInt32(projectId));
            }
            else
            {
                MessageBox.Show("sql eklenmedi");
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buton 2");
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buton 3");
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buton 4");
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buton 5");
        }


        private async void LoadDataIntoDataGridView(DataGridView dataGridView, int id)
        {
            try
            {
                SqlController sqlController = new SqlController();
                var sqls = await sqlController.GetSql(id);
                Sql[] sqlArray = sqls;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID");
                dataTable.Columns.Add("ProjectName");
                //dataTable.Columns.Add("ProjectId");
                dataTable.Columns.Add("SqlServerIp");
                dataTable.Columns.Add("SqlServerUserName");
                dataTable.Columns.Add("SqlServerPassword");

                Dictionary<int, string> projectNames = await GetProjectNames();

                foreach (Sql sql in sqlArray)
                {
                    string projectName = projectNames.ContainsKey(sql.ProjectId) ? projectNames[sql.ProjectId] : string.Empty;
                    dataTable.Rows.Add(sql.Id, projectName, sql.SqlServerIp, sql.SqlServerUserName, sql.SqlServerPassword);
                }

                // Orijinal verileri sakla

            
                // DataTable'ı DataGridView'e yükle
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("API'den veri alınamadı. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Projelerin adları alınamadı. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return projectNames;
        }
    }
}
