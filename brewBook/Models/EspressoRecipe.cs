namespace brewBook.Models
{
	public class EspressoRecipe : Recipe
	{
		public double Yield { get; set; }
		public int PreinfusionTime { get; set; }
		public int ExtractionTime { get; set; }
		public string Machine { get; set; }
	}
}