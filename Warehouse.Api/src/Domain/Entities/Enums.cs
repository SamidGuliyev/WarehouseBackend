namespace Warehouse.Api.src.Domain.Entities;

public enum ProductUnit : byte
{
    Box = 1,
    Block = 2
}

public enum OrderStatus : byte
{
    Pending,
    Completed
}