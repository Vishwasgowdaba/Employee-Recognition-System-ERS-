import { useState } from "react";
import axios from "../utils/axiosInstance";

export default function NominationForm() {
  const [form, setForm] = useState({
    senderId: "",
    receiverId: "",
    category: "",
  });

  const handleSubmit = async (e) => {
    e.preventDefault();

    await axios.post("/nomination", form);
    alert("✅ Nomination sent (Email triggered)");
  };

  return (
    <form onSubmit={handleSubmit}>
      <h3>🏆 Nominate Employee</h3>

      <input
        placeholder="Your ID"
        onChange={(e) => setForm({ ...form, senderId: e.target.value })}
      />

      <input
        placeholder="Employee ID"
        onChange={(e) => setForm({ ...form, receiverId: e.target.value })}
      />

      {/* ⭐ CATEGORY DROPDOWN */}
      <select
        onChange={(e) => setForm({ ...form, category: e.target.value })}
      >
        <option value="">Select Award</option>
        <option>Star of the Month</option>
        <option>Employee of the Month</option>
        <option>Employee of the Year</option>
      </select>

      <button type="submit">Nominate</button>
    </form>
  );
}