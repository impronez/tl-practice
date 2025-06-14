import styles from "./Header.module.scss";
import { BackButton } from "../BackButton/BackButton";

interface HeaderProps {
  title: string;
}

export const Header = ({ title }: HeaderProps) => {
  return (
    <div className={styles.headerWrapper}>
      <BackButton />
      <span className={styles.headerTitle}>{title}</span>
    </div>
  );
};
