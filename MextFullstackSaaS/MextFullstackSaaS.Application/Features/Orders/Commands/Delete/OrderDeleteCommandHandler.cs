using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MextFullstackSaaS.Application.Features.Orders.Commands.Delete
{
    public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommand, ResponseDto<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public OrderDeleteCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<ResponseDto<Guid>> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (existingOrder == null || existingOrder.UserId != _currentUserService.UserId)
            {
                return new ResponseDto<Guid>(Guid.Empty, "Order not found or access denied.");
            }

            _dbContext.Orders.Remove(existingOrder);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto<Guid>(existingOrder.Id, "Your order has been successfully deleted.");
        }
        

        
    }
}
