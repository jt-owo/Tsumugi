import { FC, ReactNode } from 'react';

import styles from './ListColumn.module.css';

type ListColumnProps = {
	children: ReactNode;
};

const ListColumn: FC<ListColumnProps> = (props) => {
	const { children } = props;

	return <div className={styles['list-column']}>{children}</div>;
};

export default ListColumn;
