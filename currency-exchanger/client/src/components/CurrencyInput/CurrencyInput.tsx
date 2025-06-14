import styles from './CurrencyInput.module.css';
import { Currency } from '../../types/Currency';

interface CurrencyInputProps {
  currency: Currency | null;
  onCurrencyChange: (currency: Currency) => void;
  onValueChange: (value: number, currencyCode: string | undefined) => void;
  value: number;
  currencies: Currency[];
}

export const CurrencyInput = ({ currency, onCurrencyChange, currencies, value, onValueChange }: CurrencyInputProps) => {
  const handleSelectChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedCode = e.target.value;
    const selected = currencies.find((c) => c.code === selectedCode);
    if (selected) {
      onCurrencyChange(selected);
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = Number(e.target.value);
    if (newValue < 0) {
      return;
    }
    onValueChange(newValue, currency?.code);
  };

  return (
    <div className={`${styles.currencyInputWrapper} fs-6`}>
      <input
        className={`${styles.currencyInput} ${styles.amountInput}`}
        type="number"
        min={0}
        value={value}
        onChange={handleInputChange}
      />
      <span className={styles.currencyInputDivider} />
      <select
        className={`${styles.currencyInput} ${styles.currencySelect}`}
        value={currency?.code || ''}
        onChange={handleSelectChange}
      >
        {currencies.map((c) => (
          <option key={c.code} value={c.code}>
            {c.code}
          </option>
        ))}
      </select>
    </div>
  );
};
