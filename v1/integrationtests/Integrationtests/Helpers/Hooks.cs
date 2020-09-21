using Npgsql;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Integrationtests.Helpers
{
    [Binding]
    public class Hooks
    {
        private const string postgresConnectionString = "postgresConnectionString";

        [BeforeScenario]
        public void CleanUp() 
        {
            var cs = TestContext.Parameters[postgresConnectionString];
            using var con = new NpgsqlConnection(cs);

            con.Open();

            var sql = "TRUNCATE TABLE users RESTART IDENTITY";
            using var cmd = new NpgsqlCommand(sql, con);

            cmd.ExecuteScalar();

            con.Close();
        }
    }
}