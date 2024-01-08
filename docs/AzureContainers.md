# Azure Containers

## Objectives
- Dockerize all the things.
- Running locally.
- Deploying to Azure Container Apps.

### Dockerize all the things

Dockerizing existing applications and finding ways to apply the DRY principle to the Dockerfiles. I know that we have a lot of duplication in the Dockerfiles and we need to find a way to reduce that duplication. I think that we can do this by using a base image that contains all the common dependencies and then we can use that base image to build the images for each of the applications.

I need to understand how data storage works for containers. I know that we can use volumes to persist data but I need to understand how that works locally.

Networking is also something that I need to understand. I know that we can use Docker Compose to create a network for our containers but I need to understand how that works locally when container publish ports and how docker hosts can do port mappings.

### Running locally

We need to cover running our application from Visual Studio, debugging, and observability on the local development environment.

I also want to cover running multiple instances of a container locally in order to simulate a cluster. This will help us to understand how to configure our applications to run in a cluster when deploying to the cloud. Hopefully, we will also understand how to observer the behaviour of our applications when running in a cluster.

For local development, I assume that Docker desktop is an orchestrator.

### Deploying to Azure Container Apps

For this application, we will be publishing our images to Azure Container Registry (ACR) and then deploying them to Azure Container Apps (ACA).

I need to understand how volumes work in Azure Container Apps. I know that we can use Azure Storage to persist data but I need to understand how that works in the context of Azure Container Apps.

I need to know we can use ACR to sign images so that we can validate that we are deploying trusted images.

Is ACA an orchestrator? I know that AKS and Service Fabric is.

## Playing with docker containers

### Playing with the Redis container
Start up a shell in the redis container and then start up a redis-cli in the redis container.:
```powershell
docker exec -it dapr-redis sh
## Note the prompt changes to a #
redis-cli
## Note the prompt changes to 127.0.0.1:6379>
```

Start up a redis-cli directly in the redis container:
```powershell
docker exec -it dapr-redis redis-cli
## Note the prompt changes to 127.0.0.1:6379>
```

```powershell
# ls -al
total 12
drwxr-xr-x 2 redis redis 4096 Jan  5 23:21 .
drwxr-xr-x 1 root  root  4096 Dec 29 21:45 ..
-rw------- 1 redis redis 2727 Jan  5 23:21 dump.rdb
# ping
sh: 3: ping: not found
# redis-cli
127.0.0.1:6379> ping
PONG
127.0.0.1:6379> set name darrel
OK
127.0.0.1:6379> get darrel
(nil)
127.0.0.1:6379> get name
"darrel"
127.0.0.1:6379> incr counter1
(integer) 1
127.0.0.1:6379> get counter
(nil)
127.0.0.1:6379> get counter1
"1"
127.0.0.1:6379> incr counter1
(integer) 2
127.0.0.1:6379> del name
(integer) 1
127.0.0.1:6379> exit
# exit
```

### Playing with the Postgres container
Installing Postgres container with a volume mapping:
```powershell
docker run --name dapr-postgres -e POSTGRES_PASSWORD=password -d -p 5432:5432 -v dapr-postgres:/var/lib/postgresql/data postgres
```

If the image does not exist, then this command will also pull the image from Docker Hub.

The volume path specified for postgres is the default path postgres will save data to. The volume mapping allows us to persist data between container restarts. Or allow us to attach another container to the same volume and access the data.

Start up a shell in the postgres container and then start up a psql in the postgres container.:
```powershell
docker exec -it dapr-postgres sh
## Note the prompt changes to a #
```
Create a new database:
```powershell
createdb -U postgres testdb
```
Connect to the database:
```powershell
psql -U postgres testdb
## Note the prompt changes to testdb=#
```
Create a table called users:
```postgresql
CREATE TABLE users (
    id SERIAL PRIMARY KEY, 
    name VARCHAR(255) NOT NULL, 
    email VARCHAR(255) NOT NULL UNIQUE
);
```
Insert a row into the users table:
```postgresql
INSERT INTO users (name, email) 
    VALUES ('darrel', 'dwschreyer@live.com');
```
Select all rows from the users table:
```postgresql
SELECT * FROM users;
```
Disconnect from the database:
```postgresql
\q
```
Disconnect from the container:
```powershell
exit
```

