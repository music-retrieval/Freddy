import React, { useEffect, useState } from 'react';

const WeatherForecast = () => {
    const [forecast, setForecast] = useState([]);

    useEffect(() => {
        fetch('/api/weatherforecast/') // Assuming your backend is running on the same host
            .then(response => response.json())
            .then(data => setForecast(data))
            .catch(error => console.error('Error fetching weather forecast:', error));
    }, []);
    console.log(forecast)
    return (
        <div>
            <h2>Weather Forecast</h2>
            <ul>
                {forecast.map((item, index) => (
                    <li key={index}>
                        Date: {item && item.Date}<br />
                        Temperature: {item && item.TemperatureC}°C / {item && item.TemperatureF}°F<br />
                        Summary: {item && item.summary}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default WeatherForecast;
