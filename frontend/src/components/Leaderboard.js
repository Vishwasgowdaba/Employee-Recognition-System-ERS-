export default function Leaderboard() {
  const data = [
    { name: "Alice", points: 120 },
    { name: "Bob", points: 100 },
    { name: "John", points: 90 },
  ];

  return (
    <div>
      <h3>🏅 Leaderboard</h3>

      <div style={{ marginTop: "10px" }}>
        {data.map((user, i) => (
          <div
            key={i}
            style={{
              display: "flex",
              justifyContent: "space-between",
              padding: "10px 10px",
              borderBottom: "5px solid #eee",
              fontSize: "20px",
              marginBottom: "10px",
            }}
          >
            <span>{i + 1}. {user.name}</span>
            <span style={{ color: "#6366f1", fontWeight: "600" }}>
              {user.points} pts
            </span>
          </div>
        ))}
      </div>
    </div>
  );
}