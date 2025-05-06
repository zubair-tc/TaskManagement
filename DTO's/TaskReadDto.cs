namespace TaskManagement.DTO_s
{
    public class TaskReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }
        public int? Priority { get; set; }
    }
}
