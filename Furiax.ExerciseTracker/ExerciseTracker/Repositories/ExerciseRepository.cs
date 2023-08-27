using Dapper;
using ExerciseTracker.Models;
using System.Data;

namespace ExerciseTracker.Repositories
{
	public class ExerciseRepository : IExerciseRepository
	{
		private readonly IDbConnection _connection;
		public ExerciseRepository(IDbConnection connection)
		{
			_connection = connection ?? throw new ArgumentException(nameof(connection));
		}

		public void Add(ExerciseModel exercise)
		{
			_connection.Execute("INSERT INTO Exercises (ExerciseType, DateStart, DateEnd, Comments) VALUES (@ExerciseType, @DateStart, @DateEnd, @Comments)",
								new { ExerciseType = exercise.ExerciseType, DateStart = exercise.DateStart, DateEnd = exercise.DateEnd, Comments = exercise.Comments });
		}

		public void Delete(int id)
		{
			_connection.Execute("DELETE FROM Exercises Where ExerciseId = @Id ", new { Id = id });
		}

		public IEnumerable<ExerciseModel> GetAll()
		{
			return _connection.Query<ExerciseModel>("SELECT * FROM Exercises").ToList();
		}

		public ExerciseModel GetExerciseById(int id)
		{
			return _connection.QuerySingleOrDefault<ExerciseModel>("SELECT * FROM Exercises WHERE ExerciseId = @Id", new { Id = id });
		}

		public void Update(ExerciseModel exercise)
		{
			_connection.Execute("UPDATE Exercises SET ExerciseType = @ExerciseType, DateStart = @DateStart, DateEnd = @DateEnd, Comments = @Comments WHERE ExerciseId = @Id",
								new { ExerciseType = exercise.ExerciseType, DateStart = exercise.DateStart, DateEnd = exercise.DateEnd, Comments = exercise.Comments, Id = exercise.ExerciseId });
		}
	}
}
