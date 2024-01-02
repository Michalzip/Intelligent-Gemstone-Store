import React, { useState, useEffect } from "react";
import {
  initializeSignalRConnection,
  fetchDataOnSignalR,
} from "../services/signalR";
import "../styles/offerts.css";

const Offerts = ({ searchPhrase, minPrice, maxPrice, pageNumber }) => {
  const [messages, setMessages] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    initializeSignalRConnection(handleReceivedMessage);
  }, []);

  useEffect(() => {
    fetchData();
  }, [pageNumber]);

  const handleReceivedMessage = (message) => {
    console.log(message);

    const parsedMessage = JSON.parse(message);

    setMessages(parsedMessage);
    setIsLoading(false);
  };

  const fetchData = async () => {
    try {
      await fetchDataOnSignalR(searchPhrase, minPrice, maxPrice, pageNumber);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  return (
    <div>
      <ul className="message-list">
        {isLoading ? (
          <li className="loading">Loading...</li>
        ) : (
          messages.map((msg, index) => (
            <li className="message-item" key={index}>
              <p>Name: {msg.Name}</p>
              <img className="loaded" src={msg.Image} alt={msg.Name} />
              <div className="details">
                <div className="profit">
                  <div className="profit-title">
                    <h3>profitability:</h3>
                  </div>
                  <div
                    className={`profit-value ${
                      msg.ProfitabilityResult >= 80
                        ? "profit-high"
                        : msg.ProfitabilityResult >= 50
                        ? "profit-medium"
                        : "profit-low"
                    }`}
                  >
                    {msg.ProfitabilityResult}
                  </div>
                </div>

                <div className="price">
                  <div className="price-title">
                    <h3>Price:</h3>
                  </div>

                  <div className="price-value"> {msg.CurrentPrice}</div>
                </div>
              </div>
            </li>
          ))
        )}
      </ul>
    </div>
  );
};

export default Offerts;
