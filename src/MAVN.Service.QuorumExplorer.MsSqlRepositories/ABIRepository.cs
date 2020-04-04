using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Lykke.Common.MsSql;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.Domain.Repositories;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Contexts;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories
{
    public class ABIRepository : IABIRepository
    {
        private readonly MsSqlContextFactory<QeContext> _contextFactory;
        private readonly CachedDataDictionary<string, ABI> _fetchCache;

        public ABIRepository(
            MsSqlContextFactory<QeContext> contextFactory)
        {
            _contextFactory = contextFactory;
            
            _fetchCache = new CachedDataDictionary<string, ABI>(async () =>
            {
                using (var context = _contextFactory.CreateDataContext())
                {
                    var items = await context.ABIs.ToListAsync();

                    return items
                        .Select(x => x.ToDto())
                        .ToDictionary(i => i.Abi);
                }
            });
        }
        
        public async Task SaveABIAsync(
            ABI abi)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                if (!context.ABIs.Any(x => x.Signature == abi.Signature))
                {
                    context.ABIs.Add(new ABIEntity
                    {
                        Abi = abi.Abi,
                        Name = abi.Name,
                        Signature = abi.Signature,
                        Type = abi.Type
                    });

                    await context.SaveChangesAsync();
                }
            }
        }

        public Task<IEnumerable<ABI>> GetAsync()
        {
            return _fetchCache.Values();
        }
    }
}
