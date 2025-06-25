import styles from './FiltersMenu.module.css';
import { useFiltersStore } from '../../stores/useFiltersStore';
import { CurrencyFilter } from '../../types/CurrencyFilter';
import { useCurrencyStore } from '../../stores/useCurrencyStore';

export const FiltersMenu = () => {
  const filters = useFiltersStore((state) => state.filters);
  const clearFilters = useFiltersStore((state) => state.clear);

  const setPaymentCurrency = useCurrencyStore((state) => state.setPaymentCurrency);
  const setPurchasedCurrency = useCurrencyStore((state) => state.setPurchasedCurrency);

  const handleSetFilterClick = (filter: CurrencyFilter) => {
    setPaymentCurrency(filter.paymentCurrency);
    setPurchasedCurrency(filter.purchasedCurrency);
  };

  if (!filters.length) return null;

  return (
    <div className={styles.filterMenuWrapper}>
      {filters.map((filter, key) => (
        <button
          key={key}
          className={styles.setFilterButton}
          onClick={() => handleSetFilterClick(filter)}
        >{`${filter.purchasedCurrency.code}/${filter.paymentCurrency.code}`}</button>
      ))}
      <button onClick={clearFilters} className={styles.clearFiltersButton}>
        Clear filters
      </button>
    </div>
  );
};
