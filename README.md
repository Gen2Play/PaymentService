# AspnetCore microservices:

## Prepare environment

* Install dotnet core version in file `global.json`
* Visual Studio 2022+ (Rider)
* Docker Desktop

---

## How to start the project
```Powershell
dotnet build
```
Go to folder contain file `docker-compose`

1. Using docker-compose
```Powershell
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans
```


## Application URLs - LOCAL Environment (Docker Container):

## Docker Application URLs - LOCAL Environment (Docker Container):
- Portainer: http://localhost:9000 - username: admin; pass: 123pa$$word!
- Kibana: http://localhost:5601 - username: elastic; pass: admin
- RabbitMQ: http://localhost:15672 - username: guest; pass: guest

2. Using Visual Studio 2022 or Rider
- Open microservices.sln - `microservices.sln`
- Run Compound to start multi projects
---
## Application URLs - DEVELOPMENT Environment:
- Payment API: http://localhost:5000/swagger/index.html
- Notification API: http://localhost:5050/swagger/index.html
---
## Application URLs - PRODUCTION Environment:

---
## Packages References

## Install Environment

- https://dotnet.microsoft.com/download/dotnet/6.0
- https://visualstudio.microsoft.com/

## References URLS

## Docker Commands:

- docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build

## Useful commands:

- ASPNETCORE_ENVIRONMENT=Production dotnet ef database update
- dotnet watch run --environment "Development"
- dotnet restore

**Development Environment:**