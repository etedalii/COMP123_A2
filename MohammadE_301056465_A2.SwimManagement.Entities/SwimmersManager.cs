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
						Swimmers.Add(registrant);

					record = reader.ReadLine();
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
			Registrant registrant = default;
			try
			{
				string[] fields = aRecord.Split(new[] { delimiter }, StringSplitOptions.None);
				checkException(fields);

				Address address = new Address(fields[3], fields[4], fields[5], fields[6]);
				registrant = new Registrant(Convert.ToUInt32(fields[0]), fields[1], Convert.ToDateTime(fields[2]), address, Convert.ToUInt64(fields[7]));

				if (GetSwimmer(registrant.Id) != null)
					throw new Exception($"The {registrant}, Swimmer with the registration number already exists");
				else
					return registrant;
			}
			catch (Exception ex)
			{
				//throw ex;
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

		private void checkException(string[] fields)
		{
			uint result;
			ulong phone;
			DateTime dt;

			if (fields.Length < 8)
				raiseException($"The {fields}, Not enough fields");

			if (!UInt32.TryParse(fields[0], out result))
				raiseException($"The {fields[0]},  Invalid registration number");

			if (!DateTime.TryParse(fields[2], out dt))
				raiseException($"The {fields[2]},  Birth date is invalid");

			if (string.IsNullOrEmpty(fields[1]))
				raiseException($"The {fields[1]}, Invalid swimmer name");

			if (!UInt64.TryParse(fields[7], out phone))
				raiseException($"The {fields[7]}, Phone number wrong format");
		}

		private void raiseException(string message)
		{
			throw new Exception(message);
		}
	}
}