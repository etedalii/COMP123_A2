using MohammadE_301056465_A2.SwimManagement.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class Event
	{
		RegistrantsSwims swimmingEvents;

		public EventDistance Distance { get; set; }

		public Stroke Stroke { get; set; }

		public List<Registrant> Swimmers { get; }

		public Event()
		{
			swimmingEvents = new RegistrantsSwims();
		}

		public Event(EventDistance distance, Stroke stroke)
		{
			Distance = distance;
			Stroke = stroke;
		}

		public void AddSwimmer(Registrant aSwimmer)
		{
			foreach (Registrant item in Swimmers)
			{
				if (item.Id == aSwimmer.Id)
				{
					throw new Exception($"Swimmer {item.Name},{item.Id} is already enter");
				}
			}
			Swimmers.Add(aSwimmer);
		}

		public void EnterSwimmersTime(Registrant aSwimmer, string time)
		{
			
		}

		public void Seed(byte maxLanes)
		{

		}

		public override string ToString()
		{
			return base.ToString();
		}

		class RegistrantsSwims
		{
			List<Registrant> sweimmers;
			List<Swim> swims;

			public Swim Swim { get; set; }

			public void AddOrUpdate(Registrant swimmer, Swim swim)
			{
				sweimmers.Add(swimmer);
				swims.Add(swim);
			}

			public bool Contains(Registrant swimmer)
			{
				bool isExist = false;
				foreach (Registrant item in sweimmers)
				{
					if(item.Id == swimmer.Id)
					{
						isExist = true;
						break;
					}
				}
				return isExist;
			}

			public Swim GetSwimmersSwim(Registrant swimmer)
			{
				bool found = false;
				foreach (Registrant item in sweimmers)
				{
					if (item.Id == swimmer.Id)
					{
						found = true;
						break;
					}
				}
				if (found)
				{
					// TODO complete here
					return Swim;
				}
				else
					throw new Exception("Swimmer has not entered event");
			}
		}
	}
}
