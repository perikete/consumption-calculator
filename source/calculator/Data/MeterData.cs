using System;
using System.Linq;

namespace CalculatorFunctions.Data
{
    public class MeterData
    {
        public string MeterName { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset ReadDate { get; set; }

        public static bool TryParse(string data, out MeterData meterData)
        {
            meterData = null;
            if (string.IsNullOrWhiteSpace(data))
                return false;

            try
            {
                var columns = data.Split(",");
                var total = columns.Skip(2).Sum(v => Convert.ToUInt32(v));

                if (!DateTimeOffset.TryParse(columns[1], out var readDate)) return false;

                meterData = new MeterData {MeterName = columns[0], Total = total, ReadDate = readDate};

                return true;
            }
            catch (Exception) // TODO: Lot of error checking/validation missing here...
            {
                return false;
            }
        }
    }
}
