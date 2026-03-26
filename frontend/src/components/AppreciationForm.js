import { useState } from "react";
import { sendAppreciation } from "../services/appreciationService";

export default function AppreciationForm() {
  const [form, setForm] = useState({
    senderId: "",
    receiverId: "",
    message: "",
  });

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await sendAppreciation(form);
      alert("✅ Appreciation sent successfully");

      // reset form
      setForm({ senderId: "", receiverId: "", message: "" });
    } catch (err) {
      alert("❌ Error sending appreciation");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h3>👏 Send Appreciation</h3>

      <input
        type="number"
        placeholder="Sender ID"
        value={form.senderId}
        onChange={(e) =>
          setForm({ ...form, senderId: e.target.value })
        }
      />

      <input
        type="number"
        placeholder="Receiver ID"
        value={form.receiverId}
        onChange={(e) =>
          setForm({ ...form, receiverId: e.target.value })
        }
      />

      <input
        type="text"
        placeholder="Message"
        value={form.message}
        onChange={(e) =>
          setForm({ ...form, message: e.target.value })
        }
      />

      <button type="submit">Send Appreciation</button>
    </form>
  );
}