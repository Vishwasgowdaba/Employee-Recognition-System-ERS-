import { useEffect, useState } from "react";
import API from "../services/api";

export default function Leaderboard() {
  const [data, setData] = useState([]);

  useEffect(() => {
    fetchLeaderboard();
  }, []);

  const fetchLeaderboard = async () => {
    try {
      const res = await API.get("/Dashboard/leaderboard");
      setData(res.data?.data || []);
    } catch (err) {
      console.log(err);
    }
  };
  const getMedal = (index) => {
  if (index === 0) return "🥇";
  if (index === 1) return "🥈";
  if (index === 2) return "🥉";
  return "🏅";
};

  return (
    <div className="leaderboard-container">
      

      {data.length === 0 ? (
        <p>No leaderboard data</p>
      ) : (
        data.map((item, index) => (
          <div
            key={index}
            className={`leaderboard-row ${
              index === 0 ? "gold" :
              index === 1 ? "silver" :
              index === 2 ? "bronze" : ""
            }`}
          >
            {/* LEFT */}
            <div className="left">
              <span className="rank">#{index + 1}</span>
              <span className="name">{item.name}</span>
            </div>

            {/* RIGHT */}
            <div className="right">
              <span className="points">{item.points} pts</span>
            </div>
          </div>
        ))
      )}
    </div>
  );
}