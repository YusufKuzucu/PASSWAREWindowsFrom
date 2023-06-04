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
using System.ComponentModel.Design;

namespace PASSWARE.Request
{
    public class CommunicationsResponse
    {
        public Communication[] Data { get; set; }
    }
    public class CommunicationController
    {
        HttpClient client = new HttpClient();
        public async Task<Communication[]> GetCommunicationData(string url)
        {
            Communication[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        CommunicationsResponse communicationsResponse = JsonConvert.DeserializeObject<CommunicationsResponse>(responseContent);
                        data = communicationsResponse.Data;
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
        public async Task<Communication[]> GetCommunication(int id)
        {
            string apiUrl = "https://localhost:44343/api/";
            Communication[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync($"{apiUrl}Communications/GetByCommunication?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        CommunicationsResponse commsResponse = JsonConvert.DeserializeObject<CommunicationsResponse>(responseContent);
                        data = commsResponse.Data;
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


        public async Task<bool> AddCommunicationData(string internalNumber, string internalEmail, string externalNumber,string externalEmail,string projectId)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();
            var communication = new
            {
                internalNumber = internalNumber,
                internalEmail = internalEmail,
                externalNumber = externalNumber,
                externalEmail= externalEmail,
                projectId = projectId,
                createdBy = ActiveUser.FirstName,
                createdDate = DateTime.Now,
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            var json = JsonConvert.SerializeObject(communication);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Communications/Post", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCommunicationData(int commId , string internalNumber, string internalEmail, string externalNumber, string externalEmail,string projectId)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();
            var communication = new
            {
                id=commId,
                internalNumber = internalNumber,
                internalEmail = internalEmail,
                externalNumber = externalNumber,
                externalEmail = externalEmail,
                projectId = projectId,
                createdBy = ActiveUser.FirstName,
                createdDate = DateTime.Now,
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            var json = JsonConvert.SerializeObject(communication);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PutAsync($"{apiUrl}Communications/Update", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteCommunicaitonData(int id)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            HttpResponseMessage responseMessage = await client.DeleteAsync($"{apiUrl}Communications/Delete?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
