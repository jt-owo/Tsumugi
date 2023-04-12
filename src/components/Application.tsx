import { FC } from 'react';
import { Routes, Route, HashRouter } from 'react-router-dom';

import Navigation from './Navigation/Navigation';

import Home from '../views/Home/Home';
import About from '../views/About/About';

import './Application.css';

const Application: FC = () => {
	return (
		<div className="app-container">
			<HashRouter>
				<Navigation />
				<Routes>
					<Route path="/" element={<Home />} />
					<Route path="/about" element={<About />} />
				</Routes>
			</HashRouter>
		</div>
	);
}

export default Application;
