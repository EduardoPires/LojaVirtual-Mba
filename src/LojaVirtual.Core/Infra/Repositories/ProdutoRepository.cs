﻿using LojaVirtual.Core.Business.Entities;
using LojaVirtual.Core.Business.Interfaces;
using LojaVirtual.Core.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Core.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly LojaVirtualContext _context;

        public ProdutoRepository(LojaVirtualContext context)
        {
            _context = context;
        }
        public async Task Insert(Produto entity, CancellationToken cancellationToken)
        {
            await _context.ProdutoSet.AddAsync(entity, cancellationToken);
        }
        public Task Edit(Produto entity, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.ProdutoSet.Update(entity));
        }

        public async Task<IEnumerable<Produto>> GetAllWithCategoria(CancellationToken cancellationToken)
        {
            return await _context.ProdutoSet
                .Include(p => p.Categoria)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Produto>> GetByCategoria(Guid categoriaId, CancellationToken cancellationToken)
        {
            return await _context.ProdutoSet.Where(p => p.CategoriaId == categoriaId).ToListAsync(cancellationToken);
        }
        public async Task<Produto> GetWithCategoriaById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.ProdutoSet
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        public async Task<Produto> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await _context.ProdutoSet.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<List<Produto>> List(CancellationToken cancellationToken)
        {
            return await _context.ProdutoSet.ToListAsync(cancellationToken);
        }

        public async Task Remove(Produto produto, CancellationToken cancellationToken)
        {
            Task.FromResult(_context.ProdutoSet.Remove(produto));
        }
        public async Task<bool> Exists(string nome, CancellationToken cancellationToken)
        {
            return await _context.ProdutoSet.AnyAsync(c => c.Nome == nome, cancellationToken);
        }

        public async Task<List<Produto>> List(Guid vendedorId, CancellationToken cancellationToken)
        {
            return await _context
                .ProdutoSet
                .Where(p => p.VendedorId == vendedorId)
                .ToListAsync(cancellationToken);            
        }

        public void Dispose()
        {
            _context.Dispose();
        }        
    }
}
