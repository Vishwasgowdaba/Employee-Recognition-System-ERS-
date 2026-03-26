// import { useState, useContext } from "react";
// import { login } from "../services/authService";
// import { AuthContext } from "../context/AuthContext";
// import { useNavigate } from "react-router-dom";

// export default function Login() {
//   const [form, setForm] = useState({ email: "", password: "" });
//   const { loginUser } = useContext(AuthContext);
//   const navigate = useNavigate();

//   const handleSubmit = async (e) => {
//     e.preventDefault();
//     try {
//       const res = await login(form);
//       loginUser(res.data);
//       navigate("/dashboard");
//     } catch {
//       alert("Login failed");
//     }
//   };

//   return (
//     <form onSubmit={handleSubmit}>
//       <h2>Login</h2>
//       <input placeholder="Email" onChange={(e) => setForm({ ...form, email: e.target.value })} />
//       <input type="password" placeholder="Password" onChange={(e) => setForm({ ...form, password: e.target.value })} />
//       <button type="submit">Login</button>
//     </form>
//   );
// }
// // import { useContext } from "react";
// // import { AuthContext } from "../context/AuthContext";
// // import { useNavigate } from "react-router-dom";

// // export default function Login() {
// //   const { loginUser } = useContext(AuthContext);
// //   const navigate = useNavigate();

// //   const handleSubmit = (e) => {
// //     e.preventDefault();

// //     // FAKE LOGIN
// //     localStorage.setItem("token", "dummy-token");

// //     loginUser({ token: "dummy-token" });
// //     navigate("/dashboard");
// //   };

// //   return (
// //     <form onSubmit={handleSubmit}>
// //       <h2>Login</h2>
// //       <button type="submit">Login</button>
// //     </form>
// //   );
// // }

import { useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const { loginUser } = useContext(AuthContext);
  const navigate = useNavigate();

  const [form, setForm] = useState({
    email: "",
    password: "",
  });

  const handleSubmit = (e) => {
    e.preventDefault();

    // fake login for now
    localStorage.setItem("token", "dummy-token");
    loginUser({ token: "dummy-token" });

    navigate("/dashboard");
  };

  return (
    <div className="auth-container">
      <form className="auth-card" onSubmit={handleSubmit}>
        <h2>Sign In</h2>
        <p className="subtitle">Enter your credentials to continue</p>

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
            <input type="checkbox" /> Remember me
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