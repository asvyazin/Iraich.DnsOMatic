using System.Linq;
using Parseq;
using Parseq.Combinators;

namespace Iraich.DnsOMatic
{
	public class UpdateServiceStatus
	{
		public static UpdateServiceStatus FromLine(string line)
		{
			var word = Chars.NoneOf(' ').Many().Select(chars => new string(chars.ToArray()));
			var argumentParser = Chars.Space().Right(word);
			var lineParser = word.Pipe(argumentParser, argumentParser.Maybe(), argumentParser.Maybe(),
				(status, ip, hostname, service) =>
					status == "good" ? FromSuccess(ip, hostname, service) : FromError(status, ip, hostname, service));
			return lineParser.Run(line.AsStream()).Left.Perform().Perform();
		}

		private static UpdateServiceStatus FromSuccess(string ip, Option<string> hostname, Option<string> service)
		{
			return new UpdateServiceStatus(true, ip, hostname, service);
		}

		private static UpdateServiceStatus FromError(string errorMessage, string ip, Option<string> hostname, Option<string> service)
		{
			return new UpdateServiceStatus(false, ip, hostname, service)
			{
				ErrorMessage = errorMessage
			};
		}

		private UpdateServiceStatus(bool success, string ip, Option<string> hostname, Option<string> service)
		{
			Success = success;
			Ip = ip;
			string hostnameStr;
			if (hostname.TryGetValue(out hostnameStr))
				Hostname = hostnameStr;
			string serviceStr;
			if (service.TryGetValue(out serviceStr))
				Service = serviceStr;
		}

		public bool Success { get; private set; }
		public string ErrorMessage { get; private set; }

		public string Ip { get; private set; }
		public string Hostname { get; private set; }
		public string Service { get; private set; }

		public override string ToString()
		{
			return string.Format("Success: {0}, ErrorMessage: {1}, Ip: {2}, Hostname: {3}, Service: {4}", Success, ErrorMessage, Ip, Hostname, Service);
		}
	}
}