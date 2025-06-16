using Dapper;
using System.Data;
using System.Text;
using TestWorkPhysicon.DataBase.Models;

namespace TestWorkPhysicon.DataBase.Repositories
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<Course>> GetCourses(string subject,
            string grade,
            string genre,
            IDbTransaction transaction = null,
            CancellationToken token = default);
    }
    public class CoursesRepository : ICoursesRepository
    {
        private readonly IDBContext _context;
        public CoursesRepository(IDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Course>> GetCourses(string subject,
            string grade,
            string genre,
            IDbTransaction transaction = null,
            CancellationToken token = default)
        {
            using (IDbConnection db = transaction?.Connection ?? _context.CreateConnection())
            {
                var sql = new StringBuilder("SELECT * FROM Courses WHERE 1=1");

                var parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(subject))
                {
                    sql.Append(" AND Subject like @Subject");
                    parameters.Add("Subject", $"%{subject}%");
                }

                if (!string.IsNullOrEmpty(grade))
                {
                    sql.Append(" AND Grade like @Grade");
                    parameters.Add("Grade", $"%{grade}%");
                }

                if (!string.IsNullOrEmpty(genre))
                {
                    sql.Append(" AND Genre like @Genre");
                    parameters.Add("Genre", $"%{genre}%");
                }

                var command = new CommandDefinition(sql.ToString(),
                    parameters,
                    transaction: transaction,
                    cancellationToken: token);

                return await db.QueryAsync<Course>(command);
            }
        }
    }
}
