namespace TaskManagement.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; } // Optional
        public int? Priority { get; set; } // Optional (1=High, 2=Medium, 3=Low)

    }
}
