import axios from "axios";

const API = axios.create({
  baseURL: "http://localhost:5163/api" // your backend URL
});

// ✅ Add token automatically
API.interceptors.request.use((req) => {
  const token = localStorage.getItem("token");

  if (token) {
    req.headers.Authorization = `Bearer ${token}`;
  }

  return req;
});

export default API;