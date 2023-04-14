import { FC, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../../state/hooks';
import { addWallet } from '../../state/slices/walletSlice';
import { getClassList } from '../../util';
import useToggle from '../../hooks/useToggle';

import View from '../../components/Layout/View/View';
import Container from '../../components/Layout/Container/Container';

import Heading from '../../components/Heading/Heading';
import Modal from '../../components/Modal/Modal';

import Wallet from './Wallet/Wallet';

import styles from './Dashboard.module.css';
import modalStyle from '../../components/Modal/Modal.module.css';

const Dashboard: FC = () => {
	const wallets = useAppSelector((state) => state.wallets.data);

	const dispatch = useAppDispatch();

    const { value: isOpen, toggle } = useToggle();
	const [walletName, setWalletName] = useState('');

	const handleAddWallet = () => {
		if (walletName === '') return;

		toggle();
		dispatch(addWallet(walletName));
		setWalletName('');
	};

	return (
		<View>
			<Container id="dashboardContainer">
				<Heading>Wallets</Heading>
				<div id={styles.walletGrid}>
					{wallets && [...wallets].sort((a, b) => a.name.localeCompare(b.name)).map((wallet) => <Wallet key={wallet.id} wallet={wallet} />)}
					<div id={styles.walletTemplate}>
						<div className={getClassList(styles.circle, styles.plus)} onClick={toggle}></div>
					</div>
				</div>
			</Container>
			<Modal isOpen={isOpen} onClose={toggle}>
				<span className={modalStyle['modal-heading']}>Wallet Name</span>
				<input type="text" value={walletName} onChange={(e) => setWalletName(e.currentTarget.value)} />
				<div className={modalStyle['modal-buttons-container']}>
					<button onClick={handleAddWallet}>Add</button>
					<button onClick={toggle} className={modalStyle.danger}>
						Cancel
					</button>
				</div>
			</Modal>
		</View>
	);
};

export default Dashboard;
