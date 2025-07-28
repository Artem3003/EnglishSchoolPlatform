# Epic 1 - Admin Panel with Services

## Description

This epic focuses on building an **Admin Panel** and essential backend services that provide core functionality for managing an English school system.

Admins should be able to create, update, and delete users, teachers, students, and assign user roles accordingly. The system must follow best practices in architecture, coding standards, and security.

## General Requirements

The system should support the following features:

- Get student/teacher by ID.
- Get all students or all teachers.
- CRUD operations for **Users**, **Students**, **Teachers**, and **Admins**.
- Global error handling for clean API responses.
- Role-based access via ASP.NET Identity.
- Validation using FluentValidation.

### Technical Specifications

- Use the latest stable version of **ASP.NET Core (Web API template)**.
- Follow **N-layer architecture** (Controllers → Services → Repositories → DbContext).
- Use **built-in dependency injection**.
- Follow **SOLID principles**.
- Use **JSON** as request/response format.
- Use **Microsoft SQL Server**.
- Add **Swagger** for API documentation.

## Entities

### User

- **Id**: `Guid`, required, unique  
- **Email**: `string`, required, unique  
- **PasswordHash**: `string`, required  
- **Role**: `enum (Admin, Teacher, Student)`, required  
- **CreatedAt**: `DateTime`, required

### Admin

- **Id**: `Guid`, required, unique  
- **UserId**: `Guid`, required (FK to `User`)

### Teacher

- **Id**: `Guid`, required, unique  
- **UserId**: `Guid`, required (FK to `User`)  
- **Subject**: `string`, required  
- **ExperienceYears**: `int`, optional

### Student

- **Id**: `Guid`, required, unique  
- **UserId**: `Guid`, required (FK to `User`)  
- **Level**: `string`, optional  
- **DateOfBirth**: `DateTime`, optional

## Task Description

Implement the Admin API to support managing users, teachers, and students. Follow clean architecture and code quality standards.

---

## User Stories

### US1E1
Add a new User
```{xml} 
POST /users
{
  "email": "admin@school.com",
  "password": "StrongPassword123!",
  "role": "Admin"
}
```
### US2E1
Get a User by ID
```{xml} 
GET /users/{id}
{
  "id": "f0a2ab11-d2a2-4413-aef3-888e198b2a72",
  "email": "admin@school.com",
  "role": "Admin",
  "createdAt": "2025-07-01T10:30:00Z"
}
```
### US3E1
Update a User
```{xml}
PUT /users
{
  "id": "f0a2ab11-d2a2-4413-aef3-888e198b2a72",
  "email": "newadmin@school.com",
  "role": "Admin"
}
```
### US4E1
Delete a User
```{xml}
DELETE /users/{id}
```
### US5E1
Create a Teacher
```{xml}
POST /teachers
{
  "userId": "f0a2ab11-d2a2-4413-aef3-888e198b2a72",
  "subject": "English Grammar",
  "experienceYears": 5
}
```
### US6E1
Get All Teachers
```{xml}
GET /teachers
```
### US7E1
Update a Teacher
```{xml} 
PUT /teachers
{
  "id": "e8b3d1c6-9b9a-4d21-bd04-8f69aeab201c",
  "subject": "Speaking and Listening",
  "experienceYears": 6
}
```
### US8E1
Delete a Teacher
```{xml}
DELETE /teachers/{id}
```
### US9E1
Create a Student
```{xml}
POST /students
{
  "userId": "ad2c77de-4f0b-44b6-a2b6-00e09a259c3f",
  "level": "B2",
  "dateOfBirth": "2005-06-15"
}
```
### US10E1
Get All Students
```{xml}
GET /students
```
### US11E1
Update a Student
```{xml}
PUT /students
{
  "id": "ea5933a2-091a-4c47-b27f-abc762dc84d6",
  "level": "C1"
}
``` 
### US12E1
Delete a Student
```{xml}
DELETE /students/{id}
```
### US13E1
Global Error Handling
```{xml}
{
  "status": 400,
  "message": "Validation failed",
  "details": ["Email is required", "Password must be at least 8 characters"]
}
```

## Non-functional requirement (Optional)
**NFR1E1**
Implement Repository and Unit of Work patterns.
**NFR2E1**
Use Entity Framework Core (latest stable version).
**NFR3E1**
Enable response caching for GET endpoints (1 minute).
**NFR4E1**
Swagger UI should be accessible at /swagger.
