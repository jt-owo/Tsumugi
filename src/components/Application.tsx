import { FC } from 'react';
import { Routes, Route, BrowserRouter } from 'react-router-dom';

import Navigation from './Navigation/Navigation';

import Home from '../views/Home/Home';
import About from '../views/About/About';

import './Application.css';

const Application: FC = () => {
	return (
		<div className="app-container">
			<BrowserRouter>
				<Navigation />
				<Routes>
					<Route path="/" element={<Home />} />
					<Route path="/about" element={<About />} />
				</Routes>
			</BrowserRouter>
		</div>
	);
}

export default Application;
