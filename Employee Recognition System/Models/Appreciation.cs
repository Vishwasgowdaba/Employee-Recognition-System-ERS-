using Employee_Recognition_System.Models;

public class Appreciation
{
    public int Id { get; set; }

    public int SenderId { get; set; }
    public Employee Sender { get; set; }   // ✅ REQUIRED

    public int ReceiverId { get; set; }
    public Employee Receiver { get; set; } // ✅ REQUIRED

    public string Message { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}