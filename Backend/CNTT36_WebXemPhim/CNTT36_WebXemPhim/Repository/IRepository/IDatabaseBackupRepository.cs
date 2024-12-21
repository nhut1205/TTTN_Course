namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface IDatabaseBackupRepository
    {
        Task BackupDatabaseAsync(string backupPath);
        Task RestoreDatabaseAsync(string restorePath);
    }
}
