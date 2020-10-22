using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Data_Migration_App.Models;
using System.Text;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Data_Migration_App.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //  GET: ProductController/Create
        
        public ActionResult Create()
        {

            //Root list = new Root();
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //String username = "VsSY3KtZ6BijgOCfyws8";
            //String password = "Q7FfKUSDb6SpHJw4ufxs";
            //String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));

            ////Form Data
            //var formData = "var1=val1&var2=val2";
            //var encodedFormData = Encoding.ASCII.GetBytes(formData);

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.trendyol.com/sapigw/suppliers/130188/v2/products");
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.Method = "POST";
            //request.ContentLength = encodedFormData.Length;
            //request.Headers.Add("Authorization", "Basic " + encoded);
            //request.PreAuthenticate = true;

            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(encodedFormData, 0, encodedFormData.Length);
            //}

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //StatusCodeResult res = this.StatusCode(200);
            //return res;

            return View();

        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Root product = new Root();
                    Item item = new Item();
                    Attributes attributes_1 = new Attributes();
                    Attributes attributes_2 = new Attributes();
                    Attributes attributes_3 = new Attributes();
                    Attributes1 attributes1 = new Attributes1();
                    Attributes2 attributes2 = new Attributes2();
                    attributes_1.attributeId = 338;
                    attributes_1.attributeValueId = 6980;
                    attributes_2.attributeId = 346;
                    attributes_2.attributeValueId = 4290;
                    attributes_3.attributeId = 47;
                    attributes_3.attributeValueId = "COLOUR";

                    attributes1.attributeId = Convert.ToInt32(collection["items.attributeId"]);
                    attributes1.attributeValueId = Convert.ToInt32(collection["items.attributeValueId"]);
                    attributes2.attributeId = Convert.ToInt32(collection["items.attributeId"]);
                    attributes2.customAttributeValue = collection["items.attributeId"];

                    Image image = new Image();
                    image.url = "https://www.sampleadress/path/folder/image_1.jpg";

                    item.barcode = collection["barcode"];
                    item.title = collection["title"];
                    item.productMainId = collection["productMainId"];
                    item.brandId = Convert.ToInt32(collection["brandId"]);
                    item.categoryId = Convert.ToInt32(collection["categoryId"]);
                    item.quantity = Convert.ToInt32(collection["quantity"]);
                    item.stockCode = collection["stockCode"];
                    item.dimensionalWeight = Convert.ToInt32(collection["dimensionalWeight"]);
                    item.description = collection["description"];
                    item.currencyType = collection["currencyType"];
                    item.listPrice = Convert.ToDouble(collection["listPrice"]);
                    item.salePrice = Convert.ToDouble(collection["salePrice"]);
                    item.vatRate = Convert.ToInt32(collection["vatRate"]);
                    item.cargoCompanyId = Convert.ToInt32(collection["cargoCompanyId"]);
                    item.shipmentAddressId = Convert.ToInt32(collection["shipmentAddressId"]);
                    item.returningAddressId = Convert.ToInt32(collection["returningAddressId"]);

                    List<Image> lstimage = new List<Image>();
                    lstimage.Add(image);
                    item.images = lstimage;
                    List<Attributes> lstAttributes = new List<Attributes>();
                    lstAttributes.Add(attributes_1);
                    lstAttributes.Add(attributes_2);
                    //List<Attributes1> lstAttributes1 = new List<Attributes1>();
                    //lstAttributes1.Add(attributes1);
                    // item.attributes = lstAttributes1;
                    //List<Attributes2> lstAttributes2 = new List<Attributes2>();
                    //lstAttributes2.Add(attributes2);
                    //item.attributes = lstAttributes2;     
                    item.attributes = lstAttributes;
                    List<Item> lstItem = new List<Item>();
                    lstItem.Add(item);
                    product.items = lstItem;

                    string productJson;
                    productJson = System.Text.Json.JsonSerializer.Serialize(product);
                    CallPostMethod(product).Wait();
                    return View();
                }
                else
                {
                    StatusCodeResult res = this.StatusCode(400);
                    return res;
                }
            }
            catch(Exception ex)
            {
                return View();
            }

            
        }

        static async Task CallPostMethod(Root _product)
        {
            using (var client = new HttpClient())
            {
                String username = "VsSY3KtZ6BijgOCfyws8";
                String password = "Q7FfKUSDb6SpHJw4ufxs";
                //client.BaseAddress = new Uri("https://api.trendyol.com/sapigw/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string authInfo = Convert.ToBase64String(Encoding.Default.GetBytes("VsSY3KtZ6BijgOCfyws8:Q7FfKUSDb6SpHJw4ufxs"));  
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
                 
                //var productjson = JsonConvert.SerializeObject(_product, Formatting.Indented);
                //var productStringContent = new StringContent(productjson);
                StringContent content = new StringContent(JsonConvert.SerializeObject(_product), Encoding.UTF8, "application/json");                 
                HttpResponseMessage responsePost = await client.PostAsync("https://api.trendyol.com/sapigw/suppliers/130188/v2/products", content);
                 
                if (responsePost.IsSuccessStatusCode)
                {
                    string httpResponseResult = responsePost.Content.ReadAsStringAsync().ContinueWith(task => task.Result).Result;
                    if (responsePost.ReasonPhrase == "OK")
                    {
                        Console.WriteLine(httpResponseResult);
                    }
                }
                else
                {
                    Console.WriteLine("Internal server error");
                }
                
            }
        }

            // GET: ProductController/Edit/5
            public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
