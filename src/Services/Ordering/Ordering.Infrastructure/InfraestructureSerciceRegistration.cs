﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infraestructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Infraestructure.Persistence;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class InfraestructureSerciceRegistration
    {
        public static IServiceCollection AddInfraestructureServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddScoped<IEMailService, EMailService>();

            return services;
        }
    }
}
