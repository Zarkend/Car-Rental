# Car rental Web API

This is an example API that provides a fleet of Cars and a repository of Companies with the next features:

* CRUD Cars
* CRUD Companies
* Rent a Car
* Return a rented Car 

This API is built in **.Net Core 3.0**

## Prerequisites

If not installed, install .Net Core 3.0 [SDK](https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-3.0.100-windows-x64-installer)

## Quick start
* Clone the repo:
 

    git clone https://github.com/Zarkend/Car-Rental.git

* Open the solution with Visual Studio 2019 or Visual Studio Code

### Visual Studio 2019
API will start at https://localhost:44352/    

### Visual Studio Code
API will start at https://localhost:5001/    

### Command line

Open the CMD, go to the web api project folder (Car-Rental/TestCompany.CarRental/TestCompany.CarRental) and use the following command

        dotnet run

Api will start at https://localhost:5001/

### Swagger

Note that this api comes with Swagger installed, browse https://localhost:44352/index.html to look at all the endpoints.

### Tutorial
All the endpoints listed on the tutorial will be with https://localhost:44352/. Change the port if you are using another program.

This tutorial gives a brief example of use:

Once the api is running proceed with the next actions (you can use a browser or Postman to make the calls)

##### Creating and reading the data
First, make sure any Car or Company exist on the database (for testing purposes it's an InMemory database so it will be empty on every run).

        GET https://localhost:44352/api/v1/Cars 
        GET https://localhost:44352/api/v1/Companies
        
Both should return empty
Now let's create some Cars and Companies

        POST https://localhost:44352/api/v1/Cars
Payload
```json
{
  "model": "Tesla",
  "registration": "BCN-123456",
  "brand": 1, 
  "type": 1
}
```

Call it again with other properties, Brand valid values are:

Tesla = 1
Renault = 2
Ferrari = 3
Audi = 4
Volskwagen = 5
Hyundai = 6
Fiat = 7
Ford = 8

And types are:

Convertible = 1
MiniVan = 2
SUV = 3

And the same goes for Companies

        POST https://localhost:44352/api/v1/Companies
Payload
```json
{
  "name": "Google"
}
```

Now the GET endpoints should return something, try them again!

        GET https://localhost:44352/api/v1/Cars 
        GET https://localhost:44352/api/v1/Companies

#### Renting a Car

To rent a car use the next endpoint

        POST https://localhost:44352/api/v1/Rentals 
Payload (modify the carIds array if you only created one car)
```json
[
  {
    "carIds": [
      1,
      2
    ],
    "companyId": 1,
    "days": 15
  }
]
```
It should give you an OK response with a body saying the cars you rented and the price it cost.

#### Returning a Car
To return a car use the next endpoint

        POST https://localhost:44352/api/v1/Returns 
Payload
```json
[
  {
    "carIds": [
      1
    ]
  }
]
```

And thats it, you already finished the flow of Renting and Returning a Car! Now play a bit with the following endpoints



## Car Endpoints

| Endpoint | Method  | Description | Parameter Type | Payload |
| ------ | ------ |------ |------ |------ |
| https://localhost:44352/api/v1/Cars/{carId} | GET | Returns a car with Id carId | Query | NO
| https://localhost:44352/api/v1/Cars/{carId} | PUT | Update a car with Id carId | Query | { "name":"string"}
| https://localhost:44352/api/v1/Cars/{carId} | DELETE | Delete a car with Id carId | Query | NO 
| https://localhost:44352/api/v1/Cars | POST | Create a car with the specified parameters | - | { "name":"string"}
| https://localhost:44352/api/v1/Cars | GET | Returns a list of cars of matching parameters | Query | NO

## Company Endpoints

| Endpoint | Method  | Description | Parameter Type | Payload |
| ------ | ------ |------ |------ |------ |
| https://localhost:44352/api/v1/Companies/{companyId} | GET | Returns a company with Id companyId | Query | NO
| https://localhost:44352/api/v1/Companies/{companyId} | PUT | Update a company with Id companyId | Query | { "name":"string"}
| https://localhost:44352/api/v1/Companies/{companyId} | DELETE | Delete a company with Id companyId | Query | NO 
| https://localhost:44352/api/v1/Companies | POST | Create a company with the specified name | - | { "name":"string"}
| https://localhost:44352/api/v1/Companies | GET | Returns a list of companies of matching parameters | Query | NO

## Rental Endpoints

| Endpoint | Method  | Description | Parameter Type | Payload |
| ------ | ------ |------ |------ |------ |
| https://localhost:44352/api/v1/Rentals | POST | Try to rent a car | - | [ { "carIds": [ 0 ], "companyId": 0, "days": 0 } ]



## Return Endpoints

| Endpoint | Method  | Description | Parameter Type | Payload |
| ------ | ------ |------ |------ |------ |
| https://localhost:44352/api/v1/Returns | GET | Try to return a list of cars | Query | { "carIds": [ 0 ] }


