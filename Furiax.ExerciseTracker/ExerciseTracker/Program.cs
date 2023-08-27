using ExerciseTracker;
using ExerciseTracker.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;

//dapper version
var host = new HostBuilder()
	.ConfigureServices((hostContext, services) =>
	{
		services.AddTransient<IDbConnection>(sp => new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Database=ExerciseTracker"));
		services.AddTransient<IExerciseRepository, ExerciseRepository>();
		services.AddTransient<ExerciseService>();
		services.AddTransient<ExerciseController>();
	})
	.Build();

using (var scope = host.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var exerciseController = services.GetRequiredService<ExerciseController>();

	exerciseController.MainMenu();
}

