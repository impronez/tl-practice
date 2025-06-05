import { create } from 'zustand';
import { PricePoint } from '../types/PricePoint';
import { API_GET_PRICES_URL } from '../config/config';

interface PricesStore {
  prices: PricePoint[];
  currentPrice: PricePoint | null;
  error: boolean | null;
  fetchPrices: (purchasedCurrencyCode: string, paymentCurrencyCode: string) => Promise<void>;
}

export const usePricesStore = create<PricesStore>((set) => ({
  prices: [],
  error: null,
  currentPrice: null,
  fetchPrices: async (purchasedCurrencyCode, paymentCurrencyCode) => {
    try {
      const fromDateTime = new Date(Date.now() - 5 * 60 * 1000).toISOString();

      const params = new URLSearchParams({
        PaymentCurrency: paymentCurrencyCode,
        PurchasedCurrency: purchasedCurrencyCode,
        FromDateTime: fromDateTime
      });

      const response = await fetch(`${API_GET_PRICES_URL}${params.toString()}`);
      if (!response.ok) throw new Error('Failed to load price points');

      const data: PricePoint[] = await response.json();
      set({ prices: data, error: false, currentPrice: data[data.length - 1] });
    } catch (err) {
      console.log(err);
      set({ error: true });
    }
  }
}));
