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
    public class FilesResponse
    {
        public Files[] Data { get; set; }
    }
    public class FilesController
    {
        HttpClient client = new HttpClient();
        public async Task<Files[]> GetFilesData(string url)
        {
            Files[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        FilesResponse filessResponse = JsonConvert.DeserializeObject<FilesResponse>(responseContent);
                        data = filessResponse.Data;
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
            }
            return data;


        }

        public async Task<bool> AddFilesData(string connectExplanation, string connectionInfo)
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
                HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Files/Post", content);
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

        public async Task<bool> UpdateFilesData(string connectExplanation, string connectionInfo)
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
                HttpResponseMessage responseMessage = await client.PutAsync($"{apiUrl}Files/Update", content);
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
                HttpResponseMessage responseMessage = await client.DeleteAsync($"{apiUrl}Files/Delete?id={id}");
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
