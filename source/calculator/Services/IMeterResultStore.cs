using System;
using System.Threading.Tasks;
using CalculatorFunctions.Data;

namespace CalculatorFunctions.Services
{
    public interface IMeterResultStore
    {
        Task<MeterResult> SaveResult(MeterResult result);

        Task<MeterResult> GetResult(Guid id);
    }
}
