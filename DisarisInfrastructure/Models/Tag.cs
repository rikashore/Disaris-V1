using System.ComponentModel.DataAnnotations;

public class Tag
{
    [Key]
    public string Name { get; set; }

    public string Content { get; set; }
}