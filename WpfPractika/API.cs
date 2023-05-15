using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfPractika
{
    internal class API
    {
        HttpClient client = new HttpClient();
        public async Task<string> Auth(string login, string pass)
        {
            try
            {
                string url = $"https://localhost:7119/api/Auth?login={login}&password={pass}";

                StringContent stringContent = new StringContent(url);

                HttpResponseMessage response = await client.PostAsync(url, stringContent);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Ошибка!");
                return ex.Message;
            }
        }

        public async Task<string> Registration(string login, string pass, string secondName, string firstName, string fatherName)
        {
            try
            {
                string url = $"https://localhost:7119/api/Register?login={login}&password={pass}&SecondName={secondName}&FirstName={firstName}&FatherName={fatherName}";

                StringContent stringContent = new StringContent(url);

                HttpResponseMessage response = await client.PostAsync(url, stringContent);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            return "";
        }

        public async Task<string> Get(string tableName)
        {
            try
            {
                string url = $"https://localhost:7119/api/{tableName}";

                HttpResponseMessage response = await client.GetAsync(url);

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            return "";
        }
        
        public async Task<string> Put(string tableName, int id, string jsonData)
        {
            try 
            { 
                var requestUri = new Uri($"https://localhost:7119/api/{tableName}/{id}");

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(requestUri, content);

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            return "";
        }

        public async Task<string> Post(string tableName, string jsonData)
        {
            try 
            { 
                var requestUri = new Uri($"https://localhost:7119/api/{tableName}");

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(requestUri, content);

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            return "";
        }

        public async Task<string> Delete(string tableName, int id)
        {
            try
            {
                string url = $"https://localhost:7119/api/{tableName}/{id}";

                HttpResponseMessage response = await client.DeleteAsync(url);

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            return "";
        }

        public async Task<string> PutChangeStatus(string tableName, int value, string sids)
        {
            try
            {
                string url = $"https://localhost:7119/api/{tableName}/ChangingTheDeletionStatus?sids={sids}&value={value}";

                var content = new StringContent(url);
                var response = await client.PutAsync(url, content);

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            return "";
        }

        public async Task<string> GetPage(string tableName, uint page, uint count)
        {
            try
            {
                string url = $"https://localhost:7119/api/{tableName}/GetPageItems?page={page}&count={count}";

                var response = await client.GetAsync(url);

                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
            return "";
        }
    }
}
