using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide.IOModels {
	public class UseCodeIn {
		[Required]
		[StringLength(8)]
		public string Code { get; set; }
	}
}
