using Newtonsoft.Json;
using PASSWARE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                string username = txtEmail.Text;
                string password = txtPassword.Text;
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Lütfen kullanıcı adı ve şifre girin.");
                    return;
                }

                // Remember Me seçeneği işaretliyse verileri kaydedin
                if (checkBox1.Checked)
                {
                    SaveRememberMeData(username, password, true);
                }


                // Remember Me seçeneği işaretliyse verileri kaydedin
                if (checkBox1.Checked)
                {
                    SaveRememberMeData(username, password, true);
                }
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
                    ActiveUser.Token = accessToken;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    HttpResponseMessage userGetResponse;
                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl + "Users/getbyidemail?email=" + txtEmail.Text))
                    {
                        userGetResponse = await client.SendAsync(requestMessage);
                    }
                    var usercontnet = await userGetResponse.Content.ReadAsStringAsync();
                    var userJson = JsonConvert.DeserializeObject<User>(usercontnet);

                    ActiveUser.SetActiveUser(userJson);

                    HttpResponseMessage userResponse;
                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl + "Users/claims?email=" + txtEmail.Text))
                    {
                        requestMessage.Headers.Add("email", txtEmail.Text);
                        userResponse = await client.SendAsync(requestMessage);
                    }

                    // Get user roles and other information
                    if (userResponse.IsSuccessStatusCode)
                    {
                        var userContent = await userResponse.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<IEnumerable<OperationClaim>>(userContent);
                        ActiveUser.Role = userContent;

                        // Check user's roles
                        if (user.Any(x => x.Name.ToLower() == "Admin".ToLower()))
                        {
                            // Admin yetkilerine sahip işlemler
                            HomePage homePage = new HomePage();
                            homePage.btnCompany.Visible = true;
                            homePage.Show();
                            this.Hide();
                            
                        }
                        else
                        {
                            // Processes with other user roles
                            HomePage homePage = new HomePage();
                            homePage.btnCompany.Visible = false;
                            homePage.Show();
                            this.Hide();
                        
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to retrieve user information!");
                    }
                }
                else
                {
                    MessageBox.Show("Login failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong: " + ex.Message);
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
            if (checkBox2.CheckState == CheckState.Checked)
            {
                txtPassword.UseSystemPasswordChar = true;
                checkBox2.Text = "Hide";
            }
            else if (checkBox2.CheckState == CheckState.Unchecked)
            {
                txtPassword.UseSystemPasswordChar = false;
                checkBox2.Text = "Show";
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

        private void SaveRememberMeData(string username, string password, bool rememberMe)
        {
            XmlDocument xmlDoc = new XmlDocument();

            // RememberMe.xml dosyasını kontrol et
            if (File.Exists("RememberMe.xml"))
            {
                xmlDoc.Load("RememberMe.xml");
            }
            else
            {
                // RememberMe.xml dosyası yoksa yeni bir dosya oluştur
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(xmlDeclaration);

                XmlNode rootNode = xmlDoc.CreateElement("RememberMeData");
                xmlDoc.AppendChild(rootNode);
            }

            XmlNode usernameNode = xmlDoc.SelectSingleNode("//Username");
            XmlNode rememberMeNode = xmlDoc.SelectSingleNode("//RememberMe");

            // Eğer düğümler zaten mevcutsa güncelle, değilse oluştur
            if (usernameNode == null)
            {
                usernameNode = xmlDoc.CreateElement("Username");
                xmlDoc.DocumentElement?.AppendChild(usernameNode);
            }
            if (rememberMeNode == null)
            {
                rememberMeNode = xmlDoc.CreateElement("RememberMe");
                xmlDoc.DocumentElement?.AppendChild(rememberMeNode);
            }
            usernameNode.InnerText = username;
            //passwordNode.InnerText = password;
            rememberMeNode.InnerText = rememberMe.ToString();

            xmlDoc.Save("RememberMe.xml");
        }
        private void LoadRememberMeData()
        {
            if (File.Exists("RememberMe.xml"))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("RememberMe.xml");

                XmlNode usernameNode = xmlDoc.SelectSingleNode("//Username");
                XmlNode rememberMeNode = xmlDoc.SelectSingleNode("//RememberMe");

                if (usernameNode != null && rememberMeNode != null)
                {
                    string username = usernameNode.InnerText;
                    bool rememberMe = Convert.ToBoolean(rememberMeNode.InnerText);

                    // Kullanıcı adı ve parolayı ilgili alanlara yerleştirme işlemleri
                    txtEmail.Text = rememberMe ? username : "";
                    checkBox1.Checked = rememberMe;
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            LoadRememberMeData();
            txtPassword.KeyPress += txtPassword_KeyPress;
        }

       
    }
}
