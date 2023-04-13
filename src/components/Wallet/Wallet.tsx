import { FC, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { IWallet } from '../../types/types';

import styles from './Wallet.module.css';

type WalletProps = {
	wallet: IWallet;
};

const Wallet: FC<WalletProps> = (props) => {
	const { wallet } = props;

	const [quickSum, setQuickSum] = useState(0);

	const navigate = useNavigate();

        useEffect(() => {
			if (wallet.transactions && wallet.transactions.length > 0) {
				let sum = 0;
				wallet.transactions.forEach((t) => {
					sum += t.value;
				});

				setQuickSum(sum);
			}
		}, [wallet.transactions]);

	return (
		<div className={styles['wallet-wrapper']}>
			<div className={styles['quick-view']}>
				<div className={styles['text']}>{quickSum}</div>
			</div>
			<div
				className={styles['wallet']}
				onClick={() => {
					navigate(`/detail/${props.wallet.id}`);
				}}
			>
				<div className={styles['options']}></div>
				<div className={styles['text']}>{wallet.name}</div>
			</div>
		</div>
	);
};

export default Wallet;
