import { createContext, useState, useEffect } from "react";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) setUser({ token });
  }, []);

  const loginUser = (data) => {
    localStorage.setItem("token", data.token);
    setUser(data);
  };

  const logoutUser = () => {
  localStorage.removeItem("token");
  setUser(null);
  window.location.href = "/"; // ⭐ redirect to login
};

  return (
    <AuthContext.Provider value={{ user, loginUser, logoutUser }}>
      {children}
    </AuthContext.Provider>
  );
};