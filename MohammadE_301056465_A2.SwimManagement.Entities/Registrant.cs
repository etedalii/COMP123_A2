using System;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class Registrant
	{
		private Club club;

		public Address Address { get; set; }
		public Club Club 
		{
			set
			{
				if (club == null)
					club = value;
				else
				{
					throw new Exception("Swimmer is registered with a different club");
				}
			}
			get
			{
				return club;
			}
		}
		public DateTime DateOfBirth { get; set; }
		public uint Id { get; }
		public string Name { get; set; }
		public ulong PhoneNumber { get; set; }


		public Registrant()
		{
			Id = RegistrationNumberGenerator.GetNext();
		}

		public Registrant(string name, DateTime dateOfBirth, Address anAddress, ulong phoneNumber)
		{
			Name = name;
			DateOfBirth = dateOfBirth;
			this.Address = anAddress;
			PhoneNumber = phoneNumber;
		}

		internal Registrant(uint regNumber, string name, DateTime dateOfBirth, Address anAddress, ulong phoneNumber)
		{
			Id = regNumber;

			Name = name;
			DateOfBirth = dateOfBirth;
			this.Address = anAddress;
			PhoneNumber = phoneNumber;
		}

		public override string ToString()
		{
			string msg = $"The registrant/swimmer:{Name}";
			return Club != null ? $"{msg} Club name is: {Club}": $"{msg} is not assigned";
		}
	}
}