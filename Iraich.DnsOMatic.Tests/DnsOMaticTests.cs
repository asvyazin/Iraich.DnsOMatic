using System.Net;
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
			_dnsoMatic.UpdateMyIp(new UpdateParameters()
				.Authorization(auth => auth.Company("Iraich")
					.Device("Test")
					.Version("0.1")
					.Credentials(new NetworkCredential("invalid", "invalid")))
				.Myip("127.0.0.1")).Wait();
		}
	}
}
