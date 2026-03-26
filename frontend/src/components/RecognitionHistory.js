export default function RecognitionHistory() {
  const data = [
    { sender: "John", receiver: "Alice", msg: "Great job!" },
    { sender: "Bob", receiver: "Sam", msg: "Well done!" },
  ];

  return (
    <div>
      <h3>📜 Recognition History</h3>

      {data.map((item, i) => (
        <div key={i} className="row">
          <span>
            <b>{item.sender}</b> → {item.receiver}
          </span>
          <span>{item.msg}</span>
        </div>
      ))}
    </div>
  );
}