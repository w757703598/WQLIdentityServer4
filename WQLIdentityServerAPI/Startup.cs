using AntennaKnowledgeBase.Api.Middleware;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WQLIdentityServerAPI.Configurations;
using WQLIdentityServerAPI.Middleware.Exceptions;


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


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //samesite
            services.AddSameSiteCookiePolicy();


            services.AddControllersWithViews()
             .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());

             });

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

            services.ConfigCors(AllowAnyDomain);

            services.ConfigureSwagger(Configuration);


            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => {
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});



            //注册认证策略
            services.AddPolicies();

            //认证服务器
            services.ConfigDataBase(Configuration);



            ////注册asp.net Identity
            //services.ConfigIdentity(Configuration);
            ////注册Identityserver4认证服务
            //services.ConfigIdentityServer(Configuration);

            //添加认证过滤
            services.ConfigAuthentication(Configuration);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {

            app.UseCookiePolicy();

            app.UseCustomException(logger);//异常捕获



            app.UseStaticFiles();

            app.UseRouting();


            app.UseCors(AllowAnyDomain);



            app.UseIdentityServer();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KnowledgeBaseAPI");

                c.RoutePrefix = "doc";
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
