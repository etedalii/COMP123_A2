namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public struct Address
	{
		public string city;
		public string postalCode;
		public string province;
		public string street;

		public Address(string street, string city, string province, string postalCode)
		{
			this.city = city;
			this.street = street;
			this.province = province;
			this.postalCode = postalCode;
		}

		public override string ToString()
		{
			return $"{street}\n{city}\n{province}\n{postalCode}";
		}
	}
}