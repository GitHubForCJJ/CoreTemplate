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
                //// ȫ���쳣����
                o.Filters.Add(typeof(GlobalExceptionFilter));
                //// ȫ��·��Ȩ�޹�Լ
                ////o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                //// ȫ��·��ǰ׺��ͳһ�޸�·��
                //o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            })
            //ȫ������Json���л�����
             .AddNewtonsoftJson(options =>
             {
                 ////����ѭ������
                 //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 ////��ʹ���շ���ʽ��key
                 options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                 //����ʱ���ʽ
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
        /// .netcore3.1 ��autoface����ע��
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {

        }
    }
}
