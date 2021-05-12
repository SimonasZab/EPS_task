using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSide.IOModels {
	public class GenerateIn {
		[Required]
		[Range(1000, 2000)]
		public ushort Count { get; set; }
		[Required]
		[Range(7, 8)]
		public byte Length { get; set; }
	}
}
