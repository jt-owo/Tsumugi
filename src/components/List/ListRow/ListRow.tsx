import { FC, ReactNode } from 'react';

import styles from './ListRow.module.css';

type ListRowProps = {
	heading?: boolean;
	children: ReactNode;
};

const ListRow: FC<ListRowProps> = (props) => {
	const { children, heading } = props;

	let className = styles['list-row'];

	if (heading) className += ' ' + styles.heading;

	return <div className={className}>{children}</div>;
};

export default ListRow;
