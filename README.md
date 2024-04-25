# RESTful-API
.NET API that exposes endpoints to retrieve and manipulate data from various sources
===================================

Pre-requisites
--------------

- Visual Studio 2022
- .Net 6.0
- ASP.NET and web development workload


Necessary Dependencies
--------------
- CsvHelper31.0.4
- Newtonsoft.Json13.0.3


Getting Started
---------------

This project implemented a.NET API that exposes endpoints to retrieve and manipulate data from various sources. The API will read and merge data, calculate aggregations, and return summarized information in JSON format.  

Testing
---------------
1. Visit https://github.com/fengyuwu/RESTful-API
2. Locate  and click the <>Code button on the top right section
3. Navigate to the HTTP tab, and copy URL to clipboard
4. Open Visual Studio 2022 (install .Net 6.0 first)
5. On the Getter Started menu, Select Clone a repository
6. Paste the URL to the repository location input box
7. Select a desired path and hit Clone Button
8. Once the project is loaded, click play button and test the following endpoint in the Local host or download Postman software and enter the following endpoint using GET. Then send the request to see the response.

or you can follow these steps:
1. Visit https://github.com/fengyuwu/RESTful-API
2. Locate  and click the <>Code button on the top right section
3. Click Download Zip
4. Extract the downloaded zip
5. Open the RestfulAPI.sln
6. Once the project is loaded, click play button and test the following endpoint in the Local host or download Postman software and enter the following endpoint using GET. Then send the request to see the response.


Notes: Endpoints can be tested using Postman or localhost server when running the project in Visual Studio 2022

Task 1 and Task 3 Endpoints:

https://localhost:7214/top-customers

https://localhost:7214/product-summary

https://localhost:7214/customer-summary

https://localhost:7214/customers

https://localhost:7214/orders

https://localhost:7214/products

https://localhost:7214/order-items



Endpoint exposed to demonstrate the correctness for task 2 only(To be deleted after demo):
https://localhost:7214/demo2-1

https://localhost:7214/demo2-2

https://localhost:7214/demo2-3

https://localhost:7214/demo2-4

https://localhost:7214/demo2-5

https://localhost:7214/demo2-6





Note: Ensure the CSV and JSON files are not opened by other programs when running the web app.


Approach and Design Choices:
---------------

To demonstrate functionality, additional API endpoints are exposed for demonstration purposes, whey will be removed when the product is ready to ship for security reasons.

LINQ is chosen over Entity Framework for its efficiency with small datasets. However, for larger projects, Entity Framework would be preferable due to its object-based database access, saving development time.

The MVC framework is employed to achieve Separation of Concerns, and custom routing allows for defining specific routes for API endpoints.

Dependency Injection is utilized to create a loosely coupled program, facilitating testing and maintenance. Data functionality is encapsulated within a service (DataService) to allow for easy dependency injection into other classes.

Separating logic from controllers and encapsulating logic within services enhances code maintainability.

Unit tests are housed in a separate project (https://github.com/fengyuwu/RestAPI/blob/main/DataServiceTests.cs) to ensure their preservation even after the software is shipped, 

SOLID principle is followed ensuring each class has a single responsibility. This makes it code easy to maintenance and understand.

Comment and #region-#endregion section are added for readability  

Naming conventions are followed to meet coding standards.

Exception handling is implemented using try-catch blocks.

Test cases are included to verify program correctness, with a recommendation for additional tests for edge cases such as empty CSV files and malformed files.

Error scenarios are handled, HTTP status code 500 internal server errors will be returned if there is an exception. In a more complex system, I would have different status codes for different situations(Success response, not found, and service unavailable, etc) so that the developer can easily pinpoint issues and it also helps with user experience. 


When calculating total orders and amounts spent in task 2, only customer IDs are returned for efficiency, though returning names is feasible albeit slower due to additional join operations.

Functionalities are broken down into small services for readability and concurrent development in a team setting.

.Net 6.0 is selected because it is a long-term support version.



