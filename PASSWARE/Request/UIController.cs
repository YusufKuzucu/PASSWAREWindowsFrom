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
    public class UIsResponse
    {
        public UI[] Data { get; set; }
    }
    public class UIController
    {
        HttpClient client = new HttpClient();
        public async Task<UI[]> GetUIData(string url)
        {
            UI[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        UIsResponse uısResponse = JsonConvert.DeserializeObject<UIsResponse>(responseContent);
                        data = uısResponse.Data;
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
        public async Task<UI[]> GetUI(int id)
        {
            string apiUrl = "https://localhost:44343/api/";
            UI[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync($"{apiUrl}UIs/GetByUI?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        UIsResponse uısResponse = JsonConvert.DeserializeObject<UIsResponse>(responseContent);
                        data = uısResponse.Data;
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
        public async Task<bool> AddUIData(string uiServerIP, string uiServerUserName, string uiServerPassword,string projectId)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();
            var ui = new
            {
                uiServerIP = uiServerIP,
                uiServerUserName = uiServerUserName,
                uiServerPassword = uiServerPassword,
                projectId = projectId,
                createdBy = ActiveUser.FirstName,
                createdDate = DateTime.Now,
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            var json = JsonConvert.SerializeObject(ui);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}UIs/Post", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateUIData(int Uıid,string uiServerIP, string uiServerUserName, string uiServerPassword,string projectId)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();
            var ui = new
            {
                id= Uıid,
                uiServerIP = uiServerIP,
                uiServerUserName = uiServerUserName,
                uiServerPassword = uiServerPassword,
                projectId = projectId,
                updatedBy = ActiveUser.FirstName,
                updatedDate = DateTime.Now,
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            var json = JsonConvert.SerializeObject(ui);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PutAsync($"{apiUrl}UIs/Update", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteUIData(int id)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            HttpResponseMessage responseMessage = await client.DeleteAsync($"{apiUrl}UIs/Delete?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
