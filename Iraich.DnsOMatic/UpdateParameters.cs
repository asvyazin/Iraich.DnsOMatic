using System;
using System.Collections.Generic;

namespace Iraich.DnsOMatic
{
	public class UpdateParameters
	{
		public UpdateParameters Hostname(string hostname)
		{
			_queryParameters["hostname"] = hostname;
			return this;
		}

		public UpdateParameters Myip(string myip)
		{
			_queryParameters["myip"] = myip;
			return this;
		}

		public UpdateParameters Wildcard(string wildcard)
		{
			_queryParameters["wilcard"] = wildcard;
			return this;
		}

		public UpdateParameters Mx(string mx)
		{
			_queryParameters["mx"] = mx;
			return this;
		}

		public UpdateParameters Backmx(string backmx)
		{
			_queryParameters["backmx"] = backmx;
			return this;
		}

		public UpdateParameters Authorization(Action<DnsOMaticAuthorizationParameters> config)
		{
			var authorization = _authorization ?? new DnsOMaticAuthorizationParameters();
			config(authorization);
			_authorization = authorization;
			return this;
		}

		public DnsOMaticAuthorizationParameters Authorization()
		{
			return _authorization;
		}

		private readonly Dictionary<string, string> _queryParameters = new Dictionary<string, string>();
		private DnsOMaticAuthorizationParameters _authorization;

		public Dictionary<string, string> QueryParameters()
		{
			return _queryParameters;
		}
	}
}