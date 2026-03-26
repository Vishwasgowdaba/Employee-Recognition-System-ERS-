import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export default function Navbar() {
  const { logoutUser } = useContext(AuthContext);

  return (
    <div style={{
      display: "flex",
      justifyContent: "space-between",
      padding: "15px 20px",
      background: "#6366f1",
      color: "white",
      borderRadius: "10px",
      marginBottom: "20px"
    }}>
      <h3>Recognition System</h3>
      <button onClick={logoutUser} style={{ background: "white", color: "#6366f1" }}>
        Logout
      </button>
    </div>
  );
}