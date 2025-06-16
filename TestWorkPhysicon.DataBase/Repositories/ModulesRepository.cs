using Dapper;
using System.Data;
using TestWorkPhysicon.DataBase.Models;

namespace TestWorkPhysicon.DataBase.Repositories
{
    public interface IModulesRepository
    {
        Task<IEnumerable<Module>> GetModules(IEnumerable<int>? coursesIds, IDbTransaction transaction = null, CancellationToken token = default);
    }
    public class ModulesRepository : IModulesRepository
    {
        private readonly IDBContext _context;
        public ModulesRepository(IDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Module>> GetModules(IEnumerable<int>? coursesIds, IDbTransaction transaction = null, CancellationToken token = default)
        {
            using (IDbConnection db = transaction?.Connection ?? _context.CreateConnection())
            {
                var sql = "SELECT * FROM Modules";
                var parameters = new DynamicParameters();

                if (coursesIds != null && coursesIds.Count() > 0)
                {
                    sql += " WHERE CourseId IN @Ids";
                    parameters.Add("Ids", coursesIds);
                }

                var command = new CommandDefinition(
                    commandText: sql,
                    parameters: parameters,
                    transaction: transaction,
                    cancellationToken: token);

                return await db.QueryAsync<Module>(command);
            }
        }
    }
}
