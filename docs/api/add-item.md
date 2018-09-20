# Add Item

Web API for adding item to existing order.

## Specification:

 * **Url Path:** /api/orders/addToOrder/{orderId} 

 * **Method:** POST 
 
 * **Input Parameters:**
   * url part -  orderId: string
   * body - { name: string, description: string, quantity: integer }
   
 * **Output:** Id of new order item(string)
 * **Errors:** Returns an error if order is not exist
 
## Request Body Example:
~~~javascript
{
 "name": "Some Name", 
 "description": "Some Description", 
 "quantity": 50
}
~~~

---
| Return: [Table of Contents](../table-of-contents.md) |
|----|
