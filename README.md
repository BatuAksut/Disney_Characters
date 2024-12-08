# Disney Characters API

This project is a simple Web API that uses an external API to list and search for Disney characters. Swagger documentation is included for easy testing and exploration of the API.

## Features

- **Get all characters**: Fetches a list of all Disney characters.
- **Search character by ID**: Fetches a Disney character by its specified ID.
- **Search character by name**: Searches for Disney characters by their name.

## Technologies Used

- **ASP.NET Core 8.0**
- **Swagger/OpenAPI**
- **HttpClient** (for connecting to the external API)
- **GitHub Actions** (for CI/CD)

## API Endpoints

### 1. Get All Characters

`GET /api/characters`

Returns a list of all Disney characters.

### 2. Get Character by ID

`GET /api/characters/{id}`

Fetches a Disney character by its given ID.

### 3. Search Characters by Name

`GET /api/characters/name?name={name}`

Searches for Disney characters matching the provided name.

## Swagger Documentation

To test the API and view the documentation, you can use the Swagger interface. Once the API is running, navigate to `https://localhost:{port}/swagger` to access Swagger UI.

## Getting Started

Clone the project to your local machine and run it:

```bash
git clone https://github.com/yourUsername/repositoryName.git](https://github.com/BatuAksut/Disney_Characters.git
cd repositoryName
dotnet build
dotnet run
