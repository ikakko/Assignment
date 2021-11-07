using Application.Common.Interfaces;
using Application.Common.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Command
{
    public class DeleteWebSiteInformationCommand : IRequest<Result<string>>
    {
        public int Id { get; set; }
    }
    public class DeleteWebSiteInformationCommandHandler : IRequestHandler<DeleteWebSiteInformationCommand, Result<string>>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteWebSiteInformationCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<Result<string>> Handle(DeleteWebSiteInformationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objectFromDb = await _dbContext.WebSiteInformations.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                _dbContext.WebSiteInformations.Remove(objectFromDb);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                return new Result<string> { IsSuccess = false, Object = ex.Message };
            }

            return new Result<string> { IsSuccess = true, Object = "Removed Successfully in Db" };
        }
    }
}
