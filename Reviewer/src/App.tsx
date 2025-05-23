import { useState } from "react";
import "./App.css";
import ReviewForm from "./components/ReviewForm/ReviewForm";
import ReviewsList from "./components/ReviewsList/ReviewsList";
import type { ReviewData } from "./types/ReviewData";

const STORAGE_KEY = "reviews";

function App() {
  const [reviews, setReviews] = useState<ReviewData[]>(() => {
    const saved = localStorage.getItem(STORAGE_KEY);
    return saved ? JSON.parse(saved) : [];
  });

  const handleAddReview = (newReview: ReviewData) => {
    setReviews((prevReviews) => {
      const updatedReviews = [...prevReviews, newReview];
      localStorage.setItem(STORAGE_KEY, JSON.stringify(updatedReviews));
      return updatedReviews;
    });
  };

  return (
    <>
      <ReviewForm onAddReview={handleAddReview} />
      <ReviewsList reviews={reviews} />
    </>
  );
}

export default App;
