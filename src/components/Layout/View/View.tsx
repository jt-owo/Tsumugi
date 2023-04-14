import { FC, ReactNode } from 'react';

import styles from './View.module.css';

type ViewProps = {
	children: ReactNode;
};

const View: FC<ViewProps> = (props) => {
	const { children } = props;

	return <div className={styles['content-container']}>{children}</div>;
};

export default View;
