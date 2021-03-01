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
			Clubs.Add(aClub);
		}

		private string fromatRecord(Club aClub, string delimiter)
		{
			return $"{aClub.ClubNumber}{delimiter}{aClub.Name}{delimiter}" +
				$"{aClub.ClubAddress.street}{delimiter}{aClub.ClubAddress.city}{delimiter}" +
				$"{aClub.ClubAddress.province}{delimiter}" +
				$"{aClub.ClubAddress.postalCode}{delimiter}" +
				$"{aClub.PhoneNumber}{delimiter}";
		}

		public Club GetClub(uint regNumber)
		{
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
			FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
			StreamReader reader = new StreamReader(fileStream);
			Club club = default;

			string record = reader.ReadLine();
			string[] fields;
			while (record != null)
			{
				//TODO fix here
				fields = record.Split(new[] { delimiter }, StringSplitOptions.None);
				try
				{
					club = new Club()
					{

					};

					Clubs.Add(club);
				}
				catch (IOException ex)
				{

				}
				finally
				{
				}
			}


			reader.Close();
			fileStream.Close();
		}

		private Club processClubRecord(string aRecord, string delimiter)
		{
			//TODO
			return null;
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
	}
}
