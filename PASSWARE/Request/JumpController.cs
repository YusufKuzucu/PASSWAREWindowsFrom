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
    public class JumpsResponse
    {
        public Jump[] Data { get; set; }
    }
    public class JumpController
    {
        HttpClient client = new HttpClient();
        public async Task<Jump[]> GetJumpData(string url)
        {
            Jump[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        JumpsResponse jumpsResponse = JsonConvert.DeserializeObject<JumpsResponse>(responseContent);
                        data = jumpsResponse.Data;
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
        public async Task<Jump[]> GetJump(int id)
        {
            string apiUrl = "https://localhost:44343/api/";
            Jump[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync($"{apiUrl}Jumps/GetByJump?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        JumpsResponse jumpsResponse = JsonConvert.DeserializeObject<JumpsResponse>(responseContent);
                        data = jumpsResponse.Data;
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

        public async Task<bool> AddJumpData(string jumpServerIP, string jumpServerUserName, string jumpServerPassword,string projectId)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/";
                HttpClient client = new HttpClient();
                var jump = new
                {
                    jumpServerIP = jumpServerIP,
                    jumpServerUserName = jumpServerUserName,
                    jumpServerPassword = jumpServerPassword,
                    projectId = projectId,
                    createdBy = ActiveUser.FirstName,
                    createdDate = DateTime.Now,
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
                var json = JsonConvert.SerializeObject(jump);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Jumps/Post", content);
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

        public async Task<bool> UpdateJumpData(int jumpdId , string jumpServerIP, string jumpServerUserName, string jumpServerPassword,string projectId)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/";
                HttpClient client = new HttpClient();
                var jump = new
                {
                    id = jumpdId,
                    jumpServerIP = jumpServerIP,
                    jumpServerUserName = jumpServerUserName,
                    jumpServerPassword = jumpServerPassword,
                    projectId = projectId,
                    updatedBy = ActiveUser.FirstName,
                    updatedDate = DateTime.Now,
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
                var json = JsonConvert.SerializeObject(jump);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PutAsync($"{apiUrl}Jumps/Update", content);
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
        public async Task<bool> DeleteJumpData(int id)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/";
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
                HttpResponseMessage responseMessage = await client.DeleteAsync($"{apiUrl}Jumps/Delete?id={id}");
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
