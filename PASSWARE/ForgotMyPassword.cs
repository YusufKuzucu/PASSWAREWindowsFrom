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
            var resetPassword = new
            {
                email = txtBoxEmail.Text,
                verificationNumber = txtBoxVerificationCode.Text,
                password = txtBoxNewPassword.Text,
            };
            var json = JsonConvert.SerializeObject(resetPassword);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Auth/ResetPassword", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Parolanız Yeniden Oluşturuldu ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Login login = new Login();
                login.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("parolanız Oluşturulamadı Lütfen Alanları Kontrol Ediniz");
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
            HttpResponseMessage userResponse;
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl + "Auth/forgotmypassword?email=" + txtEmailD.Text))
            {
                userResponse = await client.SendAsync(requestMessage);
            }

            if (userResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Doğrulama Kodu Gönderildi", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("kod gönderilemedi");

            }
        }
    }
}
