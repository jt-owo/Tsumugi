import { FC } from 'react';

import styles from './List.module.css';

type ListProps = {
	children: React.ReactNode;
};

const List: FC<ListProps> = (props) => {
	const { children } = props;

	return <div className={styles['list-container']}>{children}</div>;
};

export default List;
