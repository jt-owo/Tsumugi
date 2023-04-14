import { FC } from 'react';

import styles from './ListColumn.module.css';

const ListEmptyColumn: FC = () => {
	return <div className={styles['list-empty-column']} />;
};

export default ListEmptyColumn;
