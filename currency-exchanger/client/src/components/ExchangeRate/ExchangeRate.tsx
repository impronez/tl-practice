import { useCurrencyStore } from '../../stores/useCurrencyStore';
import { usePricesStore } from '../../stores/usePricesStore';
import styles from './ExchangeRate.module.css';

export const ExchangeRate = () => {
  const purchasedCurrency = useCurrencyStore((state) => state.purchasedCurrency);
  const paymentCurrency = useCurrencyStore((state) => state.paymentCurrency);

  const currentPrice = usePricesStore((state) => state.currentPrice);

  return (
    <div>
      <p className={`${styles.currencyLabelFrom} ${styles.noMargin}`}>1 {purchasedCurrency?.name} is</p>
      <p className={`${styles.currencyLabelTo} ${styles.noMargin}`}>
        {currentPrice?.price} {paymentCurrency?.name}
      </p>
    </div>
  );
};
