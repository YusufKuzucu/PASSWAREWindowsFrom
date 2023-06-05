namespace PASSWARE
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.ribbonTab1 = new System.Windows.Forms.RibbonTab();
            this.rPProjetcs = new System.Windows.Forms.RibbonPanel();
            this.projectList = new System.Windows.Forms.RibbonButton();
            this.ribbonTab2 = new System.Windows.Forms.RibbonTab();
            this.rPSqls = new System.Windows.Forms.RibbonPanel();
            this.ribbonTab3 = new System.Windows.Forms.RibbonTab();
            this.rPVpns = new System.Windows.Forms.RibbonPanel();
            this.ribbonTab4 = new System.Windows.Forms.RibbonTab();
            this.rPJumps = new System.Windows.Forms.RibbonPanel();
            this.ribbonTab5 = new System.Windows.Forms.RibbonTab();
            this.rPUIs = new System.Windows.Forms.RibbonPanel();
            this.ribbonTab6 = new System.Windows.Forms.RibbonTab();
            this.rPCommunications = new System.Windows.Forms.RibbonPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ribbonTab7 = new System.Windows.Forms.RibbonTab();
            this.rPCompanys = new System.Windows.Forms.RibbonPanel();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            this.ribbon1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.Size = new System.Drawing.Size(527, 72);
            this.ribbon1.OrbDropDown.TabIndex = 0;
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon1.Size = new System.Drawing.Size(1428, 170);
            this.ribbon1.TabIndex = 0;
            this.ribbon1.Tabs.Add(this.ribbonTab1);
            this.ribbon1.Tabs.Add(this.ribbonTab2);
            this.ribbon1.Tabs.Add(this.ribbonTab3);
            this.ribbon1.Tabs.Add(this.ribbonTab4);
            this.ribbon1.Tabs.Add(this.ribbonTab5);
            this.ribbon1.Tabs.Add(this.ribbonTab6);
            this.ribbon1.Tabs.Add(this.ribbonTab7);
            this.ribbon1.Text = "ribbon1";
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.Name = "ribbonTab1";
            this.ribbonTab1.Panels.Add(this.rPProjetcs);
            this.ribbonTab1.Text = "Projects";
            // 
            // rPProjetcs
            // 
            this.rPProjetcs.Items.Add(this.projectList);
            this.rPProjetcs.Name = "rPProjetcs";
            this.rPProjetcs.Text = "Project Details";
            // 
            // projectList
            // 
            this.projectList.Image = ((System.Drawing.Image)(resources.GetObject("projectList.Image")));
            this.projectList.LargeImage = ((System.Drawing.Image)(resources.GetObject("projectList.LargeImage")));
            this.projectList.Name = "projectList";
            this.projectList.SmallImage = ((System.Drawing.Image)(resources.GetObject("projectList.SmallImage")));
            this.projectList.Text = "ProjectList";
            // 
            // ribbonTab2
            // 
            this.ribbonTab2.Name = "ribbonTab2";
            this.ribbonTab2.Panels.Add(this.rPSqls);
            this.ribbonTab2.Text = "Sqls";
            // 
            // rPSqls
            // 
            this.rPSqls.Name = "rPSqls";
            this.rPSqls.Text = "Sql Details";
            // 
            // ribbonTab3
            // 
            this.ribbonTab3.Name = "ribbonTab3";
            this.ribbonTab3.Panels.Add(this.rPVpns);
            this.ribbonTab3.Text = "Vpns";
            // 
            // rPVpns
            // 
            this.rPVpns.Name = "rPVpns";
            this.rPVpns.Text = "Vpn Details";
            // 
            // ribbonTab4
            // 
            this.ribbonTab4.Name = "ribbonTab4";
            this.ribbonTab4.Panels.Add(this.rPJumps);
            this.ribbonTab4.Text = "Jumps";
            // 
            // rPJumps
            // 
            this.rPJumps.Name = "rPJumps";
            this.rPJumps.Text = "Jump Details";
            // 
            // ribbonTab5
            // 
            this.ribbonTab5.Name = "ribbonTab5";
            this.ribbonTab5.Panels.Add(this.rPUIs);
            this.ribbonTab5.Text = "UIs";
            // 
            // rPUIs
            // 
            this.rPUIs.Name = "rPUIs";
            this.rPUIs.Text = "Uıs Details";
            // 
            // ribbonTab6
            // 
            this.ribbonTab6.Name = "ribbonTab6";
            this.ribbonTab6.Panels.Add(this.rPCommunications);
            this.ribbonTab6.Text = "Communications";
            // 
            // rPCommunications
            // 
            this.rPCommunications.Name = "rPCommunications";
            this.rPCommunications.Text = "Communication Details";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 177);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1428, 495);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1420, 466);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 675);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1428, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ribbonTab7
            // 
            this.ribbonTab7.Name = "ribbonTab7";
            this.ribbonTab7.Panels.Add(this.rPCompanys);
            this.ribbonTab7.Text = "Companys";
            // 
            // rPCompanys
            // 
            this.rPCompanys.Name = "rPCompanys";
            this.rPCompanys.Text = "Company Details";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 697);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.ribbon1);
            this.KeyPreview = true;
            this.Name = "MainFrm";
            this.Text = "PassWare";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonTab ribbonTab1;
        private System.Windows.Forms.RibbonPanel rPProjetcs;
        private System.Windows.Forms.RibbonButton projectList;
        private System.Windows.Forms.RibbonTab ribbonTab2;
        private System.Windows.Forms.RibbonPanel rPSqls;
        private System.Windows.Forms.RibbonTab ribbonTab3;
        private System.Windows.Forms.RibbonPanel rPVpns;
        private System.Windows.Forms.RibbonTab ribbonTab4;
        private System.Windows.Forms.RibbonPanel rPJumps;
        private System.Windows.Forms.RibbonTab ribbonTab5;
        private System.Windows.Forms.RibbonPanel rPUIs;
        private System.Windows.Forms.RibbonTab ribbonTab6;
        private System.Windows.Forms.RibbonPanel rPCommunications;
        private System.Windows.Forms.RibbonTab ribbonTab7;
        private System.Windows.Forms.RibbonPanel rPCompanys;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}