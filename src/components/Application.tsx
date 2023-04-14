import { FC } from 'react';
import { Routes, Route, HashRouter } from 'react-router-dom';
import { useAppDispatch } from '../state/hooks';
import { loadWallets } from '../state/slices/walletSlice';

import Container from './Layout/Container/Container';
import Navigation from './Layout/Navigation/Navigation';

import Dashboard from '../views/Dashboard/Dashboard';
import Detail from '../views/Detail/Detail';
import About from '../views/About/About';

const Application: FC = () => {
	const dispatch = useAppDispatch();
	dispatch(loadWallets());

	return (
		<Container id="appContainer">
			<HashRouter>
				<Navigation />
				<Routes>
					<Route path="/" element={<Dashboard />} />
					<Route path="/detail/:id" element={<Detail />} />
					<Route path="/about" element={<About />} />
				</Routes>
			</HashRouter>
		</Container>
	);
};

export default Application;
