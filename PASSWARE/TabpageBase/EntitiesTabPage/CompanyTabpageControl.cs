using PASSWARE.Models.Entities;
using PASSWARE.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE.TabpageBase.EntitiesTabPage
{
    public class CompanyTabpageControl
    {
        private readonly HttpClient client;
        private TabControl tabControl;

        public CompanyTabpageControl(TabControl tabControl)
        {
            client = new HttpClient();
            this.tabControl = tabControl;
        }
        public async Task<TabPage> CreateTabPage(TabControl tabControl)
        {
            TabPage tabPage = new TabPage("TabPage");
            Panel panel = CreatePanel();
            tabPage.Controls.Add(panel);

            DataGridView dataGridView = CreateDataGridView();
            dataGridView.Name = "dataGridView";
            tabPage.Controls.Add(dataGridView);
            await LoadDataIntoDataGridView(dataGridView);


            Label label1 = CreateLabel("ID", "label1", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 34), 2);
            label1.Enabled = false;
            tabPage.Controls.Add(label1);

            Label label2 = CreateLabel( "CompanyName", "label2", new System.Drawing.Size(44, 16), new System.Drawing.Point(50, 80), 3);
            tabPage.Controls.Add(label2);



            TextBox textBox1 = CreateTextBox("txtcompany1", new System.Drawing.Size(318, 25), new System.Drawing.Point(174, 34), 5,"");
            textBox1.Enabled = false;
            tabPage.Controls.Add(textBox1);

            TextBox textBox2 = CreateTextBox("txtcompany2", new System.Drawing.Size(318, 25), new System.Drawing.Point(174, 80), 5, "");
            tabPage.Controls.Add(textBox2);


            Button button1 = CreateButton("Add", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 55), 7);
            button1.Click += AddCompany_Click;
            button1.Image = Properties.Resources.save;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            panel.Controls.Add(button1);

            Button button2 = CreateButton("Update", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 171), 8);
            button2.Image = Properties.Resources.update;
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Click += UpdateCompany_Click;
            panel.Controls.Add(button2);

            Button button3 = CreateButton("Delete ", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 290), 9);
            button3.Image = Properties.Resources.trash;
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Click += DeleteCompany_Click;
            panel.Controls.Add(button3);

            Button button4 = CreateButton("Pdf", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 410), 10);
            button4.Image = Properties.Resources.pdf;
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.Click += PdfCompany_Click;
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
            dataGridView.Location = new System.Drawing.Point(0, 200);
            dataGridView.RowHeadersWidth = 51;
            dataGridView.ScrollBars = ScrollBars.Both;
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.Dock = DockStyle.None;
            dataGridView.Size = new System.Drawing.Size(1325, 550);
            dataGridView.TabIndex = 1;
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
                string companyName = selectedRow.Cells["CompanyName"].Value.ToString();
             
                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcompany1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcompany2");
          
                textBox1.Text = selectedId; textBox2.Text = companyName;
            }
        }

        private void DataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count && e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView.Columns.Count)
            {
                // Seçili hücrenin değerini al
                DataGridViewCell selectedCell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string selectedId = dataGridView.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                string companyName = dataGridView.Rows[e.RowIndex].Cells["CompanyName"].Value.ToString();

                TabPage tabPage = (TabPage)dataGridView.Parent;
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcompany1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcompany2");

                textBox1.Text = selectedId; textBox2.Text = companyName;
            }
        }
        private async Task LoadDataIntoDataGridView(DataGridView dataGridView)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/Companies/GetAll";
                CompanyController companyController = new CompanyController();
                var companys = await companyController.GetCompanyData(apiUrl);
                Company[] companyArray = companys;

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID"); dataTable.Columns.Add("CompanyName");


                foreach (Company company in companyArray)
                {
                    dataTable.Rows.Add(company.Id, company.CompanyName);
                }
                dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from API.  Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void AddCompany_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcompany1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcompany2");
                DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                string compayId = textBox1.Text;
                string companyName = textBox2.Text;

                CompanyController companyController = new CompanyController();
                bool result = await companyController.AddCompaniesData(companyName);
                if (result)
                {
                    MessageBox.Show("Company Added successfully");

                   await LoadDataIntoDataGridView(dataGridView);
                }
                else
                {
                    MessageBox.Show("Company Failed to Added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private async void UpdateCompany_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcompany1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcompany2");


                Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");
                DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                int companyId = Convert.ToInt32(textBox1.Text);
                string compayName = textBox2.Text;
              
                string projectId = textBox1.Text;
                CompanyController companyController = new CompanyController();

                bool result = await companyController.UpdateCompaniesData(compayName, companyId);
                if (result)
                {
                    MessageBox.Show("Company Updated Successfully");

                  await  LoadDataIntoDataGridView(dataGridView);
                }
                else
                {
                    MessageBox.Show("Company Failed to Update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

        }
        private async void DeleteCompany_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                TabPage tabPage = (TabPage)button.Parent.Parent; // Butonun ebeveyninin ebeveyni olan TabPage'i alır
                TextBox textBox1 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcompany1");
                TextBox textBox2 = tabPage.Controls.OfType<TextBox>().FirstOrDefault(c => c.Name == "txtcompany2");

                Label label1 = tabPage.Controls.OfType<Label>().FirstOrDefault(x => x.Name == "label6");
                DataGridView dataGridView = tabPage.Controls.OfType<DataGridView>().FirstOrDefault(x => x.Name == "dataGridView");
                int companyId = Convert.ToInt32(textBox1.Text);
                string companyName=textBox2.Text;

                CompanyController companyController = new CompanyController();

                bool result = await companyController.DeleteCompaniesData(companyId);
                if (result)
                {
                    MessageBox.Show("Company Deleted Successfully");
                    await LoadDataIntoDataGridView(dataGridView);
                    textBox1.Clear(); textBox2.Clear(); 
                }
                else
                {
                    MessageBox.Show("Company Failed to Update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void PdfCompany_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buton 4");
        }
       

    }
}
