using FunFox.Business.Enums;
using FunFox.Business.Requests.Class;
using FunFox.Business.Requests.Shared;
using FunFox.Business.Requests.Students;
using FunFox.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunFox.Business.RequestHandlers.Students
{
    public class GetStudentsRequestHandler : IRequestHandler<GetStudentsRequest, PageableResponse<GetStudentsResponse>>
    {
        private readonly FunFoxDbContext dbContext;

        public GetStudentsRequestHandler(FunFoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<PageableResponse<GetStudentsResponse>> Handle(GetStudentsRequest request, CancellationToken cancellationToken)
        {
            var query = dbContext
                .Students
                .Include(s => s.User)
                .Where(s =>
                    (request.Id == null || s.Id == request.Id.Value)
                    &&
                    (request.ClassLevel == null || s.Level == (int)request.ClassLevel)
                );

            var totalRecords = query.Count();

            var data = await query
                .Select(s => new GetStudentsResponse
                {
                    Id = s.Id,
                    Level = (ClassLevel)s.Level,
                    UserId = s.User.Id,
                    Name = s.User.Name,
                    Email = s.User.Email
                })
                .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return new PageableResponse<GetStudentsResponse>(data, request.PageNo, request.PageSize, totalRecords, totalRecords / request.PageSize);
        }
    }
}
