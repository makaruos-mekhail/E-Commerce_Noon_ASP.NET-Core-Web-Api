using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
	public class User : IdentityUser
	{
		public string FirstName { get; protected set; }

		public string FirstNameAr { get; protected set; }

		public string LastName { get; protected set; }

		public string LastNameAr { get; protected set; }

		public string Address { get; protected set; }

		public string AddressAr { get; protected set; }

		public string City { get; protected set; }

		public string CityAr { get; protected set; }

		public string Country { get; protected set; }

		public string CountryAr { get; protected set; }

		public string? PostalCode { get; protected set; }

		public string Phone { get; protected set; }

		public DateTime CreationDateTime { get; protected set; }

		public UserStatus UserStatus { get; protected set; }

		public UserType Type { get; protected set; }

		public virtual ICollection<Order> Orders { get; protected set; }

		public virtual ICollection<ProductReview> Reviews { get; protected set; }

		public virtual WishList WishList { get; protected set; }

		public long WishListId { get; protected set; }

	}
}
