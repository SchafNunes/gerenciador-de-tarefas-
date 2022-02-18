using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Text;

namespace gerenciador_de_tarefas_
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "gerenciador_de_tarefas_", Version = "v1" });
            });

            var chaveCriptografiaEmBytes = Encoding.ASCII.GetBytes(ChaveJWT.ChaveSecreta);
            services.AddAuthentication(autenticacao => {
                autenticacao.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                autenticacao.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(autenticacao => {
                autenticacao.RequireHttpsMetadata = false;
                autenticacao.SaveToken = true;
                autenticacao.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chaveCriptografiaEmBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "gerenciador_de_tarefas_ v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(cors => 
                cors.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
