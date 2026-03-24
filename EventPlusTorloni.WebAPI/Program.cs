using Azure.AI.ContentSafety;
using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

var endpoint = "";
var apiKey = "";


var client = new ContentSafetyClient(new Uri(endpoint), new Azure.AzureKeyCredential(apiKey));
builder.Services.AddSingleton(client);

builder.Services.AddDbContext<EventContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Registrar as Repositories (Injeção de Dependência)
builder.Services.AddScoped<ITipoEventoRepository, TipoEventoRepository > ();

builder.Services.AddScoped<ITipoUsuarioRepository, TipoUsuarioRepository>();

builder.Services.AddScoped<IInstituicaoRepository, InstituicaoRepository>();

builder.Services.AddScoped<IUsuariorRepository, UsuarioRepository>();

builder.Services.AddScoped<IEventoRepository, EventoRepository>();

builder.Services.AddScoped<IPresencaRepository, PresencaRepository>();

builder.Services.AddScoped<IComentarioEventoRepository, ComentarioEventoRepository>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
    .AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida quem está solicitando
        ValidateIssuer = true,

        //valida quem está reclamando
        ValidateAudience = true,

        //define se o tempo de expiração será válido
        ValidateLifetime = true,

        //forma de criptografia e válida a chave de autenticação
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("eventplus-chave-autenticacao-webapi-dev")),

        //valida o tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(5),

        //nome do issuer (de onde está vindo)
        ValidIssuer = "api_eventplus",

        //nome do audience (para onde ele está indo)
        ValidAudience = "api_eventplus"
    };
});

//Adiciona Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de Eventos",
        Description = "Aplicação para gerenciamento de eventos",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Maria123-ctrl",
            Url = new Uri("https://github.com/Maria123-ctrl")
        },
        License = new OpenApiLicense
        {
            Name = "Exemplo de licensa",
            Url = new Uri("https://example.com/license")
        }
    });

    //Usando a autenticação no swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger(options => { });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
