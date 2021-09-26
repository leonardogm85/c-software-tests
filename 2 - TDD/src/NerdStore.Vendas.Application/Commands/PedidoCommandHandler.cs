using MediatR;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Commands
{
    public class PedidoCommandHandler : IRequestHandler<AdicionarItemPedidoCommand, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMediator _mediator;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository, IMediator mediator)
        {
            _pedidoRepository = pedidoRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            var pedidoItem = new PedidoItem(
                message.ProdutoId,
                message.ProdutoNome,
                message.Quantidade,
                message.ValorUnitario);

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteId);

            pedido.AdicionarItem(pedidoItem);

            _pedidoRepository.Adicionar(pedido);

            var evento = new PedidoItemAdicionadoEvent(
                pedido.ClienteId,
                pedido.Id,
                pedidoItem.ProdutoId,
                pedidoItem.ProdutoNome,
                pedidoItem.Quantidade,
                pedidoItem.ValorUnitario);

            // await _mediator.Publish(evento, cancellationToken);

            pedido.AdicionarEvento(evento);

            return await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
