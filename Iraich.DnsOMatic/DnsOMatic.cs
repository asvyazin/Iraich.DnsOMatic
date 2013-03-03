using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Iraich.DnsOMatic
{
	public class Client : IDnsOMatic
	{
		private const string MyIpUrl = "http://myip.dnsomatic.com";
		private const string UpdateMyIpUrl = "https://updates.dnsomatic.com";

		public async Task<string> GetMyIp()
		{
			var response = await new HttpClient().GetAsync(MyIpUrl);
			return await response.Content.ReadAsStringAsync();
		}

		public async Task UpdateMyIp(UpdateParameters parameters)
		{
			var uriBuilder = new UriBuilder(UpdateMyIpUrl)
			{
				Path = "/nic/update",
				Query = BuildQuery(parameters.QueryParameters()),
			};
			var response = await CreateHttpClient(parameters.Authorization()).GetAsync(uriBuilder.Uri);
			var responseContent = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
				throw new Exception(responseContent);
		}

		private static HttpClient CreateHttpClient(DnsOMaticAuthorizationParameters parameters)
		{
			if (parameters == null) return new HttpClient();

			var credentials = parameters.Credentials();
			var result = new HttpClient(credentials == null ? null : new HttpClientHandler {Credentials = credentials});
			var productName = string.Format("{0}-{1}", parameters.Company(), parameters.Device());
			result.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(productName, parameters.Version()));
			return result;
		}

		private static string BuildQuery(Dictionary<string, string> parameters)
		{
			return string.Join("&",
				parameters.Select(p => string.Format("{0}={1}", p.Key, p.Value)));
		}
	}
}