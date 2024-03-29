﻿using System;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public class Swim
	{
		/// Represents the number of the heat, Heat is an individual race where a number of swimmers that race at the same time
		public ushort Heat { get; set; }

		/// Represents the number of the lane that swimmer is assigned in a	heat.
		public byte Lane { get; set; }

		/// Represents the final time of a swim (example: 1:05.52)
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
			return $"H: {Heat}, L: {Lane}, Swim Time: {time}";
		}
	}
}
