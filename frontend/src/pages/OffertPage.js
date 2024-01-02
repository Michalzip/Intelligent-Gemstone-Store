import Offerts from "../components/offerts";
import SearchBar from "../components/searchBar";
import Pagination from "../components/pagination";
import React, { useState, useEffect } from "react";

import "../styles/shared.css";
const OffertPage = () => {
  const [pageNumber, setPageNumber] = useState(1);
  const [searchPhrase, setSearchPhrase] = useState("");
  const [minPrice, setMinPrice] = useState(0);
  const [maxPrice, setMaxPrice] = useState(0);

  return (
    <div>
      <div className="title">Gemstone Store</div>

      <SearchBar
        searchPhrase={searchPhrase}
        setSearchPhrase={setSearchPhrase}
        minPrice={minPrice}
        setMinPrice={setMinPrice}
        maxPrice={maxPrice}
        setMaxPrice={setMaxPrice}
      />
      <Offerts
        searchPhrase={searchPhrase}
        minPrice={minPrice}
        maxPrice={maxPrice}
        pageNumber={pageNumber}
      />

      <Pagination pageNumber={pageNumber} setPageNumber={setPageNumber} />
    </div>
  );
};

export default OffertPage;
