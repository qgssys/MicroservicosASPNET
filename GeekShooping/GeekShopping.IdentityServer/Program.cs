using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//aqui eu busco a connectionString da model Base e Context criada.
var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection,
                                                new MySqlServerVersion(
                                                    new Version(8, 4, 0)
                                                    )
                                                )
                                            );
//Fim serviço do dbContext/options
builder.Services.AddHttpContextAccessor();
//Aqui estou adicionando o serviço do Identity services - stores - token providers
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MySQLContext>()
    .AddDefaultTokenProviders();

//Aqui, a variável identityServerBuilder armazena o retorno de AddIdentityServer, para chamar AddDeveloperSigningCredential() corretamente.
//var identityServerBuilder = 
builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddInMemoryIdentityResources(
    IdentityConfiguration.IdentityResources)
.AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
.AddInMemoryClients(IdentityConfiguration.Clients)
.AddAspNetIdentity<ApplicationUser>()
.AddDeveloperSigningCredential();
//identityServerBuilder.AddDeveloperSigningCredential();
//Final da chamada ao metodo AddDeveloperSigningCredential()

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

//aqui add UseHttpsRedirection
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//esta chamada deve ser exatamente aqui entre UseRouting() e UseIdentityServer(), senao, nao vai funcionar
app.UseIdentityServer();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
