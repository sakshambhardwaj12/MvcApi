using MvcApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcApi.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public async Task<ActionResult> IndexAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Product/");
                if (response.IsSuccessStatusCode)
                {
                    var jasonString = response.Content.ReadAsStringAsync();
                    jasonString.Wait();
                    var ProductList = JsonConvert.DeserializeObject<List<Products>>(jasonString.Result);
                    return View(ProductList);
                }
                return null;
            }
        }

        public async Task<ActionResult> GetProductById(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Product/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Products product = await response.Content.ReadAsAsync<Products>();
                    return View(product);
                }
                return RedirectToAction("IndexAsync");
            }
        }
        public async Task<ActionResult> Insert()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Insert(Products product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/");
                HttpResponseMessage response = await client.PostAsJsonAsync("api/Product", product);
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("IndexAsync");
                }
            }
            return View();
        }
        public async Task<ActionResult> Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Product/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Products product = await response.Content.ReadAsAsync<Products>();
                    return View(product);
                }
            }
            return RedirectToAction("IndexAsync");
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, Products product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync("api/Product/" + id, product);

                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("IndexAsync");
                }
            }
            return View();
        }
        public async Task<ActionResult> Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Product/" + id);
                if (response.IsSuccessStatusCode)
                {
                    Products product = await response.Content.ReadAsAsync<Products>();
                    return View(product);
                }
            }
            return RedirectToAction("IndexAsync");
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int? id, Products product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44365/");
                client.DefaultRequestHeaders.Accept.Clear();
                HttpResponseMessage response = await client.DeleteAsync("api/Product/" + id);
                if (response.IsSuccessStatusCode)
                {

                    return RedirectToAction("IndexAsync");
                }
            }
            return View();
        }
    }
}