using Application.Common.Interfaces;
using Application.Common.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Queries
{
    public class GetWebsiteInformationAtSpecificTimeQuery : IRequest<Result<WebSiteInformation>>
    {
        public string Url { get; set; }
        public DateTime Date{ get; set; }
    }
    public class GetWebsiteInformationAtSpecificTimeQueryHandler : IRequestHandler<GetWebsiteInformationAtSpecificTimeQuery, Result<WebSiteInformation>>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetWebsiteInformationAtSpecificTimeQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<Result<WebSiteInformation>> Handle(GetWebsiteInformationAtSpecificTimeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var webSiteInfo = await _dbContext.WebSiteInformations.FirstOrDefaultAsync(x => x.Url.Equals(request.Url) && x.Date == request.Date);

                return new Result<WebSiteInformation> { IsSuccess = true, Object = webSiteInfo };
            }
            catch (Exception)
            {
                return new Result<WebSiteInformation> { IsSuccess = false };
            }
        }
    }
}
