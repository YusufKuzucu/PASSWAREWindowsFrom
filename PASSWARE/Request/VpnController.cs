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
    public class VpnsResponse
    {
        public Vpn[] Data { get; set; }
    }
    public class VpnController
    {
        HttpClient client = new HttpClient();
        public async Task<Vpn[]> GetVpnData(string url)
        {
            Vpn[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        VpnsResponse vpnsResponse = JsonConvert.DeserializeObject<VpnsResponse>(responseContent);
                        data = vpnsResponse.Data;
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
        public async Task<Vpn[]> GetVpn(int id)
        {
            string apiUrl = "https://localhost:44343/api/";
            Vpn[] data = null;
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);

                HttpResponseMessage response = await client.GetAsync($"{apiUrl}Vpns/GetByVpn?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    try
                    {
                        VpnsResponse vpnsResponse = JsonConvert.DeserializeObject<VpnsResponse>(responseContent);
                        data = vpnsResponse.Data;
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
        public async Task<bool> AddVpnData(string vpnProgramName, string vpnConnectionAddress, string vpnPassword,string projectId)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/";
                HttpClient client = new HttpClient();
                var vpn = new
                {
                    vpnProgramName = vpnProgramName,
                    vpnConnectionAddress = vpnConnectionAddress,
                    vpnPassword = vpnPassword,
                    projectId = projectId,
                    createdBy = ActiveUser.FirstName,
                    createdDate = DateTime.Now,
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
                var json = JsonConvert.SerializeObject(vpn);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync($"{apiUrl}Vpns/Post", content);
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

        public async Task<bool> UpdateVpnData(int vpnId,string vpnProgramName, string vpnConnectionAddress, string vpnPassword,string projectId)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/";
                HttpClient client = new HttpClient();
                var vpn = new
                {
                    id = vpnId,
                    vpnProgramName = vpnProgramName,
                    vpnConnectionAddress = vpnConnectionAddress,
                    vpnPassword = vpnPassword,
                    projectId = projectId,
                    updatedBy = ActiveUser.FirstName,
                    updatedDate = DateTime.Now,
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
                var json = JsonConvert.SerializeObject(vpn);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PutAsync($"{apiUrl}Vpns/Update", content);
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
        public async Task<bool> DeleteVpnData(int id)
        {
            try
            {
                string apiUrl = "https://localhost:44343/api/";
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ActiveUser.Token);
                HttpResponseMessage responseMessage = await client.DeleteAsync($"{apiUrl}Vpns/Delete?id={id}");
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
