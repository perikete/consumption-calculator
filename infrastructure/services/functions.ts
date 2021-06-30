import * as pulumi from "@pulumi/pulumi";
import * as azure from "@pulumi/azure";

export const setupFunctions = (
	resourceGroupName: pulumi.Input<string>,
	blobContainerConnectionString: pulumi.Input<string>
): azure.appservice.ArchiveFunctionApp => {
	const dotnetApp = new azure.appservice.ArchiveFunctionApp("http-dotnet", {
		resourceGroupName: resourceGroupName,
		archive: new pulumi.asset.FileArchive("../source/calculator/build"),
		appSettings: {
			runtime: ".NET",
			AzureStorage: blobContainerConnectionString
		}
	});

	return dotnetApp;
};
