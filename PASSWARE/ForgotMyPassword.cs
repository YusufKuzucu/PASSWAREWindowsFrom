using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace PASSWARE
{
    public partial class ForgotMyPassword : Form
    {
        HttpClient client;
        private string apiUrl = "https://localhost:44343/api/";
        public ForgotMyPassword()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private async void btnNewPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtBoxEmail.Text;
                string verificationNumber = txtBoxVerificationCode.Text;
                string password = txtBoxNewPassword.Text;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(verificationNumber) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                var resetPassword = new
                {
                    email = email,
                    verificationNumber = verificationNumber,
                    password = password,
                };
                var json = JsonConvert.SerializeObject(resetPassword);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Auth/ResetPassword", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Your Password Has Been Regenerated", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Your Password Could Not Be Created. Please check the fields.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }

        }

        private void ForgotMyPassword_Load(object sender, EventArgs e)
        {

            txtBoxEmail.Visible = false;
            txtBoxVerificationCode.Visible = false;
            txtBoxNewPassword.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            btnNewPassword.Visible = false;
        }

        private async void btnVerificationCode_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmailD.Text;
                if (string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Please enter your e-mail address.");
                    return;
                }

                HttpResponseMessage userResponse;
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl + "Auth/forgotmypassword?email=" + email))
                {
                    userResponse = await client.SendAsync(requestMessage);
                }

                if (userResponse.IsSuccessStatusCode)
                {
                    MessageBox.Show("Verification Code Sent", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmailD.Visible = false;
                    btnVerificationCode.Visible = false;
                    label1.Visible = false;
                    txtBoxEmail.Visible = true;
                    txtBoxVerificationCode.Visible = true;
                    txtBoxNewPassword.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    label4.Visible = true;
                    btnNewPassword.Visible = true;
                }
                else
                {
                    MessageBox.Show("Failed to Send Verification Code");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(" Are you sure you want to close the program?", "Passware program", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
