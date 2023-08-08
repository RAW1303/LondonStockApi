# RoyWeller.LondonStockApi

## Setup

### Database Creation
Before starting the applcation please run  `dotnet ef database update` from the directory containing RoyWeller.LondonStockApi.csproj to create the SQLite database

### Service Bus Setup (optional)
In order to use the high frequency trade endpoint you will need to create an Azure Service bus and queue. 

Once created please update the ServiceBusConnectionString and TradesQueueName values in `local.settings.json`


## Execution
In order to run locally you will require Visual Studio with the Azure Functions SDK installed. Once Setup the API can be 

### Create trade
URL: ./api/trades

Method: POST

Example JSON:
```
{
    "stockTicker": "ccccc",
    "price": 1,
    "quantity": 4,
    "brokerId": 1
}
```

This endpoint directly creates a trade on the database. For the purpose of this exercise 2 brokers have been setup in the database with ids 1 and 2. Other values will fail validation due to no authorised broker with that id.

### Create trade (high frequency)
URL: ./api/trades/highfrequency

Method: POST

Schema:

This endpoint uses an Azure Service Bus Queue to process the trades in batches, reducing the load on databse

### Get all stocks
URL: ./api/stocks

Method: GET

### Get single stock
URL: ./api/stocks/{ticker}

Method: GET

### Query stocks
URL: ./api/stocks/query

Method: POST

Example JSON:
```
[
    "aaaaa",
    "bbbbb"
]
```

