using Autofac;
using Autofac.Extensions.DependencyInjection;
using HotelSystem.IoC;
using HotelSystem.WebAPI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new HotelAutofacModule());
});

builder.Services.AddAutoMapper(cfg => {}, typeof(HotelSystem.BLL.MappingProfile), typeof(ApiMappingProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

app.MapControllers();
app.Run();