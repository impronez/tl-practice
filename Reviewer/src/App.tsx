import "./App.css";
import ReviewForm from "./components/ReviewForm/ReviewForm";
import ReviewsList from "./components/ReviewsList/ReviewsList";
import { useReviews } from "./hooks/useReviews";

function App() {
  const { reviews, addReview } = useReviews();

  return (
    <>
      <ReviewForm onAddReview={addReview} />
      <ReviewsList reviews={reviews} />
    </>
  );
}

export default App;
