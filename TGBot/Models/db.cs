using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TGBot.Models
{
    public class db
    {
        public SqlConnection ConnectionStrings { get; set; } 

        public db()
        {
            var configuration = GetConfiguration();
            ConnectionStrings = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }

        public IConfigurationRoot GetConfiguration()
        {
            var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return build.Build();
        }
    }
}
