﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class Club
	{
		public Address ClubAddress { get; set; }

		//Represents the unique id of the club
		public uint ClubNumber { get; private set; }
		public string Name { get; set; }
		public ulong PhoneNumber { get; set; }
		public List<Registrant> Swimmers { get; }

		public Club()
		{
			ClubNumber = RegistrationNumberGenerator.GetNext();
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

			Swimmers.Add(aSwimmer);
		}

		public override string ToString()
		{
			string result = $"{ClubNumber},{Name},{PhoneNumber}\n Club members are:\n";
			foreach (Registrant item in Swimmers)
			{
				result += $"{item.Id},{item.Club.Name},{item.Address.street},{item.Address.city},{item.Address.province},{item.Address.postalCode},{item.PhoneNumber}\n";
			}
			return result;
		}
	}
}