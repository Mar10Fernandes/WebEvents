using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Logging;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace App.Controllers
{
    public class ContactController : Controller
    {


        private readonly ILogger _logger;
        private static HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:60048/"),
            
        };
        
        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                //client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage response = client.GetAsync("/api/Contact").Result;
                string stringData = response.Content.ReadAsStringAsync().Result;
                List<Contact> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Contact>>(stringData);

                return View(data);
            }
            catch(Exception ex)
            {
                _logger.LogError("Index. ex: ", ex);
                return RedirectToAction("Shared/Error");
            }
                
                 
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Contact cnt)
        {
            try
            {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string stringData = Newtonsoft.Json.JsonConvert.SerializeObject(cnt);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync("/api/Contact", contentData).Result;


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError("Create item. ex: ", ex);
                return RedirectToAction("Shared/Error");
            }

               
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                HttpResponseMessage response = client.DeleteAsync("/api/Contact/" + id).Result;
                ViewBag.Message = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete item. ex: ", ex);
                return RedirectToAction("Shared/Error");
            }

            
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/Contact/"+ id + "/0//0/0").Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    List<Contact> resultobjs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Contact>>(result);
                    return View(resultobjs.FirstOrDefault());

                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Edit item. ex: ", ex);
                return RedirectToAction("Shared/Error");
            }
        }
        [HttpPost]
        public IActionResult Edit(Contact cnt )
        {
            try
            {

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string stringData = Newtonsoft.Json.JsonConvert.SerializeObject(cnt);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync($"api/Contact/{cnt.ID}", contentData).Result;
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    Contact resultobj = Newtonsoft.Json.JsonConvert.DeserializeObject<Contact>(result);
                    return RedirectToAction("Index");
                }


                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Edit item. ex: ", ex);
                return RedirectToAction("Shared/Error");
            }

        }
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Search(string SearchString, int id)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;

                if (id == 2)
                {
                    response = client.GetAsync("api/Contact/0/0/" + SearchString + "/0").Result;
                }
                else if (id == 3)
                {
                    response = client.GetAsync("api/Contact/0/0/0/" + SearchString).Result;
                }
                else
                {
                    response = client.GetAsync("api/Contact/0/" + SearchString + "/0/0").Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    List<Contact> resultobjs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Contact>>(result);
                    return View(resultobjs);

                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Search item. ex: ", ex);
                return RedirectToAction("Shared/Error");
            }
            

        }

        


    }
}
