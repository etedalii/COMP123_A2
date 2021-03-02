using System;
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
			return $"{aClub.ClubNumber}{delimiter}{aClub.Name}{delimiter}{aClub.ClubAddress.street}{delimiter}{aClub.ClubAddress.city}{delimiter}" +
				$"{aClub.ClubAddress.province}{delimiter}{aClub.ClubAddress.postalCode}{delimiter}{aClub.PhoneNumber}{delimiter}";
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
				checkException(fields);

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
					string result = $"{club.ClubNumber}{delimiter}{club.Name}{delimiter}{club.PhoneNumber}{delimiter}";
					writer.WriteLine(result);
				}
			}
			catch (IOException)
			{

			}
			finally
			{
				if (fileStream != null)
					fileStream.Close();

				if (writer != null)
					writer.Close();
			}
		}

		private void checkException(string[] fields)
		{
			uint result;
			ulong phone;
			string club = $"{fields[0]},{fields[1]},{fields[2]}, {fields[3]}, {fields[4]}, {fields[5]},{fields[6]}";

			if (fields.Length < 7)
				raiseException($"Invalid club record. Not enough fields:\n{club}");

			if (!UInt32.TryParse(fields[0], out result))
				raiseException($"Invalid club record. Club number is not valid:\n{club}");

			if (string.IsNullOrEmpty(fields[1]))
				raiseException($"Invalid club record. Invalid club name:\n{club}");

			if (!UInt64.TryParse(fields[6], out phone))
				raiseException($"Invalid club record. Phone number wrong format:\n{club}");
		}

		private void raiseException(string message)
		{
			throw new Exception(message);
		}
	}
}
