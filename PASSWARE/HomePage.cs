using PASSWARE.Models;
using PASSWARE.Models.Entities;
using PASSWARE.Properties;
using PASSWARE.Request;
using PASSWARE.TabpageBase;
using PASSWARE.TabpageBase.EntitiesTabPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            ProjectTabpageList projectTabpageList = new ProjectTabpageList(tabControl1);
            TabPage tabPage = await projectTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "Project";
            tabPage.BackColor= Color.White;
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            SqlTabpageList sqlTabpageList = new SqlTabpageList(tabControl1);
            TabPage tabPage = await sqlTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "SQl";
            tabPage.BackColor = Color.White;
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            tabControl1.MouseDown += tabControl1_MouseDown;
            tabControl1.DrawItem += tabControl1_DrawItem;
            //tabControl1.MouseClick += tabControl1_MouseClick;

        }

        private async void btnVpn_Click(object sender, EventArgs e)
        {
            VpnTabpageList vpnTabpageList = new VpnTabpageList(tabControl1);
            TabPage tabPage = await vpnTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "VPN";
            tabPage.BackColor = Color.White;
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void btnUı_Click(object sender, EventArgs e)
        {
            UITabpageList uıTabpageList = new UITabpageList(tabControl1);
            TabPage tabPage = await uıTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "UI";
            tabPage.BackColor = Color.White;
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            JumpTabpageList jumpTabpageList = new JumpTabpageList(tabControl1);
            TabPage tabPage = await jumpTabpageList.CreateTabPage(tabControl1);
            tabPage.BackColor = Color.White;
            tabPage.Text = "Jump";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void btnCommunication_Click(object sender, EventArgs e)
        {
            CommunicationTabpageList commTabpageList = new CommunicationTabpageList(tabControl1);
            TabPage tabPage = await commTabpageList.CreateTabPage(tabControl1);
            tabPage.BackColor = Color.White;
            tabPage.Text = "Communication";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }
        //private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left) // Orta fare düğmesine basıldığında
        //    {
        //        TabControl tabControl = (TabControl)sender;
        //        int tabIndex = -1;
        //        for (int i = 0; i < tabControl.TabCount; i++)
        //        {
        //            Rectangle tabRect = tabControl.GetTabRect(i);
        //            Rectangle closeButtonRect = new Rectangle(tabRect.Right - 15, tabRect.Top + 4, 10, 10); // Kapatma düğmesinin alanı

        //            if (closeButtonRect.Contains(e.Location))
        //            {
        //                tabIndex = i;
        //                break;
        //            }
        //        }

        //        if (tabIndex != -1)
        //        {
        //            TabPage tabPage = tabControl.TabPages[tabIndex];
        //            tabControl.TabPages.Remove(tabPage);
        //            tabPage.Dispose();
        //        }
        //    }
        //}


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

        private async void btnCompany_Click(object sender, EventArgs e)
        {
            CompanyTabpageControl companyTabpageControl = new CompanyTabpageControl(tabControl1);
            TabPage tabPage = await companyTabpageControl.CreateTabPage(tabControl1);
            tabPage.BackColor = Color.White;
            tabPage.Text = "Company";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainFrm mainfrm = new MainFrm();
            mainfrm.ShowDialog();
        }

        private void HomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Confirm
        }

        private void HomePage_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }





        //private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    TabControl tabControl = (TabControl)sender;
        //    Graphics g = e.Graphics;
        //    Rectangle tabRect = tabControl.GetTabRect(e.Index);

        //    // TabPage başlığını çizin
        //    string tabPageText = tabControl.TabPages[e.Index].Text;
        //    g.DrawString(tabPageText, tabControl.Font, Brushes.Black, tabRect.X + 5, tabRect.Y + 5);

        //    // Kapatma düğmesini çizin
        //    using ( Image closeButtonImage = Properties.Resources.indir) // Kapatma resmi dosya yolunu doğru şekilde ayarlayın
        //    {
        //        int closeButtonWidth = closeButtonImage.Width;
        //        int closeButtonHeight = closeButtonImage.Height;
        //        int closeButtonX = tabRect.Right - closeButtonWidth - 5;
        //        int closeButtonY = tabRect.Top + (tabRect.Height - closeButtonHeight) / 2;

        //        g.DrawImage(closeButtonImage, closeButtonX, closeButtonY, closeButtonWidth, closeButtonHeight);

        //        // Kapatma düğmesinin tıklama olayını ele al
        //        if (e.Index == tabControl.SelectedIndex)
        //        {
        //            tabControl.Tag = e.Index;
        //        }
        //    }
        //}

        //private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        //{
        //    TabControl tabControl = (TabControl)sender;
        //    int closeButtonWidth = 10;
        //    int closeButtonHeight = 10;
        //    int closeButtonX = tabControl.GetTabRect((int)tabControl.Tag).Right - closeButtonWidth - 5;
        //    int closeButtonY = tabControl.GetTabRect((int)tabControl.Tag).Top + (tabControl.GetTabRect((int)tabControl.Tag).Height - closeButtonHeight) / 2;

        //    Rectangle closeButtonRect = new Rectangle(closeButtonX, closeButtonY, closeButtonWidth, closeButtonHeight);

        //    // Kapatma düğmesine tıklanıp tıklanmadığını kontrol et
        //    if (closeButtonRect.Contains(e.Location))
        //    {
        //        TabPage tabPage = tabControl.TabPages[(int)tabControl.Tag];
        //        tabControl.TabPages.Remove(tabPage);
        //        tabPage.Dispose();
        //    }
        //}






    }
}
