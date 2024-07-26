using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stajproje.com;
using stajproje.Model;
using stajproje.Model.DTO;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace stajproje.Controllers
{

	[Route("api/v1/odemeler")]
	[ApiController]
	public class NYOSYController:Controller
	{
		private readonly DataContext _datacontext;
        public NYOSYController(DataContext datacontext)
		{
			_datacontext = datacontext;
		}
        [HttpGet("GetStoredProcedure")]
        public async Task<ActionResult<List<NYO_SosyalYardimModel>>> GetAllNYO_SosyalYardimModelSP()
        {
            var result = await _datacontext.NYO_SosyalYardimModels.FromSqlRaw("getir").ToListAsync();
            return Ok(result);
        }

		[HttpPost]
		public ActionResult Post(NYOSYCreateDTO nyosydto)
		{

			var nyososyalyardim = new NYO_SosyalYardimModel
			{
				TCKIMLIKNO = nyosydto.TCKIMLIKNO,
				MUSTERIAD = nyosydto.MUSTERIAD,
				MUSTERISOYAD = nyosydto.MUSTERISOYAD,
				ODEME_KD = nyosydto.ODEME_KD,
				ODEME_TTR = nyosydto.ODEME_TTR,
				ODEME_TR = nyosydto.ODEME_TR,
				ODEME_ACK = nyosydto.ODEME_ACK
			};
			_datacontext.NYO_SosyalYardimModels.Add(nyososyalyardim);
			_datacontext.SaveChanges();
            return CreatedAtAction("Get", new { ODEMENO = nyososyalyardim.ODEMENO }, nyososyalyardim);
        }


		[HttpPut("{ODEMENO}")]
		public ActionResult Put(int ODEMENO,NYOSYUpdateDTO nyosydto)
		{
			var nyososyalyardim = _datacontext.NYO_SosyalYardimModels.FirstOrDefault(p => p.ODEMENO == ODEMENO);
			if(nyososyalyardim is null)
			{
                nyososyalyardim = new NYO_SosyalYardimModel
                {
                    TCKIMLIKNO = nyosydto.TCKIMLIKNO,
                    MUSTERIAD = nyosydto.MUSTERIAD,
                    MUSTERISOYAD = nyosydto.MUSTERISOYAD,
                    ODEME_KD = nyosydto.ODEME_KD,
                    ODEME_TTR = nyosydto.ODEME_TTR,
                    ODEME_TR = nyosydto.ODEME_TR,
                    ODEME_ACK = nyosydto.ODEME_ACK
                };
                _datacontext.NYO_SosyalYardimModels.Add(nyososyalyardim);
                _datacontext.SaveChanges();
                return CreatedAtAction("Get", new { ODEMENO = nyososyalyardim.ODEMENO }, nyososyalyardim);
            }

			nyososyalyardim.TCKIMLIKNO = nyosydto.TCKIMLIKNO;
			nyososyalyardim.MUSTERIAD = nyosydto.MUSTERIAD;
			nyososyalyardim.MUSTERISOYAD = nyosydto.MUSTERISOYAD;
			nyososyalyardim.ODEME_KD = nyosydto.ODEME_KD;
			nyososyalyardim.ODEME_TTR = nyosydto.ODEME_TTR;
			nyososyalyardim.ODEME_TR = nyosydto.ODEME_TR;
			nyososyalyardim.ODEME_ACK = nyosydto.ODEME_ACK;

			_datacontext.SaveChanges();
			return NoContent();
		}


		[HttpDelete]
		public ActionResult Delete(int ODEMENO)
		{
			var nyososyalyardim = _datacontext.NYO_SosyalYardimModels.FirstOrDefault(p => p.ODEMENO == ODEMENO);
			if (nyososyalyardim is null) return NotFound();
			_datacontext.Remove(nyososyalyardim);
			_datacontext.SaveChanges();
			return NoContent();
		}


    }
}

