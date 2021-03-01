using System;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public static class RegistrationNumberGenerator
	{
		static uint nextRegistrationNumber;

		public static uint GetNext()
		{
			nextRegistrationNumber = (uint)new Random().Next();

			return nextRegistrationNumber;
		}
	}
}
