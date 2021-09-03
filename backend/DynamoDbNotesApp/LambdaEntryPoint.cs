using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DynamoDbNotesApp
{
    public class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
    {
        public LambdaEntryPoint()
        {
            LambdaLogger.Log("constructor called");
        }

        protected override void Init(IWebHostBuilder builder)
        {
            try
            {
                LambdaLogger.Log("Startup called.");

                builder
                 .UseStartup<Startup>();

            }
            catch (Exception ex)
            {
                LambdaLogger.Log("Exception throw in LambdaEntryPoint: " + ex);
            }

        }
    }
}
