namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public struct Address
	{
		public string City;
		public string PostalCode;
		public string Province;
		public string Street;

		public Address(string street, string city, string province, string postalCode)
		{
			this.City = city;
			this.Street = street;
			this.Province = province;
			this.PostalCode = postalCode;
		}

		public override string ToString()
		{
			return $"{Street}\n{City}\n{Province}\n{PostalCode}";
		}
	}
}