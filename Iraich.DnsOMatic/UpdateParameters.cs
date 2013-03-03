using System;
using System.Collections.Generic;

namespace Iraich.DnsOMatic
{
	public class UpdateParameters
	{
		public UpdateParameters Hostname(string hostname)
		{
			_allParameters["hostname"] = hostname;
			return this;
		}

		public UpdateParameters Myip(string myip)
		{
			_allParameters["myip"] = myip;
			return this;
		}

		public UpdateParameters Wildcard(string wildcard)
		{
			_allParameters["wilcard"] = wildcard;
			return this;
		}

		public UpdateParameters Mx(string mx)
		{
			_allParameters["mx"] = mx;
			return this;
		}

		public UpdateParameters Backmx(string backmx)
		{
			_allParameters["backmx"] = backmx;
			return this;
		}

		public UpdateParameters Authorization(Action<DnsOMaticAuthorizationParameters> config)
		{
			var authorization = new DnsOMaticAuthorizationParameters();
			config(authorization);
			_authorization = authorization;
			return this;
		}

		public DnsOMaticAuthorizationParameters Authorization()
		{
			return _authorization;
		}

		private readonly Dictionary<string, string> _allParameters = new Dictionary<string, string>();
		private DnsOMaticAuthorizationParameters _authorization;

		public Dictionary<string, string> AllParameters
		{
			get { return _allParameters; }
		}
	}
}