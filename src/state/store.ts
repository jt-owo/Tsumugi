import { configureStore, ThunkAction, Action } from '@reduxjs/toolkit';
import walletsReducer from './slices/walletSlice';

export const store = configureStore({
	reducer: {
		wallets: walletsReducer
	}
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
export type AppThunk<ReturnType = void> = ThunkAction<ReturnType, RootState, unknown, Action<string>>;
