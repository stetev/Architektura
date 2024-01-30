using System.ComponentModel.DataAnnotations;
using LiteDB;

namespace RestauraceApp.Models
{
	// modelova třída, která odpovídá struktuře dat, která chci ukládat. 
	public class Rezervace
	{

		public int RezervaceID { get; set; }

		[Required(ErrorMessage = "Jméno hosta je povinné.")]
		public string? JmenoHosta { get; set; }

		[Required(ErrorMessage = "Datum je povinné.")]
		public DateTime Datum { get; set; }

		[Required(ErrorMessage = "Počet hostů je povinný.")]
		[Range(1, 30, ErrorMessage = "Počet hostů musí být mezi 1 a 30.")]
		public int PocetHostu { get; set; }

	}
}
