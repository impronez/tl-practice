import styles from "./Textarea.module.css";

interface TextareaProps {
  placeholder: string;
  onChange: (value: string) => void;
}

export default function Textarea({ placeholder, onChange }: TextareaProps) {
  const handleChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    onChange?.(e.target.value);
  };

  return (
    <textarea
      className={styles.textarea}
      placeholder={placeholder}
      onChange={handleChange}
    />
  );
}
