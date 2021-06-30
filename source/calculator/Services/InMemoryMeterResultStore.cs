using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CalculatorFunctions.Data;
using Microsoft.Extensions.Logging;

namespace CalculatorFunctions.Services
{
    public class InMemoryMeterResultStore : IMeterResultStore
    {
        private readonly ILogger<InMemoryMeterResultStore> _logger;
        private readonly IDictionary<Guid, MeterResult> _meterResults;

        public InMemoryMeterResultStore(ILogger<InMemoryMeterResultStore> logger)
        {
            _logger = logger;
            _meterResults = new Dictionary<Guid, MeterResult>();
        }

        public async Task<MeterResult> SaveResult(MeterResult result)
        {
            result.Id = Guid.NewGuid();
            _meterResults.Add(result.Id, result);

            return await Task.FromResult(result);
        }

        public async Task<MeterResult> GetResult(Guid id)
        {
            if (_meterResults.TryGetValue(id, out var result))
                return await Task.FromResult(result);

            return null;
        }
    }
}
