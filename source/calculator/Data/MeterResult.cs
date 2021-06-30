using System;

namespace CalculatorFunctions.Data
{
    public class MeterResult
    {
        public Guid Id { get; set; }
        public string MeterName { get; set; }
        public DateTimeOffset ReadStartDate { get; set; }
        public DateTimeOffset ReadEndDate { get; set; }
        public decimal MonthlyConsumption { get; set; }
    }
}
