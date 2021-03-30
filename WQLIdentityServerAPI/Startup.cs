using AntennaKnowledgeBase.Api.Middleware;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Reflection;
using WQLIdentityServerAPI.Configurations;
using WQLIdentityServerAPI.Middleware;
using WQLIdentityServerAPI.Middleware.Exceptions;
using WQLIdentityServerAPI.Models;

namespace WQLIdentityServerAPI
{
    public class Startup
    {
        private readonly string AllowAnyDomain = "AllowAnyDomain";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        private SettingOptions _options;

        private IServiceCollection _services;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注册appsettings.json 中的settings
            _options = Configuration.GetSection(SettingOptions.Name).Get<SettingOptions>();
            services.Configure<SettingOptions>(Configuration.GetSection(SettingOptions.Name));

            services.ConfigCors(AllowAnyDomain);

            //samesite
            services.AddSameSiteCookiePolicy();



            services.AddControllersWithViews()
                 .AddRazorRuntimeCompilation()
                 .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());

                 });

            services.ConfigMiniProfiler(_options);




            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            services.Configure<IISServerOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });


            //自动转换
            services.ConfigAutoMapper();



            services.ConfigureSwagger(Configuration);


            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});



            //注册认证策略
            services.AddPolicies();

            //认证服务器
            services.ConfigDataBase(_options);



            ////注册asp.net Identity
            //services.ConfigIdentity(Configuration);
            ////注册Identityserver4认证服务
            //services.ConfigIdentityServer(Configuration);

            //添加认证过滤
            services.ConfigAuthentication(Configuration);


            _services = services;
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (_options.UseMinProfiler)
            {
                app.UseMiniProfiler();
            }
            

            app.UseCookiePolicy();

            app.UseCustomException(logger);//异常捕获



            app.UseStaticFiles();

            app.UseRouting();


            app.UseCors(AllowAnyDomain);

            app.UseAllServicesMildd(_services);

            app.UseIdentityServer();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger(c => {

                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            });

            app.UseSwaggerUI(c =>
            {
 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KnowledgeBaseAPI");
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("WQLIdentityServerAPI.swaggerIndex.html");
                c.RoutePrefix = "doc";
                c.OAuthClientId("IdentityServer4");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });



        }

        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterModule(new AutofacModule(Configuration));
        }



    }
}
