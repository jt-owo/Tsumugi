import { FC } from "react";
import { useAppSelector } from "../../state/hooks";

import View from "../../components/View/View";

const About: FC = () => {

    const wallets = useAppSelector(state => state.wallets.data);

    const exportToJSON = () => {
		const element = document.createElement('a');
		var blob = new Blob([JSON.stringify(wallets)], { type: 'text/plain;charset=utf-8' });
		element.href = URL.createObjectURL(blob);
		element.download = `tsumugi_data_${new Date().toLocaleString()}_.json`;
		document.body.appendChild(element);
		element.click();
		document.body.removeChild(element);
	};

    return (
		<View>
			<p>Tsumugi is a simple finance manager.</p>
			<p>This website uses the browsers local storage to store your data.</p>
			<p>v0.0.1</p>
			<div>
				<button onClick={exportToJSON}>Export Data</button>
				<button className="danger" onClick={() => window.localStorage.clear()}>
					Clear all data
				</button>
			</div>
		</View>
	);
}

export default About;