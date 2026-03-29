import { useState, useEffect } from "react";
import API from "../services/api";

export default function NominationForm({refreshData}) {
  const [categories, setCategories] = useState([]);
  const managerId = localStorage.getItem("userId");
  const [employees, setEmployees] = useState([]);
  const [form, setForm] = useState({
    employeeId: "",
    awardCategoryId: ""
  });
  useEffect(() => {
  fetchEmployees();
  fetchCategories();
}, []);

const fetchEmployees = async () => {
  try {
    const res = await API.get("/Employee");
    setEmployees(res.data.data);
  } catch (err) {
    console.log(err);
  }
};

const fetchCategories = async () => {
  try {
    const res = await API.get("/Award");  // or your endpoint
     console.log("CATEGORIES:", res.data);
     setCategories(res.data.data || res.data);
  } catch (err) {
    console.log(err);
  }
};

  const handleSubmit = async (e) => {
  e.preventDefault();

  const payload = {
    nominatedById: Number(localStorage.getItem("userId")),
    employeeId: Number(form.employeeId),
    awardCategoryId: Number(form.awardCategoryId)
  };

  console.log("PAYLOAD:", payload); // 🔥 ADD THIS

  try {
    await API.post("/Nomination", payload);

    alert("✅ Nomination submitted successfully");
    refreshData();
   

    setForm({
      employeeId: "",
      awardCategoryId: ""
    });

  } catch (err) {
  const message = err.response?.data;

  if (message?.includes("Already nominated")) {
    alert("⚠️ This employee is already nominated!");
  } else {
    alert("Error submitting nomination");
  }

  console.log(err.response);
}
};

  return (
    <form className="form-ui" onSubmit={handleSubmit}>
      
      

      <select
  value={form.employeeId}
  onChange={(e) =>
    setForm({ ...form, employeeId: e.target.value })
  }
>
  <option value="">Select Employee</option>

  {employees
    .filter((emp) => emp.employeeId != managerId)
    .map((emp) => (
      <option key={emp.employeeId} value={emp.employeeId}>
        {emp.name}
      </option>
  ))}
</select>

      <select
  value={form.awardCategoryId}
  onChange={(e) =>
    setForm({ ...form, awardCategoryId: e.target.value })
  }
>
  <option value="">Select Category</option>

  {categories.map((cat) => (
    <option key={cat.id} value={cat.id}>
  {cat.name} ({cat.points} pts)
</option>
  ))}

</select>

      <button type="submit">Submit Nomination</button>
    </form>
  );
}