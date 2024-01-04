import * as signalR from "@microsoft/signalr";
import { fetchGemstoneData } from "../api/fetchGemstoneData";

let connection;

export const initializeSignalRConnection = (handleReceivedMessage) => {
  if (!connection) {
    connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7294/WebSocketMessageHub")
      .build();

    connection
      .start()
      .then(() => {
        console.log("SignalR connection established");
      })
      .catch((error) => {
        console.error("SignalR connection error: ", error);
      });

    connection.on("clientgotmessage", handleReceivedMessage);

    connection.onclose(() => {
      console.log("SignalR connection closed");
    });
  }
};

export const fetchDataOnSignalR = async (
  searchPhrase,
  minPrice,
  maxPrice,
  pageNumber
) => {
  try {
    await fetchGemstoneData(searchPhrase, minPrice, maxPrice, pageNumber);
  } catch (error) {
    console.error("Error fetching data:", error);
  }
};
