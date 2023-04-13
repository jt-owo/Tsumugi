/* eslint-disable react-hooks/rules-of-hooks */
import { FC, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../state/hooks';
import { updateWallet } from '../../state/slices/walletSlice';

import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend, ChartData } from 'chart.js';
import { Bar } from 'react-chartjs-2';

import { ITransaction } from '../../types/types';

import View from '../../components/View/View';
import Modal from '../../components/Modal/Modal';

import newGuid from '../../util/newGuid';

import styles from './Detail.module.css';
import modalStyle from '../../components/Modal/Modal.module.css';

ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

export const options = {
	responsive: true,
	plugins: {
		legend: {
			display: false
		}
	},
	layout: {
		padding: {
			bottom: 30
		}
	}
};

type DetailParams = {
	id: string;
};

const Detail: FC = () => {
	const { id } = useParams<DetailParams>();

	const dispatch = useAppDispatch();
	const navigate = useNavigate();

	const wallets = useAppSelector((state) => state.wallets.data);
	const wallet = wallets.find((x) => x.id === id);

	if (!wallet) {
		navigate('/');
		return null;
	}

	const [barData, setBarData] = useState<ChartData<'bar', number[], string>>({ labels: [], datasets: [] });

	const [isOpen, setOpen] = useState(false);

	const [title, setTitle] = useState('');
	const [date, setDate] = useState(new Date().toISOString().substring(0, 10));
	const [note, setNote] = useState('');
	const [value, setValue] = useState(0);

	const handleAddTransaction = () => {
		if (title === '' || date === '' || value === 0) return;

		const updateData = {
			...wallet,
			transactions: [...wallet.transactions]
		};

		updateData.transactions.push({
			id: newGuid(),
			date,
			title,
			note,
			value,
			category: ''
		});

		dispatch(updateWallet(updateData));
	};

        const sum = (arr: ITransaction[]) => {
			if (arr.length < 1) return 0;

			let sum = 0;

			arr.forEach((item: ITransaction) => {
				sum += item.value;
			});

			return sum;
		};

	useEffect(() => {
		const revenue = wallet.transactions.filter((t) => !t.value.toString().startsWith('-'));
		const expenses = wallet.transactions.filter((t) => t.value.toString().startsWith('-'));

		setBarData({
			labels: ['REVENUE', 'EXPENSES'],
			datasets: [
				{
					label: 'Amount of Money',
					data: [sum(revenue), sum(expenses)],
					backgroundColor: ['rgba(67, 181, 129, 0.2)', 'rgba(240, 71, 71, 0.2)'],
					borderColor: ['rgba(60, 162, 116, 1)', 'rgba(216, 63, 63, 1)'],
					borderWidth: 1
				}
			]
		});
	}, [wallet.transactions]);

	return (
		<View>
			<div id={styles['detail-container']}>
				<div id={styles['data-container']}>
					<h1>{wallet.name}</h1>
					<table id={styles['transaction-table']}>
						<thead>
							<tr>
								<th>Date</th>
								<th>Title</th>
								<th>Note</th>
								<th>Amount</th>
							</tr>
						</thead>
						<tbody>
							{wallet.transactions.map((t) => {
								return (
									<tr key={t.id}>
										<td>{t.date}</td>
										<td>{t.title}</td>
										<td>{t.note}</td>
										<td>{t.value}</td>
									</tr>
								);
							})}
						</tbody>
					</table>
					<div className={styles['chart-container']}>
						<div id={styles["trend"]}></div>
						<div id={styles["months"]}>
							<h3>THIS MONTH</h3>
							<Bar options={options} data={barData} width={400} height={400} />
						</div>
					</div>
				</div>
				<div id={styles['button-container']}>
					<button onClick={() => setOpen(true)}>Add Transaction</button>
				</div>
			</div>
			<Modal className={modalStyle['trans-modal']} isOpen={isOpen} onClose={() => setOpen(false)}>
				<span className={modalStyle['modal-heading']}>Transaction</span>
				<label>Title</label>
				<input type="text" value={title} onChange={(e) => setTitle(e.currentTarget.value)} />

				<label>Date</label>
				<input type="date" value={date} onChange={(e) => setDate(e.currentTarget.value)} />

				<label>Amount</label>
				<input type="text" value={value} onChange={(e) => setValue(+e.currentTarget.value)} />

				<label>Note</label>
				<textarea value={note} onChange={(e) => setNote(e.currentTarget.value)} />
				<div className={modalStyle['modal-buttons-container']}>
					<button onClick={handleAddTransaction}>Add</button>
					<button onClick={() => setOpen(false)} className={modalStyle.danger}>
						Cancel
					</button>
				</div>
			</Modal>
		</View>
	);
};

export default Detail;
