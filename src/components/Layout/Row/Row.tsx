import { FC, ReactNode } from 'react';

import styles from './Row.module.css';

type RowProps = {
	children: ReactNode;
};

const Row: FC<RowProps> = (props) => {
	const { children } = props;

	return <div className={styles.row}>{children}</div>;
};

export default Row;
