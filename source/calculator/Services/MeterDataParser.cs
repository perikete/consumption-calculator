using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CalculatorFunctions.Data;

namespace CalculatorFunctions.Services
{
    public class MeterDataParser : IMeterDataParser
    {
        public async Task<IEnumerable<MeterData>> GetMeterData(string meterConsumptionData)
        {
            using var stringReader = new StringReader(meterConsumptionData);
            var line = await stringReader.ReadLineAsync();
            var meterDatas = new List<MeterData>();

            while (!string.IsNullOrWhiteSpace(line))
            {
                line = await stringReader.ReadLineAsync(); // skip header

                if (MeterData.TryParse(line, out var meterData))
                    meterDatas.Add(meterData);
            }

            return meterDatas;
        }
    }
}
