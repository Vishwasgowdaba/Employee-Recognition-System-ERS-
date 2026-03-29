import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import API from "../services/api";
import { AuthContext } from "../context/AuthContext";
import "./styles.css";
import logo from "../assets/nexerlogo.png";
import bg from "../assets/nexerbg.png";

function Register() {
  const navigate = useNavigate();
  const { loginUser } = useContext(AuthContext);

  const [form, setForm] = useState({
    name: "",
    email: "",
    password: "",
    role: ""
  });

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const res = await API.post("/Auth/register", {
        name: form.name,
        email: form.email,
        password: form.password,
        role: form.role,
      });

      console.log("REGISTER RESPONSE:", res.data);

      // ✅ Auto login
      loginUser({
        token: res.data.token,
        name: res.data.name,
        userId: res.data.userId,
        role: res.data.role,
      });

      alert("✅ Registered & Logged in!");

      navigate("/dashboard");
    } catch (err) {
      console.error(err);
      alert("❌ Registration failed");
    }
  };

  return (
    <div
      className="auth-container"
      style={{
        backgroundImage: `url(${bg})`,
        backgroundSize: "cover",
        backgroundPosition: "center",
      }}
    >
      <form className="auth-card" onSubmit={handleSubmit}>
        <img src={logo} alt="logo" className="logo" />

        <h2>Create Account</h2>

        <input
          type="text"
          placeholder="Full Name"
          onChange={(e) =>
            setForm({ ...form, name: e.target.value })
          }
        />

        <input
          type="email"
          placeholder="Email"
          onChange={(e) =>
            setForm({ ...form, email: e.target.value })
          }
        />

        <input
          type="password"
          placeholder="Password"
          onChange={(e) =>
            setForm({ ...form, password: e.target.value })
          }
        />
        <select
  onChange={(e) =>
    setForm({ ...form, role: e.target.value })
  }
  className="auth-input"
>
  <option value="">Select Role</option>
  <option value="Employee">Employee</option>
  <option value="Manager">Manager</option>
</select>

        <button type="submit" className="auth-btn">
          Register
        </button>

        <p className="bottom-text">
          Already have an account?{" "}
          <span onClick={() => navigate("/")} className="link">
            Sign In
          </span>
        </p>
      </form>
    </div>
  );
}

export default Register;