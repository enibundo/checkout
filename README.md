# CheckoutGateway
Here I have implemented a small PaymentGateway REST Api. 

![Swagger APIs](pic1.PNG "Swagger APIs")
![Swagger Response](pic2.PNG "Swagger Response")

# Application logging
For logging I used SeriLog for structured logging. 
Todo: use Logstash (https://www.elastic.co/logstash). 

# Application metrics
I have never had the chance to work with metrics directly before.
I would use something like https://docs.datadoghq.com/developers/metrics/types/?tab=count
It's interesting to know :
- number of payment requests
- amount of payment
- currency counters
- number of failures/sucesss
- counter by return status code
- etc. 

# Containerization

# Authentication
For API access by each merchant API keys would be used. 

1. In Azure I use OCP Keys :
https://docs.microsoft.com/en-us/azure/cognitive-services/authentication?tabs=powershell
that are used directly in the API Gateway (Apim) before accessing our service. 
I would use that or something simmilar if I was deploying to the cloud. 

2. Develop some kind of AuthorizationHandler like : 
https://josef.codes/asp-net-core-protect-your-api-with-api-keys/ 
In this case I would need to persist the keys somewhere.

# API client

# Build script / CI

# Performance testing

# Encryption

# Data storage

# Anything else you feel may benefit your solution from a technical perspective.