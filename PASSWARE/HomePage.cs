using PASSWARE.Models;
using PASSWARE.Models.Entities;
using PASSWARE.Request;
using PASSWARE.TabpageBase;
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
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            SqlTabpageList sqlTabpageList = new SqlTabpageList(tabControl1);
            TabPage tabPage = await sqlTabpageList.CreateTabPage(tabControl1);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            TabControl tabControl = (TabControl)tabControl1;
        }

        private async void btnVpn_Click(object sender, EventArgs e)
        {
            VpnTabpageList vpnTabpageList = new VpnTabpageList(tabControl1);
            TabPage tabPage = await vpnTabpageList.CreateTabPage(tabControl1);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void btnUı_Click(object sender, EventArgs e)
        {
            UITabpageList uıTabpageList = new UITabpageList(tabControl1);
            TabPage tabPage = await uıTabpageList.CreateTabPage(tabControl1);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            JumpTabpageList jumpTabpageList = new JumpTabpageList(tabControl1);
            TabPage tabPage = await jumpTabpageList.CreateTabPage(tabControl1);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }

        private async void btnCommunication_Click(object sender, EventArgs e)
        {
            CommunicationTabpageList commTabpageList = new CommunicationTabpageList(tabControl1);
            TabPage tabPage = await commTabpageList.CreateTabPage(tabControl1);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }
    }
}
