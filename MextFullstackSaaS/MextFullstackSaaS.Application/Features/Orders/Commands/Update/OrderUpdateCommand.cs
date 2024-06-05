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

         public static Order MapToOrder(OrderUpdateCommand orderUpdateCommand, Order oldOrder)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                IconDescription = orderUpdateCommand.IconDescription,
                ColourCode = orderUpdateCommand.ColourCode,
                Model = orderUpdateCommand.Model,
                DesignType = orderUpdateCommand.DesignType,
                Size = orderUpdateCommand.Size,
                Shape = orderUpdateCommand.Shape,
                Quantity = orderUpdateCommand.Quantity,
                CreatedOn = DateTimeOffset.UtcNow,
            };
        }

        
    }
}
