using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;    
using Microsoft.Graph.Auth;


namespace GraphClient
{
    class Program
    {
        private const string _clientId = "ec4a2e38-a2ad-4bb9-ab34-4897a11604c1";
        private const string _tenantId = "383da738-edda-4d40-9934-c26a29d21789";

        public static async Task Main(string[] args)
        {
            var app = PublicClientApplicationBuilder
                .Create(_clientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
                .WithRedirectUri("http://localhost")
                .Build();

            List<string> scopes = new List<string> 
            { 
                "user.read" 
            };

            DeviceCodeProvider provider = new DeviceCodeProvider(app, scopes);
            GraphServiceClient client = new GraphServiceClient(provider);
            User myProfile = await client.Me.Request().GetAsync();
            Console.WriteLine($"Name:\t{myProfile.DisplayName}");
            Console.WriteLine($"AAD Id:\t{myProfile.Id}");

        }
    }
}
