# [IOrder](../../Checkout.CustomerLib/DomainModel/Contract/IOrder.cs)

Abstraction for manupulation with specific order.

## Get Properties

 * **Id**  - id of the order 
 * **Items** - collection of order items [abstractions](../../Checkout.CustomerLib/DomainModel/Contract/IOrderItem.cs)
 
## Methods:
* **Purge** - triggers api call for removing all items from order.
* **AddOrderItem** - triggers api call for adding item to order

## Usage Example:
~~~javascript
await mainOrder.AddOrderItem("Xiaomi Mi10", "Brand new chinese phone", 4);
await mainOrder.Purge();
~~~

---
| Return: [Table of Contents](../table-of-contents.md) |
|----|
