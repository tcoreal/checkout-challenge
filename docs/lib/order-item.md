# [IOrderItem](../../Checkout.CustomerLib/DomainModel/Contract/IOrderItem.cs)

Abstraction for manupulation with specific order.

## Get Properties

 * **Id**  - id of the order item
 * **Name**  - name of the order item
 * **Description**  - id description the order item
 * **Quantity**  - quantity of the order item
 * **IsDeleted** - shows if order item was deleted(after calling RemoveItem method)
 
## Methods:
* **ChangeQuantity** - triggers api call for changing quantity of the order item.
* **RemoveItem** - triggers api call for removing current item from order

## Usage Example:
~~~javascript
if (orderItem.Name.Contains("Samsung"))
{
    await orderItem.RemoveItem();
}
~~~

---
| Return: [Table of Contents](../table-of-contents.md) |
|----|
