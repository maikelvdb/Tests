import { useEffect, useState } from "react";
import "./App.css";
import { sign } from "crypto";

interface Forecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

function App() {
  const [login, setLogin] = useState(false);
  const [forecasts, setForecasts] = useState<Forecast[]>();

  useEffect(() => {}, []);

  const contents =
    forecasts === undefined ? (
      <p>
        <em>
          Loading... Please refresh once the ASP.NET backend has started. See{" "}
          <a href="https://aka.ms/jspsintegrationreact">
            https://aka.ms/jspsintegrationreact
          </a>{" "}
          for more details.
        </em>
      </p>
    ) : (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
          </tr>
        </thead>
        <tbody>
          {forecasts.map((forecast) => (
            <tr key={forecast.date}>
              <td>{forecast.date}</td>
              <td>{forecast.temperatureC}</td>
              <td>{forecast.temperatureF}</td>
              <td>{forecast.summary}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );

  return (
    <div>
      <h1 id="tableLabel">Weather forecast</h1>
      <p>This component demonstrates fetching data from the server.</p>

      {!login ? (
        <button type="button" className="btn btn-primary" onClick={loginCall}>
          Login
        </button>
      ) : (
        <button type="button" className="btn btn-primary" onClick={signoutCall}>
          Signout
        </button>
      )}
      <button
        type="button"
        className="btn btn-primary"
        onClick={populateWeatherData}
      >
        Load Weather Data
      </button>
      {contents}
    </div>
  );

  async function populateWeatherData() {
    const response = await fetch("weatherforecast");
    if (response.ok) {
      const data = await response.json();
      setForecasts(data);
    }
  }

  async function signoutCall() {
    const response = await fetch("signout");
    if (response.ok) {
      setLogin(false);
    }
  }

  async function loginCall() {
    const response = await fetch("login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: "test",
        password: "password",
      }),
    });

    if (response.ok) {
      setLogin(true);
    } else {
      setLogin(false);
    }
  }
}

export default App;
