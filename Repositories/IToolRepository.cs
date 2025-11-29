using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using jcAP.API.Objects.Tools;

namespace jcAP.API.Repositories
{
    public interface IToolRepository
    {
        Task<IEnumerable<ToolDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ToolDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ToolDto> CreateAsync(ToolDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, ToolDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}