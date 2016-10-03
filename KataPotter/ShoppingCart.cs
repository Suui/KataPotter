using System.Collections.Generic;
using System.Linq;


namespace KataPotter
{
	public class ShoppingCart
	{
		private List<Book> Books { get; } = new List<Book>();
		private Dictionary<int, decimal> DiscountFor { get; } = new Dictionary<int, decimal>
		{
			{ 0, 0 },
			{ 1, 1 },
			{ 2, 0.95m },
			{ 3, 0.9m },
			{ 4, 0.8m },
			{ 5, 0.75m }
		};

		public decimal TotalPrice()
		{
			const int bookPrice = 8;
			return NumberOfIdenticalBooks() * bookPrice
			       + NumberOfDifferentBooks() * bookPrice * DiscountFor[NumberOfDifferentBooks()];
		}

		private int NumberOfIdenticalBooks() => Books.Count - NumberOfDifferentBooks();

		private int NumberOfDifferentBooks() => Books.GroupBy(book => book.Title).Count();

		public void Add(Book book) => Books.Add(book);
	}
}