using DotnetCoding.Core.Models;

public partial class ProductDetail : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public int ApprovalStatus { get; set; }
    public int? ApproveReason { get; set; }
    public int? ApprovedBy { get; set; }
    public DateTime? ApproveDate { get; set; }
    public int UserId { get; set; }

    public virtual UserDetail? User { get; set; }
}