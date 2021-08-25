using System;
using System.Collections.Generic;

namespace Features.Core
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        IEnumerable<TEntity> ObterTodos();
        void Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        void Remover(Guid id);
    }
}
