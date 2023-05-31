﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE
{
    public class HomePageControl
    {
        //private static Label label1;
        public static TabControl CreateTabControlWithTabPage()
        {
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.None;

            TabPage tabPage = CreateTabPage();

            tabControl.TabPages.Add(tabPage);

            return tabControl;
        }

        private static TabPage CreateTabPage()
        {
            TabPage tabPage = new TabPage("TabPage");

            Panel panel = CreatePanel();
            tabPage.Controls.Add(panel);


            DataGridView dataGridView = CreateDataGridView();
            tabPage.Controls.Add(dataGridView);


            Label label1 = CreateLabel("label 1", new System.Drawing.Size(44, 16), new System.Drawing.Point(57, 65), 2);
            tabPage.Controls.Add(label1);

            Label label2 = CreateLabel("label 2", new System.Drawing.Size(44, 16), new System.Drawing.Point(57, 130), 3);
            tabPage.Controls.Add(label2);

            Label label3 = CreateLabel("label 3", new System.Drawing.Size(44, 16), new System.Drawing.Point(57, 201), 4);
            tabPage.Controls.Add(label3);


            Label label4 = CreateLabel("label 4", new System.Drawing.Size(44, 16), new System.Drawing.Point(57, 250), 8);
            tabPage.Controls.Add(label4);





            TextBox textBox1 = CreateTextBox("textbox 1", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 58), 5);
            tabPage.Controls.Add(textBox1);

            TextBox textBox2 = CreateTextBox("textbox 2", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 124), 6);
            tabPage.Controls.Add(textBox2);

            TextBox textBox3 = CreateTextBox("textbox 3", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 195), 7);
            tabPage.Controls.Add(textBox3);

            TextBox textBox4 = CreateTextBox("textbox 4", new System.Drawing.Size(318, 22), new System.Drawing.Point(174, 250), 9);
            tabPage.Controls.Add(textBox4);






            Button button1 = CreateButton("Button 1", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 55), 7);
            button1.Click += Button1_Click;
            panel.Controls.Add(button1);

            Button button2 = CreateButton("Button 2", new System.Drawing.Size(192, 62), new System.Drawing.Point(3, 171), 8);
            button2.Click += Button2_Click;
            panel.Controls.Add(button2);
            return tabPage;
        }

        private static Panel CreatePanel()
        {
            Panel panel = new Panel();
            //panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            // | System.Windows.Forms.AnchorStyles.Right)));
            panel.Dock = DockStyle.Right;
            panel.BackColor = System.Drawing.Color.Gray;
            panel.Location = new System.Drawing.Point(1331, 3);
            panel.Size = new System.Drawing.Size(200, 477);
            panel.TabIndex = 0;
            return panel;
        }

        private static Button CreateButton(string text, Size size, Point location, int tabındex)
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
        private static Label CreateLabel(string text, Size size, Point location, int tabındex)
        {
            Label label = new Label();
            label.Text = text;
            label.Size = size;
            label.ForeColor = Color.Black;
            label.Location = location;
            label.AutoSize = true;
            label.TabIndex = tabındex;
            return label;
        }
        private static TextBox CreateTextBox(string text, Size size, Point location, int tabındex)
        {
            TextBox textBox = new TextBox();
            textBox.Text = text;
            textBox.ForeColor = Color.Black;
            textBox.Size = size;
            textBox.Location = location;
            textBox.TabIndex = tabındex;
            return textBox;
        }
        private static DataGridView CreateDataGridView()
        {
            DataGridView dataGridView = new DataGridView();
            //dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            //| System.Windows.Forms.AnchorStyles.Left)
            //| System.Windows.Forms.AnchorStyles.Right)));
            dataGridView.Anchor = AnchorStyles.Bottom|AnchorStyles.Top|AnchorStyles.Left;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new System.Drawing.Point(3, 292);
            dataGridView.RowHeadersWidth = 51;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.Size = new System.Drawing.Size(1321, 175);
            dataGridView.TabIndex = 1;
            return dataGridView;
        }



        private static void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buton 1");

        }
        private static void Button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buton 2");
        }
        private static void Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("buton 3");
        }
    }
}