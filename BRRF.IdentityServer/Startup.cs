// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using BRRF.IdentityServer.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using BRRF.IdentityServer.Repository.DbContexts;
using IdentityServer4.Validation;
using IdentityServer4.Services;
using BRRF.IdentityServer.Repository;
using IdentityServer4.Models;
using Newtonsoft.Json;

namespace BRRF.IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            _config = builder.Build();
            //_config = config;
            //_env = env;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=IdentityServer4.EntityFramework-2.0.0;trusted_connection=yes;";
            var connectionString = _config.GetConnectionString("IdentityServerConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(connectionString)
            );

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                .AddTransient<IProfileService, ProfileService>()
                .AddTransient<IAuthRepository, AuthRepository>();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddTestUsers(TestUsers.Users)
                // this adds the config data from DB (clients, resources, CORS)
                .AddConfigurationStore(options =>
                {
                    options.ResolveDbContextOptions = (provider, builder) =>
                    {
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    };
                    //options.ConfigureDbContext = builder =>
                    //    builder.UseSqlServer(connectionString,
                    //        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 10; // interval in seconds, short for testing
                });
            //.AddConfigurationStoreCache();
           
            services.AddMvc()
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            return services.BuildServiceProvider(validateScopes: true);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute(); 
        }
    }
}
