namespace TaskManagement.DTO_s
{
    public class TaskCreateDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? DueDate { get; set; }
        public int? Priority { get; set; }
    }
}
