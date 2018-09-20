# [Orders Creator](../../Checkout.CustomerLib/OrdersCreator.cs)

API for easy creation of orders.

## Input constructor parameters:

 * **apiUrl**  - url to Web API 

 * **ILoggerWriter:** - allows to implement own logger(logs nothing by default)  
 
 * **IJsonSerializer:** - allows to implement own json serializer(uses newtonsoft by default)
 
## Methods:
* *CreateOrder**  - starts a fluent api call with folowed methods
   * **WithItem** -  virtually adds order item to a newly crated order 
   * **Build** - commits an api call, creating order and items. Retuns id of newly created order   

## Example:
~~~javascript
var creator = new OrdersCreator(ApiUrl, new ConsoleLoggerWriter());
return await creator.CreateOrder()
    .WithItem("Iphone Xs", "Brand new Iphone", 10)
    .WithItem("Iphone Xs Max", "Brand new Iphone", 3)
    .WithItem("Samsung Galaxy 9", "Brand new Samsung", 5)
    .Build();
~~~

---
| Return: [Table of Contents](../table-of-contents.md) |
|----|
