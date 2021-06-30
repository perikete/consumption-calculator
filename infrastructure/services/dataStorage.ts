import * as storage from "@pulumi/azure-native/storage";
import * as pulumi from "@pulumi/pulumi";

export const setupDataStorage = (
	resourceGroupName: pulumi.Input<string>,
	accountName: pulumi.Input<string>
): storage.BlobContainer => {
	const blobContainer = new storage.BlobContainer("consumption-data", {
		accountName,
		resourceGroupName
	});

	for (let i = 1; i < 6; i++) {
		// Upload sample csv file
		new storage.Blob(`consumption-${i}.csv`, {
			resourceGroupName: resourceGroupName,
			accountName: accountName,
			containerName: blobContainer.name,
			source: new pulumi.asset.FileAsset(
				`./services/storage-initial-data/consumption-${i}.csv`
			),
			contentType: "text/html"
		});
	}

	return blobContainer;
};
