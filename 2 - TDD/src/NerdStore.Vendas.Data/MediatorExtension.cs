using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Data
{
    public static class MediatorExtension
    {
        public static async Task PublicarEventos(this IMediatorHandler mediator, VendasContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(e => (e.Entity.Notificacoes?.Any()).GetValueOrDefault())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(e => e.Entity.Notificacoes)
                .ToList();

            domainEntities
                .ForEach(e => e.Entity.LimparEventos());

            var tasks = domainEvents
                .Select(async domainEvent => await mediator.PublicarEvento(domainEvent));

            await Task.WhenAll(tasks);
        }
    }
}
