using HotelListing.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console();
//    


/// <summary>
/// This is good for inline configuration. Also outputTemplate is good enough here.
/// </summary>
//.Net6 IwebHostBuilder is in builder.Host
//builder.Host.UseSerilog((ctx, lc) => lc
//.WriteTo.File(
//    path: @"d:\hotellistings\logs\log-.txt",
//    outputTemplate: "{Timestamp: yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",  //detailed information, should use that template
//    rollingInterval: RollingInterval.Day,  //Create file once a day
//    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
//    ).ReadFrom.Configuration(ctx.Configuration));   // .CreateLogger() - no need it will be occur Exception cause builder.Build() also execute CreateLogger!


/// <summary>
/// Better way to use Configuration in ctx(json file) and write it in JSON.
/// 
/// </summary>
builder.Host.UseSerilog((ctx,lc)=> lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));



builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "HotelListing", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p => p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");  // name of policy that it should use

app.UseAuthorization();

app.MapControllers();


try
{
    Log.Information("Application is starting");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application Failed to start!");
}
finally
{
    Log.CloseAndFlush();

}

