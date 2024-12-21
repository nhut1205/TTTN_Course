using CNTT36_WebXemPhim.DTO.Admin.Subscription;

namespace CNTT36_WebXemPhim.Repository.IRepository
{
    public interface ISubscriptionRepository
    {
        Task<List<SubscriptionDto>> GetSubscriptionsByMonthAsync(int month);
        Task<List<SubscriptionDto>> GetSubscriptionsWithinDateRangeAsync(DateOnly startDate, DateOnly endDate);
    }
}
