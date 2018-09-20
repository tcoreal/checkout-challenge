# [Orders Storage](../../Checkout.CustomerLib/OrdersStorage.cs)

API for fetching existing orders.

## Input constructor parameters:

 * **apiUrl**  - url to Web API 

 * **ILoggerWriter:** - allows to implement own logger(logs nothing by default)  
 
 * **IJsonSerializer:** - allows to implement own json serializer(uses newtonsoft by default)
 
## Methods:
* **GetOrderById** - fetches an [order abstraction](../../Checkout.CustomerLib/DomainModel/Contract/IOrder.cs)  by order id(string).
* **GetAllOrders** - fetches [all orders ](../../Checkout.CustomerLib/DomainModel/Contract/IOrder.cs)  for specific user.

## Example:
~~~javascript
private static async Task PrintStorage(OrdersStorage storage)
{
  foreach (var order in await storage.GetAllOrders())
  {
      Console.WriteLine($"Order with Id:{order.Id}");
      foreach (var orderItem in order.Items)
      {
        PrintOrderItem(orderItem);
      }
   }
}
~~~

---
| Return: [Table of Contents](../table-of-contents.md) |
|----|
