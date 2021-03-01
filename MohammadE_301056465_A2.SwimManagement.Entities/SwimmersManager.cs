using System;
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
			Registrant registrant = default;
			foreach (Registrant item in Swimmers)
			{
				if (aSwimmer.Id == item.Id)
				{
					registrant = item;
					break;
				}
			}

			return $"{registrant.Id}{delimiter}{registrant.Name}{delimiter}{registrant.DateOfBirth}{delimiter}{registrant.Address.street}{delimiter}" +
				$"{registrant.Address.city}{delimiter}{registrant.Address.province}{delimiter}{registrant.Address.postalCode}{delimiter}{registrant.PhoneNumber}" +
				$"{delimiter}{registrant.Club.ClubNumber}";
		}

		public Registrant GetSwimmer(uint regNumber)
		{
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
					Swimmers.Add(registrant);
				}
			}
			catch (IOException ex)
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
			//Add Exception fields
			string[] fields = aRecord.Split(new[] { delimiter }, StringSplitOptions.None);
			Registrant registrant = new Registrant()
			{

			};
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
					string result = $"{item.Id}{delimiter}{item.Name}{delimiter}{item.PhoneNumber}{delimiter}";
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