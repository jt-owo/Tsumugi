import { PayloadAction, createSlice } from '@reduxjs/toolkit';
import { IWallet } from '../../types/types';
import newGuid from '../../util/newGuid';

export type WalletsState = {
    data: IWallet[]
};

const initialState: WalletsState = {
    data: []
};

export const walletsSlice = createSlice({
	name: 'wallets',
	initialState,
	reducers: {
        addWallet: (state, action: PayloadAction<string>) => {
            state.data.push({
                id: newGuid(),
                name: action.payload,
                transactions: []
            });

            window.localStorage.setItem('wallets', JSON.stringify(state.data));
        },
		loadWallets: (state) => {
            const stored = window.localStorage.getItem('wallets');
            if (stored) state.data = JSON.parse(stored) as IWallet[];
		},
        updateWallet: (state, action: PayloadAction<IWallet>) => {
            const wallet = state.data.find(x => x.id === action.payload.id);
            if (wallet) {
                const index = state.data.indexOf(wallet);
                if (index > -1) {
                    state.data[index] = action.payload;
                    window.localStorage.setItem('wallets', JSON.stringify(state.data));
                }
            }
        }
	}
});

export const { loadWallets, addWallet, updateWallet } = walletsSlice.actions;

export default walletsSlice.reducer;
