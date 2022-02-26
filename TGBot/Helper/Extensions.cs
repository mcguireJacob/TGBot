using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGBot.Models;
using JSON = Newtonsoft.Json.JsonConvert;

namespace TGBot.Helper
{
    public static  class Extensions
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            return services

            .AddTransient<ComplexEntity>();
            
        }


        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JSON.SerializeObject(value));
        }

        /// <summary>
        /// Accessor to allow getting session with a key similarly to older .Net
        /// </summary>
        /// <typeparam name="T">Type of parameter to be returned</typeparam>
        /// <param name="session">Controller session</param>
        /// <param name="key">Key of the session value to return</param>
        /// <returns>Session value as T</returns>
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default : JSON.DeserializeObject<T>(value);
        }




    }
}
