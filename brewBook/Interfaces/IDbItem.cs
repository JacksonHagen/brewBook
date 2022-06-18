using System;

namespace brewBook.Interfaces
{
	public interface IDbItem
	{
		int Id { get; set; }
		string CreatorId { get; set; }
		DateTime CreatedAt { get; set; }
		DateTime UpdatedAt { get; set; }
	}
}