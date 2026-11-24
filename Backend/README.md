# Backend local development

The API uses SQL Server and reads `ConnectionStrings:DefaultConnection` from
.NET User Secrets when running in `Development`. From the repository root,
save your connection string with:

```powershell
dotnet user-secrets set 'ConnectionStrings:DefaultConnection' 'Server=YOUR_SERVER_OR_INSTANCE;Database=employeebenefitsdb;Integrated Security=True;Encrypt=True;' --project '.\Backend\src\EmployeeBenefitsWorkflow.Api'
dotnet run --project '.\Backend\src\EmployeeBenefitsWorkflow.Api' --launch-profile http
```

Replace `YOUR_SERVER_OR_INSTANCE` with the server name you use in SSMS and
adjust the database name if needed. The example uses Windows Authentication;
for SQL Server Authentication, use `User ID` and `Password` in place of
`Integrated Security=True`. For a local server with an untrusted development
certificate, you can add `TrustServerCertificate=True` to your local secret.

User Secrets are stored unencrypted in your local user profile, outside the
repository. For deployments, set `ConnectionStrings__DefaultConnection`
through the host's secret configuration instead.

## API errors

The exception handler returns JSON Problem Details with a status code, title,
client-safe detail, and the original request path. Invalid input returns 400,
missing users or requests return 404, and decisions on requests that are no
longer under review return 409. Unimplemented login and registration return
501. Unexpected failures return a generic 500 message; details remain in
server logs. ASP.NET Core model-validation failures return 400 with an `errors`
object.

Run the focused exception tests with:

```powershell
dotnet test '.\Backend\tests\EmployeeBenefitsWorkflow.Tests\EmployeeBenefitsWorkflow.Tests.csproj'
```
