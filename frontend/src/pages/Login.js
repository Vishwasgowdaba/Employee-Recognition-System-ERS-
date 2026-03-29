import { useState,useContext } from "react";
import API from "../services/api";
import { useNavigate } from "react-router-dom";
import "./styles.css";
import bg from "../assets/nexerbg.png";
import logo from "../assets/nexerlogo.png";
import { AuthContext } from "../context/AuthContext";

function Login() {
const navigate = useNavigate();
const { loginUser } = useContext(AuthContext);
 // ✅ ADD THIS
  const [form, setForm] = useState({
    email: "",
    password: ""
  });

 const handleSubmit = async (e) => {
  e.preventDefault();

  try {
    const res = await API.post("/Auth/login", {
      email: form.email,
      password: form.password,
    });

    console.log("LOGIN RESPONSE:", res.data);

    // ✅ store token
    loginUser({
  token: res.data.token,
  name: res.data.name,
  userId: res.data.userId 
});

// store for dashboard usage
localStorage.setItem("name", res.data.name);
localStorage.setItem("userId", res.data.userId);
localStorage.setItem("role", res.data.role);

  navigate("/dashboard");

  }
   catch (err) {
    console.log(err);
    alert("❌ Invalid credentials");
  }
};
  return (
  <div
    className="auth-container"
    style={{
      backgroundImage: `url(${bg})`,
      backgroundSize: "cover",
      backgroundPosition: "center"
    }}
  >
    <form className="auth-card" onSubmit={handleSubmit}>
      
      {/* Logo */}
      <img src={logo} alt="logo" className="logo" />

      <h2>Employee Recognition System</h2>

      <input
        type="email"
        placeholder="Email"
        onChange={(e) => setForm({ ...form, email: e.target.value })}
      />

      <input
        type="password"
        placeholder="Password"
        onChange={(e) => setForm({ ...form, password: e.target.value })}
      />

      <div className="auth-options">
        <label>
          <input type="checkbox" /><span>Remember me</span>
        </label>
        <span className="link">Forgot password?</span>
      </div>

      <button type="submit" className="auth-btn">
        Sign In
      </button>

      <p className="bottom-text">
        Don't have an account?{" "}
        <span onClick={() => navigate("/register")} className="link"> 
          Create one
        </span>
      </p>
    </form>
  </div>
);
}

export default Login;