using System.Linq;
using Parseq;
using Parseq.Combinators;

namespace Iraich.DnsOMatic
{
	public class UpdateMyIpStatus
	{
		public static UpdateMyIpStatus FromResponse(string response)
		{
			var linefeed = Chars.Sequence("\n").Or(Chars.Sequence("\r\n"));
			var line = Chars.Any().Many().Select(chars => new string(chars.ToArray()));
			var parser = line.SepBy(linefeed).Select(lines => lines.Select(UpdateServiceStatus.FromLine));
			var services = parser.Run(response.AsStream()).Left.Perform().Perform();
			return new UpdateMyIpStatus
			{
				Services = services.ToArray()
			};
		}

		public UpdateServiceStatus[] Services { get; private set; }
	}
}