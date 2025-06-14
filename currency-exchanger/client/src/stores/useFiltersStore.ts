import { create } from 'zustand';
import { CurrencyFilter } from '../types/CurrencyFilter';
import { Currency } from '../types/Currency';

interface FiltersStore {
  filters: CurrencyFilter[];
  add: (paymentCurrency: Currency | null, purchasedCurrency: Currency | null) => void;
  clear: () => void;
}

export const useFiltersStore = create<FiltersStore>((set, get) => ({
  filters: [],

  add: (paymentCurrency, purchasedCurrency) => {
    if (paymentCurrency == null || purchasedCurrency == null) return;

    const filters = get().filters;
    const exists = filters.some(
      (f) => f.paymentCurrency.code === paymentCurrency.code && f.purchasedCurrency.code === purchasedCurrency.code
    );

    if (!exists) {
      set({
        filters: [...filters, { paymentCurrency, purchasedCurrency }]
      });
    }
  },

  clear: () => set({ filters: [] })
}));
