using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MidiVelocityMapper.Helper;
using MidiVelocityMapper.Models;
using MidiVelocityMapper.VelocityCalculators;

namespace MidiVelocityMapper
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddCors(o => o.AddPolicy("CORS-Policy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors("CORS-Policy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api";
            });

            SetupMidiVelopcityMapper();
        }

        private void SetupMidiVelopcityMapper()
        {
            var settings = new SettingsHelper(Constants.SettingsFile).GetSettings();

            var midiIn = settings.MidiIn;
            var midiOut = settings.MidiOut;
            var curve = settings.Curves.SingleOrDefault(x => x.Name == settings.SelectedCurve);

            var calculator = new DefaultVelocityCalculator(curve.Map);
            MidiVelocityMapper.Instance.Init(calculator, midiIn, midiOut);
            MidiVelocityMapper.Instance.OnVelocityConverted += (message) => { Console.WriteLine(message); };
        }
    }
}
