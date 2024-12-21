using AutoMapper;
using CNTT36_WebXemPhim.DTO.Admin.Subscription;
using CNTT36_WebXemPhim.Models;
using CNTT36_WebXemPhim.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CNTT36_WebXemPhim.Repository
{
    public class SubscriptionRepository: ISubscriptionRepository
    {
        private readonly SellCourseContext dbContext;
        private readonly IMapper mapper;

        public SubscriptionRepository(SellCourseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<List<SubscriptionDto>> GetSubscriptionsByMonthAsync(int month)
        {
            List<Subscription> subscriptions = await dbContext.Subscriptions.Where(s => s.StartDate.Month == month).ToListAsync();
            return mapper.Map<List<SubscriptionDto>>(subscriptions);
        }

        public async Task<List<SubscriptionDto>> GetSubscriptionsWithinDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            var subscriptions = await dbContext.Subscriptions
                .Where(s => s.StartDate >= startDate && s.StartDate <= endDate) // Lọc theo điều kiện
                .ToListAsync();

            // Sử dụng AutoMapper để chuyển đổi sang DTO
            return mapper.Map<List<SubscriptionDto>>(subscriptions);
        }
    }
}
