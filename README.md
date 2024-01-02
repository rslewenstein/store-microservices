# Store (Shopping cart services)
- Simple project used to study about microservices and apply some technologies and knowledges.
- Basically there are 2 WebApis: Product and Shopping Cart.
- The Product Api sends a products list or unique product (get by id) to Front-end app, using HTTP requests.
- The front-end app sends to ShoppingCart Api by HTTP request: [UserId, ProductId, Quantity, Price].
- The ShoppingCart Api sends by messaging (RabbitMQ) to Products Api: [ProductId and Qunatity].
- Product Api consumes the queue and update the quantity by ProductId.

<img src="Util/img/store.jpg">


### Technologies applied:

- [x] - .Net 7
    - Product WebApi:
        - [x] - Clean Architecture
        - [x] - Dapper
        - [x] - Mapper
        - [x] - SQLite Database
        - [x] - RabbitMQ (Consumer)
        - [ ] - Logger
        - [ ] - Unit Tests
    - ShoppingCart WebApi:
        - [x] - Clean Architecture
        - [x] - Dapper
        - [x] - Mapper
        - [x] - SQLite Database
        - [x] - RabbitMQ (Producer)
        - [ ] - Logger 
        - [ ] - Unit Tests
- [ ] - K6 Load Test
- [ ] - Docker
- [?] - Front End (I don't know if I will do it).

#### RabbitMQ

- https://www.rabbitmq.com/download.html
- docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management

- http://localhost:15672/
- user: guest
- password: guest
- queue: update_product_quantity

- dotnet add package RabbitMQ.Client