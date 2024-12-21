namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface IPermissionRepository
    {
        Task<bool> HasPermissionAsync(string username, string resource, string action);
    }
}
