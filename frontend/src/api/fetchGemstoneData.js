export const fetchGemstoneData = async (
  searchPhrase,
  minPrice,
  maxPrice,
  pageNumber
) => {
  try {
    const response = await fetch(
      `https://localhost:7294/admin/get-list-of-stone?PageNumber=${pageNumber}&phrase=${searchPhrase}&minPrice=${minPrice}&maxPrice=${maxPrice}`,
      {
        method: "GET",
        credentials: "include",
      }
    );

    if (!response.ok) {
      throw new Error("Network response was not ok");
    }
  } catch (error) {
    throw new Error("Error fetching data:", error);
  }
};
