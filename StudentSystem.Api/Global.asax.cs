using StudentSystem.Api.Filter;
using StudentSystem.Api.Map;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StudentSystem.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // GlobalFilters.Filters.Add(new MvcCookieAuthenticationFilter("CookieAuth"));
            var httpConfiguration = System.Web.Http.GlobalConfiguration.Configuration;
            httpConfiguration.Filters.Add(new ApiTokenAuthenticationFilter());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //  RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            httpConfiguration.EnsureInitialized();

            this.ConfigJsonSettings();
            this.ConfigFormatters(httpConfiguration);

            var assembly = Assembly.GetExecutingAssembly();
            var allTypes = new List<Type>();
            allTypes.AddRange(assembly.GetTypes().Where(type => type != null));
            foreach (var type in allTypes)
            {
                AutoMapperHelper.CreateMap(type);
            }
            AutoMapperHelper.Register();  //创建映射完毕后进行注册

        }

        #region JosnSetting
        private void ConfigJsonSettings()
        {
            var jsSettings = new JsonSerializerSettings();
            ConfigJsonSettings(jsSettings);
            JsonConvert.DefaultSettings = () => jsSettings;
        }

        private void ConfigJsonSettings(JsonSerializerSettings jsSettings)
        {
#if DEBUG
            jsSettings.Formatting = Formatting.Indented;
#endif

            jsSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            jsSettings.ContractResolver = new CamelCasePropertyNamesContractResolver
            {
                IgnoreSerializableAttribute = true
            };

            jsSettings.Converters.Add(new VersionConverter());
            jsSettings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss",
                DateTimeStyles = DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal
            });
        }

        private void ConfigFormatters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);

            var jsonFormat = httpConfiguration.Formatters.JsonFormatter;
#if DEBUG
            jsonFormat.Indent = true;
#endif

            ConfigJsonSettings(jsonFormat.SerializerSettings);
        }
        #endregion
    }
}
