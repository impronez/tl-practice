import { useState } from "react";
import type { ReviewData } from "../types/ReviewData";

const STORAGE_KEY = "reviews";

export function useReviews() {
  const [reviews, setReviews] = useState<ReviewData[]>(() => {
    const saved = localStorage.getItem(STORAGE_KEY);
    return saved ? JSON.parse(saved) : [];
  });

  const addReview = (newReview: ReviewData) => {
    setReviews((prev) => {
      const updated = [...prev, newReview];
      localStorage.setItem(STORAGE_KEY, JSON.stringify(updated));
      return updated;
    });
  };

  return { reviews, addReview };
}
