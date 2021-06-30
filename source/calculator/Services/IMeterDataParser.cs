using System.Collections.Generic;
using System.Threading.Tasks;
using CalculatorFunctions.Data;

namespace CalculatorFunctions.Services
{
    public interface IMeterDataParser
    {
        Task<IEnumerable<MeterData>> GetMeterData(string meterConsumptionData);
    }
}