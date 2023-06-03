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

        private void button1_Click(object sender, EventArgs e)
        {
            //TabControl tabControl = HomePageControl.CreateTabControlWithTabPage();
            //this.tabControl1.TabPages.AddRange(tabControl.TabPages.Cast<TabPage>().ToArray());
          
            //HomePageControl homePageControl=new HomePageControl();
            //TabPage tabPage = homePageControl.CreateTabPage();
            //tabControl1.TabPages.Add(tabPage);
            //tabControl1.SelectedTab = tabPage;

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            SqlTabpageList sqlTabpageList = new SqlTabpageList(tabControl1);
            TabPage tabPage = await sqlTabpageList.CreateTabPage(tabControl1);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
        }
    }
}
