import { render, screen } from '@testing-library/react';
import { Provider } from 'react-redux';
import { store } from '../app/store';
import Application from '../components/Application';

test('renders app', () => {
	render(
        <Provider store={store}>
            <Application />
        </Provider>
	);

	expect(screen.getByText(/Tsumugi/i)).toBeInTheDocument();
});
