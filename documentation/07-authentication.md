# 07. Authentication and Authorization

## Overview
Media Manager separates identity verification from access control. **Authentication** is delegated to an external enterprise identity provider, while **Authorization** logic remains tightly coupled to the internal PostgreSQL database.

## Implementation Guidelines

### 1. External Authentication (OIDC)
* Do **not** build custom password management, login screens, or MFA flows in the backend API.
* The system expects clients (Web Interface, Sync Daemon) to authenticate via an external OpenID Connect (OIDC) provider (e.g., Auth0, Microsoft Entra ID).
* The Web API authenticates requests by validating **JWT Bearer Tokens** using standard ASP.NET Core middleware (`AddJwtBearer`).

### 2. Identity Mapping & Local Authorization
* The internal database controls access via the `Users`, `Roles`, `Permissions`, `RolePermissions`, and `UserPermissions` tables.
* When a JWT token is validated, ASP.NET Core Claims Transformation middleware must extract the subject claim (`sub`) and map it to the corresponding `UserId` in the local database.
* The user's internal database permissions are then converted into ASP.NET Core Claims.

### 3. Securing Endpoints
* Minimal API endpoints must be secured using standard `.RequireAuthorization("PolicyName")` extensions.
* Policies should map to specific internal permissions (e.g., `ManageCatalog`, `ViewMedia`, `ConfigureSystem`).

## Daemon Authentication
* The Sync Daemon operates autonomously and must authenticate using a Machine-to-Machine flow (Client Credentials flow) to obtain a JWT from the OIDC provider.
* The API will validate this token exactly like a standard user token, mapping it to a specialized system user account in the `Users` table with restricted daemon permissions.
