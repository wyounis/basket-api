using AutoMapper;
using BasketAPI.Core.Validation;
using BasketAPI.Data;
using BasketAPI.Data.Interfaces;
using BasketAPI.Services;
using BasketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BasketAPI
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDataContext, InMemoryDataContext>();
            services.AddTransient<IBasketService, BasketService>();
            services.AddAutoMapper();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelAttribute));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("beta", new Info { Title = "Basket API", Version = "beta" });
                c.IncludeXmlComments(string.Format(@"{0}/BasketAPI.XML", System.AppDomain.CurrentDomain.BaseDirectory));
                c.EnableAnnotations();
            });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/beta/swagger.json", "Basket API");
            });

            app.UseMvc();
        }
    }
}
