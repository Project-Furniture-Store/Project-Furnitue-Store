using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Project_FurnitureStore.Controllers
{
    public class CartController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public CartController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

      
        


    }
}
