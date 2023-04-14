import { FC, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { IWallet } from '../../../types/types';

import Number from '../../../components/Number/Number';

import styles from './Wallet.module.css';

type WalletProps = {
	wallet: IWallet;
};

const Wallet: FC<WalletProps> = (props) => {
	const { wallet } = props;

	const [quickSum, setQuickSum] = useState(0);

	const navigate = useNavigate();

	const handleOnClick = () => navigate(`/detail/${props.wallet.id}`);

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
				<div className={styles['text']}>
					<Number value={quickSum} />
				</div>
			</div>
			<div className={styles['wallet']} onClick={handleOnClick}>
				<div className={styles['options']}></div>
				<div className={styles['text']}>{wallet.name}</div>
			</div>
		</div>
	);
};

export default Wallet;
