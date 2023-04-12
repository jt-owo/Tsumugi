import { FC } from "react";
import { NavLink } from "react-router-dom"

import styles from './Navigation.module.css';

const Navigation: FC = () => {
    return (
		<div className={styles['nav-container']}>
			<NavLink to="/">Dashboard</NavLink>
			<NavLink to="/about" className={styles['last']}>About</NavLink>
		</div>
	);
}

export default Navigation;