import { FC } from 'react';

import styles from './ListColumn.module.css';

type ListCheckboxColumnProps = {
    id: string;
    onChange: (id: string) => void;
};

const ListCheckboxColumn: FC<ListCheckboxColumnProps> = (props) => {
	const { id, onChange } = props;

	return (
		<div className={styles['list-checkbox-column']}>
			<input onChange={() => onChange(id)} type="checkbox" />
		</div>
	);
};

export default ListCheckboxColumn;
