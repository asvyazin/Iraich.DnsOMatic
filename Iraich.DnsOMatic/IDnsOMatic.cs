using System.Threading.Tasks;

namespace Iraich.DnsOMatic
{
	public interface IDnsOMatic
	{
		Task<string> GetMyIp();
		Task<UpdateMyIpStatus> UpdateMyIp(UpdateParameters parameters);
	}
}