import { FC, ReactNode } from 'react';

import styles from './Container.module.css';

type ContainerProps = {
	id: string;
	children: ReactNode;
	bottom?: boolean;
};

const Container: FC<ContainerProps> = (props) => {
	const { id, children, bottom } = props;

	let className = styles.container;

	if (bottom) {
		className += ` ${styles.bottom}`;
	}

	return (
		<div id={id} className={className}>
			{children}
		</div>
	);
};

export default Container;
