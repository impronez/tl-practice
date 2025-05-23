import styles from "./Input.module.css";

interface InputProps {
  label: string;
  type: string;
  placeholder: string;
  name: string;
  maxLength: number;
  minLength: number;
  onChange: (value: string) => void;
  required?: boolean;
}

export default function Input({
  label,
  type,
  placeholder,
  name,
  minLength,
  maxLength,
  onChange,
  required = false,
}: InputProps) {
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    onChange?.(e.target.value);
  };

  return (
    <div className={styles.inputContainer}>
      <input
        placeholder={placeholder}
        type={type}
        name={name}
        minLength={minLength}
        maxLength={maxLength}
        required={required}
        onChange={handleChange}
      />
      <span>{label}</span>
    </div>
  );
}
