import { useState } from 'react';
import styles from './CurrenciesInfo.module.css';
import { useCurrencyStore } from '../../stores/useCurrencyStore';
import { Currency } from '../../types/Currency';

export const CurrenciesInfo = () => {
  const purchasedCurrency = useCurrencyStore((state) => state.purchasedCurrency);
  const paymentCurrency = useCurrencyStore((state) => state.paymentCurrency);
  const [isOpen, setIsOpen] = useState(true);

  const renderCurrencyInfo = (currency?: Currency | null) => {
    if (!currency) return null;

    return (
      <>
        <p className="fw-bold m-0">
          {currency.name} – {currency.code} – {currency.symbol}
        </p>
        <p className="lh-sm text-secondary">{currency.description}</p>
      </>
    );
  };

  return (
    <div>
      <div className="d-flex justify-content-center align-items-center w-100">
        <span className={styles.currenciesInfoButtonDivider} />
        <button
          className={styles.currenciesInfoButton}
          onClick={() => {
            setIsOpen(!isOpen);
          }}
        >
          PLN/CAD: about
          <span
            className={`${styles.currenciesInfoButtonIcon} ${isOpen ? styles.arrowUpIcon : styles.arrowDownIcon}`}
          />
        </button>
        <span className={styles.currenciesInfoButtonDivider} />
      </div>
      {isOpen && (
        <div className="d-flex flex-column align-items-left">
          {renderCurrencyInfo(purchasedCurrency)}
          {renderCurrencyInfo(paymentCurrency)}
        </div>
      )}
    </div>
  );
};
