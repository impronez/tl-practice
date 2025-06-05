import { useEffect, useState } from 'react';
import styles from './CurrentDateTime.module.css';

export const CurrentDateTime = () => {
  const [time, setTime] = useState('');

  useEffect(() => {
    const updateTime = () => {
      const utc = new Date().toUTCString().replace(/:\d{2} GMT$/, ' UTC');
      setTime(utc);
    };

    updateTime();
    const interval = setInterval(updateTime, 1000);

    return () => clearInterval(interval);
  }, []);

  return <span className={styles.dateTime}>{time}</span>;
};
