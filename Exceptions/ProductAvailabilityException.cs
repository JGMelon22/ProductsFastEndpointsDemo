namespace ProductsFastEndpointsDemo.Exceptions;

public class ProductAvailabilityException : Exception
{
    public int Quantity { get; }
    public bool IsAvailable { get; set; }

    public ProductAvailabilityException(int quantity, bool isAvailable) :
        base($"Product quantity can not be {quantity} if availability is {isAvailable}")
    {
        Quantity = quantity;
        IsAvailable = isAvailable;
    }
}
