import styles from "./Header.module.scss";
import { BackButton } from "../BackButton/BackButton";

interface HeaderProps {
  title: string;
}

export const Header = ({ title }: HeaderProps) => {
  return (
    <div className={styles.headerWrapper}>
      <BackButton />
      <h1 className={styles.headerTitle}>{title}</h1>
    </div>
  );
};
