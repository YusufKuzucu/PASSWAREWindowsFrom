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

namespace PASSWARE
{
    public partial class RegisterPage : Form
    {
        HttpClient client;
        private string apiUrl = "https://localhost:44343/api/";
        public RegisterPage()
        {
            InitializeComponent();
            client = new HttpClient();
        }

        private async void buttonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Please enter an email address.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter a password.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    MessageBox.Show("Please enter a name.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MessageBox.Show("Please enter a surname.");
                    return;
                }

                var registerCredentials = new
                {
                    email = txtEmail.Text,
                    password = txtPassword.Text,
                    firstName = txtFirstName.Text,
                    lastName = txtLastName.Text,
                };

                var json = JsonConvert.SerializeObject(registerCredentials);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Auth/register", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kullanıcı Kaydınız Başarılı Bir Şekilde Oluşturuldu");
                    Login logIn = new Login();
                    logIn.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı kaydınız oluşturulamadı. Lütfen alanları kontrol ediniz!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
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
