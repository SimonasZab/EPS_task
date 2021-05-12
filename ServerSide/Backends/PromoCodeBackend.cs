using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Backends {
	public class PromoCodeBackend {
		public static string GenerateCode(string samples, int codeLength) {
			StringBuilder stringBuilder = new StringBuilder();
			Random rnd = new Random();
			for (int i = 0; i < codeLength; i++)
				stringBuilder.Append(samples[rnd.Next(samples.Length)]);
			return stringBuilder.ToString();
		}
	}
}