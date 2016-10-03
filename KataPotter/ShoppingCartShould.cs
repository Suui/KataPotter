﻿using System.Collections.Generic;
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
			AnEmptyShoppingCart().TotalPrice().Should().Be(0);
		}

		[Test]
		public void have_a_price_of_eight_for_a_single_book()
		{
			var shoppingCart = AShoppingCartWith(new Book("A Book"));

			shoppingCart.TotalPrice().Should().Be(8);
		}

		[Test]
		public void have_a_five_percent_discount_for_two_different_books_of_the_series()
		{
			var shoppingCart = AShoppingCartWith(new List<Book>
			{
				new Book("A Book"),
				new Book("A Different Book")
			});

			shoppingCart.TotalPrice().Should().Be(16 * 0.95m);
		}

		[Test]
		public void have_no_discount_for_two_identical_books_of_the_series()
		{
			var shoppingCart = AShoppingCartWith(new List<Book>
			{
				new Book("Same Book"),
				new Book("Same Book")
			});

			shoppingCart.TotalPrice().Should().Be(16);
		}

		private static ShoppingCart AShoppingCartWith(List<Book> books)
		{
			var shoppingCart = new ShoppingCart();
			books.ForEach(book => shoppingCart.Add(book));
			return shoppingCart;
		}

		private ShoppingCart AnEmptyShoppingCart() => new ShoppingCart();

		private static ShoppingCart AShoppingCartWith(Book book) => AShoppingCartWith(new List<Book> { book });
	}

	public class ShoppingCart
	{
		private List<Book> Books { get; } = new List<Book>();

		public decimal TotalPrice()
		{
			var price = Books.Count * 8;
			if (Books.Count == 2)
			{
				if (Books[0].Title.Equals(Books[1].Title)) return price;
				return price * 0.95m;
			}

			return price;
		}

		public void Add(Book book) => Books.Add(book);
	}

	public class Book
	{
		public string Title { get; }

		public Book(string title)
		{
			Title = title;
		}
	}
}
