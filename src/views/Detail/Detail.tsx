/* eslint-disable react-hooks/rules-of-hooks */
import { FC, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useAppDispatch, useAppSelector } from '../../state/hooks';
import { updateWallet } from '../../state/slices/walletSlice';
import { formatDate, newGuid, sum } from '../../util';
import useToggle from '../../hooks/useToggle';

import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend, ChartData, ChartOptions } from 'chart.js';
import { Bar } from 'react-chartjs-2';

import View from '../../components/Layout/View/View';
import Container from '../../components/Layout/Container/Container';
import Row from '../../components/Layout/Row/Row';
import Column from '../../components/Layout/Column/Column';

import Heading from '../../components/Heading/Heading';
import Button from '../../components/Button/Button';
import List from '../../components/List/List';
import ListRow from '../../components/List/ListRow/ListRow';
import ListColumn from '../../components/List/ListColumn/ListColumn';
import Number from '../../components/Number/Number';
import Modal from '../../components/Modal/Modal';

import styles from './Detail.module.css';
import modalStyle from '../../components/Modal/Modal.module.css';
import ListCheckboxColumn from '../../components/List/ListColumn/ListCheckboxColumn';
import ListEmptyColumn from '../../components/List/ListColumn/ListEmptyColumn';

ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

export const options: ChartOptions = {
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

	const [deleteList, setDeleteList] = useState<string[]>([]);

	const { value: isOpen, toggle } = useToggle();
	const { value: isEdit, toggle: toggleEdit } = useToggle();

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

		// Reset dialog values.
		setTitle('');
		setDate(new Date().toISOString().substring(0, 10));
		setNote('');
		setValue(0);
		toggle();
	};

	const toggleItemDeleteList = (id: string) => {
		const index = deleteList.indexOf(id);
		if (index > -1) {
			deleteList.splice(index, 1);
		} else {
			deleteList.push(id);
		}

		setDeleteList([...deleteList]);
	};

	const handleDeleteTransactions = () => {
		const updateData = {
			...wallet,
			transactions: [...wallet.transactions]
		};

		deleteList.forEach((id) => {
			const index = updateData.transactions.findIndex((x) => x.id === id);
			if (index > -1) {
				updateData.transactions.splice(index, 1);
			}
		});

		dispatch(updateWallet(updateData));
        cancelEdit();
	};

    const cancelEdit = () => {
        setDeleteList([]);
        toggleEdit();
    }

	useEffect(() => {
		const revenue = wallet.transactions.filter((t) => !t.value.toString().startsWith('-'));
		const expenses = wallet.transactions.filter((t) => t.value.toString().startsWith('-'));

		setBarData({
			labels: ['REVENUE', 'EXPENSES'],
			datasets: [
				{
					label: 'Amount',
					data: [sum(revenue), sum(expenses) * -1],
					backgroundColor: ['rgba(67, 181, 129, 0.2)', 'rgba(240, 71, 71, 0.2)'],
					borderColor: ['rgba(60, 162, 116, 1)', 'rgba(216, 63, 63, 1)'],
					borderWidth: 1
				}
			]
		});
	}, [wallet.transactions]);

	return (
		<View>
			<Container id="transactionContainer">
				<Row>
					<Column>
						<Heading>{wallet.name}</Heading>
					</Column>
				</Row>
				{wallet.transactions.length > 0 ? (
					<Row>
						<Column>
							<List>
								<ListRow heading>
									<ListColumn>Date</ListColumn>
									<ListColumn>Title</ListColumn>
									<ListColumn>Note</ListColumn>
									<ListColumn>Amount</ListColumn>
									{isEdit && <ListEmptyColumn />}
								</ListRow>
								{[...wallet.transactions]
									.sort((a, b) => new Date(a.date).valueOf() - new Date(b.date).valueOf())
									.map((transaction) => (
										<ListRow key={transaction.id}>
											<ListColumn>{formatDate(transaction.date)}</ListColumn>
											<ListColumn>{transaction.title}</ListColumn>
											<ListColumn>{transaction.note}</ListColumn>
											<ListColumn>
												<Number value={transaction.value} />
											</ListColumn>
											{isEdit && <ListCheckboxColumn id={transaction.id} onChange={toggleItemDeleteList} />}
										</ListRow>
									))}
							</List>
						</Column>
						<Column>
							<Container id={styles.chartContainer}>
								<Heading level={1} center>
									TOTAL
								</Heading>
								<Bar options={options} data={barData} />
							</Container>
						</Column>
					</Row>
				) : (
					<p>No wallet data available.</p>
				)}
			</Container>
			<Container id="buttonContainer" bottom>
				<Button onClick={toggle}>+</Button>
                <Button type="confirm" hide={isEdit || wallet.transactions.length < 1} onClick={toggleEdit}>
                    Edit
                </Button>
				<Button type="danger" hide={!isEdit} onClick={cancelEdit}>
					Cancel
				</Button>
				<Button type="danger" display='right' hide={deleteList.length < 1} onClick={handleDeleteTransactions}>
					Delete Selected
				</Button>
			</Container>
			<Modal className={modalStyle['trans-modal']} isOpen={isOpen} onClose={toggle}>
				<span className={modalStyle['modal-heading']}>Transaction</span>
				<label>Title</label>
				<input type="text" value={title} onChange={(e) => setTitle(e.currentTarget.value)} />

				<label>Date</label>
				<input type="date" value={date} onChange={(e) => setDate(e.currentTarget.value)} />

				<label>Amount</label>
				<input type="number" value={value} onChange={(e) => setValue(+e.currentTarget.value)} />

				<label>Note</label>
				<textarea value={note} onChange={(e) => setNote(e.currentTarget.value)} />
				<div className={modalStyle['modal-buttons-container']}>
					<button onClick={handleAddTransaction}>Add</button>
					<button onClick={toggle} className={modalStyle.danger}>
						Cancel
					</button>
				</div>
			</Modal>
		</View>
	);
};

export default Detail;