### Playing with Sql Server container
Installing Sql Server container:
```powershell
docker run --name dapr-sqlserver -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123" -p 14331:1433 mcr.microsoft.com/mssql/server:2019-latest
```
Connect to the sql server instance:
```powershell
docker exec -it dapr-sqlserver "bash"
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Password123"
```


### Playing with a ASP.Net Core container

See the official docs for [Docker images for ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-8.0).

See Andew Lock's blog post [Exploring the .NET 8 preview updates to Docker images in .NET 8](https://andrewlock.net/exploring-the-dotnet-8-preview-updates-to-docker-images-in-dotnet-8/).

Create a new blank solution in a sub-folder called src:
```powershell
## From the root folder of the repository
md src
cd src
dotnet new sln -n Junk
```
Add a new ASP.Net Core Web API project with docker support to the solutions:
```powershell
## From the src folder
dotnet new webapi -n Junk.Api
dotnet sln add Junk.Api
```
Create a docker file for the ASP.Net Core Web API project:
```powershell
## From the src folder
cd Junk.Api
type nul > Dockerfile
code Dockerfile
```
Edit the Dockerfile to look like this:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY ./out .
ENTRYPOINT ["dotnet", "Junk.Api.dll"]
```
Build the ASP.Net Core Web API project:
```powershell
## From the src\Junk.Api folder
dotnet publish -c Release -o out
```
Build the docker image:
```powershell
## From the src\Junk.Api folder
docker build -t junk-api .
```
Run the docker image:
```powershell
docker run -it --rm -p 5999:8080 --name junk-api-01 junk-api
```
>NOTE: The internal port needs to be `8080` since .Net 8.0 uses that port. The external port can be anything you want.

### Moving onto multi-stage docker files

See the official docs for [Docker multi-stage builds](https://docs.docker.com/develop/develop-images/multistage-build/).

Before we start, we need to create a additional ASP.NET Blazor application to our solution. This will allow us to test the multi-stage docker file with multiple projects.

Create a new ASP.Net Core Blazor Server project with docker support to the solutions:
```powershell
## From the src folder
dotnet new blazorserver -n Junk.Blazor
dotnet sln add Junk.Blazor
```

Next we will create a multi-stage docker file for the ASP.Net Core Web API project. This file will:
 - Set up a container to build the projects.
 - Build the solution.
 - Create an image for the ASP.Net Core Web API project.
 - Run the ASP.Net Core Web API project container.
 - Run the ASP.Net Core Web API project.
 - Create an image for the Asp.Net Blazor WebAssembly project.
 - Run the Asp.Net Blazor WebAssembly project container.
 - Run the Asp.Net Blazor WebAssembly project.

Create a docker file with the name `buildApi.Dockerfile` that looks like this:
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /source

# Copy the files
COPY Junk.Api/. ./Junk.Api/
WORKDIR /source/Junk.Api
# restore the packages
RUN dotnet restore
# publish the application
RUN dotnet publish -c Release -o out

# Create image for Junk.Api
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /source/Junk.Api/out .
ENTRYPOINT ["dotnet", "Junk.Api.dll"]
```
Build the application and images:
```powershell
## From the src folder
docker build -f buildApi.Dockerfile -t junk-api .
```

Create a docker file with the name `buildBlazor.Dockerfile` that looks like this:
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /source

# Copy the files
COPY Junk.Blazor/. ./Junk.Blazor/
WORKDIR /source/Junk.Blazor
# restore the packages
RUN dotnet restore
# publish the application
RUN dotnet publish -c Release -o out

# Create image for Junk.Blazor
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /source/Junk.Blazor/out .
ENTRYPOINT ["dotnet", "Junk.Blazor.dll"]
```
Build the application and images:
```powershell
## From the src folder
docker build -f buildBlazor.Dockerfile -t junk-blazor .
```

Enter docker compose. Create a docker compose file with the name `docker-compose.yml` that looks like this:
```yaml
version: '3.8'
services:
  api:
    build:
      context: .
      dockerfile: buildApi.Dockerfile
    image: junk-api
    ports:
      - "5999:8080"

  blazor:
    build:
      context: .
      dockerfile: buildBlazor.Dockerfile
    image: junk-blazor
    ports:
      - "5998:8080"
```
Start the containers:
```powershell
## From the src folder
docker-compose up --build
```
