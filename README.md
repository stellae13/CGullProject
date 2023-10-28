# CGullProject
Seagull-themed merchandise!
  
Front End Repo:https://github.com/bsumner2/CGullFrontEnd  
Unit Testing Repo: https://github.com/stellae13/CGullTests
  

Endpoints   
Item/GetAllItems()  
▪ Type: GET  
▪ Response: Array of Items, empty array if none exist  
Expected input: N/A  
Expected output: JSON Array of all Items in our database   
  
Item/AddItemToCart(cartId, itemId, quantity)   
▪ Type: POST/PUT (depends on your implementation)  
▪ Response: Success/Failure  
Expected input: cartID, productID, quantity  
Expected output: Success if userID exists, productID exists, quantity <= product.Stock  
  
GetCart(cartId)   
▪ Type: GET   
▪ Response: All cart details, including totals  
Expected input: cartID  
Expected output: Array of Products, GetTotals(cartID)  
  
GetTotals(cartId)  
▪ Type: GET  
▪ Response: Regular Total, Bundle Total, Total with Tax  
Expected input: cartID  
Expected output: Decimal array of size 3 with array[0] = Regular Total, array[1] = Bundle Total, and array[3] = total with tax  

ProcessPayment(cartId, cardNumber, exp, cardholderName, cvc)   
▪ Type: POST   
Expected input: cartID, cardNumber, exp, cardHolderName, cvc  
Success if all of the inputs are valid  
Failure otherwise  
