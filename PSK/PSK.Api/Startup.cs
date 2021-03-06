﻿using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSK.Api.AutoMapper;
using PSK.Api.Filters;
using PSK.DataAccess;
using PSK.DataAccess.Interfaces;
using PSK.Domain.Identity;
using PSK.Persistence;
using PSK.Services;

namespace PSK.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<Employee, UserRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<Employee, UserRole, DataContext, int>>()
                .AddRoleStore<RoleStore<UserRole, DataContext, int>>()
                .AddDefaultTokenProviders();

            var connectionString = Configuration.GetValue<string>("PskConnectionString");

            services.AddDbContext<IDataContext, DataContext>(options => { options.UseSqlServer(connectionString); });

            services.AddSingleton(Configuration);

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile<MappingProfile>(); });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc(options =>
            {
                options.Filters.Add(new LogAttribute());
                options.Filters.Add(new ExceptionFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Register Dependencies
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            containerBuilder.RegisterType<EmployeeDataAccess>().As<IEmployeeDataAccess>();
            containerBuilder.RegisterType<EmployeeService>().As<IEmployeeService>();
            containerBuilder.RegisterType<DataInitializer>().As<IDataInitializer>();
            containerBuilder.RegisterType<DataContext>().AsSelf();

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
                databaseInitializer.InitializeRoles();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}