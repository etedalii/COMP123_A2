using System;

namespace MohammadE_301056465_A2.SwimManagement.Entities
{
	public static class RegistrationNumberGenerator
	{
		private static uint nextRegistrationNumber;

		public static uint GetNext()
		{
			nextRegistrationNumber = nextRegistrationNumber + 1;

			return nextRegistrationNumber;
		}
	}
}
