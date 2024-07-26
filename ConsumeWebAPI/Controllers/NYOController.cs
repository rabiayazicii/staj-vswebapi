using System;
using ConsumeWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConsumeWebAPI.Controllers
{
	public class NYOController:Controller
	{
		Uri baseAdsress = new Uri("https://localhost:7117/api");
		private readonly HttpClient _client;

		public NYOController()
		{
			_client = new HttpClient();
			_client.BaseAddress = baseAdsress;
		}

        public IActionResult Index()
		{
			List<NYOViewModel> list = new List<NYOViewModel>();
			HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/product/Get").Result;

			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				list = JsonConvert.DeserializeObject<List<NYOViewModel>>(data);
			}
			return View(list);
		}
	}
}

