import { create } from 'zustand';
import type { Currency } from '../types/Currency';
import { API_GET_CURRENCIES_URL } from '../config/config';

interface CurrencyStore {
  currencies: Currency[];
  error: boolean | null;
  purchasedCurrency: Currency | null;
  paymentCurrency: Currency | null;

  fetchCurrencies: () => Promise<void>;
  setPurchasedCurrency: (code: Currency) => void;
  setPaymentCurrency: (code: Currency) => void;
}

const loadCurrencies = async (): Promise<Currency[]> => {
  const response = await fetch(API_GET_CURRENCIES_URL);
  if (!response.ok) throw new Error('Failed to fetch currencies');
  const data: Currency[] = await response.json();
  return data;
};

const canSetCurrency = (newCurrency: Currency, otherCurrency: Currency | null) => {
  return !otherCurrency || otherCurrency.code !== newCurrency.code;
};

export const useCurrencyStore = create<CurrencyStore>((set, get) => ({
  currencies: [],
  error: null,
  purchasedCurrency: null,
  paymentCurrency: null,
  prices: [],
  fetchCurrencies: async () => {
    try {
      const data = await loadCurrencies();
      const currentCurrencies = get().currencies;

      if (JSON.stringify(currentCurrencies) !== JSON.stringify(data)) {
        set({ currencies: data, error: false });
      }
      // инцициализация валют
      const { purchasedCurrency, paymentCurrency } = get();
      if (!purchasedCurrency || !paymentCurrency) {
        set({ purchasedCurrency: data[0], paymentCurrency: data[1] });
      }
    } catch (err) {
      set({ error: true });
    }
  },
  setPurchasedCurrency: (currency) => {
    const currentPurchased = get().purchasedCurrency;
    const currentPayment = get().paymentCurrency;
    if (canSetCurrency(currency, currentPayment) && currency != currentPurchased) {
      set({ purchasedCurrency: currency });
    }
  },

  setPaymentCurrency: (currency) => {
    const currentPayment = get().paymentCurrency;
    const currentPurchased = get().purchasedCurrency;
    if (canSetCurrency(currency, currentPurchased) && currency != currentPayment) {
      set({ paymentCurrency: currency });
    }
  }
}));
