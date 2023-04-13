import { FC } from 'react';

import styles from './Button.module.css';

type ButtonProps = {
	children: string;
	type?: 'confirm' | 'danger';
	onClick?: (e: React.MouseEvent) => void;
};

const Button: FC<ButtonProps> = (props) => {
	const { onClick, type, children } = props;

	let className = styles.button;
	if (type) {
		className = styles[type];
	}

	return (
		<button className={className} onClick={onClick}>
			{children}
		</button>
	);
};

export default Button;
