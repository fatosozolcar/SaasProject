using MediatR;
using MextFullstackSaaS.Application.Common.Models;
using MextFullstackSaaS.Application.Features.Orders.Commands.Add;
using MextFullstackSaaS.Domain.Entities;
using MextFullstackSaaS.Domain.Enums;

namespace MextFullstackSaaS.Application.Features.Orders.Commands.Update
{
    public class OrderUpdateCommand : IRequest<ResponseDto<Guid>>
    {
         public Guid Id { get; set; }
        public string IconDescription { get; set; }
        public string ColourCode { get; set; }
        public AIModelType Model { get; set; }
        public DesignType DesignType { get; set; }
        public IconSize Size { get; set; }
        public IconShape Shape { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; } // Added UpdatedOn property
        public Guid UserId { get; set; }
        public string CreatedByUserId { get; set; }

         public static Order MapToOrder(OrderUpdateCommand request, OrderAddCommand orderAddCommand)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                IconDescription = orderAddCommand.IconDescription,
                ColourCode = orderAddCommand.ColourCode,
                Model = orderAddCommand.Model,
                DesignType = orderAddCommand.DesignType,
                Size = orderAddCommand.Size,
                Shape = orderAddCommand.Shape,
                Quantity = orderAddCommand.Quantity,
                CreatedOn = DateTimeOffset.UtcNow,
            };
        }

        internal static Order MapToOrder(OrderUpdateCommand request, Order existingOrder)
        {
            throw new NotImplementedException();
        }
    }
}
