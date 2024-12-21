using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CNTT36_WebXemPhim.Repository
{
    public class DatabaseBackupRepository : IDatabaseBackupRepository
    {
        private readonly string _connectionString;

        public DatabaseBackupRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public async Task BackupDatabaseAsync(string backupPath)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("BackupDatabase", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BackupPath", backupPath);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task RestoreDatabaseAsync(string restorePath)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Chuyển kết nối sang master database trước khi thực hiện restore
                using (var command = new SqlCommand("USE master", connection))
                {
                    await command.ExecuteNonQueryAsync();
                }

                using (var command = new SqlCommand($"ALTER DATABASE [SellCourse] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", connection))
                {
                    await command.ExecuteNonQueryAsync();
                }

                using (var command = new SqlCommand("RESTORE DATABASE [SellCourse] FROM DISK = @RestorePath WITH REPLACE", connection))
                {
                    command.Parameters.AddWithValue("@RestorePath", restorePath);
                    await command.ExecuteNonQueryAsync();
                }

                using (var command = new SqlCommand("ALTER DATABASE [SellCourse] SET MULTI_USER", connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}