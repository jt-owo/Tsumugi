import { FC, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../../state/hooks';
import { addWallet } from '../../state/slices/walletSlice';

import View from '../../components/View/View';
import Wallet from '../../components/Wallet/Wallet';
import Modal from '../../components/Modal/Modal';

import styles from './Dashboard.module.css';
import modalStyle from '../../components/Modal/Modal.module.css';

const Dashboard: FC = () => {
	const wallets = useAppSelector((state) => state.wallets.data);

	const dispatch = useAppDispatch();

	const [isOpen, setOpen] = useState(false);
	const [walletName, setWalletName] = useState('');

	const handleAddWallet = () => {
        if (walletName === '') return;

		setOpen(false);
		dispatch(addWallet(walletName));
		setWalletName('');
	};

	return (
		<View>
			<div>
				<h1>Wallets</h1>
				<div className={styles['wallet-grid']}>
					{wallets && wallets.map((wallet) => <Wallet key={wallet.id} wallet={wallet} />)}
					<div className={styles['wallet-template']}>
						<div className={styles.circle + ' ' + styles.plus} onClick={() => { setOpen(true); } }></div>
					</div>
				</div>
			</div>
			<Modal isOpen={isOpen} onClose={() => setOpen(false)}>
				<span className={modalStyle['modal-heading']}>Wallet Name</span>
				<input type="text" value={walletName} onChange={(e) => setWalletName(e.currentTarget.value)} />
				<div className={modalStyle['modal-buttons-container']}>
					<button onClick={handleAddWallet}>Add</button>
					<button onClick={() => setOpen(false)} className={modalStyle.danger}>
						Cancel
					</button>
				</div>
			</Modal>
		</View>
	);
};

export default Dashboard;
