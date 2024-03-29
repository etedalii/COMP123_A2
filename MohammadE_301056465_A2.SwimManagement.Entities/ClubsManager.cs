﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class ClubsManager
	{
		public List<Club> Clubs { get; set; }

		public void AddClub(Club aClub)
		{
			if (Clubs == null)
				Clubs = new List<Club>();

			Clubs.Add(aClub);
		}

		private string fromatRecord(Club aClub, string delimiter)
		{
			string street = !string.IsNullOrEmpty(aClub.ClubAddress.Street) ? aClub.ClubAddress.Street : string.Empty;
			string city = !string.IsNullOrEmpty(aClub.ClubAddress.City) ? aClub.ClubAddress.City : string.Empty;
			string province = !string.IsNullOrEmpty(aClub.ClubAddress.Province) ? aClub.ClubAddress.Province : string.Empty;
			string postalCode = !string.IsNullOrEmpty(aClub.ClubAddress.PostalCode) ? aClub.ClubAddress.PostalCode : string.Empty;

			return $"{aClub.ClubNumber}{delimiter}{aClub.Name}{delimiter}{street}{delimiter}{city}{delimiter}{province}{delimiter}{postalCode}{delimiter}{aClub.PhoneNumber}";
		}

		public Club GetClub(uint regNumber)
		{
			if (Clubs == null)
				Clubs = new List<Club>();

			foreach (Club item in Clubs)
			{
				if (item.ClubNumber == regNumber)
				{
					return item;
				}
			}

			return null;
		}

		public void LoadClubs(string fileName, string delimiter)
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
					Club club = processClubRecord(record, delimiter);
					Clubs.Add(club);

					record = reader.ReadLine();
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
			finally
			{
				if (reader != null)
					reader.Close();

				if (fileStream != null)
					fileStream.Close();
			}
		}

		private Club processClubRecord(string aRecord, string delimiter)
		{
			try
			{
				string[] fields = aRecord.Split(new[] { delimiter }, StringSplitOptions.None);
				uint result;
				ulong phone;
				string clubStr = $"{fields[0]},{fields[1]},{fields[2]}, {fields[3]}, {fields[4]}, {fields[5]},{fields[6]}";
				if (fields.Length < 7)
					throw new Exception($"Invalid club record. Not enough fields:\n{clubStr}");

				if (!UInt32.TryParse(fields[0], out result))
					throw new Exception($"Invalid club record. Club number is not valid:\n{clubStr}");

				if (string.IsNullOrEmpty(fields[1]))
					throw new Exception($"Invalid club record. Invalid club name:\n{clubStr}");

				if (!UInt64.TryParse(fields[6], out phone))
					throw new Exception($"Invalid club record. Phone number wrong format:\n{clubStr}");

				Address address = new Address(fields[2], fields[3], fields[4], fields[5]);
				Club club = new Club(Convert.ToUInt32(fields[0]), fields[1], address, Convert.ToUInt64(fields[6]));

				if (GetClub(club.ClubNumber) != null)
					throw new Exception($"Invalid club record. Club with the registration number already exists:\n{club}");
				else
					return club;
			}
			catch (IOException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void SaveClubs(string fileName, string delimiter)
		{
			FileStream fileStream = default;
			StreamWriter writer = default;

			try
			{
				fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
				writer = new StreamWriter(fileStream);

				foreach (Club club in Clubs)
				{
					string result = fromatRecord(club, delimiter);
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