using System;
namespace stajproje.Model.Entities
{
	public class Response
	{
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<NYO_SosyalYardimModel> odemelistesi { get; set; }
        public NYO_SosyalYardimModel nyo_SosyalYardimModel { get; set; }
		
	}
}

