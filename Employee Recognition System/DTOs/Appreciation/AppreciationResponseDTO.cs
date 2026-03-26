namespace Employee_Recognition_System.DTOs.Appreciation
{
    public class AppreciationResponseDTO
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public string SenderName { get; set; }

        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }

        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}