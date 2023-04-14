import { FC } from 'react';

import styles from './Button.module.css';

type ButtonProps = {
	children: string;
	type?: 'confirm' | 'danger';
	display?: 'right';
	hide?: boolean;
	onClick?: (e: React.MouseEvent) => void;
};

const Button: FC<ButtonProps> = (props) => {
	const { display, hide, onClick, type, children } = props;

	if (hide) return null;

	let className = styles.button;
	if (type) {
		className += ` ${styles[type]}`;
	}

	if (display) {
		className += ` ${styles.right}`;
	}

	return (
		<button className={className} onClick={onClick}>
			{children}
		</button>
	);
};

export default Button;
