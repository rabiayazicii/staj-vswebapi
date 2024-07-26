using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumeWebAPI.Models
{
	public class NYOViewModel
	{
        [Key]
        public int ODEMENO { get; set; }
        [Required]
        [DisplayName("TC KIMLIK NO")]
        public string TCKIMLIKNO { get; set; }
        public string MUSTERIAD { get; set; }
        public string MUSTERISOYAD { get; set; }
        public int ODEME_KD { get; set; }
        public int ODEME_TTR { get; set; }
        public DateTime ODEME_TR { get; set; }
        public string ODEME_ACK { get; set; }
    }
}

