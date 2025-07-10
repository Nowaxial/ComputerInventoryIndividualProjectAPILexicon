namespace ComputerInventory.Core.Request;

public class InventoryRequestParams : RequestParams
{
    public bool IncludeUsers { get; set; } = false;
}