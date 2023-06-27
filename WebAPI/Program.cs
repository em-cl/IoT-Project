using Application;
using Infrastructure;
using Infrastructure.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation;
using Presistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
	.AddPresentation()
	.AddApplication()
	.AddInfrastructure(builder.Configuration.GetSection("MailSettings"))
	.AddPresistence(
		builder.Configuration
			.GetConnectionString("DefaultConnection")!);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Services.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseHsts();
}

/*app.UseHttpsRedirection(); - Redirects HTTP requests to HTTPS.
 This middleware should be placed at the beginning to ensure that 
 all requests are securely redirected.*/
app.UseHttpsRedirection();

/*app.UseStaticFiles(); - Enables serving static files, such as
 HTML, CSS, JavaScript, and images. 
 It should be placed after app.UseHttpsRedirection(); 
 to ensure that static files are also served securely.*/
app.UseStaticFiles();

/*app.UseRouting(); - Sets up routing for your application.
 It should be placed after app.UseStaticFiles(); 
 to ensure that static files are served before routing is performed.*/
app.UseRouting();

/*app.UseAuthorization(); - Enables authorization for protected resources.
 It should be placed after app.UseRouting(); 
 so that routing is set up before authorization checks are performed.*/
app.UseAuthorization();

/*app.MapControllers(); - Maps controllers for handling API requests.
 It should be placed after app.UseAuthorization(); 
 to ensure that authorization checks are performed before reaching the controllers.*/
app.MapControllers();

/*app.MapBlazorHub(); - Maps the Blazor hub for handling Blazor-specific functionality.
 It should be placed after app.MapControllers(); 
 to ensure that API requests are handled before Blazor-specific functionality.*/
app.MapBlazorHub();

/*app.MapFallbackToPage("/_Host"); - Maps a fallback page for all unmatched routes,
 which is typically used for hosting a Blazor application. 
 It should be placed after app.MapBlazorHub(); to 
 ensure that Blazor-specific functionality is set up before handling unmatched routes.*/
app.MapFallbackToPage("/_Host");

/*app.Run(); - Executes the final middleware and request handling.
 It should be placed at the end to ensure that all the preceding components 
 have been set up before executing the application logic.*/
await app.RunAsync();
