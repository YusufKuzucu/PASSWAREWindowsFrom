using Newtonsoft.Json;
using PASSWARE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PASSWARE.Models;

namespace PASSWARE.Request
{
    public class SqlsResponse
    {
        public Sql[] Data { get; set; }
    }
    public class SqlController
    {
        HttpClient client = new HttpClient();
        public async Task<Sql[]> GetSqlData(string url)
        {
            Sql[] veriler = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        SqlsResponse jumpsResponse = JsonConvert.DeserializeObject<SqlsResponse>(responseContent);
                        veriler = jumpsResponse.Data;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: JSON yanıtı geçerli bir dizi (array) yapısını içermiyor.");
                    }
                }
                else
                {
                    MessageBox.Show("Hata kodu: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beklenmeyen bir hata oluştu: " + ex.Message);
                // Hata kaydını loglama veya diğer gerekli işlemleri burada gerçekleştirebilirsiniz.
                throw; // Hatanın takipçisine iletilmesi.
            }
            return veriler;


        }
        public async Task<bool> AddSqlData(string sqlServerIP, string sqlServerUserName, string sqlServerPassword)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();
            var sql = new
            {
                sqlServerIP = sqlServerIP,
                sqlServerUserName = sqlServerUserName,
                sqlServerPassword = sqlServerPassword,
                projectId=1,
                createdby = ActiveUser.FirstName,
                createdDate = DateTime.Now,
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            var json = JsonConvert.SerializeObject(sql);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Sqls/Post", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Sql eklendi");
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateSqlData(string sqlServerIP, string sqlServerUserName, string sqlServerPassword)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();
            var sql = new
            {
                sqlServerIP = sqlServerIP,
                sqlServerUserName = sqlServerUserName,
                sqlServerPassword = sqlServerPassword,
                projectId = 1,
                createdby = ActiveUser.FirstName,
                createdDate = DateTime.Now,
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            var json = JsonConvert.SerializeObject(sql);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Sqls/Update", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Sql updated");
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteSqlData(int id)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            HttpResponseMessage responseMessage = await client.DeleteAsync($"{apiUrl}Sqls/Delete?id={id}");
            

            if (responseMessage.IsSuccessStatusCode)
            {
                MessageBox.Show("Sql Deleted");
                return true;
            }
            return false;
        }

    }
}
