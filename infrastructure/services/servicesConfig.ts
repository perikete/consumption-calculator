import * as pulumi from "@pulumi/pulumi";

export type ServicesConfig = {
	resourceGroupName: pulumi.Input<string>;
	storageConfig: ServiceStorageConfig;
	functionConfig: ServiceFunctionConfig;
};

export type ServiceStorageConfig = {
	storageAccountName: pulumi.Input<string>;
};

export type ServiceFunctionConfig = {
	storagePrimaryConnectionString: pulumi.Input<string>;
};