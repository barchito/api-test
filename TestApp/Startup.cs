using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FluentValidation.AspNetCore;
using TestApp.Model;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using TestApp.DAL.Interface;
using TestApp.DAL.People;
using TestApp.BAL.Interface;
using TestApp.BAL;

namespace TestApp
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}



        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment Env { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            //var config2 = Configuration.GetConnectionString("DefaultConnection");

            //if (Env.IsEnvironment("Test"))
            //{
            //    services.AddDbContext<TestAPIContext>(options =>
            //        options.UseInMemoryDatabase(databaseName: "StarWars"));
            //}
            //else
            //{
            //    services.AddDbContext<TestAPIContext>(options =>
            //        options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            //}

            services.AddDbContext<TestAPIContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<TestAPIContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped(typeof(IDataAccess<Person, int>), typeof(DataAccessRepository));
            services.AddScoped(typeof(IPeopleRepository), typeof(PeopleRepository));
            services.AddMvc().AddFluentValidation()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<IdentifierValidator>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc();
        }
    }
}
