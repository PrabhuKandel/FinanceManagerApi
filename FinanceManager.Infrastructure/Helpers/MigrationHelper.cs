

using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

namespace FinanceManager.Infrastructure.Helpers
{
    public static  class MigrationHelper
    {
        /// <summary>
        /// Runs an embedded SQL script as part of a migration.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder.</param>
        /// <param name="resourceName">The full embedded resource name (e.g. "EmployeeCRUD.Infrastructure.Scripts.MyScript.sql").</param>
        /// <param name="assembly">Optional assembly. Defaults to the assembly where MigrationHelper is defined.</param>
        public static void RunSqlScript(MigrationBuilder migrationBuilder, string resourceName, Assembly? assembly = null)
        {
            assembly ??= Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new InvalidOperationException($"Embedded resource '{resourceName}' not found in assembly '{assembly.FullName}'.");
            }
            using var reader = new StreamReader(stream);
            var sql = reader.ReadToEnd();
            migrationBuilder.Sql(sql);  
        }
    }
}
