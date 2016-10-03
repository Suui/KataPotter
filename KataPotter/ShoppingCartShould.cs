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

		[Test]
		public void have_a_ten_percent_discount_for_three_different_books_of_the_series()
		{
			var shoppinCart = AShoppingCartWith(new List<Book>
			{
				new Book("First Book"),
				new Book("Second Book"),
				new Book("Third Book")
			});

			shoppinCart.TotalPrice().Should().Be(24 * 0.9m);
		}

		[Test]
		public void have_no_discount_for_three_identical_books_of_the_series()
		{
			var shoppingCart = AShoppingCartWith(new List<Book>
			{
				new Book("Same Book"),
				new Book("Same Book"),
				new Book("Same Book")
			});

			shoppingCart.TotalPrice().Should().Be(24);
		}

		[Test]
		public void have_a_5_percent_discount_for_two_different_books_of_the_series_but_not_for_the_third_duplicated_one()
		{
			var shoppingCart = AShoppingCartWith(new List<Book>
			{
				new Book("Same Book"),
				new Book("Different Book"),
				new Book("Same Book")
			});

			shoppingCart.TotalPrice().Should().Be(8 + 16 * 0.95m);
		}

		[Test]
		public void have_a_twenty_percent_discount_for_four_different_books_of_the_series()
		{
			var shoppingCart = AShoppingCartWith(new List<Book>
			{
				new Book("First Book"),
				new Book("Second Book"),
				new Book("Third Book"),
				new Book("Fourth Book")
			});

			shoppingCart.TotalPrice().Should().Be(32 * 0.80m);
		}

		[Test]
		public void have_a_twenty_five_percent_discount_for_four_different_books_of_the_series()
		{
			var shoppingCart = AShoppingCartWith(new List<Book>
			{
				new Book("First Book"),
				new Book("Second Book"),
				new Book("Third Book"),
				new Book("Fourth Book"),
				new Book("Fifth Book")
			});

			shoppingCart.TotalPrice().Should().Be(40 * 0.75m);
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

	public class Book
	{
		public string Title { get; }

		public Book(string title)
		{
			Title = title;
		}
	}
}
