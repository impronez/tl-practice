import Slider from "../Slider/Slider";
import Input from "../Input/Input";
import styles from "./ReviewForm.module.css";
import Textarea from "../Textarea/Textarea";
import type { ReviewData } from "../../types/ReviewData";
import { useEffect, useRef, useState } from "react";
import type { FormData } from "../../types/FormData";

interface ReviewFormProps {
  onAddReview: (review: ReviewData) => void;
}

const initialFormData: FormData = {
  username: "",
  review: "",
  cleanliness: 0,
  service: 0,
  speed: 0,
  location: 0,
  speechCulture: 0,
};

export default function ReviewForm({ onAddReview }: ReviewFormProps) {
  const formRef = useRef<HTMLFormElement>(null);

  const [isFormValid, setIsFormValid] = useState(false);
  const [formData, setFormData] = useState<FormData>(initialFormData);

  const handleFieldChange = (name: keyof FormData, value: number | string) => {
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const rating =
      (formData.cleanliness +
        formData.service +
        formData.speed +
        formData.location +
        formData.speechCulture) /
      5;

    const newReview: ReviewData = {
      id: Date.now(),
      name: formData.username,
      comment: formData.review,
      rating: rating,
    };

    onAddReview(newReview);

    setFormData(initialFormData);
    if (formRef.current) {
      formRef.current.reset();
    }
  };

  useEffect(() => {
    const isValid =
      formData.username.trim() !== "" &&
      formData.review.trim() !== "" &&
      formData.cleanliness > 0 &&
      formData.service > 0 &&
      formData.speed > 0 &&
      formData.location > 0 &&
      formData.speechCulture > 0;

    setIsFormValid(isValid);
  }, [formData]);

  return (
    <div className={styles.reviewFormWrapper}>
      <div className={styles.reviewFormContainer}>
        <p className={styles.title}>
          Помогите нам сделать процесс бронирования лучше
        </p>
        <form
          ref={formRef}
          className={styles.reviewForm}
          onSubmit={handleSubmit}
        >
          <Slider
            label="Чистенько"
            value={formData.cleanliness}
            onChange={(value) => handleFieldChange("cleanliness", value)}
          />
          <Slider
            label="Сервис"
            value={formData.service}
            onChange={(value) => handleFieldChange("service", value)}
          />
          <Slider
            label="Скорость"
            value={formData.speed}
            onChange={(value) => handleFieldChange("speed", value)}
          />
          <Slider
            label="Место"
            value={formData.location}
            onChange={(value) => handleFieldChange("location", value)}
          />
          <Slider
            value={formData.speechCulture}
            label="Культура речи"
            onChange={(value) => handleFieldChange("speechCulture", value)}
          />
          <Input
            onChange={(value) => handleFieldChange("username", value)}
            label="*Имя"
            type="text"
            placeholder="Как вас зовут?"
            name="username"
            minLength={1}
            maxLength={30}
          />
          <Textarea
            placeholder="Напишите, что понравилось, что было непонятно"
            onChange={(value) => handleFieldChange("review", value)}
          />
          <button
            className={`${styles.submitButton} ${isFormValid && styles.submitButtonActive}`}
            type="submit"
            disabled={!isFormValid}
          >
            Отправить
          </button>
        </form>
      </div>
    </div>
  );
}
