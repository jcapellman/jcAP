using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using jcAP.API.Entities;
using jcAP.API.Objects.Tools;
using jcAP.API.Repositories.Common;

namespace jcAP.API.Repositories
{
    public interface IToolRepository : IRepository<ToolEntity>
    {
        Task<IEnumerable<ToolDto>> GetAllDtosAsync(CancellationToken cancellationToken = default);
        Task<ToolDto?> GetDtoByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ToolDto> CreateFromDtoAsync(ToolDto dto, CancellationToken cancellationToken = default);
        Task UpdateFromDtoAsync(Guid id, ToolDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}