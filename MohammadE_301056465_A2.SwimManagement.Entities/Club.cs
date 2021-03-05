using System;
using System.Collections.Generic;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class Club
	{
		public Address ClubAddress { get; set; }

		/// <summary>
		/// Represents the unique id of the club
		/// </summary>
		public uint ClubNumber { get; private set; }
		public string Name { get; set; }
		public ulong PhoneNumber { get; set; }
		public List<Registrant> Swimmers { get; }

		public Club()
		{
			ClubNumber = RegistrationNumberGenerator.GetNext();
			if (Swimmers == null)
				Swimmers = new List<Registrant>();
		}

		public Club(string name, Address anddress, ulong phoneNumber)
		{
			Name = name;
			ClubAddress = anddress;
			PhoneNumber = phoneNumber;
			if (Swimmers == null)
				Swimmers = new List<Registrant>();
		}

		internal Club(uint regNumber, string name, Address anddress, ulong phoneNumber)
		{
			ClubNumber = regNumber;
			Name = name;
			ClubAddress = anddress;
			PhoneNumber = phoneNumber;
			if (Swimmers == null)
				Swimmers = new List<Registrant>();
		}

		public void AddSwimmer(Registrant aSwimmer)
		{
			foreach (Registrant item in Swimmers)
			{
				if (item.Id == aSwimmer.Id)
				{
					throw new Exception($"Swimmer already assigned to {Name}");
				}
			}
			aSwimmer.Club = this;
			Swimmers.Add(aSwimmer);
		}

		public override string ToString()
		{
			string result = $"Name: {Name}\n Address:\n";
			result += $"\t{ClubAddress.Street}\n\t{ClubAddress.City}\n\t{ClubAddress.Province}\n\t{ClubAddress.PostalCode}\nPhone:{PhoneNumber}\nReg number:{ClubNumber}\nSwimmers:\n";

			foreach (Registrant swimmer in Swimmers)
			{
				result += "\t" + swimmer.Name + "\n";
			}
			return result;
		}
	}
}