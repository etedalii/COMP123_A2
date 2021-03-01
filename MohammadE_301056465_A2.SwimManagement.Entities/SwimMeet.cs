using MohammadE_301056465_A2.SwimManagement.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class SwimMeet
	{
		public PoolType Course { get; set; }
		public DateTime EndDate { get; set; }
		public List<Event> Events { get; set; }
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
		}

		public SwimMeet(string name, DateTime start, DateTime end, PoolType course, byte noOfLanes)
		{
			Name = name;
			StartDate = start;
			EndDate = end;
			Course = course;
			NoOfLanes = noOfLanes;
		}

		public void AddEvent(Event anEvent)
		{
			Events.Add(anEvent);
		}

		public void Seed()
		{
			
		}

		public override string ToString()
		{
			return $"{Name},{StartDate},{EndDate},{Course}{NoOfLanes}";
		}
	}
}
