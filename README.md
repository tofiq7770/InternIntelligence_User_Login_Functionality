# User Login Functionality API (ASP.NET Core)

This project implements the backend logic for user authentication in an ASP.NET Core application, focusing on security and integration with the frontend. It provides APIs for user login, logout, and secure token-based authentication using JSON Web Tokens (JWT).

## Table of Contents
1. [Overview](#overview)
2. [Setup](#setup)
3. [Endpoints](#endpoints)
4. [Authentication Flow](#authentication-flow)
5. [Security Features](#security-features)
6. [Integration with Frontend](#integration-with-frontend)
7. [Error Handling](#error-handling)
8. [Rate Limiting](#rate-limiting)
9. [JWT Token](#jwt-token)
10. [License](#license)

## Overview
This API serves as the authentication system for a web application, enabling users to log in and out securely. It uses JWT tokens for stateless authentication and integrates with ASP.NET Core Identity to handle user management.

### Features:
- **Login**: Authenticates users based on email/username and password.
- **Logout**: Signs out the user by invalidating the session.
- **JWT Token Generation**: Provides a secure token upon successful login.
- **Security**: Implements security features such as HTTPS, input validation, and token expiration.
- **Error Handling**: Provides descriptive error messages for failed login attempts.

## Setup
### Requirements:
- .NET 6 or later
- SQL Server (or another relational database)
- Visual Studio or Visual Studio Code

### Steps:
1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/USER_LOGIN_FUNCTIONALITY.git
   ```
2. **Install dependencies**:
   Make sure you have all necessary packages installed (e.g., Microsoft.AspNetCore.Identity, JWT-related packages).
   ```bash
   dotnet restore
   ```
3. **Set up the database**:
   Ensure the connection string is configured correctly in `appsettings.json`.

4. **Run the application**:
   ```bash
   dotnet run
   ```

5. **Testing the endpoints**:
   You can test the API using tools like Postman or Swagger (available in development mode).

## Endpoints
### 1. **POST** `/api/account/register`
Registers a new user in the system.

#### Request Body:
```json
{
  "username": "user123",
  "email": "user@example.com",
  "password": "password123"
}
```

#### Response:
```json
{
  "token": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9..."
}
```

### 2. **POST** `/api/account/login`
Logs in an existing user by verifying their credentials.

#### Request Body:
```json
{
  "usernameOrEmail": "user@example.com",
  "password": "password123"
}
```

#### Response:
```json
{
  "token": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9..."
}
```

### 3. **POST** `/api/account/logout`
Logs out the user by invalidating the session.

#### Response:
```json
{
  "message": "Logged out successfully"
}
```

## Authentication Flow

1. **User Registration**:
   - The user provides a `username`, `email`, and `password`.
   - The password is securely hashed using ASP.NET Core Identity.
   - A JWT token is generated upon successful registration.

2. **User Login**:
   - The user submits their `username/email` and `password`.
   - The credentials are verified against the database using ASP.NET Core Identity.
   - Upon successful verification, a JWT token is returned.

3. **User Logout**:
   - The user is signed out, and any session data is cleared.
   - The frontend can manage session expiry based on token expiration.

## Security Features

1. **HTTPS**: 
   - Ensure all communication between the client and server is encrypted using HTTPS. 
   - In production, HTTPS should be enforced by configuring the web server.

2. **Password Hashing**:
   - User passwords are never stored in plaintext. They are hashed using a secure hashing algorithm (e.g., SHA256 or bcrypt via ASP.NET Core Identity).

3. **JWT Token Expiration**:
   - Tokens expire after a set period (e.g., 7 days). Once expired, the client will need to re-authenticate or refresh the token.

4. **Input Validation**:
   - User inputs (e.g., username, email, password) are validated and sanitized before processing to prevent SQL injection and other attacks.

5. **Token Security**:
   - The JWT token uses HMAC-SHA512 for signing, ensuring integrity and authenticity.
   - Tokens are signed with a secret key and cannot be tampered with without detection.

## Integration with Frontend

- **Frontend Flow**: After a successful login, the frontend (e.g., React or Angular) stores the JWT token in `localStorage` or `sessionStorage`.
- **Subsequent Requests**: For protected resources, the frontend attaches the JWT token in the `Authorization` header using the `Bearer` scheme.
  
  Example:
  ```javascript
  fetch('/api/protected-resource', {
    headers: {
      'Authorization': 'Bearer <JWT_TOKEN>'
    }
  });
  ```

## Error Handling

- **Invalid Credentials**: If the login credentials are incorrect, the API returns a `401 Unauthorized` status with a message like:
  ```json
  {
    "message": "Invalid login attempt."
  }
  ```

- **Bad Request**: If the request body is malformed or validation fails (e.g., missing required fields), the API returns a `400 Bad Request` with details:
  ```json
  {
    "errors": {
      "username": ["Username is required"],
      "password": ["Password is required"]
    }
  }
  ```

- **Internal Server Error**: If something goes wrong server-side, the API returns a `500 Internal Server Error` with a general error message.

## Rate Limiting

- **Purpose**: Prevent brute-force attacks by limiting the number of login attempts.
- **Implementation**: Rate limiting can be added to the login endpoint to restrict the number of requests a user can make in a certain period (e.g., 5 attempts per minute).

```csharp
services.AddMemoryCache();
services.AddInMemoryRateLimiting();
```

## JWT Token

JWT tokens are used for stateless authentication. After the user logs in, the backend generates a JWT containing:
- **Claims**: Information about the user (e.g., `userId`, `username`).
- **Expiration**: A timestamp indicating when the token expires.
- **Signature**: A cryptographic signature to verify the authenticity of the token.

The client can use the token in subsequent requests to authenticate themselves.

Example of a JWT structure:
```json
{
  "header": {
    "alg": "HS512",
    "typ": "JWT"
  },
  "payload": {
    "userId": "f14934a7-9336-4b6e-8a46-b926387c43c2",
    "username": "john_doe",
    "exp": 1734804182
  },
  "signature": "lLvjpvmnDvptP5w2No7qOM5Sivbl90ctbHIn3ytOd6u8B-d6u9X2nIGslfgOft-8g6uoPv8rXMiTl7taLRwfgQ=="
}
```

## License
MIT License
 
