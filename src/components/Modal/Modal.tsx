import { FC, useEffect } from 'react';

import Portal from '../Portal/Portal';

import styles from './Modal.module.css';

export interface ModalProps {
	isOpen: boolean;
	onClose: () => void;
	isFading?: boolean;
	className?: string;
	children?: JSX.Element | JSX.Element[];
}

const Modal: FC<ModalProps> = (props) => {
	const { className, children, isOpen, onClose, isFading } = props;

	useEffect(() => {
		const closeOnEscapeKey = (e: KeyboardEvent) => (e.key === 'Escape' ? onClose() : null);
		document.body.addEventListener('keydown', closeOnEscapeKey);
		return () => {
			document.body.removeEventListener('keydown', closeOnEscapeKey);
		};
	}, [onClose]);

	if (!isOpen) return null;

	return (
		<Portal wrapperID="portal-modal-container">
			<div className={styles['modal']}>
				<div className={`${className || styles['modal-content']} ${isFading ? styles.fading : styles.visible}`}>{children}</div>
			</div>
		</Portal>
	);
};

export default Modal;
