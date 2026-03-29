import { useEffect, useState } from "react";
import API from "../services/api";

export default function NominationList(props) {
  const [nominations, setNominations] = useState([]);
  const fetchNominations = async () => {
  try {
    const res = await API.get("/Nomination");

    console.log("NOMINATIONS:", res.data); // 🔥 ADD

    setNominations(res.data.data || res.data); // ✅ FIX

  } catch (err) {
    console.log(err);
  }
};
 useEffect(() => {
    fetchNominations();   // initial load
  }, []);

  // 🔥 ADD THIS (TRIGGERS AFTER REFRESH)
  useEffect(() => {
    if (props.refresh) {
      fetchNominations();
    }
  }, [props.refresh]);

  const updateStatus = async (id, status) => {
    try {
      await API.put(`/Nomination/${id}/status`, {
        status: status
        
      });

      alert(`✅ ${status} successfully`);
      fetchNominations();
      props.refreshPoints();
      props.refreshRank();
      props.refresh();

    } catch (err) {
      console.log(err);
    }
   
  };

  return (
    <div className="card form-card">
      <h2>📋 Nominations</h2>

      {nominations.map((item) => (
        <div key={item.id} className="history-item">

          <div key={item.id} className="history-item">

  {/* LEFT SIDE */}
  <div className="left">
  🏆 {item.employeeName} nominated for {item.awardName}
</div>

  {/* RIGHT SIDE */}
  <div className="right">

    {/* ✅ STATUS BADGE */}
    <span className={`status ${item.status?.toLowerCase()}`}>
      {item.status}
    </span>

    {/* ACTION BUTTONS */}
    <button
  disabled={item.status !== "Pending"}
  onClick={() => updateStatus(item.id, "Approved")}
>
  ✅
</button>

<button style={{ marginLeft: "20px" }}
  disabled={item.status !== "Pending"}
  onClick={() => updateStatus(item.id, "Rejected")}
>
  ❌
</button>

  </div>

</div>

        </div>
      ))}
    </div>
  );
}