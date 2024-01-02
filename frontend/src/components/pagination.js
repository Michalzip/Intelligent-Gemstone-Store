import React from "react";
import "../styles/pagination.css";
const Pagination = ({ pageNumber, setPageNumber }) => {
  return (
    <div className="pagination-buttons">
      <button
        onClick={() => setPageNumber((prevPage) => prevPage - 1)}
        disabled={pageNumber === 1}
      >
        Previous Page
      </button>
      <button onClick={() => setPageNumber((prevPage) => prevPage + 1)}>
        Next Page
      </button>
    </div>
  );
};

export default Pagination;
