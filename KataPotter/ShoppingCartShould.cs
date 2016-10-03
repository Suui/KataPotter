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
	}

	public class ShoppingCart
	{
		public decimal TotalPrice()
		{
			return 0;
		}
	}
}
