using FunFox.Business.Enums;
using FunFox.Business.Requests.Class;
using FunFox.Business.Requests.Shared;
using FunFox.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunFox.Business.RequestHandlers.Class
{
    public class GetClassesRequestHandler : IRequestHandler<GetClassesRequest, PageableResponse<GetClassesResponse>>
    {
        private readonly FunFoxDbContext dbContext;

        public GetClassesRequestHandler(FunFoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PageableResponse<GetClassesResponse>> Handle(GetClassesRequest request, CancellationToken cancellationToken)
        {
            var query = dbContext
                .Classes
                .Include(c => c.StudentClasses)
                .Include(c => c.CreatedBy)
                .ThenInclude(sc => sc.Student)
                .Where(c =>
                    c.IsActive
                    &&
                    (request.Id == null || c.Id == request.Id.Value)
                    &&
                    (string.IsNullOrWhiteSpace(request.Keyword) || c.Title.Contains(request.Keyword) || c.Level.ToString() == request.Keyword)
                );

            var totalRecords = query.Count();

            var data = await query
            .Select(c => new GetClassesResponse
            {
                Id = c.Id,
                Level = (ClassLevel)c.Level,
                Title = c.Title,
                ClassFrom = c.ClassFrom,
                ClassSize = c.ClassSize,
                ClassTo = c.ClassTo,
                CreatedAt = c.CreatedAt,
                DetailHTML = c.DetailHTML,
                Image = c.Image,
                StudentCount = c.StudentClasses.Count,
                AlreadyEnrolled = request.StudentId == null ? false : c.StudentClasses.Any(sc => sc.StudentId == request.StudentId)
            })
            .Skip((request.PageNo - 1) * request.PageSize)
            .Take(request.PageSize)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

            return new PageableResponse<GetClassesResponse>(data, request.PageNo, request.PageSize, totalRecords, totalRecords / request.PageSize);
        }
    }
}
