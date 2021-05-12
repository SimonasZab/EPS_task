using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClientSide.IOModels;

namespace ClientSide {
	class Program {
		private readonly static string API_URL = "http://localhost:41250/promocode/";

		static async Task Main(string[] args) {
			Console.WriteLine("This is a client app");
			Console.WriteLine("Commands:");
			Console.WriteLine("Generate <CodesCount> <CodeLength> - generates <CodesCount> amount of codes with <CodeLength> length");
			Console.WriteLine("<CodesCount> - number from [1000:2000]");
			Console.WriteLine("<CodeLength> - number from [7:8]");
			Console.WriteLine("UseCode <Code> - uses <Code> code");
			Console.WriteLine("<Code> - string of length [7:8]");
			Console.WriteLine("Exit - closes the app");

			bool running = true;
			while (running) {
				string line = Console.ReadLine();
				string[] words = line.Split(' ');
				if (words.Length == 0) {
					continue;
				}
				string commandName = words[0];
				switch (commandName) {
					default:
						Console.WriteLine("Unknown command");
						break;
					case "Exit":
						running = false;
						break;
					case "Generate":
						await PerformGenerateCommand(words);
						break;
					case "UseCode":
						await PerformUseCodeCommand(words);
						break;
				}
			}
		}

		public static async Task PerformGenerateCommand(string[] words) {
			if (words.Length != 3) {
				Console.WriteLine("Command expected 2 parameters");
				return;
			}
			if (!ushort.TryParse(words[1], out ushort codesCount)) {
				Console.WriteLine("Unable to parse parameters");
				return;
			}
			if (codesCount < 1000 || codesCount > 2000) {
				Console.WriteLine("Invalid parameter");
				return;
			}
			if (!byte.TryParse(words[2], out byte codeLength)) {
				Console.WriteLine("Unable to parse parameters");
				return;
			}
			if (codeLength < 7 || codeLength > 8) {
				Console.WriteLine("Invalid parameter");
				return;
			}

			GenerateOut generateOut = await HttpPost<GenerateOut>("generate", new GenerateIn { Count = codesCount, Length = codeLength });
			Console.WriteLine("Response: " + generateOut.Result);
		}

		public static async Task PerformUseCodeCommand(string[] words) {
			if (words.Length != 2) {
				Console.WriteLine("Command expected 1 parameters");
				return;
			}
			string code = words[1];
			if (code.Length < 7 || code.Length > 8) {
				Console.WriteLine("Invalid parameter");
				return;
			}

			UseCodeOut useCodeOut  = await HttpPost<UseCodeOut>("useCode", new UseCodeIn { Code = code });
			Console.WriteLine("Response: " + ((UseCodeResult)useCodeOut.Result).ToString());
		}

		public static async Task<T> HttpPost<T>(string method, object contentObj) where T : class {
			Console.WriteLine("Sending request...");
			HttpClient client = new HttpClient();
			var content = new StringContent(
				JsonConvert.SerializeObject(contentObj),
				Encoding.UTF8, "application/json"
			);
			HttpResponseMessage response = await client.PostAsync(API_URL + method, content);
			if (response.StatusCode != System.Net.HttpStatusCode.OK) {
				Console.WriteLine("[ERROR] Something went wrong");
				return null;
			}
			Console.WriteLine("Request status is OK");
			string responseBody = await response.Content.ReadAsStringAsync();
			T responseContent = JsonConvert.DeserializeObject<T>(responseBody);
			return responseContent;
		}
	}
}
