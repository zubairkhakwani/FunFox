using FunFox.Business.Enums;
using FunFox.Business.Requests.Class;
using FunFox.Business.Requests.Shared;
using FunFox.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunFox.Business.RequestHandlers.Class
{
    public class GetClassesForAdminRequestHandler : IRequestHandler<GetClassesForAdminRequest, PageableResponse<GetClassesForAdminResponse>>
    {
        private readonly FunFoxDbContext dbContext;

        public GetClassesForAdminRequestHandler(FunFoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PageableResponse<GetClassesForAdminResponse>> Handle(GetClassesForAdminRequest request, CancellationToken cancellationToken)
        {
            var query = dbContext
                .Classes
                .Include(c => c.StudentClasses)
                .Include(c => c.CreatedBy)
                .ThenInclude(sc => sc.Student)
                .Where(c =>
                    (string.IsNullOrWhiteSpace(request.Keyword) || c.Title.Contains(request.Keyword) || c.Level.ToString() == request.Keyword)
                );

            var totalRecords = query.Count();

            var data = await query
            .Select(c => new GetClassesForAdminResponse
            {
                Id = c.Id,
                Level = (ClassLevel)c.Level,
                Title = c.Title,
                ClassFrom = c.ClassFrom,
                ClassSize = c.ClassSize,
                ClassTo = c.ClassTo,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy.Name,
                DetailHTML = c.DetailHTML,
                Image = c.Image,
                IsActive = c.IsActive,
                StudentCount = c.StudentClasses.Count,
            })
            .Skip((request.PageNo - 1) * request.PageSize)
            .Take(request.PageSize)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

            return new PageableResponse<GetClassesForAdminResponse>(data, request.PageNo, request.PageSize, totalRecords, totalRecords / request.PageSize);
        }
    }
}
