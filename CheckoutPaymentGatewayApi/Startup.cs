using CheckoutPaymentGateway.Bank;
using CheckoutPaymentGateway.Payment;
using CheckoutPaymentGateway.Services;
using CheckoutPaymentGateway.Store;
using CheckoutPaymentGatewayApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CheckoutPaymentGatewayApi
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
            services.AddLogging();

            services.AddTransient<IPaymentGateway, PaymentGateway>();
            services.AddTransient<IBank, AlwaysSucceedBank>();
            services.AddTransient<ICreditCardValidator, CreditCardValidator>();
            services.AddTransient<IMaskCreditCardService, MaskCreditCardService>();
            services.AddSingleton<IPaymentRepository, MemoryPaymentRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CustomErrorHandlerMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
