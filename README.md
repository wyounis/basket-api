# basket-api
A simple shopping basket api using dotnet core


## Givens:
Your company has decided to create a new line of business.  As a start to this effort, they’ve come to you to help develop a prototype.  It is expected that this prototype will be part of a beta test with some actual customers, and if successful, it is likely that the prototype will be expanded into a full product.

Your part of the prototype will be to develop a Web API that will be used by customers to manage a basket of items. The business describes the basic workflow is as follows:

This API will allow our users to set up and manage an order of items.  The API will allow users to add and remove items and change the quantity of the items they want.  They should also be able to simply clear out all items from their order and start again.

The functionality to complete the purchase of the items will be handled separately and will be written by a different team once this prototype is complete.  

For the purpose of this exercise, you can assume there’s an existing data storage solution that the API will use, so you can either create stubs for that functionality or simply hold data in memory.

Feel free to make any assumptions whenever you are not certain about the requirements, but make sure your assumptions are made clear either through the design or additional documentation.

## Based on these Givens:
### Required APIs:
-	Create API to get a user basket.
-	Create API to add an item to a customer basket. 
-	Create API to patch the quantity of item.
-	Create API to remove item from basket.
-	Create API to remove basket.
### Assumptions:
-	Assuming that the client holds the user identifier from a previous step/service.
-	Assuming that a user will always have exactly one basket, no more (so the user id can be used as a unique identifier for his basket) (Customer Id = Basket Id).
-	Assuming that the items and prices will be provided as an input for the basket API (In a microservice architecture, items and their details should be handled in a different service).
-	Ignoring the item taxes (Assuming it will be calculated and displayed only after order placement).
-	Assuming a unified currency.
-	Assuming the price of items will not change (If the user create a cart will).
-	Assuming we’ll receive a valid user id always (no customer id validation).
-	Assuming we’ll receive a valid item id always (no items validation).
### Used technologies and plugins:
-	The project is using Dotnet core 2.1
-	Swashbuckle is used to provide a Swagger UI page with full documentation for all APIs 
To access the Swagger UI run the project and browse to the route:
http://localhost:5000/swagger/index.html 
-	Automapper is used to map Data models to Data Transfer Models. 
### Solution structure:
- The solution contains two projects, the API Project and the Unit Testing 
- The unit testing project is for demonstration purposes, the coverage of unit testing itself is not complete.
