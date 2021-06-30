using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CalculatorFunctions.Data;
using CalculatorFunctions.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CalculatorFunctions
{
    public class Consumption
    {
        private readonly IMeterResultStore _meterResultStore;
        private readonly IMeterDataParser _meterDataParser;
        private readonly ILogger _logger;

        private const int
            FilesToProcessBatchSize = 2; // TODO: This could be improved and moved to a configuration setting.

        public Consumption(IMeterResultStore meterResultStore, IMeterDataParser meterDataParser,
            ILogger<Consumption> logger)
        {
            _meterResultStore = meterResultStore;
            _meterDataParser = meterDataParser;
            _logger = logger;
        }

        [FunctionName("Calculate")]
        public async Task<IActionResult> Run(
            [Blob("consumption-data", FileAccess.ReadWrite, Connection = "AzureStorage")] BlobContainerClient container,
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
            HttpRequest req)
        {
            var blobPages = container.GetBlobsAsync(BlobTraits.Metadata, prefix: "consumption")
                .AsPages(default, FilesToProcessBatchSize);

            var results = new List<MeterResult>();


            await foreach (var blobPage in blobPages)
            {
                foreach (var blob in blobPage.Values)
                {
                    _logger.LogInformation($"Processing file: {blob.Name}");

                    var blobClient = container.GetBlobClient(blob.Name);
                    var blobResponse = await blobClient.DownloadAsync();
                    using var streamReader = new StreamReader(blobResponse.Value.Content);
                    var blobData = await streamReader.ReadToEndAsync();
                    var meterDatas = await _meterDataParser.GetMeterData(blobData);

                    if (meterDatas.Any())
                    {
                        var meterResult = meterDatas.ToMeterResult();
                        results.Add(meterResult);
                        await _meterResultStore.SaveResult(meterResult);
                    }

                    _logger.LogInformation($"Finished processing file: {blob.Name}");
                }
            }

            return new OkObjectResult(results);
        }
    }
}

