using System;
using Microsoft.AspNetCore.Mvc;
using stajproje.com;
using stajproje.Data;

namespace stajproje.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly DataContext _context;

        private readonly ILogger<HomeController> _logger;
        public ActionResult Index(string searchby, string search)
        {

            string connectionString = "Server=localhost;Database=Odemeler;User Id=SA;Password=reallyStrongPwd123;Encrypt=True;TrustServerCertificate=True;";

            ConnectionClass _connectionclass = new ConnectionClass(connectionString);
            try
            {
                var nyoList = _connectionclass.GetAllUsers();

                if (nyoList == null || !nyoList.Any())
                {

                    return View(nyoList);
                }
                else
                {
                    if (string.IsNullOrEmpty(search))
                    {

                        return View(nyoList);
                    }
                    else
                    {
                        if (searchby.ToLower() == "tckimlikno")
                        {

                            var searchByTC = nyoList.Where(p => p.TCKIMLIKNO != null && p.TCKIMLIKNO.ToLower().Contains(search.ToLower()));
                            return View(searchByTC);
                        }

                        else if (searchby.ToLower() == "odemeno")
                        {
                            var searchbyodeme = nyoList.Where(p => p.ODEMENO != null && p.ODEMENO.ToString().Contains(search.ToLower()));
                            return View(searchbyodeme);
                        }


                    }
                }
                return View(nyoList);

            }
            catch (Exception ex)
            {

                return View();
            }
        }

    }
}

