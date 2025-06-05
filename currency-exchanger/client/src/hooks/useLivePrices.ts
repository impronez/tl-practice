import { useEffect } from 'react';
import { usePricesStore } from '../stores/usePricesStore';

const updateTimeIntervalMs = 10000;

export const useLivePrices = (purchasedCurrencyCode: string | undefined, paymentCurrencyCode: string | undefined) => {
  const fetchPrices = usePricesStore((state) => state.fetchPrices);

  useEffect(() => {
    if (!purchasedCurrencyCode || !paymentCurrencyCode) return;

    fetchPrices(purchasedCurrencyCode, paymentCurrencyCode);

    const interval = setInterval(() => {
      fetchPrices(purchasedCurrencyCode, paymentCurrencyCode);
    }, updateTimeIntervalMs);

    return () => clearInterval(interval);
  }, [purchasedCurrencyCode, paymentCurrencyCode, fetchPrices]);
};
