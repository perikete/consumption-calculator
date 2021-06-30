## Sample Meter Calculation App

#### Tech stack:

* Azure Function for the Calculation and processing
* Pulumi for Infra as Code(For a quick start and local setup, [click here](https://www.pulumi.com/docs/get-started/azure/begin/))

### Folder Structure
* /Infrastructure: Contains the infrastructure as code(Pulumi)
* /source: Azure Function for the calculation and file processing(dotnet C#)

### Assumptions:
* The calculator is assuming that each CSV file contains data for a single meter and monthly readings.

### NOT DONE:
* Missing unit test/integration tests.
* Code tidiness(code comments/more refactoring).
* Calculation results are stored in memory, no hard storage.
* More automated/streamlined deploy process.

Deploy process:

 * Build Azure Function 

1)
	```cd source\calculator```
2)
	```dotnet build -o ./build```

* Deploy infrastructure
1) 
	```cd infrastructure```
2) 	```npm install #install pulumi required packages```
2) 
	```pulumi up```