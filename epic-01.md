# Epic 1 - Lesson Management System

## Description

The Lesson Management System provides a backend service for managing educational activities such as lessons, homework, student attendance, and calendar events.  
It is designed for schools, online education platforms, or training centers where teachers and students interact through lessons, assignments, and schedules.  

The system enables teachers to schedule and manage lessons, assign and grade homework, track student attendance, and organize events in a shared calendar.  
Students can view their lessons, submit homework, and access events or deadlines.

## General Requirements

The system should support the following features:

* CRUD for lessons.  
* CRUD for homework.  
* CRUD for homework assignments.  
* CRUD for student lessons (attendance).  
* CRUD for calendar events.  
* Global error handling.  

### Technical Specifications

- Use the latest stable version of **ASP.NET Core (Web API template)**.
- Follow **N-layer architecture** (Controllers → Services → Repositories → DbContext).
- Use **built-in dependency injection**.
- Follow **SOLID principles**.
- Use **JSON** as request/response format.
- Use **Microsoft SQL Server**.
- Add **Swagger** for API documentation.

## Entities

**Lesson**:
* **Id**: Guid, required, unique  
* **Title**: string, required  
* **Description**: string, required  
* **ScheduledDateTime**: DateTime, required  
* **DurationMinutes**: int, required  
* **Type**: LessonType, required  
* **Status**: LessonStatus, required  
* **MeetingLink**: string, optional  
* **Materials**: string, optional  
* **CreatedAt**: DateTime, required

Navigation properties 
* List of StudentLessons  
* List of Homeworks  
* List of CalendarEvents  

---

**StudentLesson**:
* **Id**: Guid, required, unique  
* **LessonId**: Guid, required  
* **Lesson**: Lesson  
* **AttendanceStatus**: AttendanceStatus, required  
* **Notes**: string, optional  
* **AttendedAt**: DateTime, optional  

---

**Homework**:
* **Id**: Guid, required, unique  
* **Title**: string, required  
* **Description**: string, required  
* **Instructions**: string, required  
* **DueDate**: DateTime, required  
* **CreatedAt**: DateTime, required  
* **LessonId**: Guid, optional  
* **Lesson**: Lesson?  

Navigation properties:
* List of HomeworkAssignments 

---

**HomeworkAssignment**:
* **Id**: Guid, required, unique  
* **HomeworkId**: Guid, required  
* **Homework**: Homework  
* **SubmissionText**: string, optional  
* **AttachmentUrl**: string, optional  
* **SubmittedAt**: DateTime, optional  
* **Status**: AssignmentStatus, required  
* **Grade**: int, optional  
* **TeacherFeedback**: string, optional  
* **GradedAt**: DateTime, optional  

---

**CalendarEvent**:
* **Id**: Guid, required, unique  
* **Title**: string, required  
* **Description**: string, optional  
* **StartDateTime**: DateTime, required  
* **EndDateTime**: DateTime, required  
* **Type**: EventType, required  
* **Color**: string, optional  
* **LessonId**: Guid, optional  
* **Lesson**: Lesson  

---

### Enums

**LessonType**:
* Individual  
* Group  
* Workshop  

**LessonStatus**:
* Scheduled  
* InProgress  
* Completed  
* Cancelled  

**AttendanceStatus**:
* Present  
* Absent  
* Late  

**AssignmentStatus**:
* Assigned  
* InProgress  
* Submitted  
* Graded  
* Late  

**EventType**:
* Lesson  
* Assignment  
* Exam  
* Holiday  
* Personal  

---

## User Stories

### US1E1 - User story 1
Add a new Lesson endpoint
```{xml}
Url: /lessons
Type: POST
Request Example:
"lesson": {
  "title": "English lesson",
  "description": "Present Simple",
  "scheduledDateTime": "2025-09-01T10:00:00",
  "durationMinutes": 60,
  "teacherId": "teacher-123",
  "type": "Individual",
  "status": "Scheduled"
}
```

### US2E1 - User story 2
Get Lesson by id endpoint
```{xml} 
Url: /lessons/{id}
Type: GET
Response Example:
{
  "id": "30dd879c-ee2f-11db-8314-0800200c9a66",
  "title": "English Lesson",
  "description": "Present Simple",
  "scheduledDateTime": "2025-09-01T10:00:00",
  "durationMinutes": 60,
  "teacherId": "teacher-123",
  "type": "Individual",
  "status": "Scheduled"
}
```

