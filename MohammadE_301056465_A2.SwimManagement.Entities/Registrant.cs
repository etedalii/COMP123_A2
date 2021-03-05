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
                {
					club = value;
                }
					
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

		public Registrant(string name, DateTime dateOfBirth, Address anAddress, ulong phoneNumber) : this() 
		{
			Name = name;
			DateOfBirth = dateOfBirth;
			Address = anAddress;
			PhoneNumber = phoneNumber;
		}

		internal Registrant(uint regNumber, string name, DateTime dateOfBirth, Address anAddress, ulong phoneNumber)
		{
			Id = regNumber;

			Name = name;
			DateOfBirth = dateOfBirth;
			Address = anAddress;
			PhoneNumber = phoneNumber;
		}

		public override string ToString()
		{ 
			string msg = $"Name: {Name}\nAddress:\n\t{Address.Street}\n\t{Address.City}\n\t{Address.Province}\n\t{Address.PostalCode}\nPhone:{PhoneNumber}\nDOB:{DateOfBirth}\nId:{Id}\n";
			
			msg += (Club != null? $"Club: {Club.Name}" : "Club: Not assigned");
			
			return msg;
		}
	}
}