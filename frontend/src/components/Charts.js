import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
  BarChart,
  Bar,
} from "recharts";

export default function Charts() {
  const data = [
    { name: "Jan", points: 30 },
    { name: "Feb", points: 50 },
    { name: "Mar", points: 80 },
    { name: "Apr", points: 120 },
  ];

  return (
    <div>
      <h3>📊 Performance Overview</h3>

      <ResponsiveContainer width="100%" height={200}>
        <LineChart data={data}>
          <XAxis dataKey="name" />
          <YAxis />
          <Tooltip />
          <Line type="monotone" dataKey="points" stroke="#6366f1" />
        </LineChart>
      </ResponsiveContainer>

      <h3 style={{ marginTop: "20px" }}>🏆 Monthly Points</h3>

      <ResponsiveContainer width="100%" height={200}>
        <BarChart data={data}>
          <XAxis dataKey="name" />
          <YAxis />
          <Tooltip />
          <Bar dataKey="points" fill="#4f46e5" />
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
}