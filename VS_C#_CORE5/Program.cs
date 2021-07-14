using DevAttic.ConfigCrypter.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestingMultiEnvWithConfigCrypter
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				})
			
				.ConfigureAppConfiguration(cfg =>
				{
					cfg.AddEncryptedAppSettings(crypter =>
					{
						crypter.CertificatePath = "forConfig.pfx";
						crypter.KeysToDecrypt = new List<string> { "EncryptedKeys:TestPhrase2" };
					});
				})
			;
	}
}
