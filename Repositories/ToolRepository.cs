using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using jcAP.API.Data;
using jcAP.API.Entities;
using jcAP.API.Objects.Tools;
using jcAP.API.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace jcAP.API.Repositories
{
    public class ToolRepository : EfRepository<ToolEntity>, IToolRepository
    {
        public ToolRepository(AppDbContext db) : base(db) { }

        public async Task<IEnumerable<ToolDto>> GetAllDtosAsync(CancellationToken cancellationToken = default)
        {
            return await _set
                .AsNoTracking()
                .Select(e => new ToolDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Version = e.Version,
                    Category = e.Category,
                    InputSchema = e.InputSchema,
                    OutputSchema = e.OutputSchema,
                    InvocationEndpoint = string.IsNullOrWhiteSpace(e.InvocationEndpoint) ? null : new Uri(e.InvocationEndpoint),
                    Status = e.Status
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<ToolDto?> GetDtoByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var e = await _set.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (e == null) return null;

            return new ToolDto
            {
                Id = e.Id,
                Name = e.Name,
                Version = e.Version,
                Category = e.Category,
                InputSchema = e.InputSchema,
                OutputSchema = e.OutputSchema,
                InvocationEndpoint = string.IsNullOrWhiteSpace(e.InvocationEndpoint) ? null : new Uri(e.InvocationEndpoint),
                Status = e.Status
            };
        }

        public async Task<ToolDto> CreateFromDtoAsync(ToolDto dto, CancellationToken cancellationToken = default)
        {
            var entity = new ToolEntity
            {
                Id = dto.Id != Guid.Empty ? dto.Id : Guid.NewGuid(),
                Name = dto.Name,
                Version = dto.Version,
                Category = dto.Category,
                InputSchema = dto.InputSchema,
                OutputSchema = dto.OutputSchema,
                InvocationEndpoint = dto.InvocationEndpoint?.ToString(),
                Status = dto.Status,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };

            await _set.AddAsync(entity, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return dto with { Id = entity.Id };
        }

        public async Task UpdateFromDtoAsync(Guid id, ToolDto dto, CancellationToken cancellationToken = default)
        {
            var entity = await _set.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (entity == null) throw new KeyNotFoundException($"Tool {id} not found");

            entity.Name = dto.Name;
            entity.Version = dto.Version;
            entity.Category = dto.Category;
            entity.InputSchema = dto.InputSchema;
            entity.OutputSchema = dto.OutputSchema;
            entity.InvocationEndpoint = dto.InvocationEndpoint?.ToString();
            entity.Status = dto.Status;
            entity.Modified = DateTime.UtcNow;

            _set.Update(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _set.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (entity == null) return;

            _set.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}