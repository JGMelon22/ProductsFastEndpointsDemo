# Products — FastEndpoints Demo

A small **Products CRUD** API for learning [FastEndpoints](https://fast-endpoints.com/), built with .NET 10, EF Core and PostgreSQL.

## Stack

.NET 10 · FastEndpoints · EF Core (Npgsql) · PostgreSQL · Scalar (API docs) · NUnit + NSubstitute

## Run it

```bash
# 1. Set your DB connection in ProductsFastEndpointsDemo.API/appsettings.Development.json
#    ConnectionStrings:Default → Host=localhost;Port=5432;Database=productsdb;Username=postgres;Password=...

# 2. Create the schema
cd ProductsFastEndpointsDemo.API
dotnet ef database update

# 3. Run
dotnet run
```

API docs (Development): `https://localhost:7016/scalar/v1`

## Endpoints

Base path: `/api/product`

| Method | Route | Description |
| ------ | ----- | ----------- |
| `POST`  | `/create` | Create a product |
| `GET`   | `/{id}` | Get one by id |
| `GET`   | `/list?pageNumber=&pageSize=` | List (paginated) |
| `PATCH` | `/{id}` | Update |
| `DELETE`| `/{id}` | Delete |

Product: `{ name, price (0–9999.99), quantity (>=0), isAvailable }` — `id` is a server-generated Guid.

## Business rule

Quantity and availability must agree: `quantity = 0` can't be `available`, and `quantity > 0` can't be `unavailable`. Otherwise a `ProductAvailabilityException` is thrown.

## Tests

```bash
dotnet test
```