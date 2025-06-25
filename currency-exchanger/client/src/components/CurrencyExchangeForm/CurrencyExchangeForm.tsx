import { useEffect, useRef, useState } from 'react';
import { useCurrencyStore } from '../../stores/useCurrencyStore';
import { CurrencyInput } from '../CurrencyInput/CurrencyInput';
import { usePricesStore } from '../../stores/usePricesStore';

export const CurrencyExchangeForm = () => {
  const currencies = useCurrencyStore((state) => state.currencies);
  const purchasedCurrency = useCurrencyStore((state) => state.purchasedCurrency);
  const paymentCurrency = useCurrencyStore((state) => state.paymentCurrency);
  const setPurchasedCurrency = useCurrencyStore((state) => state.setPurchasedCurrency);
  const setPaymentCurrency = useCurrencyStore((state) => state.setPaymentCurrency);

  const currentPrice = usePricesStore((state) => state.currentPrice);

  const [purchasedCurrencyValue, setPurchasedCurrencyValue] = useState<number>(1);
  const [paymentCurrencyValue, setPaymentCurrencyValue] = useState<number>(0);
  const isFirstLoad = useRef(true);

  useEffect(() => {
    if (isFirstLoad.current && currentPrice?.price != null) {
      setPaymentCurrencyValue(currentPrice.price);
      isFirstLoad.current = false;
    }
  }, [currentPrice]);

  const handleCurrencyValueChange = (value: number, currencyCode: string | undefined) => {
    if (!currencyCode || currentPrice?.price == null) return;

    if (currencyCode === purchasedCurrency?.code) {
      setPurchasedCurrencyValue(value);
      setPaymentCurrencyValue(value * currentPrice.price);
    } else if (currencyCode === paymentCurrency?.code) {
      setPaymentCurrencyValue(value);
      setPurchasedCurrencyValue(value / currentPrice.price);
    }
  };

  return (
    <>
      <CurrencyInput
        currency={purchasedCurrency}
        onCurrencyChange={setPurchasedCurrency}
        currencies={currencies}
        value={purchasedCurrencyValue}
        onValueChange={handleCurrencyValueChange}
      />
      <CurrencyInput
        currency={paymentCurrency}
        onCurrencyChange={setPaymentCurrency}
        currencies={currencies}
        value={paymentCurrencyValue}
        onValueChange={handleCurrencyValueChange}
      />
    </>
  );
};
