using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Common.MsSql;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.Domain.Repositories;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Contexts;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities;
using Nethereum.Util;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories
{
    public class FunctionCallRepository : IFunctionCallRepository
    {
        private readonly MsSqlContextFactory<QeContext> _contextFactory;

        public FunctionCallRepository(
            MsSqlContextFactory<QeContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task SaveFunctionCalls(
            IReadOnlyCollection<FunctionCall> functionCalls)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var hasher = Sha3Keccack.Current;
                
                var functionCallEntities = functionCalls.Select(x => new FunctionCallEntity
                {
                    FunctionName = x.FunctionName,
                    FunctionSignature = x.FunctionSignature,
                    ParametersJson = x.ParametersJson,
                    TransactionHash = x.TransactionHash
                });

                var functionCallParameterEntities = functionCalls
                    .SelectMany(x => x.Parameters, (x, y) => (FunctionCall: x, Parameter: y))
                    .Select(x => new FunctionCallParameterEntity
                    {
                        TransactionHash = x.FunctionCall.TransactionHash,
                        ParameterName = x.Parameter.ParameterName,
                        ParameterOrder = x.Parameter.ParameterOrder,
                        ParameterType = x.Parameter.ParameterType,
                        ParameterValue = x.Parameter.ParameterValue,
                        ParameterValueHash = hasher.CalculateHash(x.Parameter.ParameterValue)
                    });

                context.FunctionCalls.AddRange(functionCallEntities);
                context.FunctionCallParameters.AddRange(functionCallParameterEntities);

                await context.SaveChangesAsync();
            }
        }
    }
}
