import * as pulumi from "@pulumi/pulumi";
import * as resources from "@pulumi/azure-native/resources";
import * as storage from "@pulumi/azure-native/storage";
import * as services from "./services";
import * as azure from "@pulumi/azure";

// Create an Azure Resource Group
const resourceGroup = new resources.ResourceGroup("consumption-calculator-rg");

// Create an Azure resource (Storage Account)
const storageAccount = new storage.StorageAccount("sa", {
	resourceGroupName: resourceGroup.name,
	sku: {
		name: storage.SkuName.Standard_LRS
	},
	kind: storage.Kind.StorageV2
});

// Export the primary key of the Storage Account
const storageAccountKeys = pulumi
	.all([resourceGroup.name, storageAccount.name])
	.apply(([resourceGroupName, accountName]) =>
		storage.listStorageAccountKeys({ resourceGroupName, accountName })
	);
export const primaryStorageKey = storageAccountKeys.keys[0].value;

const storageAccountInfo = pulumi
	.all([storageAccount.name, resourceGroup.name])
	.apply(([name, resourceGroupName]) =>
		azure.storage.getAccount({
			name: name,
			resourceGroupName: resourceGroupName
		})
	);

const registeredServices = services.setupConsumptionServices({
	resourceGroupName: resourceGroup.name,
	storageConfig: { storageAccountName: storageAccount.name },
	functionConfig: {
		storagePrimaryConnectionString:
			storageAccountInfo.primaryConnectionString
	}
});

export const storageAccountConnectionString =
	storageAccountInfo.primaryConnectionString.apply((c) => c);

export const functionEndpoint =
	registeredServices.calculateFunction.functionApp.defaultHostname.apply(
		(ep) => `https://${ep}/api/Calculate`
	);
