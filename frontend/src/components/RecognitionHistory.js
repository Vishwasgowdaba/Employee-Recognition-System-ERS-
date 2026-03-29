import { useEffect, useState } from "react";
import API from "../services/api";

// 🔥 ADDED props (data)
export default function RecognitionHistory({ data: externalData }) {

  const [data, setData] = useState([]);
  const currentusername = localStorage.getItem("name");

  // 🔥 UPDATED useEffect (handles both cases)
  useEffect(() => {
    if (externalData) {
      setData(externalData);   // 🔥 use dashboard data
    } else {
      fetchActivity();         // fallback to API
    }
  }, [externalData]);

  // ✅ your original function (unchanged)
  const fetchActivity = async () => {
    try {
      const res = await API.get("/Appreciation");
      setData(res.data || []);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <div>
      {data.map((item, index) => (
        <div key={index} className="history-item">

          {/* LEFT SIDE */}
          <div className="left">
            👏 {item.senderName === currentusername ? "You" : item.senderName} appreciated{" "}
            {item.receiverName === currentusername ? "You" : item.receiverName}
          </div>

          {/* RIGHT SIDE */}
          <div className="right">
            {item.message}
          </div>

        </div>
      ))}
    </div>
  );
}