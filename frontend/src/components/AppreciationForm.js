import { useState, useEffect } from "react";
//import axios from "axios";
import API from "../services/api";

export default function AppreciationForm(props) {
  const senderId = localStorage.getItem("userId");
  const [employees, setEmployees] = useState([]);
  const [form, setForm] = useState({
    senderId: "",
    receiverId: "",
    message: "",
  });
  useEffect(() => {
  fetchEmployees();
  }, []);

  const fetchEmployees = async () => {
  try {
    const res = await API.get("/Employee");
    setEmployees(res.data.data);
  } catch (err) {
    console.log(err);
  }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await API.post("/Appreciation", {
        senderId: Number(form.senderId),
        receiverId: Number(form.receiverId),
        message: form.message,
      });

      alert("✅ Appreciation sent successfully");
      props.refreshRank();
       props.refresh();        
      props.refreshPoints();  

      setForm({
        senderId: "",
        receiverId: "",
        message: "",
      });

    } catch (err) {
      console.log(err);
      alert("❌ Error sending appreciation");
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ display: "flex", flexDirection: "column", gap: "10px" }}>
      <input className="auth-input"
        type="number"
        placeholder="Sender ID"
        value={form.senderId}
        onChange={(e) =>
          setForm({ ...form, senderId: e.target.value })
        }
      />

      {/* <input
        type="number"
        placeholder="Receiver ID"
        value={form.receiverId}
        onChange={(e) =>
          setForm({ ...form, receiverId: e.target.value })
        }
      /> */}
      <select
  value={form.receiverId}
onChange={(e) =>
  setForm({ ...form, receiverId: e.target.value })
}
  className="auth-input"
>
  <option value="">Select Employee</option>

{employees
  .filter((emp) => emp.employeeId != senderId)
  .map((emp) => (
    <option key={emp.employeeId} value={emp.employeeId}>
      {emp.name}
    </option>
))}

</select>

      <textarea
        placeholder="Write appreciation..."
        value={form.message}
        onChange={(e) =>
          setForm({ ...form, message: e.target.value })
        }
      />

      <button type="submit">Send Appreciation</button>
    </form>
  );
}