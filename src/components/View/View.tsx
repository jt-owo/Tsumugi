import { FC } from "react";

import styles from './View.module.css';

type ViewProps = {
    children?: JSX.Element | JSX.Element[];
}

const View: FC<ViewProps> = (props) => {
	const { children } = props;

	return <div className={styles['content-container']}>{children}</div>;
};

export default View;