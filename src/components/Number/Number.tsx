import { FC } from 'react';

import styles from './Number.module.css';

type NumberProps = {
	value: number;
};

const Number: FC<NumberProps> = (props) => {
	const { value } = props;

	const isNegative = (number: number) => {
		return number.toString().startsWith('-');
	};

	const className = isNegative(value) ? styles.negative : styles.positive;

	return <span className={className}>{value}</span>;
};

export default Number;
