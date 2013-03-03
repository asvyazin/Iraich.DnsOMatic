using System.Net;

namespace Iraich.DnsOMatic
{
	public class DnsOMaticAuthorizationParameters
	{
		private string _company;
		private string _device;
		private string _version;
		private ICredentials _credentials;

		public DnsOMaticAuthorizationParameters Company(string company)
		{
			_company = company;
			return this;
		}

		public DnsOMaticAuthorizationParameters Device(string device)
		{
			_device = device;
			return this;
		}

		public DnsOMaticAuthorizationParameters Version(string version)
		{
			_version = version;
			return this;
		}

		public DnsOMaticAuthorizationParameters Credentials(ICredentials credentials)
		{
			_credentials = credentials;
			return this;
		}

		public ICredentials Credentials()
		{
			return _credentials;
		}

		public string Company()
		{
			return _company;
		}

		public string Device()
		{
			return _device;
		}

		public string Version()
		{
			return _version;
		}
	}
}