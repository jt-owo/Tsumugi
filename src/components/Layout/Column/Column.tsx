import { FC, ReactNode } from 'react';

import styles from './Column.module.css';

type ColumnProps = {
	children: ReactNode;
};

const Column: FC<ColumnProps> = (props) => {
	const { children } = props;

	return <div className={styles.column}>{children}</div>;
};

export default Column;
