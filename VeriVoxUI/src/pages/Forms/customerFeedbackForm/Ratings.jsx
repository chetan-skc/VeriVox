import React,{useState} from 'react'
import { Icon } from 'react-icons-kit';
import {starEmpty} from 'react-icons-kit/icomoon/starEmpty'
import { FaStar } from 'react-icons/fa';

const Ratings = () => {
    const [rating, setRating] = useState(0);

  const handleRatingChange = (newRating) => {
    setRating(newRating);
  };
    
  return (
    <div>
        {[1, 2, 3, 4, 5].map((starValue) => (
        <span
          key={starValue}
          className={`star ${starValue <= rating ? 'active' : ''}`}
          onClick={() => handleRatingChange(starValue)}
        >
          <FaStar size={40} style={{ color: starValue <= rating ? '#FFC000' : 'gray', backgroundColor:'white' }} />
        </span>
      ))}
    </div>
  )
}

export default Ratings;
