using Application.Common.Interfaces;
using Application.Common.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Command
{
    public class EditWebSiteInformationCommand : IRequest<Result<WebSiteInformation>>
    {
        public int Id { get; set; }
        public WebSiteInformation WebInfo { get; set; }
    }
    public class EditWebSiteInformationCommandHandler : IRequestHandler<EditWebSiteInformationCommand, Result<WebSiteInformation>>
    {
        private readonly IApplicationDbContext _dbContext;

        public EditWebSiteInformationCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Result<WebSiteInformation>> Handle(EditWebSiteInformationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objectFromDb = await _dbContext.WebSiteInformations.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                objectFromDb.Date = request.WebInfo.Date;
                objectFromDb.IsWebSiteUp = request.WebInfo.IsWebSiteUp;
                objectFromDb.Url = request.WebInfo.Url;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                return new Result<WebSiteInformation> { IsSuccess = false };
            }

            return new Result<WebSiteInformation> { IsSuccess = true, Object = request.WebInfo };
        }
    }
}
