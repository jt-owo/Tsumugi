import { useCallback, useState } from 'react';

const useToggle = (defaultValue: boolean = false) => {
	const [value, setValue] = useState(defaultValue);

    const toggle = useCallback(() => setValue((x) => !x), []);

	return { value, toggle };
}

export default useToggle;
