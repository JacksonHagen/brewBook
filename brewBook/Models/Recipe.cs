using System;
using brewBook.Interfaces;

namespace brewBook.Models
{
	public class Recipe : IDbItem
	{
		public Profile Creator { get; set; }
		public Bean Beans { get; set; }
		public int BeanId { get; set; }
		public string Title { get; set; }
		public double Dose { get; set; }
		public int Temp { get; set; }
		public string Grinder { get; set; }
		public string GrindSetting { get; set; }
		public string Description { get; set; }
		// NOTE this is where the interface is implemented
		public int Id { get; set; }
		public string CreatorId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}