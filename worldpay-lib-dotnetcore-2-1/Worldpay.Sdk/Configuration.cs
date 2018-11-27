using Microsoft.Extensions.Configuration;
using System;
using System.IO;
//using System.Configuration;

namespace Worldpay.Sdk
{
    public class Configuration
    {

        static IConfigurationRoot _config;

        static Configuration()
        {
            _config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile(@"appsettings.json", optional: false, reloadOnChange: true)
                            .Build();
        }


        /// <summary>
        /// Uri of the token request API
        /// </summary>
        public static string TokenUrl
        {
            //get { return ConfigurationManager.AppSettings["TokenUrl"]; }
            get { return _config.GetSection(@"AppConfiguration")[@"TokenUrl"]; }
        }

        /// <summary>
        /// Base Uri of the service
        /// </summary>
        public static string BaseUrl
        {
            //get { return ConfigurationManager.AppSettings["BaseUrl"]; }
            get { return _config.GetSection(@"AppConfiguration")[@"BaseUrl"]; }
        }

        /// <summary>
        /// The secret key for service authorization
        /// </summary>
        public static string ServiceKey
        {
            //get { return ConfigurationManager.AppSettings["ServiceKey"]; }
            get { return _config.GetSection(@"AppConfiguration")[@"ServiceKey"]; }
        }

        /// <summary>
        /// The merchant id corresponding to the service and client key
        /// </summary>
        public static string MerchantId
        {
            //get { return ConfigurationManager.AppSettings["MerchantId"]; }
            get { return _config.GetSection(@"AppConfiguration")[@"MerchantId"]; }
        }

        /// <summary>
        /// The client key for service authorization
        /// </summary>
        public static string ClientKey
        {
            //get { return ConfigurationManager.AppSettings["ClientKey"]; }
            get { return _config.GetSection(@"AppConfiguration")[@"ClientKey"]; }
        }


        public static string OrderLog
        {
            //get { return ConfigurationManager.AppSettings["OrderLog"]; }
            get { return _config.GetSection(@"AppConfiguration")[@"OrderLog"]; }
        }


        public static string WebhookUrl
        {
            //get { return ConfigurationManager.AppSettings["WebhookUrl"]; }
            get { return _config.GetSection(@"AppConfiguration")[@"WebhookUrl"]; }
        }
    }
}
