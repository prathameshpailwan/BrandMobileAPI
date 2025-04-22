using Brand_Web_API.BAL;
using Brand_Web_API.Common;
using Brand_Web_API.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<MobileTransaction>(); // MobileTransaction service
builder.Services.AddScoped<MobileDbConnection>(); // MobileDbConnection service
builder.Services.AddScoped<ConversionsClass>();

// Enable Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Configure CORS Policy (UPDATED)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // ✅ Allow only Angular app (secure)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // ✅ Allow authentication cookies/tokens
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Apply CORS BEFORE Authorization
app.UseCors("AllowAll"); // 🚀 Moved this line here

app.UseAuthorization();

app.MapControllers();

app.Run();
