using MiddleWareExample.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//static file

app.UseStaticFiles();

//midleware
//m1
app.Use(async (context, next) =>
{
    Console.WriteLine("before M1");
    await next();
    Console.WriteLine("after M1");
});
//m2
app.Use(async (context, next) =>
{
    Console.WriteLine("before M2");
    await next();
    Console.WriteLine("after M2");
});
//m3
app.Use(async (context, next) =>
{
    Console.WriteLine("before M3");
    await next();
    Console.WriteLine("after M3");
});




app.UseHttpsRedirection();

app.UseIpRateLimiting();

app.UseAuthorization();

app.MapControllers();

app.Run();
