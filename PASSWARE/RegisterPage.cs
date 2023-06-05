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
            var registerCredentials = new
            {
                email = txtEmail.Text,
                password = txtPassword.Text,
                firstName = txtFirstName.Text,
                LastName = txtLastName.Text,
            };
            var json = JsonConvert.SerializeObject(registerCredentials);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Auth/register", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Kullanıcı Kaydınız Başarılı Bir Şekilde Oluşturldu");
                Login logIn = new Login();
                logIn.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı kaydınız oluşturulamadı lütfen alanları kontrol ediniz!");

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
    }
}
