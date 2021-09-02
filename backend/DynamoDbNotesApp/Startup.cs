using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using DynamoDbNotesApp.Gateway;
using DynamoDbNotesApp.Gateway.Interfaces;
using DynamoDbNotesApp.UseCase;
using DynamoDbNotesApp.UseCase.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DynamoDbNotesApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            RegisterUseCases(services);
            RegisterGateways(services);

            ConfigureDynamoDbAsync(services).GetAwaiter().GetResult();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });


        }

        private void RegisterUseCases(IServiceCollection services)
        {
            services.AddScoped<IGetByIdUseCase, GetByIdUseCase>();
            services.AddScoped<IGetAllUseCase, GetAllUseCase>();
            services.AddScoped<ICreateNoteUseCase, CreateNoteUseCase>();
            services.AddScoped<IUpdateNoteUseCase, UpdateNoteUseCase>();
            services.AddScoped<IDeleteNoteUseCase, DeleteNoteUseCase>();
        }

        private void RegisterGateways(IServiceCollection services)
        {
            services.AddScoped<INotesGateway, NotesGateway>();
        }


        private async Task ConfigureDynamoDbAsync(IServiceCollection services)
        {

            bool localMode = false;
            _ = bool.TryParse(Environment.GetEnvironmentVariable("DynamoDb_LocalMode"), out localMode);

            if (localMode)
            {
                services.AddSingleton<IAmazonDynamoDB>(sp =>
                {
                    var clientConfig = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8000" };
                    return new AmazonDynamoDBClient(clientConfig);
                });
            }
            else
            {


                services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
                services.AddAWSService<IAmazonDynamoDB>();
            }          

            services.AddScoped<IDynamoDBContext>(sp =>
            {
                var db = sp.GetService<IAmazonDynamoDB>();
                return new DynamoDBContext(db);
            });
        }
    }
}
