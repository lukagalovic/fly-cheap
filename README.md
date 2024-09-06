# Fly Cheap

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop) - Download and install Docker Desktop.

## Getting Started

1. Build and start the application using Docker Compose:

   ```bash
   docker-compose up --build
   ```

2. Open your browser and navigate to:

   ```
   http://localhost:3000/api
   ```

## Termination

1. To stop the application and remove the containers, you can use the following command:

   ```bash
   docker-compose down
   ```

2. If you want to remove the volumes (which includes the database data) as well, use:

   ```bash
   docker-compose down -v
   ```

## Application Flow

1. Login with ```client_id``` and ```client_secret``` which are provided by Amadeus API after registering on their website.
2. Search for flights


## Debugging with IIS Express

Application is also configured to be run and debugged inside Visual studio with IIS Express.
After running ```docker-compose up --build```:
1. Build and run application from Visual Studio using IIS Express
2. Run
   ```bash
   npm run dev
   ```
   from frontend project directory ```root/frontend/web```

### On Build

- **Airports Seeding**: During build, the table Airports is seeded:
