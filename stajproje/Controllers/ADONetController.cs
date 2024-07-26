using System;
using System.Data.SqlClient;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using stajproje.com;
using stajproje.Data;
using stajproje.Model;
using stajproje.Model.DTO;
using stajproje.Model.Entities;

namespace stajproje.Controllers
{
    [Route("api/v1/odemeler")]
    [ApiController]
    public class ADONetController : Controller
    {
        private readonly ConnectionClass _con;
        private readonly ILogger<ADONetController> _logger;
        public ADONetController(IConfiguration configuration, ILogger<ADONetController> logger)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _con = new ConnectionClass(connectionString);
            _logger = logger;
        }

        [HttpGet("Sorgula-Sosyal-Odeme")]
        public async Task<ActionResult<List<NYO_SosyalYardimModel>>> CallStoredProcedure(string tckn)
        {

            try
            {
                var results = await _con.CallStoredProcedureAsync(tckn);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stored procedure çağrılırken bir hata oluştu.");
                return StatusCode(500, "Stored procedure çağrılırken bir hata oluştu.");
            }
        }

        [HttpGet("Sorgula-odemeno")]
        public async Task<ActionResult<List<NYO_SosyalYardimModel>>> CallStoredProcedure(int odemeno)
        {

            try
            {
                var results = await _con.GetByOdemeno(odemeno);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stored procedure çağrılırken bir hata oluştu.");
                return StatusCode(500, "Stored procedure çağrılırken bir hata oluştu.");
            }
        }
        [HttpPost("OdemeYap")]
        public JsonResult OdemeYap(string tckn, int odemeno)
        {
            string connectionString = "Server=localhost;Database=Odemeler;User Id=SA;Password=reallyStrongPwd123;Encrypt=True;TrustServerCertificate=True;";

            ConnectionClass _connectionclass = new ConnectionClass(connectionString);
            bool success = _connectionclass.OdemeSPAsync(tckn, odemeno);
            return Json(new { success });
        }


        [HttpPost("Create")]
        public string Post([FromBody] NYOSYCreateDTO nyososyalyardim)
        {
            string msg = string.Empty;
            try
            {

                msg = _con.InsertOdeme(nyososyalyardim);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        [HttpGet("Read")]
        public async Task<ActionResult<List<NYO_SosyalYardimModel>>> CallStoredProcedure()
        {

            try
            {
                var results = await _con.GetAllStoredProcedureAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stored procedure çağrılırken bir hata oluştu.");
                return StatusCode(500, "Stored procedure çağrılırken bir hata oluştu.");
            }
        }



        [HttpPut("Update")]
        public string Updates([FromBody] NYO_SosyalYardimModel nyososyalyardim)
        {
            string msg = string.Empty;
            try
            {

                msg = _con.Update(nyososyalyardim);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        [HttpDelete("Delete/{ODEMENO}")]
        public async Task<ActionResult<List<NYO_SosyalYardimModel>>> DeleteOdeme(int ODEMENO)//ÇALIŞIYOR
        {

            try
            {
                var results = await _con.Delete(ODEMENO);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stored procedure çağrılırken bir hata oluştu.");
                return StatusCode(500, "Stored procedure çağrılırken bir hata oluştu.");
            }
        }




    }
}



