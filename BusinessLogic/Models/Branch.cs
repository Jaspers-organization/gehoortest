using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Models;

public class Branch
{
    public int Id { get; set; }

    public string? Name { get; set; }
}
