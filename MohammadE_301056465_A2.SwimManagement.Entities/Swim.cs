using System;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class Swim
	{
		public ushort Heat { get; set; }
		public byte Lane { get; set; }
		public DateTime Time { get; set; }

		public Swim()
		{

		}

		public Swim(ushort heat, byte lane)
		{
			Heat = heat;
			Lane = lane;
		}

		public override string ToString()
		{
			string time = Time != default ? Time.ToShortTimeString() : "no time";
			return $"Heat number:{Heat}, Lane number:{Lane}, Swim Time:,{time}";
		}
	}
}
