import React from 'react';
import { createRoot } from 'react-dom/client';
import { Provider } from 'react-redux';
import { store } from './state/store';
import Application from './components/Application';
import reportWebVitals from './util/reportWebVitals';

import './index.css';

const container = document.getElementById('root')!;
const root = createRoot(container);

root.render(
	<React.StrictMode>
		<Provider store={store}>
			<Application />
		</Provider>
	</React.StrictMode>
);

reportWebVitals();
