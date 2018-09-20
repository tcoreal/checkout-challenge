# Change Quantity

Web API Call for changing quantity of order.

## Specification:

 * **Url Path:** /api/orders/changeOrderItem/{orderId} 

 * **Method:** POST 
 
 * **Input Parameters:**
   * url part -  orderId: string
   * body - { orderItemId: string, quantity: integer }
   
 * **Output:** None
 * **Errors:** Returns an error if order or order item does not exist
 
## Request Body Example:
~~~javascript
{
 "orderItemId": "CE44F6F9-F03D-474F-8D76-4673CCD41C5A", 
 "quantity": 30
}
~~~

---
| Return: [Table of Contents](../table-of-contents.md) |
|----|
