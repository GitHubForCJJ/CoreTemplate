using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arch.EntityFrameworkCore.UnitOfWork;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Temp.Data;
using Temp.Filters;
using Temp.Repository;
using Temp.SetupExtensions;

namespace Temp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerSetup();
            services.AddDbContext<BloggingContext>(opt => opt.UseMySql(Configuration.GetConnectionString("UserDbRead")))
            //.AddDbContext<BloggingContext>(opt => opt.UseInMemoryDatabase("UnitOfWork"))
            .AddUnitOfWork<BloggingContext>()
            .AddCustomRepository<Blog, BlogRepository>()
            .AddCustomRepository<LoginToken, LoginTokenRepository>();
            //.AddCustomRepository<Blog, CustomBlogRepository>();

            services.AddControllers(o =>
            {
                //// 全局异常过滤
                o.Filters.Add(typeof(GlobalExceptionFilter));
                //// 全局路由权限公约
                ////o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                //// 全局路由前缀，统一修改路由
                //o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            })
            //全局配置Json序列化处理
             .AddNewtonsoftJson(options =>
             {
                 ////忽略循环引用
                 //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 ////不使用驼峰样式的key
                 options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                 //设置时间格式
                 options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                 //options.SerializerSettings.Converters.
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();

            app.UseAuthorization();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp v1");
                c.RoutePrefix = "";
            });

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        /// <summary>
        /// .netcore3.1 下autoface程序集注入
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {

        }
    }
}
