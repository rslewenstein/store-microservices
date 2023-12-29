# Store 

- Project used to study about microservices.
- event-driven

<!-- <img src="Util/img/store.jpg"> -->

#### RabbitMQ

- https://www.rabbitmq.com/download.html
- docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management

- http://localhost:15672/
- user: guest
- password: guest
- queue: update_product_quantity

- dotnet add package RabbitMQ.Client