import { FC, ReactNode } from 'react';

import styles from './ListColumn.module.css';

type ListColumnProps = {
	children: ReactNode;
    width?: number;
};

const ListColumn: FC<ListColumnProps> = (props) => {
	const { children, width } = props;

    let style = {};
    if (width) {
        style = {
            flexBasis: width + 'px'
        }
    }

	return (
		<div style={style} className={styles['list-column']}>
			{children}
		</div>
	);
};

export default ListColumn;
