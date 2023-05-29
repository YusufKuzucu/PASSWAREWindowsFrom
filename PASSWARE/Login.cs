using Newtonsoft.Json;
using PASSWARE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE
{
    public partial class Login : Form
    {
        private static HttpClient client;
        public string accessToken;
        private string apiUrl = "https://localhost:44343/api/";
        public Login()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var loginCredentials = new
            {
                email = txtEmail.Text,
                password = txtPassword.Text,
            };
            var serializedBody = JsonConvert.SerializeObject(loginCredentials);
            var content = new StringContent(serializedBody, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{apiUrl}Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var tokenResult = response.Content.ReadAsStringAsync().Result;
                accessToken = JsonConvert.DeserializeObject<AccessToken>(tokenResult).Token;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpResponseMessage userResponse;
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl + "Users/claims?email=" + txtEmail.Text))
                {
                    requestMessage.Headers.Add("email", txtEmail.Text);

                    userResponse = await client.SendAsync(requestMessage);
                }
                // Kullanıcı rollerini ve diğer bilgileri al

                if (userResponse.IsSuccessStatusCode)
                {
                    var userContent = await userResponse.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<IEnumerable<OperationClaim>>(userContent);

                    // Kullanıcının rollerini kontrol et
                    if (user.Any(x => x.Name.ToLower() == "Admin".ToLower()))
                    {
                        // Admin yetkilerine sahip işlemler
                        MessageBox.Show("Admin olarak giriş yapıldı!");
                        HomePage homePage = new HomePage();
                        homePage.Show();
                        this.Hide();

                    }
                    else
                    {
                        // Diğer kullanıcı rollerine sahip işlemler
                        MessageBox.Show("Kullanıcı olarak giriş yapıldı!");
                        HomePage homePage = new HomePage();
                        homePage.Show();
                        this.Hide();

                        //toolStripStatusLabel1.Text = accessToken;
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı bilgileri alınamadı!");
                }
            }
            else
            {
                MessageBox.Show("Giriş başarısız!");
            }

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            registerPage.Show();
            this.Hide();
        }

        private void btnForgotMyPassword_Click(object sender, EventArgs e)
        {
            ForgotMyPassword forgotMyPassword = new ForgotMyPassword();
            forgotMyPassword.Show();
            this.Hide();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.CheckState==CheckState.Checked)
            {
                    txtPassword.UseSystemPasswordChar = true;
                checkBox2.Text = "Hide";
            }
            else if(checkBox2.CheckState==CheckState.Unchecked)
            {
                txtPassword.UseSystemPasswordChar=false;
                checkBox2.Text = "Show";
            }
        }
    }
}