Get all Lessons endpoint
```{xml} 
Url: /lessons
Type: GET
Response Example:
[
  {
    "id": "30dd879c-ee2f-11db-8314-0800200c9a66",
    "title": "Math Lesson",
    "description": "Algebra basics"
  }
]
```

### US3E1 - User story 3
Update a Lesson endpoint
```{xml}
Url: /lessons
Type: PUT
Request Example:
{
  "lesson": {
    "id": "30dd879c-ee2f-11db-8314-0800200c9a66",
    "title": "Advanced Math Lesson",
    "description": "Linear equations",
    "status": "InProgress"
  }
}"role": "Admin"
}
```

### US4E1 - User story 4
Delete a Lesson endpoint
```{xml}
Url: /lessons/{id}
Type: DELETE
```

### US5E1 - User story 5
Add a Homework endpoint
```{xml}
Url: /homeworks
Type: POST
Request Example:
{
  "homework": {
    "title": "Homework 1",
    "description": "Solve problems",
    "instructions": "Page 10, tasks 1-5",
    "dueDate": "2025-09-10T23:59:00",
    "teacherId": "teacher-123",
    "lessonId": 1
  }
}
```

### US6E1 - - User story 6
Get Homework by id endpoint
```{xml}
Url: /homeworks/{id}
Type: GET
Response Example:
{
  "id": "30dd879c-ee2f-11db-8314-0800200c9a66",
  "title": "Homework 1",
  "description": "Solve problems",
  "instructions": "Page 10, tasks 1-5",
  "dueDate": "2025-09-10T23:59:00"
}
```

Get all Homeworks endpoint
```{xml}
Url: /homeworks
Type: GET
Response Example:
[
  {
    "id": "30dd879c-ee2f-11db-8314-0800200c9a66",
    "title": "Homework 1"
  }
]

```
### US7E1 - User story 7
Update Homework endpoint
```{xml} 
Url: /homeworks
Type: PUT
Request Example:
{
  "homework": {
    "id": "30dd879c-ee2f-11db-8314-0800200c9a66",
    "title": "Homework 1 Updated",
    "description": "Updated tasks"
  }
}
```

### US8E1 - User story 8
Delete Homework endpoint
```{xml}
Url: /homeworks/{id}
Type: DELETE
```

### US9E1 - User story 9
Add a Homework Assignment endpoint
```{xml}
Url: /assignments
Type: POST
Request Example:
{
  "assignment": {
    "homeworkId": "30dd879c-ee2f-11db-8314-0800200c9a66",
    "studentId": "student-123"
  }
}
```

### US10E1 - - User story 10
Submit Homework Assignment endpoint
```{xml}
Url: /assignments/{id}/submit
Type: PUT
Request Example:
{
  "submissionText": "Solution",
  "attachmentUrl": "http://file.com/solution.pdf"
}
```

### US11E1 -- User story 11
Grade Homework Assignment endpoint
```{xml}
Url: /assignments/{id}/grade
Type: PUT
Request Example:
{
  "grade": 95,
  "teacherFeedback": "Great work!"
}
``` 

### US12E1 - - User story 12
Add Calendar Event endpoint
```{xml}
Url: /calendar
Type: POST
Request Example:
{
  "event": {
    "title": "Exam",
    "description": "Math final exam",
    "startDateTime": "2025-09-15T10:00:00",
    "endDateTime": "2025-09-15T12:00:00",
    "type": "Exam",
    "userId": "30dd879c-ee2f-11db-8314-0800200c9a66"
  }
}
```

### US13E1 - User story 13
Get Calendar Events by user endpoint
```{xml}
Url: /calendar/user/{userId}
Type: GET
Response Example:
[
  {
    "id": "30dd879c-ee2f-11db-8314-0800200c9a66",
    "title": "Exam",
    "type": "Exam"
  }
]
```

### US14E1 - User story 14
Update Calendar Event endpoint
```{xml}
Url: /calendar
Type: PUT
Request Example:
{
  "event": {
    "id": "30dd879c-ee2f-11db-8314-0800200c9a66",
    "title": "Exam Updated",
    "description": "New room"
  }
}
```

### US15E1 - User story 15
Delete Calendar Event endpoint
```{xml}
Url: /calendar/{id}
Type: DELETE
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
