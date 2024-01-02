import React, { useState } from "react";
import { fetchDataOnSignalR } from "../services/signalR";
import "../styles/searchbar.css";
const SearchBar = ({
  searchPhrase,
  setSearchPhrase,
  minPrice,
  setMinPrice,
  maxPrice,
  setMaxPrice,
}) => {
  const fetchData = async () => {
    try {
      await fetchDataOnSignalR(searchPhrase, minPrice, maxPrice, 1);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  return (
    <div className="search-bar">
      <input
        type="text"
        placeholder="Search gemstones..."
        value={searchPhrase}
        onChange={(e) => setSearchPhrase(e.target.value)}
      />
      <input
        type="number"
        placeholder="Min Price"
        value={minPrice}
        onChange={(e) => setMinPrice(e.target.value)}
      />
      <input
        type="number"
        placeholder="Max Price"
        value={maxPrice}
        onChange={(e) => setMaxPrice(e.target.value)}
      />
      <button onClick={fetchData}>Search</button>
    </div>
  );
};

export default SearchBar;
