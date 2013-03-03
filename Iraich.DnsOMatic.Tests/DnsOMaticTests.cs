using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iraich.DnsOMatic.Tests
{
	[TestClass]
	public class DnsOMaticTests
	{
		private IDnsOMatic _dnsoMatic;

		[TestInitialize]
		public void Setup()
		{
			_dnsoMatic = new Client();
		}

		[TestMethod]
		public void TestGetMyIp()
		{
			var result = _dnsoMatic.GetMyIp().Result;
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestUpdateMyIp()
		{
			_dnsoMatic.UpdateMyIp(GetBasicUpdateParameters()
				.Authorization(auth => auth.Credentials(new NetworkCredential("invalid", "invalid")))
				.Myip("127.0.0.1")).Wait();
		}

		private const string MyUsername = "xxx";
		private const string MyPassword = "xxx";

		[TestMethod]
		public void TestGetAndUpdateMyIp()
		{
			GetAndUpdateIp(GetBasicUpdateParameters().Authorization(auth => auth.Credentials(new NetworkCredential(MyUsername, MyPassword)))).Wait();
		}

		private static UpdateParameters GetBasicUpdateParameters()
		{
			return new UpdateParameters().Authorization(auth => auth.Company("Iraich").Device("Test").Version("1.0"));
		}

		private async Task GetAndUpdateIp(UpdateParameters parameters)
		{
			var myip = await _dnsoMatic.GetMyIp();
			Console.WriteLine("new IP: {0}", myip);
			await _dnsoMatic.UpdateMyIp(parameters.Myip(myip));
		}
	}
}
