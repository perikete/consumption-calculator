import * as dataStorage from "./dataStorage";
import * as functions from "./functions";
import { ServicesConfig } from "./servicesConfig";
import * as azure from "@pulumi/azure";
import * as storage from "@pulumi/azure-native/storage";

export type RequiredServices = {
	calculateFunction: azure.appservice.ArchiveFunctionApp;
	blobContainer: storage.BlobContainer;
};

export const setupConsumptionServices = (
	servicesConfig: ServicesConfig
): RequiredServices => {
	const blobContainer = dataStorage.setupDataStorage(
		servicesConfig.resourceGroupName,
		servicesConfig.storageConfig.storageAccountName
	);

	const calculateFunction = functions.setupFunctions(
		servicesConfig.resourceGroupName,
		servicesConfig.functionConfig.storagePrimaryConnectionString
	);

	return { calculateFunction, blobContainer };
};
