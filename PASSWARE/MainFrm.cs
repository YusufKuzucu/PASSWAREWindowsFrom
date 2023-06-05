using PASSWARE.TabpageBase;
using PASSWARE.TabpageBase.EntitiesTabPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private async void projectList_Click(object sender, EventArgs e)
        {
            ProjectTabpageList projectTabpageList = new ProjectTabpageList(tabControl1);
            TabPage tabPage = await projectTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "Project";
            tabPage.BackColor = Color.White;
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void sqlsList_Click(object sender, EventArgs e)
        {
            SqlTabpageList sqlTabpageList = new SqlTabpageList(tabControl1);
            TabPage tabPage = await sqlTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "SQl";
            tabPage.BackColor = Color.White;
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void vpnList_Click(object sender, EventArgs e)
        {
            VpnTabpageList vpnTabpageList = new VpnTabpageList(tabControl1);
            TabPage tabPage = await vpnTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "VPN";
            tabPage.BackColor = Color.White;
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void jumpList_Click(object sender, EventArgs e)
        {
            JumpTabpageList jumpTabpageList = new JumpTabpageList(tabControl1);
            TabPage tabPage = await jumpTabpageList.CreateTabPage(tabControl1);
            tabPage.BackColor = Color.White;
            tabPage.Text = "Jump";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void uıList_Click(object sender, EventArgs e)
        {
            UITabpageList uıTabpageList = new UITabpageList(tabControl1);
            TabPage tabPage = await uıTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "UI";
            tabPage.BackColor = Color.White;
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void communicationList_Click(object sender, EventArgs e)
        {
            CommunicationTabpageList commTabpageList = new CommunicationTabpageList(tabControl1);
            TabPage tabPage = await commTabpageList.CreateTabPage(tabControl1);
            tabPage.BackColor = Color.White;
            tabPage.Text = "Communication";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void companyList_Click(object sender, EventArgs e)
        {
            CompanyTabpageControl companyTabpageControl = new CompanyTabpageControl(tabControl1);
            TabPage tabPage = await companyTabpageControl.CreateTabPage(tabControl1);
            tabPage.BackColor = Color.White;
            tabPage.Text = "Company";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            tabControl1.MouseDown += tabControl1_MouseDown;
            tabControl1.DrawItem += tabControl1_DrawItem;
            this.KeyPreview = true;
            this.KeyDown += MainFrm_KeyDown;
        }
        private void MainFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.Refresh();
                StartProgressBar();
            }
        }
        private async void StartProgressBar()
        {
            // ProgressBar'ı sıfırla ve görünür yap
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = true;


            int totalSteps = 100; // İşlemin toplam adım sayısı
            for (int step = 0; step <= totalSteps; step++)
            {
                toolStripProgressBar1.Value = (int)((double)step / totalSteps * 100);


                await Task.Delay(10);

                if (step == totalSteps)
                {
                    toolStripProgressBar1.Value = 0;
                    toolStripProgressBar1.Visible = false;
                }
            }
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = (TabControl)sender;
            Graphics g = e.Graphics;
            Rectangle tabRect = tabControl.GetTabRect(e.Index);

            // Sekme metnini çizme
            string tabText = tabControl.TabPages[e.Index].Text;

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            g.DrawString(tabText, e.Font, Brushes.Black, tabRect, stringFormat);

            // Kapatma düğmesini çizme
            Rectangle closeButtonRect = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 10, 10);
            g.FillRectangle(Brushes.White, closeButtonRect);
            g.DrawRectangle(Pens.Black, closeButtonRect);
            g.DrawString("x", e.Font, Brushes.Black, closeButtonRect, stringFormat);
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Orta fare düğmesine basıldığında
            {
                TabControl tabControl = (TabControl)sender;
                int tabIndex = -1;
                for (int i = 0; i < tabControl.TabCount; i++)
                {
                    Rectangle tabRect = tabControl.GetTabRect(i);
                    Rectangle closeButtonRect = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 10, 10); // Kapatma düğmesinin alanı

                    if (closeButtonRect.Contains(e.Location))
                    {
                        tabIndex = i;
                        break;
                    }
                }

                if (tabIndex != -1)
                {
                    TabPage tabPage = tabControl.TabPages[tabIndex];
                    tabControl.TabPages.Remove(tabPage);
                    tabPage.Dispose();
                }
            }
        }
        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirm Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
