global using WebAPITextRPG.Models;
global using WebAPITextRPG.Services.CharacterService;
global using WebAPITextRPG.Dtos.Character;
global using AutoMapper;
global using WebAPITextRPG.Services.ItemService;
global using WebAPITextRPG.Services.SpellService;
global using WebAPITextRPG.Services.WeaponService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ISpellService, SpellService>();
builder.Services.AddScoped<IWeaponService, WeaponService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
