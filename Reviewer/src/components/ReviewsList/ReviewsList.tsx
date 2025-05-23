import styles from "./ReviewsList.module.css";
import avatar from "../../assets/avatar.jpg";
import type { ReviewData } from "../../types/ReviewData";

interface ReviewsListProps {
  reviews: ReviewData[];
}

export default function ReviewsList({ reviews }: ReviewsListProps) {
  return (
    <div className={styles.reviewsListContainer}>
      {reviews.map((review) => (
        <div className={styles.reviewContainer} key={review.id}>
          <img className={styles.avatar} src={avatar} />
          <div className={styles.reviewCcontent}>
            <div className={styles.reviewInfoContainer}>
              <span className={styles.username}>{review.name}</span>
              <span className={styles.mark}>{review.rating}/5</span>
            </div>
            <p className={styles.reviewComment}>{review.comment}</p>
          </div>
        </div>
      ))}
    </div>
  );
}
