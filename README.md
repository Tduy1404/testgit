## Environment Variables

The project uses environment variables for sensitive configuration. Create a `.env` file in the root directory with the following variables:

```plaintext
DB_CONNECTION_STRING=your-connection-string
ASPNETCORE_ENVIRONMENT=Development
```

For development, you can copy `.env.example` to `.env` and fill in your local values:

```bash
cp .env.example .env
```

⚠️ Never commit the `.env` file to version control!

## Running the Project

1. Install dependencies:

```bash
dotnet restore
```

2. Run the project:

```bash
dotnet run
```

3. Access the API at `https://localhost:5001`

## API Documentation

The API documentation is available at `https://localhost:5001/swagger`.

## Health Checks

The project includes health checks at `https://localhost:5001/health`.

## Database Migrations

To create a new migration:

```bash
dotnet ef database update
```


## Database Migrations


```bash
dotnet ef database update
```
