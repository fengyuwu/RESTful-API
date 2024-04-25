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
Additional API endpoints are exposed for demonstration purposes, They will be removed when the product is ready to ship for security reason.

LINQ instead of Entity Framework because the datasets are small, for more complex projects, I would use EF because it allows us to use objects to access the DB, saving development time.

MVC framework is used because it can achieve Separation of concerns and Routing allows me to define custom routes for API endpoints

Dependency Injection was used to create a loosely coupled program so that testing and maintaining would be easy to do. Less rewrite is required. Data operating functionality is written as a service(DataService) so that if other classes need it, it can pass it as a dependency. 

Separated logic from the Controller and making logic as a service can make code more maintainable.

Unit tests are housed in a separate project t (https://github.com/fengyuwu/RestAPI/blob/main/DataServiceTests.cs) to ensure their preservation even after the software is shipped, 

Solid principle is followed so that the class only has one responsibility. This makes it easy to maintain and understand. 

Comment and #region-#endregion are added for readability  

Class and var naming convention is followed to meet coding standards

the try-catch block is implemented to handle the exception

Test cases are added to check for the correctness of the program. More test cases should be developed for edge cases like empty CSV files. Since the project only has small amount of dataset, only some tests are written to ensure the correctness of the program.

Error scenarios are handled, 500 internal server errors will be returned if there is an exception for now due to the small size of the project and limited time. In a more complex system. I would have a different http code for different situations, like FILE not found, or bad input for the request. All situation will have different status  code  so that when developing, it can let developer know what to chase after.

When calculating the total number of orders and amount spent at task 2, only the customer id is returned. The name can also be returned but it will be slower due to another join and customer id is enouch to identify the person. 

Service are breakdown into different microservice, so that developer can work on different services at the same time, this approach of breaking down service into smaller ones can speed up dev process and make code easy to read instead of having a big service.


