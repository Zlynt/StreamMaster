import SMButton from "@components/sm/SMButton";
import SMPopUp from "@components/sm/SMPopUp";
import { useIsTrue } from "@lib/redux/hooks/isTrue";
import { useQueryFilter } from "@lib/redux/hooks/queryFilter";
import { useSelectAll } from "@lib/redux/hooks/selectAll";
import { useSelectedItems } from "@lib/redux/hooks/selectedItems";
import {
	AutoSetEPG,
	AutoSetEPGFromParameters,
} from "@lib/smAPI/SMChannels/SMChannelsCommands";
import {
	AutoSetEPGFromParametersRequest,
	AutoSetEPGRequest,
	SMChannelDto,
} from "@lib/smAPI/smapiTypes";
import React, { useMemo } from "react";

interface AutoSetEPGSMChannelDialogProperties {
	readonly smChannel?: SMChannelDto;
	readonly menu?: boolean;
}

const AutoSetEPGSMChannelDialog = ({
	menu,
	smChannel,
}: AutoSetEPGSMChannelDialogProperties) => {
	const selectedItemsKey = "selectSelectedSMChannelDtoItems";
	const { queryFilter } = useQueryFilter("streameditor-SMChannelDataSelector");
	const { selectedItems } = useSelectedItems<SMChannelDto>(selectedItemsKey);
	const { selectAll } = useSelectAll("streameditor-SMChannelDataSelector");
	const { isTrue: smTableIsSimple } = useIsTrue("isSimple");

	const ReturnToParent = React.useCallback(() => {}, []);
	const getTotalCount = useMemo(
		() => selectedItems?.length ?? 0,
		[selectedItems],
	);

	const save = React.useCallback(async () => {
		if (
			selectedItems === undefined &&
			smChannel === undefined &&
			selectAll === true
		) {
			ReturnToParent();
			return;
		}

		if (selectAll === true) {
			if (!queryFilter) {
				ReturnToParent();
				return;
			}

			const request = {} as AutoSetEPGFromParametersRequest;
			request.Parameters = queryFilter;

			AutoSetEPGFromParameters(request)
				.then(() => {})
				.catch((error) => {
					console.error(error);
				})
				.finally(() => {
					ReturnToParent();
				});

			return;
		}

		if (selectedItems.length === 0 && smChannel === undefined) {
			ReturnToParent();
			return;
		}

		const request = {
			Ids:
				smChannel === undefined
					? selectedItems.map((item) => item.Id)
					: [smChannel.Id],
		} as AutoSetEPGRequest;

		await AutoSetEPG(request)
			.then(() => {})
			.catch((error) => {
				console.error("Set EPG Error: ", error.message);
				throw error;
			});
	}, [ReturnToParent, queryFilter, selectAll, selectedItems, smChannel]);

	if (menu === true) {
		return (
			<SMPopUp
				buttonClassName="icon-blue"
				icon="pi-sparkles"
				iconFilled
				info=""
				label={`Auto Set (${selectAll ? "All" : getTotalCount}) EPG`}
				menu
				modal
				onOkClick={async () => save()}
				placement={smTableIsSimple ? "bottom-end" : "bottom"}
				title="Auto Set EPG"
				tooltip="Auto Set EPG"
			>
				<div className="text-container sm-center-stuff">
					Auto Set ({selectAll ? "All" : getTotalCount}) channels?
				</div>
			</SMPopUp>
		);
	}

	return (
		<SMButton
			icon="pi-sparkles"
			buttonClassName="icon-blue"
			onClick={async () => save()}
			tooltip="Auto Set EPG"
		/>
	);
};

AutoSetEPGSMChannelDialog.displayName = "AutoSetEPGSMChannelDialog";

export default React.memo(AutoSetEPGSMChannelDialog);
