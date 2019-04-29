using System;

namespace DAF.Helper
{
    public class Helper
    {
        /// <summary>
        /// Формирует строку подключения к базе данных.
        /// </summary>
        public static string GetConnectionString()
        {
            var machineName = Environment.MachineName;
            string connectionString;

            switch (machineName)
            {
                case "ROMANPC":
                    connectionString = "commi.ddns.net,1433";
                    return $@"Server={connectionString};Database=blog;User Id=AquaDB;Password=!CommiFamily;";

                case "AQUA":
                    connectionString = "AQUA\\COMMIDB";
                    break;

                default:
                    connectionString = machineName;
                    break;
            }

            return $@"data source={connectionString};initial catalog=blog;Integrated Security=SSPI;MultipleActiveResultSets=True;App=EntityFramework";
        }
    }
}
