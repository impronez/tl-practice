import { useState } from "react";
import "./App.css";
import ReviewForm from "./components/ReviewForm/ReviewForm";
import ReviewsList from "./components/ReviewsList/ReviewsList";
import type { ReviewData } from "./types/ReviewData";

function App() {
  const [reviews, setReviews] = useState<ReviewData[]>([]);

  const handleAddReview = (newReview: ReviewData) => {
    setReviews((prevReviews) => [...prevReviews, newReview]);
  };

  return (
    <>
      <ReviewForm onAddReview={handleAddReview} />
      <ReviewsList reviews={reviews} />
    </>
  );
}

export default App;
