# Employee Recognition System - .NET Backend API

## Prerequisites
- .NET 8 SDK
- SQL Server (LocalDB or full instance)

## Setup

1. **Update connection string** in `EmployeeRecognition.API/appsettings.json`

2. **Create database & apply migrations:**
   ```bash
   cd EmployeeRecognition.API
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

3. **Run the API:**
   ```bash
   cd EmployeeRecognition.API
   dotnet run
   ```
   API runs at `https://localhost:5001` (or check console output).

4. **Test with Swagger:** Navigate to `https://localhost:5001/swagger`

## Default Admin Account
- Email: `admin@company.com`
- Password: `Admin@123`

## API Endpoints

| Method | Endpoint | Auth | Description |
|--------|----------|------|-------------|
| POST | /api/auth/login | No | Login |
| POST | /api/auth/register | No | Register |
| GET | /api/auth/me | Yes | Current user |
| GET | /api/employees | Yes | All employees |
| GET | /api/awardcategories | Yes | All categories |
| GET | /api/recognitions?status=&type= | Yes | List recognitions |
| GET | /api/recognitions/my | Yes | My recognitions |
| POST | /api/recognitions | Yes | Create recognition |
| PUT | /api/recognitions/{id}/approve | Manager/Admin | Approve nomination |
| PUT | /api/recognitions/{id}/reject | Manager/Admin | Reject nomination |

## Frontend Integration
Set `VITE_API_URL=https://localhost:5001/api` in your React app's `.env` file, or the Vite proxy will forward `/api` requests automatically.
