using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connString = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddSession(o =>
{
    o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    o.Cookie.Name = "ContosoPizza.Session";
    o.Cookie.HttpOnly = true;
});
builder.Services.AddStackExchangeRedisCache( o =>
{
    o.Configuration = connString;
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable Sessions
app.UseSession();

app.MapControllers();

app.Run();
