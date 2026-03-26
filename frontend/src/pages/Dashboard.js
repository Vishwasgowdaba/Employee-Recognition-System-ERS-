import { useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import Charts from "../components/Charts";
import Leaderboard from "../components/Leaderboard";
import AppreciationForm from "../components/AppreciationForm";
import NominationForm from "../components/NominationForm";
import RecognitionHistory from "../components/RecognitionHistory";

export default function Dashboard() {
  const { logoutUser } = useContext(AuthContext);
  const [tab, setTab] = useState("dashboard"); // ⭐ control view

  return (
    <div className="main-full">
        <div className="container">
      {/* Topbar */}
      <div className="topbar">
        <h2>🏆 Employee Recognition System</h2>
        <button onClick={logoutUser}>Logout</button>
      </div>

      {/* NAV MENU */}
      <div className="nav-tabs">
  <button
    className={tab === "dashboard" ? "active" : ""}
    onClick={() => setTab("dashboard")}
  >
    Dashboard
  </button>

  <button
    className={tab === "appreciation" ? "active" : ""}
    onClick={() => setTab("appreciation")}
  >
    Appreciation
  </button>

  <button
    className={tab === "nomination" ? "active" : ""}
    onClick={() => setTab("nomination")}
  >
    Nomination
  </button>

  <button
    className={tab === "leaderboard" ? "active" : ""}
    onClick={() => setTab("leaderboard")}
  >
    Leaderboard
  </button>
</div>
      {/* CONTENT SWITCH */}
      {tab === "dashboard" && (
        <>
          <div className="grid">
            <div className="card stat">
              <h3>120</h3>
              <p>Total Points Earned</p>
            </div>
            <div className="card">
  <RecognitionHistory />
</div>
            <div className="card stat">
              <h3>15</h3>
              <p>Recognitions This Month</p>
            </div>
            <div className="card stat">
              <h3>#2</h3>
              <p>Your Rank</p>
            </div>
          </div>

          <div className="card" style={{ marginTop: "20px" }}>
            <Charts />
          </div>
        </>
      )}

      {tab === "appreciation" && (
        <div className="container-center">
        <div className="container-center">
        <div className="card">
          <AppreciationForm />
        </div>
        </div>
        </div>
      )}

      {tab === "nomination" && (
          <div className="container-center">
        <div className="container-center">
        <div className="card">
          <NominationForm />
        </div>
        </div> 
        </div>     
      )}

      {tab === "leaderboard" && (
        <div className="container-center">
        <div className="container-center">
        <div className="card">
          <Leaderboard />
        </div>
        </div>
        </div>
      )}
      </div>
    </div>
  );
}