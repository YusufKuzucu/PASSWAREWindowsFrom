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
    public class ProjectsResponse
    {
        public Project[] Data { get; set; }
    }
    public class ProjectController
    {
        HttpClient client = new HttpClient();
        public async Task<Project[]> GetProjectData(string url)
        {
            Project[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        ProjectsResponse projectsResponse = JsonConvert.DeserializeObject<ProjectsResponse>(responseContent);
                        data = projectsResponse.Data;
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
        public async Task<Project[]> GetProject(int id)
        {
            string apiUrl = "https://localhost:44343/api/";
            Project[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync($"{apiUrl}Projects/GetByProject?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        ProjectsResponse projectsResponse = JsonConvert.DeserializeObject<ProjectsResponse>(responseContent);
                        data = projectsResponse.Data;
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
        public async Task<bool> AddProjectData(string projectName, string projectServerIP,string projectServerUserName,string projectServerPassword,string companyId)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();
            var project = new
            {
                projectName = projectName,
                projectServerIP = projectServerIP,
                projectServerUserName = projectServerUserName,
                projectServerPassword= projectServerPassword,
                companyId = companyId,
                createdBy = ActiveUser.FirstName,
                createdDate = DateTime.Now,
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            var json = JsonConvert.SerializeObject(project);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Projects/Post", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProjectData(int projectId,string projectName, string projectServerIP,string projectServerUserName,string projectServerPassword,string companyId)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();
            var project = new
            {
                id= projectId,
                projectName = projectName,
                projectServerIP = projectServerIP,
                projectServerUserName = projectServerUserName,
                projectServerPassword = projectServerPassword,
                companyId = companyId,
                updatedBy = ActiveUser.FirstName,
                updatedDate = DateTime.Now,
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            var json = JsonConvert.SerializeObject(project);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PutAsync($"{apiUrl}Projects/Update", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteProjectData(int id)
        {
            string apiUrl = "https://localhost:44343/api/";
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
            HttpResponseMessage responseMessage = await client.DeleteAsync($"{apiUrl}Projects/Delete?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
