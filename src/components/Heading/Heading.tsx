import { FC } from 'react';

import styles from './Heading.module.css';

type HeadingProps = {
	level?: number;
	center?: boolean;
	children: string;
};

const Heading: FC<HeadingProps> = (props) => {
	const { level, center, children } = props;

	let className = styles.heading;

	if (level && level > 0) {
		className += ' ' + styles['level-' + level.toString()];
	}

	if (center) {
		className += ' ' + styles.center;
	}

	return <div className={className}>{children}</div>;
};

export default Heading;
