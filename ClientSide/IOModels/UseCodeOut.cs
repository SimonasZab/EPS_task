using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientSide.IOModels {
	public class UseCodeOut {
		public byte Result { get; set; }
	}

	public enum UseCodeResult : byte {
		SUCCESS,
		ALREADY_REDEEMED,
		DOES_NOT_EXIST
	};
}
