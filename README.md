
1.	To test CheckoutClient.exe from the command line

Open the Checkout solution in Visual Studio.
Build the solution in Release mode.
Open the Windows command prompt.
Navigate to C:\Degree53\CheckoutSolution\CheckoutClient\bin\Release\
Enter CheckoutClient.exe T23 A99 A99 C40 B15 C40 A99 B15 B15 A99 A99 B15 A99 A99 B15
The total for this basket should be £9.29


2.	To run CheckoutServices.Tests

Open the Checkout solution in Visual Studio and build in Debug mode.
Set CheckoutService.Tests as the Startup project.
Select Test -> Debug -> All Tests.


3.	To test the CheckoutService in browser.

Open the Checkout solution in Visual Studio and build in Debug mode.
Set CheckoutService as the Startup project and debug.
http://localhost:51022/api/Checkout/GetBasketTotal?basketJSON={"Items":[{"SKU":"T23","Quantity":1},{"SKU":"A99","Quantity":7},{"SKU":"C40","Quantity":2},{"SKU":"B15","Quantity":5}]}


4. To test the CheckoutClient in Visual Studio.

Open the Checkout solution in Visual Studio and build in Debug mode.
Set the Command Line Arguments via the CheckoutClient -> Properties -> Debug tab.
Set CheckoutCient as the Startup project and debug.







