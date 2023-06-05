using Newtonsoft.Json;
using PASSWARE.Models;
using PASSWARE.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PASSWARE.Request
{
    public class LinksResponse
    {
        public Link[] Data { get; set; }
    }
    public class LinkController
    {
        HttpClient client = new HttpClient();
        public async Task<Link[]> GetJumpData(string url)
        {
            Link[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        LinksResponse linksResponse = JsonConvert.DeserializeObject<LinksResponse>(responseContent);
                        data = linksResponse.Data;
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
            return data;


        }

        public async Task<bool> AddLinkData(string connectExplanation, string connectionInfo)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/";
                HttpClient client = new HttpClient();
                var link = new
                {
                    connectExplanation = connectExplanation,
                    connectionInfo = connectionInfo,
                    projectId = 1,
                    createdBy = ActiveUser.FirstName,
                    createdDate = DateTime.Now,
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
                var json = JsonConvert.SerializeObject(link);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Links/Post", content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }

        }

        public async Task<bool> UpdateLinkData(string connectExplanation, string connectionInfo)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/";
                HttpClient client = new HttpClient();
                var link = new
                {
                    connectExplanation = connectExplanation,
                    connectionInfo = connectionInfo,
                    projectId = 1,
                    updatedBy = ActiveUser.FirstName,
                    updatedDate = DateTime.Now,
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
                var json = JsonConvert.SerializeObject(link);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PutAsync($"{apiUrl}Links/Update", content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }

        }
        public async Task<bool> DeleteLinkData(int id)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/";
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
                HttpResponseMessage responseMessage = await client.DeleteAsync($"{apiUrl}Links/Delete?id={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }

        }
    }
}
