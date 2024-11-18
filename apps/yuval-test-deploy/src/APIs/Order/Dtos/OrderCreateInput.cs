namespace YuvalTestDeploy.APIs.Dtos;

public class OrderCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Id { get; set; }

    public DateTime UpdatedAt { get; set; }
}
