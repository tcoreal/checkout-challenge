# Remove Item

Web API for adding item to existing order.

## Specification:

 * **Url Path:** /api/orders/removeOrderItem/{orderId} 

 * **Method:** POST 
 
 * **Input Parameters:**
   * url part -  orderId: string
   * body - { orderItemId: string }
   
 * **Output:** None
 * **Errors:** Returns an error if order does not exist
 
## Request Body Example:
~~~javascript
{
 "orderItemId": "6B012EDE-7E73-431B-B8CF-1177366A0FA6" 
}
~~~

---
| Return: [Table of Contents](../table-of-contents.md) |
|----|
