
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApiPiagetDois.Data;
using Microsoft.EntityFrameworkCore;
using ApiPiagetDois.Data;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApiPiagetDoisContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiPiagetDoisContext")
    ?? throw new InvalidOperationException("Connection string 'ApiPiagetDoisContext' not found.")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});





var app = builder.Build();

// Aplicar migrações do banco de dados automaticamente na inicialização
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApiPiagetDoisContext>();
    dbContext.Database.Migrate(); // Cria o banco e as tabelas se não existirem
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Policy Cors
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
