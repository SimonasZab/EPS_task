using Microsoft.AspNetCore.Mvc;
using ServerSide.Backends;
using ServerSide.IOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerSide.Models;

namespace ServerSide.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class PromoCodeController : ControllerBase {
		[HttpPost("generate")]
		public async Task<IActionResult> Generate([FromBody] GenerateIn data) {
			var dbContext = new MyDBContext();
			string samples = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			List<PromoCode> codes = new List<PromoCode>();
			DateTime startTime = DateTime.Now;

			while (true) {
				string code = PromoCodeBackend.GenerateCode(samples, data.Length);
				if(!await dbContext.PromoCodes.AnyAsync(x => x.Code == code)) {
					codes.Add(new PromoCode() { Code = code, Redeemed = false });
					if (codes.Count == data.Count) {
						break;
					}
				}
				if (DateTime.Now > startTime.AddSeconds(5)) {
					return Ok(new GenerateOut() { Result = false });
				}
			}
			await dbContext.PromoCodes.AddRangeAsync(codes);
			await dbContext.SaveChangesAsync();
			return Ok(new GenerateOut() { Result = true });
		}

		[HttpPost("useCode")]
		public async Task<IActionResult> UseCode([FromBody] UseCodeIn data) {
			var dbContext = new MyDBContext();
			PromoCode promoCode = await dbContext.PromoCodes.FirstOrDefaultAsync(x => x.Code == data.Code);
			if (promoCode == null) {
				return Ok(new UseCodeOut() { Result = (byte)UseCodeResult.DOES_NOT_EXIST });
			}
			if (promoCode.Redeemed) {
				return Ok(new UseCodeOut() { Result = (byte)UseCodeResult.ALREADY_REDEEMED });
			}
			promoCode.Redeemed = true;
			await dbContext.SaveChangesAsync();
			return Ok(new UseCodeOut() { Result = (byte)UseCodeResult.SUCCESS });
		}
	}
}
