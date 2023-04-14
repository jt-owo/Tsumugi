import { FC, useEffect, useRef, useState } from 'react';
import { useAppDispatch, useAppSelector } from '../../state/hooks';

import View from '../../components/Layout/View/View';
import Container from '../../components/Layout/Container/Container';

import Button from '../../components/Button/Button';
import { uploadData } from '../../state/slices/walletSlice';

const About: FC = () => {
	const wallets = useAppSelector((state) => state.wallets.data);

    const dispatch = useAppDispatch();

	const [file, setFile] = useState<File | null>(null);

    const ref = useRef<HTMLInputElement>(null);

	const exportToJSON = () => {
		const element = document.createElement('a');
		var blob = new Blob([JSON.stringify(wallets)], { type: 'text/plain;charset=utf-8' });
		element.href = URL.createObjectURL(blob);
		element.download = `tsumugi_data_${new Date().toLocaleString()}_.json`;
		document.body.appendChild(element);
		element.click();
		document.body.removeChild(element);
	};

	const handleChangeFile = (files: FileList | null) => {
		if (!files || files.length < 1) return;

		setFile(files[0]);
	};

	useEffect(() => {
		if (!file) return;

		const reader = new FileReader();
		reader.readAsText(file, 'UTF-8');
        reader.onerror = () => console.error('error reading file');
		reader.onload = (evt: ProgressEvent<FileReader>) => {
            if (!evt.target || ! evt.target.result) return;
            dispatch(uploadData(evt.target.result.toString()));
		};
	}, [dispatch, file]);

	return (
		<View>
			<Container id="aboutContainer">
				<p>Tsumugi is a simple finance manager.</p>
				<p>This website uses the browsers local storage to store your data.</p>
				<p>v0.0.1</p>
			</Container>
			<Container id="versionContainer" bottom>
				<Button onClick={exportToJSON}>Export Data</Button>
				<Button onClick={() => ref.current?.click()}>Upload Data</Button>
				<Button type="danger" onClick={() => window.localStorage.clear()}>
					Clear Data
				</Button>
				<input ref={ref} type="file" onChange={(e) => handleChangeFile(e.target.files)} hidden />
			</Container>
		</View>
	);
};

export default About;
