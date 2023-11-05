import React, { useState } from 'react';
import { FaStar } from 'react-icons/fa';

const Ratings = () => {
  const [rating, setRating] = useState(0);
  const [numStars, setNumStars] = useState(5);

  const handleRatingChange = (newRating) => {
    setRating(newRating);
  };

  const handleSliderChange = (event) => {
    const newNumStars = parseInt(event.target.value, 10);
    setNumStars(newNumStars);
    setRating(0);
  };

  return (
    <div>
      {[...Array(numStars)].map((_, index) => {
        const starValue = index + 1;
        return (
          <span
            key={index}
            className={`star ${starValue <= rating ? 'active' : ''}`}
            onClick={() => handleRatingChange(starValue)}
          >
            <FaStar size={40} style={{ color: starValue <= rating ? '#FFC000' : 'gray' }} />
          </span>
        );
      })}

      <p className="mx-4 mt-2 fw-medium">Selected rating: {rating}</p>

        <label className="d-flex align-items-center">
        <span className="mr-2 fw-medium">Select the number of stars:</span>
        <input
            className="mx-2 mt-1"
            type="range"
            min="3"
            max="10"
            step="1"
            value={numStars}
            onChange={handleSliderChange}
        />
        <span>{numStars}</span>
        </label>

    </div>
  );
};

export default Ratings;
