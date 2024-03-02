import React, { useEffect, useState } from "react";

const WeatherForecast = () => {
  const [forecast, setForecast] = useState([]);

  useEffect(() => {
    fetch("http://localhost:8080/weatherforecast", {
      method: "GET",
      headers: {
        Accept: "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => setForecast(data))
      .catch((error) =>
        console.error("Error fetching weather forecast:", error)
      );
  }, []);

  return (
    <div>
      <h2>Weather Forecast</h2>
      <ul>
        {forecast &&
          forecast.map((item, index) => (
            <li key={index}>
              Date: {item.date}
              <br />
              Temperature: {item.temperatureC}°C / {item.temperatureF}°F
              <br />
              Summary: {item.summary}
            </li>
          ))}
      </ul>
    </div>
  );
};

export default WeatherForecast;
