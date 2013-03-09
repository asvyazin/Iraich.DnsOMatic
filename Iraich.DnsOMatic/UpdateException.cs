using System;

namespace Iraich.DnsOMatic
{
	public enum ErrorCode
	{
		Unknown,
		BadAuth,
		NotFqdn,
		NoHost,
		NumHost,
		Abuse,
		BadAgent,
		LocalhostOnly,
		DnsError,
		Sos
	}

	public class UpdateException: Exception
	{
		public UpdateException(string errorMessage)
		{
			ErrorMessage = errorMessage;
			ErrorCode = ParseErrorCode(errorMessage);
		}

		private static ErrorCode ParseErrorCode(string errorMessage)
		{
			switch (errorMessage)
			{
				case "badauth":
					return ErrorCode.BadAuth;
				case "notfqdn":
					return ErrorCode.NotFqdn;
				case "nohost":
					return ErrorCode.NoHost;
				case "numhost":
					return ErrorCode.NumHost;
				case "abuse":
					return ErrorCode.Abuse;
				case "badagent":
					return ErrorCode.BadAgent;
				case "good 127.0.0.1":
					return ErrorCode.LocalhostOnly;
				case "dnserr":
					return ErrorCode.DnsError;
				case "911":
					return ErrorCode.Sos;
				default:
					return ErrorCode.Unknown;
			}
		}

		public string ErrorMessage { get; set; }
		public ErrorCode ErrorCode { get; set; }
	}
}