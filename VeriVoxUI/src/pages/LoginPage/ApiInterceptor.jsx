async function ApiInterceptor(apiUrl, method, _data = null) {
  const token = sessionStorage.getItem("jwtToken");
  try {
    const modifiedOptions = {
      method: method,
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
    };
    if (_data) {
      modifiedOptions.body = JSON.stringify(_data);
    }
    const response = await fetch(apiUrl, modifiedOptions);

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    const contentType = response.headers.get("content-type");
    if (contentType && contentType.includes("application/json")) {
      const data = await response.json();

      return data;
    } else {
      const data = await response.text();
      return data;
    }
  } catch (error) {
    //console.error("Error fetching data:", error);
    throw error;
  }
}
export default ApiInterceptor;
