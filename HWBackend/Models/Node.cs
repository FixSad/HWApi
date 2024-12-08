namespace HWBackend.Models;

public class Node
{
    public int Id { get; set; }
    public string? ParentId { get; set; }
    public string Name { get; set; }
}