using Application.Common.Interfaces;
using Application.Common.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Command
{
    public class AddWebSiteInformationCommand : IRequest<Result<string>>
    {
        public WebSiteInformation  WebInfo { get; set; }
    }

    public class AddWebSiteInformationCommandHandler : IRequestHandler<AddWebSiteInformationCommand, Result<string>>
    {
        private readonly IApplicationDbContext _dbContext;

        public AddWebSiteInformationCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Result<string>> Handle(AddWebSiteInformationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _dbContext.WebSiteInformations.AddAsync(request.WebInfo, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return new Result<string> { IsSuccess = false, Object = ex.Message };
            }

            return new Result<string> { IsSuccess = true, Object = "Added Successfully in Db"};
        }
    }
}
