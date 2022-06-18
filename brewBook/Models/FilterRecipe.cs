using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace brewBook.Models
{
    public class FilterRecipe : Recipe
    {
        public int TimeInSeconds { get; set; }
				public int TotalWeight { get; set; }
				public string Brewer { get; set; }
    }
}