using GlodinAutoTradeModel.Models;
using GoldinAutoTrade.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GoldinAutoTrade.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly HttpClient client = Helper.ApiHttpClient.GetApiClient();

       async Task<Tuple<bool>> IProductRepository.AddProduct(Product product)
        {
            if (product.Image != null)
            {
                using (var client = Helper.ApiHttpClient.GetApiClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        byte[] uploadData;
                        using (var reader = new BinaryReader(product.Image.InputStream))
                        {
                            uploadData = reader.ReadBytes(product.Image.ContentLength);
                        }

                        product.FileName = product.Image.FileName;

                        formData.Add(new StreamContent(new MemoryStream(uploadData)), "document", product.Image.FileName);
                        product.Image = null;
                        formData.Add(new StringContent(JsonConvert.SerializeObject(product)), "Product");

                        HttpResponseMessage response = await client.PutAsync($"api/Product/AddProduct", formData);

                        return new Tuple<bool>(response.IsSuccessStatusCode);
                    }
                }
            }
            return new Tuple<bool>(false);
        }

        async Task<Tuple<Product>> IProductRepository.GetProduct(int Id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/Product/GetProduct?Id={Id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var product = JsonConvert.DeserializeObject<Product>(content);
                        return new Tuple<Product>(product);
                    }
                }
                return new Tuple<Product>(new Product());
            }
            catch (Exception ex)
            {
                return new Tuple<Product>(new Product());
            }
        }

        async Task<Tuple<List<Product>, bool>> IProductRepository.GetProducts()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/Product/GetProducts");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var products = JsonConvert.DeserializeObject<List<Product>>(content);

                        products = SetImages(products);

                        return new Tuple<List<Product>, bool>(products, true);
                    }
                }
                return new Tuple<List<Product>, bool>(new List<Product>(), false);
            }
            catch (Exception ex)
            {
                return new Tuple<List<Product>, bool>(new List<Product>(), false);
            }
        }

        private List<Product> SetImages(List<Product> products) 
        {
            foreach (var product in products) 
            {
                if(product.ByteImage != null)
                {
                    product.ConvertedImage = ConvertImage(product.ByteImage);
                }
            }
            return products;
            
        }

        private string ConvertImage(byte[] arrayImage)
        {
            return $"data:image/png;base64,{Convert.ToBase64String(arrayImage, 0, arrayImage.Length)}";
        }
       

        async Task<Tuple<Product>> IProductRepository.UpdateProductInStock(int Id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/Product/UpdateProductInStock?Id={Id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var product = JsonConvert.DeserializeObject<Product>(content);
                        return new Tuple<Product>(product);
                    }
                }
                return new Tuple<Product>(new Product());
            }
            catch (Exception ex)
            {
                return new Tuple<Product>(new Product());
            }
        }
    }
}