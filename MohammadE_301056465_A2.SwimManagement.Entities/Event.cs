using MohammadE_301056465_A2.SwimManagement.Enumerations;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class Event
	{
		RegistrantsSwims swimmingEvents;

		public EventDistance Distance { get; set; }

		public Stroke Stroke { get; set; }

		public List<Registrant> Swimmers
		{
			get
			{
				return swimmingEvents.swimmers;
			}
		}

		public Event()
		{
			swimmingEvents = new RegistrantsSwims();
		}

		public Event(EventDistance distance, Stroke stroke) : this()
		{
			Distance = distance;
			Stroke = stroke;
		}

		public void AddSwimmer(Registrant aSwimmer)
		{
			foreach (Registrant item in swimmingEvents.swimmers)
			{
				if (item.Id == aSwimmer.Id)
				{
					throw new Exception($"Swimmer {item.Name},{item.Id} is already enter");
				}
			}
			Swim swim = new Swim();
			swimmingEvents.AddOrUpdate(aSwimmer, swim);
		}

		public void EnterSwimmersTime(Registrant aSwimmer, string time)
		{
			for (int i = 0; i < swimmingEvents.swimmers.Count; i++)
			{
				if (swimmingEvents.swimmers[i].Id == aSwimmer.Id)
				{
					Swim swim = swimmingEvents.swims[i];

					string[] result = time.Split(':');
					string secound = result[1].Split('.')[0];
					string milisec = result[1].Split('.')[1];
					DateTime dateValue = new DateTime(1, 1, 1, 0, Convert.ToInt32(result[0]), Convert.ToInt32(secound), Convert.ToInt32(milisec));
					swim.Time = dateValue;

					swimmingEvents.swims[i] = swim;
				}
			}
		}

		public void Seed(byte maxLanes)
		{
			byte heat = 1;
			byte lane = 1;
			for (int i = 0; i < swimmingEvents.swimmers.Count; i++)
			{
				if (i != 0 && i % maxLanes != 0)
				{
					Swim swim = swimmingEvents.swims[i];
					swim.Lane = (byte)lane++;
					swim.Heat = heat;

					swimmingEvents.swims[i] = swim;
				}
				else
				{
					heat++;
					lane = 1;
				}
			}
		}

		public override string ToString()
		{
			string result = $"{Distance},{Stroke}\n";
			for (int i = 0; i < swimmingEvents.swimmers.Count; i++)
			{
				Swim swim = swimmingEvents.swims[i];
				result += $"{swimmingEvents.swimmers[i].Name}\n";
				if (swim == null)
					result += $"Not seeded/no swim\n";
			}
			return result;
		}

		private class RegistrantsSwims
		{
			public List<Registrant> swimmers = new List<Registrant>();
			public List<Swim> swims = new List<Swim>();

			public Swim Swim { get; set; }

			public void AddOrUpdate(Registrant swimmer, Swim swim)
			{
				if (!Contains(swimmer))
				{
					swimmers.Add(swimmer);
				}
				else
				{
					int index = 0;
					foreach (Registrant item in swimmers)
					{
						if (item.Id == swimmer.Id)
						{
							break;
						}
						else
							index++;
					}

					swimmers[index] = swimmer;
				}
				swims.Add(swim);
			}

			public bool Contains(Registrant swimmer)
			{
				bool isExist = false;
				foreach (Registrant item in swimmers)
				{
					if (item.Id == swimmer.Id)
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
				foreach (Registrant item in swimmers)
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
