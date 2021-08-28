using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_desafio21dias.Servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using web_renderizacao_server_side.Helpers;

namespace api_desafio21dias
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                builder =>
                                {
                                    builder.WithOrigins("http://localhost:4200",
                                                        "https://localhost:5009", 
                                                        "https://www.torneseumprogramador.com.br")
                                                        .AllowAnyHeader()
                                                        .AllowAnyMethod();
                                });
            });
            Program.AlunoApi = Configuration.GetConnectionString("AlunosApi");
            string strCnn = Configuration.GetConnectionString("MinhaConexao");
            Config.AdministradorApi = Configuration.GetConnectionString("AdministradorApi");
            services.AddDbContext<DbContexto>(options => options.UseSqlServer(strCnn));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Web API deafio 21 dias",
                    Version = "v1",
                    Description = "Web API Feita para materiais no desafio 21 dias"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio 21 dias"));
        
            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
