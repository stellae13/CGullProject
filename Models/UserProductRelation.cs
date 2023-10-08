namespace CGullProject;

public class UserProductRelation {
    public Guid UserId { get; set; }
    public string ProductId { get; set; } = "";
    public int Quantity { get; set; }
}