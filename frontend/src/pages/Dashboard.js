import { useState, useContext, useEffect } from "react";
import { AuthContext } from "../context/AuthContext";
import "./dashboard.css";
import API from "../services/api";      
import Leaderboard from "../components/Leaderboard";
import AppreciationForm from "../components/AppreciationForm";
import NominationList from "../components/NominationList";
import NominationForm from "../components/NominationForm";
import RecognitionHistory from "../components/RecognitionHistory";

export default function Dashboard() {
  const { logoutUser } = useContext(AuthContext);

  const [recognitionCount, setRecognitionCount] = useState(0);
  const name = localStorage.getItem("name");
  const userId = localStorage.getItem("userId");
  const role = localStorage.getItem("role");

  const [points, setPoints] = useState(0);
  const [tab, setTab] = useState("dashboard");
  const [darkMode, setDarkMode] = useState(true);
  const [rank, setRank] = useState("-");

  useEffect(() => {
    fetchRank();
    fetchPoints();
    fetchRecognitionCount();
    fetchAppreciations();
    fetchNominations();
  }, []);
  const [appreciations, setAppreciations] = useState([]);
  const givenCount = appreciations.filter(
    (item) => item.senderId == Number(userId)
  ).length;

  const receivedCount = appreciations.filter(
    (item) => item.receiverId == Number(userId)
  ).length;

const fetchAppreciations = async () => {
  try {
    const res = await API.get("/Appreciation");
    setAppreciations(res.data); // ✅ your API returns array directly
  } catch (err) {
    console.log(err);
  }
};
  const fetchRecognitionCount = async () => {
  try {
    const res = await API.get("/Recognition"); // adjust if your API differs
    const list = res.data.data;

    const count = list.filter(
      (item) => item.senderId == Number(userId)
    ).length;

    setRecognitionCount(count);
  } catch (err) {
    console.log(err);
  }
};

  const fetchRank = async () => {
    try {
      const res = await API.get("/Dashboard/leaderboard");
      const list = res.data.data;

      const index = list.findIndex(
        (item) => item.employeeId == Number(userId)
      );

      if (index !== -1) {
        setRank(index + 1);
      }

    } catch (err) {
      console.log(err);
    }
  };
  const [nominations, setNominations] = useState([]);

const fetchNominations = async () => {
  try {
    const res = await API.get("/Nomination");
    setNominations(res.data.data || []);
  } catch (err) {
    console.log(err);
  }
};

  const fetchPoints = async () => {
    try {
      const res = await API.get("/Employee");
      const list = res.data.data;

      const user = list.find(
        (item) => item.employeeId == Number(userId)
      );

      if (user) {
        setPoints(user.points);
      }

    } catch (err) {
      console.log(err);
    }
  };

  return (
    <div className={darkMode ? "dashboard-container dark" : "dashboard-container light"}>

      {/* 🔝 TOPBAR */}
      <div className="topbar">
        <h1>🏆 Employee Recognition System</h1>

        <div className="topbar-right">
          <button onClick={() => setDarkMode(!darkMode)}>
            {darkMode ? "🌙" : "☀️"}
          </button>

          <button 
            className={tab === "dashboard" ? "active" : ""}
            onClick={() => setTab("dashboard")}
          >
            Dashboard
          </button>

          <button 
            className={tab === "leaderboard" ? "active" : ""}
            onClick={() => setTab("leaderboard")}
          >
            Leaderboard
          </button>

          <div className="user-section">
          👤 {name} (ID: {userId})
          <button className="logout-btn" onClick={logoutUser}>Logout</button>
          </div>
        </div>
      </div>

      {/* 📊 DASHBOARD */}
      {tab === "dashboard" && (
        <>
          {/* STATS */}
          <div className="grid">
            <div className="card stat">
              <h3>⭐{points}</h3>
              <p>Total Points</p>
            </div>

            <div className="card stat">
              <h3 className="rank">#{rank}</h3>
              <p>Leaderboard Rank</p>
            </div>

            <div className="card stat">
            <h3>👏{givenCount}</h3>
            <p>Recognitions Given</p>
            </div>

           <div className="card stat">
            <h3>🎉{receivedCount}</h3>
            <p>Recognitions Received</p>
            </div>
          </div>

          

        <div className="card form-card history-card">
    <h2>📜 Recognition History</h2>
    <RecognitionHistory data={appreciations} />
  </div>
          {/* FORMS */}
         
            <div className="card form-card">
              <h2>👏 Send Appreciation</h2>
              <AppreciationForm 
                refreshRank={fetchRank}  
                refreshPoints={fetchPoints}
                 refresh={fetchAppreciations}
              />
            </div>

            {role === "Manager" && (
              <div className="card form-card">
                <h2>🏆 Send Nomination</h2>
                <NominationForm refreshData={fetchAppreciations}/>
              </div>
            )}
         

          {/* NOMINATION LIST */}
          {role === "Manager" && (
            <NominationList refreshData={fetchAppreciations}/>
          )}
        </>
      )}

      {/* 🏆 LEADERBOARD */}
      {tab === "leaderboard" && (
        <div className="card leaderboard-section">
          <h2>📊 Leaderboard</h2>
          <Leaderboard />
        </div>
      )}
    </div>
  );
}