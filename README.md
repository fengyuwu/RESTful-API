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

LINQ instead of Entity Framework because the datasets are small, for more complex projects, I would use EF because it allows us to use objects to aceess the DB, saving development time.


