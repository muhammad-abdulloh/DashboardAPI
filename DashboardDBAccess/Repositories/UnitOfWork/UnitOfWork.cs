using System;
using System.Linq;
using DashboardDBAccess.Contracts;
using DashboardDBAccess.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DashboardDBAccess.Repositories.UnitOfWork
{
    /// <inheritdoc />
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DashboardDbContext _context;

        /// <inheritdoc />
        public void Dispose()
        {
            _context.Dispose();
        }

        
        private void ChangeModifiedAt()
        {
            var modified = _context.ChangeTracker.Entries()
                .Where(t => t.Entity is IHasModificationDate && t.State == EntityState.Modified)
                .Select(t => t.Entity)
                .ToArray();
            
            foreach (var entity in modified)
            {
                if (entity is IHasModificationDate track)
                {
                    track.ModifiedAt = DateTimeOffset.UtcNow;
                }
            }
        }

        /// <inheritdoc />
        public void Save()
        {
            ChangeModifiedAt();

            _context.SaveChanges();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(DashboardDbContext context)
        {
            _context = context;
        }
    }
}
