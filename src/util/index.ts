import { ITransaction } from '../types/types';

/**
 * Generates a new guid.
 *
 * https://stackoverflow.com/questions/105034/how-to-create-a-guid-uuid
 * @returns Guid.
 */
export function newGuid() {
	const S4 = () => (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
	return `${S4() + S4()}-${S4()}-${S4()}-${S4()}-${S4()}${S4()}${S4()}`;
}

export const sum = (arr: ITransaction[]) => {
	let sum = 0;

	arr.forEach((item: ITransaction) => {
		sum += item.value;
	});

	return sum;
};

export const formatDate = (date: string) => {
	const ret = new Date(date);
	return ret.toLocaleDateString();
};

export const getClassList = (...classes: string[]) => {
    let classList = '';
    if (classes && classes.length > 0) {
        classes.forEach((c) => {
            if (classList === '') classList += `${c}`;
            else classList += ` ${c}`;
        });
    }

    return classList;
}
