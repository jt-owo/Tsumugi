export interface IWallet {
	id: string;
	name: string;
	transactions: ITransaction[];
}

export interface ITransaction {
	id: string;
	date: string;
	title: string;
	note: string;
	value: number;
	category: string;
}
