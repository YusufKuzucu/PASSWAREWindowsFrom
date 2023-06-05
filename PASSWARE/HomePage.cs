using PASSWARE.Models;
using PASSWARE.Models.Entities;
using PASSWARE.Properties;
using PASSWARE.Request;
using PASSWARE.TabpageBase;
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
            tabPage.Text = "PROJECT";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            SqlTabpageList sqlTabpageList = new SqlTabpageList(tabControl1);
            TabPage tabPage = await sqlTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "SQl";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            tabControl1.MouseDown += tabControl1_MouseDown;

            //tabControl1.DrawItem += tabControl1_DrawItem;
            //tabControl1.MouseClick += tabControl1_MouseClick;

        }

        private async void btnVpn_Click(object sender, EventArgs e)
        {
            VpnTabpageList vpnTabpageList = new VpnTabpageList(tabControl1);
            TabPage tabPage = await vpnTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "VPN";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void btnUı_Click(object sender, EventArgs e)
        {
            UITabpageList uıTabpageList = new UITabpageList(tabControl1);
            TabPage tabPage = await uıTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "UI";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            JumpTabpageList jumpTabpageList = new JumpTabpageList(tabControl1);
            TabPage tabPage = await jumpTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "JUMP";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void btnCommunication_Click(object sender, EventArgs e)
        {
            CommunicationTabpageList commTabpageList = new CommunicationTabpageList(tabControl1);
            TabPage tabPage = await commTabpageList.CreateTabPage(tabControl1);
            tabPage.Text = "COMMUNİCATİON";
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
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


        //private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    TabControl tabControl = (TabControl)sender;
        //    Graphics g = e.Graphics;
        //    Rectangle tabRect = tabControl.GetTabRect(e.Index);

        //    // TabPage başlığını çizin
        //    string tabPageText = tabControl.TabPages[e.Index].Text;
        //    g.DrawString(tabPageText, tabControl.Font, Brushes.Black, tabRect.X + 5, tabRect.Y + 5);

        //    // Kapatma düğmesini çizin
        //    using (Pen pen = new Pen(Color.Black))
        //    {
        //        int closeButtonWidth = 10;
        //        int closeButtonHeight = 10;
        //        int closeButtonX = tabRect.Right - closeButtonWidth - 5;
        //        int closeButtonY = tabRect.Top + (tabRect.Height - closeButtonHeight) / 2;

        //        g.DrawRectangle(pen, closeButtonX, closeButtonY, closeButtonWidth, closeButtonHeight);
        //        g.DrawLine(pen, closeButtonX + 2, closeButtonY + 2, closeButtonX + closeButtonWidth - 2, closeButtonY + closeButtonHeight - 2);
        //        g.DrawLine(pen, closeButtonX + closeButtonWidth - 2, closeButtonY + 2, closeButtonX + 2, closeButtonY + closeButtonHeight - 2);
        //    }
        //}






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
