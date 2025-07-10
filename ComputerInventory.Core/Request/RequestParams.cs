using System.ComponentModel.DataAnnotations;

namespace ComputerInventory.Core.Request;

public class RequestParams
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 20;
}