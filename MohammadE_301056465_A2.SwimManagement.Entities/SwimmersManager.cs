﻿using System;
using System.Collections.Generic;
using System.IO;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class SwimmersManager
	{
		public ClubsManager ClubManager { get; set; }
		public List<Registrant> Swimmers { get; set; }

		public SwimmersManager(ClubsManager manager)
		{
			ClubManager = manager;
		}

		public void AddSwimmer(Registrant aSwimmer)
		{
			Swimmers.Add(aSwimmer);
		}

		private string formatRecord(Registrant aSwimmer, string delimiter)
		{
			string street = !string.IsNullOrEmpty(aSwimmer.Address.Street) ? aSwimmer.Address.Street : string.Empty;
			string city = !string.IsNullOrEmpty(aSwimmer.Address.City) ? aSwimmer.Address.City : string.Empty;
			string province = !string.IsNullOrEmpty(aSwimmer.Address.Province) ? aSwimmer.Address.Province : string.Empty;
			string postalCode = !string.IsNullOrEmpty(aSwimmer.Address.PostalCode) ? aSwimmer.Address.PostalCode : string.Empty;

			string msg = $"{aSwimmer.Id}{delimiter}{aSwimmer.Name}{delimiter}{aSwimmer.DateOfBirth}{delimiter}{street}{delimiter}{city}{delimiter}{province}{delimiter}{postalCode}{delimiter}" +
				$"{aSwimmer.PhoneNumber}";

			if (aSwimmer.Club != null)
				msg += $"{ delimiter}{ aSwimmer.Club.ClubNumber}";
			
			return msg;
		}

		public Registrant GetSwimmer(uint regNumber)
		{
			if (Swimmers == null)
				Swimmers = new List<Registrant>();

			foreach (Registrant item in Swimmers)
			{
				if (item.Id == regNumber)
				{
					return item;
				}
			}

			return null;
		}

		public void LoadSwimmers(string fileName, string delimiter)
		{
			FileStream fileStream = default;
			StreamReader reader = default;

			try
			{
				fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				reader = new StreamReader(fileStream);

				string record = reader.ReadLine();
				while (record != null)
				{
					Registrant registrant = processSwimmerRecord(record, delimiter);
					if (registrant != null)
					{
						Swimmers.Add(registrant);
					}
					record = reader.ReadLine();
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
			catch (Exception)
			{
			}
			finally
			{
				if (reader != null)
					reader.Close();

				if (fileStream != null)
					fileStream.Close();
			}
		}

		private Registrant processSwimmerRecord(string aRecord, string delimiter)
		{
			Registrant registrant = default;
			try
			{
				string[] fields = aRecord.Split(new[] { delimiter }, StringSplitOptions.None);
				uint result;
				ulong phone;
				DateTime dt;
				string swimmer = $"{fields[0]},{fields[1]},{fields[2]}{fields[3]},{fields[4]},{fields[5]},{fields[6]},{fields[7]},{fields[8]}";
				if (fields.Length < 8)
					throw new Exception($"Invalid swimmer record. Not enough fields:\n{swimmer}");

				if (!UInt32.TryParse(fields[0], out result))
					throw new Exception($"Invalid swimmer record. Invalid registration number:\n{swimmer}");

				if (!DateTime.TryParse(fields[2], out dt))
					throw new Exception($"Invalid swimmer record. Birth date is invalid:\n{swimmer}");

				if (string.IsNullOrEmpty(fields[1]))
					throw new Exception($"Invalid swimmer record. Invalid swimmer name:\n{swimmer}");

				if (!UInt64.TryParse(fields[7], out phone))
					throw new Exception($"Invalid swimmer record. Phone number wrong format:\n{swimmer}");

				if (!UInt64.TryParse(fields[8], out phone))
					throw new Exception($"Invalid swimmer record. Club affiliation exists but not valid:\n{swimmer}");

				Address address = new Address(fields[3], fields[4], fields[5], fields[6]);
				registrant = new Registrant(Convert.ToUInt32(fields[0]), fields[1], Convert.ToDateTime(fields[2]), address, Convert.ToUInt64(fields[7]));
				registrant.Club = ClubManager.GetClub(Convert.ToUInt32(fields[8]));

				if (GetSwimmer(registrant.Id) != null)
					throw new Exception($"Invalid swimmer record. Swimmer with the registration number already exists:\n{registrant}");
				else
					return registrant;
			}
			catch (IOException ex)
			{
				throw ex;
			}
			catch (Exception)
			{
			}
			return registrant;
		}

		public void SaveSwimmers(string fileName, string delimiter)
		{
			FileStream fileStream = default;
			StreamWriter writer = default;

			try
			{
				fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				writer = new StreamWriter(fileStream);

				foreach (Registrant item in Swimmers)
				{
					string result = formatRecord(item, delimiter);
					writer.WriteLine(result);
				}
			}
			catch (IOException) { }
			catch (Exception) { }
			finally
			{
				if (writer != null)
					writer.Close();

				if (fileStream != null)
					fileStream.Close();
			}
		}
	}
}