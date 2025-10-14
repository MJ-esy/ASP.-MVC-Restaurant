using System.ComponentModel.DataAnnotations;

namespace ASP.MVC.DTOs
{
  public class TableDTO
  {
    public int TableId { get; set; }
    public int TableNum { get; set; }
    public int Capacity { get; set; }
  }

  public class UpdateTableDTO
  {
    public int TableId { get; set; }
    public int TableNum { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
  }

  public class TableSummaryDTO
  {
    public int TableId { get; set; }
    public int TableNum { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }
    public string Status { get; set; } = string.Empty;
  }

  public class SetTableAvailabilityDTO
  {
    [Required]
    public bool IsAvailable { get; set; }
  }
}
