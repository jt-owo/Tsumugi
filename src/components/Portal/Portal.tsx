import { FC, useLayoutEffect, useState } from 'react';
import { createPortal } from 'react-dom';

const createWrapper = (wrapperID: string): HTMLDivElement => {
	const wrapperEl = document.createElement('div');
	wrapperEl.setAttribute('id', wrapperID);
	document.body.appendChild(wrapperEl);
	return wrapperEl;
};

interface PortalProps {
	wrapperID: string;
	children: JSX.Element | JSX.Element[];
}

const Portal: FC<PortalProps> = (props) => {
	const { children, wrapperID } = props;

	const [wrapperElement, setWrapperElement] = useState<HTMLDivElement>();

	useLayoutEffect(() => {
		let element = document.getElementById(wrapperID) as HTMLDivElement;
		let systemCreated = false;
		if (!element) {
			element = createWrapper(wrapperID);
			systemCreated = true;
		}
		setWrapperElement(element);

		return () => {
			if (systemCreated && element.parentNode) {
				element.parentNode.removeChild(element);
			}
		};
	}, [wrapperID]);

	if (wrapperElement == null) return null;

	return createPortal(children, wrapperElement);
};

export default Portal;
