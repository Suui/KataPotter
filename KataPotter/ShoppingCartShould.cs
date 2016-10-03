using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

/* TODO
	
*/

namespace KataPotter
{
	[TestFixture]
	class ShoppingCartShould
	{
		[Test]
		public void have_a_total_price_of_zero_when_it_is_empty()
		{
			var shoppingCart = new ShoppingCart();

			shoppingCart.TotalPrice().Should().Be(0);
		}

		[Test]
		public void have_a_price_of_eight_for_a_single_book()
		{
			var shoppingCart = new ShoppingCart();
			shoppingCart.Add(new Book("A book"));

			shoppingCart.TotalPrice().Should().Be(8);
		}

		[Test]
		public void have_a_five_percent_discount_for_two_different_books_of_the_series()
		{
			var shoppingCart = new ShoppingCart();
			shoppingCart.Add(new Book("A Book"));
			shoppingCart.Add(new Book("A Different Book"));

			shoppingCart.TotalPrice().Should().Be(16 * 0.95m);
		}
	}

	public class ShoppingCart
	{
		private List<Book> Books { get; } = new List<Book>();

		public decimal TotalPrice()
		{
			var price = Books.Count * 8;
			if (Books.Count == 2) return price * 0.95m;

			return price;
		}

		public void Add(Book book)
		{
			Books.Add(book);
		}
	}

	public class Book
	{
		public Book(string title)
		{
			
		}
	}
}
