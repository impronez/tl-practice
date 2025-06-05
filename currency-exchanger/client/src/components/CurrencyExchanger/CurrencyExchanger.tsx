import { useEffect } from 'react';
import { useCurrencyStore } from '../../stores/useCurrencyStore';
import { CurrenciesInfo } from '../CurrenciesInfo/CurrenciesInfo';
import { CurrencyChart } from '../CurrencyChart/CurrencyChart';
import { CurrentDateTime } from '../CurrentDateTime/CurrentDateTime';
import styles from './CurrencyExchanger.module.css';
import { Loader } from '../Loader/Loader';
import { FetchError } from '../Error/Error';
import { CurrencyExchangeForm } from '../CurrencyExchangeForm/CurrencyExchangeForm';
import { ExchangeRate } from '../ExchangeRate/ExchangeRate';
import { useShallow } from 'zustand/shallow';
import { useLivePrices } from '../../hooks/useLivePrices';
import { useFiltersStore } from '../../stores/useFiltersStore';

export const CurrencyExchanger = () => {
  const { error, fetchCurrencies, purchasedCurrency, paymentCurrency } = useCurrencyStore(
    useShallow((state) => ({
      error: state.error,
      fetchCurrencies: state.fetchCurrencies,
      purchasedCurrency: state.purchasedCurrency,
      paymentCurrency: state.paymentCurrency
    }))
  );

  const addFilter = useFiltersStore((state) => state.add);

  useLivePrices(purchasedCurrency?.code, paymentCurrency?.code);

  useEffect(() => {
    fetchCurrencies();

    const interval = setInterval(() => {
      fetchCurrencies();
    }, 30000);

    return () => clearInterval(interval);
  }, [fetchCurrencies]);

  const handleSaveFilterButton = () => {
    addFilter(paymentCurrency, purchasedCurrency);
  };

  return (
    <>
      {error === null ? (
        <Loader />
      ) : error == false ? (
        <div className={styles.exchangerWrapper}>
          <div className={styles.exchangerContent}>
            <div className="d-flex d-flex justify-content-between align-items-center">
              <ExchangeRate />
              <button className={styles.saveFilterButton} onClick={handleSaveFilterButton}>
                + Save Filter
              </button>
            </div>
            <div className="d-flex d-flex justify-content-between pb-3 gap-3">
              <div className="d-flex flex-column w-50 gap-3">
                <span className={styles.dateTimeElement}>
                  <CurrentDateTime />
                </span>
                <CurrencyExchangeForm />
              </div>
              <CurrencyChart />
            </div>
            <CurrenciesInfo />
          </div>
        </div>
      ) : (
        <FetchError />
      )}
    </>
  );
};
