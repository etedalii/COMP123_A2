using MohammadE_301056465_A2.SwimManagement.Enumerations;
using System;
using System.Collections.Generic;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class SwimMeet
	{
		public PoolType Course { get; set; }
		public DateTime EndDate { get; set; }
		public List<Event> Events { get; }
		public string Name { get; set; }
		public byte NoOfLanes { get; set; }
		public DateTime StartDate { get; set; }

		public SwimMeet()
		{
			Name = string.Empty;
			StartDate = DateTime.Now;
			EndDate = DateTime.Now;
			Course = PoolType.SCM;
			NoOfLanes = 8;

			if (Events == null)
				Events = new List<Event>();
		}

		public SwimMeet(string name, DateTime start, DateTime end, PoolType course, byte noOfLanes)
		{
			Name = name;
			StartDate = start;
			EndDate = end;
			Course = course;
			NoOfLanes = noOfLanes;

			if (Events == null)
				Events = new List<Event>();
		}

		public void AddEvent(Event anEvent)
		{
			Events.Add(anEvent);
		}

		public void Seed()
		{
			foreach (Event item in Events)
			{
				item.Seed(NoOfLanes);
			}
		}

		public override string ToString()
		{
			string msg = $"Swim meet name: {Name}\nFrom-to: {StartDate} to {EndDate}\nPool type: {Course}\nNo lanes: {NoOfLanes}\nEvents:\n";
			foreach (Event item in Events)
			{
				msg += $"\t{item.Distance.ToString()} {item.Stroke.ToString()}\n\tSwimmers:\n";
				foreach (Registrant swimmer in item.Swimmers)
				{
					msg += $"\t{swimmer.Name}\n";
				}
			}
			return msg;
		}
	}
}
