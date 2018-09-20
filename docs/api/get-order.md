# Get Order

Web API Call for fetching order by id.

## Specification:

 * **Url Path:** /api/orders/{orderId} 

 * **Method:** GET
 
 * **Input Parameters:**
   * url part -  orderId: string
   
 * **Output:**  { id: string, userId: ulong, orderItems:[ { id:string, name: string, description: string, quantity: integer} ] }
 
 
## Request Body Example:
~~~javascript
{ 
  "id": "44B02D90-9CED-4643-94BE-0ECB2245A4D7", 
  "userId": 567, 
  "orderItems": 
  [ 
      { 
        "id": "AB939EAB-1838-412D-B261-CDB417DF4B14", 
        "name": "some name", 
        "description": "some description", 
        "quantity": 45
       } 
  ] 
}
~~~

---
| Return: [Table of Contents](../table-of-contents.md) |
|----|
