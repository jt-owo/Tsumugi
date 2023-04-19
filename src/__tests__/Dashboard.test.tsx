import { render, screen } from '@testing-library/react';
import { Provider } from 'react-redux';
import { store } from '../state/store';
import Dashboard from '../views/Dashboard/Dashboard';

test('renders dashbaord', () => {
	render(
		<Provider store={store}>
			<Dashboard />
		</Provider>
	);

	expect(screen.getByText(/Wallets/i)).toBeInTheDocument();
});
