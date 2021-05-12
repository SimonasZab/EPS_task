using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSide.Models {
	public class PromoCode : Entity {
		public string Code { get; set; }
		public bool Redeemed { get; set; }
	}
}
