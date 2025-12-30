# OTP Demo API

A minimal ASP.NET Core API that demonstrates phone-number OTP authentication backed by ASP.NET Core Identity, Entity Framework Core, and JWT issuance.

## Features

- Request and verify 6‑digit OTP codes via a simple REST API.
- Persisted OTPs and users using Entity Framework Core (SQL Server).
- JWT token issuance upon successful OTP verification.
- Pluggable SMS sender service (currently logs SMS messages to the console).

## Tech Stack

- **.NET 9** (ASP.NET Core)
- **Entity Framework Core** with **SQL Server**
- **ASP.NET Core Identity**
- **JWT Bearer Authentication**
- **Swagger / OpenAPI**

## Getting Started

### Prerequisites

- .NET SDK 9.0+
- SQL Server (local instance or container)

### Configuration

Set the following configuration values in `src/OtpDemo.Api/appsettings.Development.json`, `appsettings.json`, or via user secrets/environment variables:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=OtpDemo;User Id=sa;Password=Your_password123;TrustServerCertificate=true"
  },
  "Jwt": {
    "Key": "your-strong-signing-key",
    "Issuer": "OtpDemoIssuer",
    "Audience": "OtpDemoAudience"
  }
}
```

> **Note:** The `Jwt:Key` must be a sufficiently long secret for HMAC SHA-256 signing.

### Database Setup

Apply migrations (from the repo root):

```bash
dotnet ef database update --project src/OtpDemo.Api
```

### Run the API

```bash
dotnet run --project src/OtpDemo.Api
```

In development, Swagger UI is available at:

```
https://localhost:5001/swagger
```

(Exact port may vary based on your environment.)

## API Endpoints

Base URL: `/api/otp`

### Request OTP

**POST** `/api/otp/request`

```json
{
  "phoneNumber": "+15551234567"
}
```

**Response**

```
OTP sent.
```

### Verify OTP

**POST** `/api/otp/verify`

```json
{
  "phoneNumber": "+15551234567",
  "code": "123456"
}
```

**Response**

```json
{
  "token": "<jwt-token>"
}
```

## Notes

- OTP codes are valid for **3 minutes** and can only be used once.
- The default `SmsSenderService` logs the message to the console. Replace it with a real SMS provider for production use.

## Project Structure

```
OtpDemo
├── OtpDemo.sln
└── src
    └── OtpDemo.Api
        ├── Controllers
        ├── Data
        ├── Entities
        ├── Services
        └── Program.cs
```

## License

This project is licensed under the terms of the [MIT License](LICENSE).
