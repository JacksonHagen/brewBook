using System;
using System.ComponentModel.DataAnnotations;
using brewBook.Interfaces;

namespace brewBook.Models
{
	public class Bean : IDbItem
	{
		public string Origin { get; set; }
		public string Roaster { get; set; }
		public string Process { get; set; }
		[Range(1, 11)]
		public int RoastLevel { get; set; }
		// NOTE 1 is super light, 10 is super dark
		public int Id { get; set; }
		public string CreatorId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

	}
}