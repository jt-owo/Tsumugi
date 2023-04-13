import { FC } from "react";
import { NavLink, useLocation } from "react-router-dom"

import styles from './Navigation.module.css';

const Navigation: FC = () => {
    const location = useLocation();

    const getActiveStyle = (path: string) => {
        return `${location.pathname === path ? styles.active : ''}`;
    }

    return (
		<div className={styles['nav-container']}>
			<NavLink to="/" className={getActiveStyle('/')}>
				Dashboard
			</NavLink>
			<NavLink to="/about" className={styles.last + ' ' + getActiveStyle('/about')}>
				About
			</NavLink>
		</div>
	);
}

export default Navigation;