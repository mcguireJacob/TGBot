using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGBot.Models;

namespace TGBot.Helper
{
    public static  class Extensions
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            return services

            .AddTransient<ComplexEntity>();
            
        }



      
    }
}
