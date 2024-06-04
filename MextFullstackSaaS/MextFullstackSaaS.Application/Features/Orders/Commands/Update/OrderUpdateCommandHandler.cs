using MediatR;
using MextFullstackSaaS.Application.Common.Interfaces;
using MextFullstackSaaS.Application.Common.Models;
using MextFullstackSaaS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MextFullstackSaaS.Application.Features.Orders.Commands.Update
{
    public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommand, ResponseDto<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public OrderUpdateCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<ResponseDto<Guid>> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (existingOrder == null || existingOrder.UserId != _currentUserService.UserId)
            {
                return new ResponseDto<Guid>(Guid.Empty, "Order not found or access denied.");
            }

            var order = OrderUpdateCommand.MapToOrder(request, existingOrder);

            _dbContext.Orders.Update(order);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto<Guid>(order.Id, "Your order updated successfully.");
        }
    }
}
