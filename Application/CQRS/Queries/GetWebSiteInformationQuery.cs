using Application.Common.Interfaces;
using Application.Common.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries
{
    public class GetWebSiteInformationQuery : IRequest<object>
    {
        public int? Id { get; set; }
    }

    public class GetWebSiteInformationQueryHandler : IRequestHandler<GetWebSiteInformationQuery, object>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetWebSiteInformationQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<object> Handle(GetWebSiteInformationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id != null)
                {
                    var objectFromDb = await _dbContext.WebSiteInformations.FirstOrDefaultAsync(x => x.Id == request.Id.Value, cancellationToken);

                    return new Result<WebSiteInformation> { IsSuccess = true, Object = objectFromDb };
                }

                var WebSiteInformations = await _dbContext.WebSiteInformations.ToListAsync(cancellationToken);

                return new Result<List<WebSiteInformation>> { IsSuccess = true, Object = WebSiteInformations };
            }
            catch (Exception ex)
            {
                return new Result<string> { IsSuccess = false, Object = ex.Message };
            }
        }
    }
}
