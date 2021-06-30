using System.Collections.Generic;
using System.Linq;
using CalculatorFunctions.Data;

namespace CalculatorFunctions
{
    public static class MeterDataExtensions
    {
        public static MeterResult ToMeterResult(this IEnumerable<MeterData> meterDatas)
        {
            return new MeterResult
            {
                MeterName = meterDatas.First().MeterName,
                ReadEndDate = meterDatas.Max(o => o.ReadDate),
                ReadStartDate = meterDatas.Min(o => o.ReadDate),
                MonthlyConsumption = meterDatas.Sum(o => o.Total)
            };
        }
    }
}
