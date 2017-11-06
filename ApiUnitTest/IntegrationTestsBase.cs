using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using TestApp.Model;

namespace ApiUnitTest
{
    public class IntegrationTestsBase<TStartup> : IDisposable
        where TStartup : class
    {
        private readonly TestServer server;
        
        private string projectRootFolder;
      
        public IntegrationTestsBase()
        {

            //var host = new WebHostBuilder()
            //    .UseContentRoot($"..\\..\\..\\..\\..\\TestApp\\TestApp\\")
            //                .UseStartup<TStartup>()
            //                .ConfigureServices(ConfigureServices);

            var host = new WebHostBuilder()
                            .UseStartup<TStartup>()
                            .ConfigureServices(ConfigureServices);


            this.server = new TestServer(host);
            this.Client = this.server.CreateClient();
        }

        public HttpClient Client { get; }

        public void Dispose()
        {
            this.Client.Dispose();
            this.server.Dispose();
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TestAPIContext>(
                optionsBuilder => optionsBuilder.UseInMemoryDatabase());
        }
    }
}
