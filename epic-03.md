# Epic 3 - Logging and Swagger

## General requirements

System should support the following features:
* Inbound requests logging.  
* Error logging.  
* Swagger API documentation.  

---

## Technical specifications

* Use built-in logging infrastructure.  
* Use a global exception handler for error logging.  
* `.txt` files should be used as log storage.  
* A separate log file per day should be created automatically.  

---

## Task Description

### E03 US1 - User story 1
Add **Swagger infrastructure**.  
All endpoints (students, teachers, lessons, homework, calendar, profiles, etc.) should be accessible via the Swagger page.  

---

### E03 US2 - User story 2
Create **user-friendly response messages** in case of error (for example: “Lesson not found”, “Homework submission failed”, “Unauthorized access”).  

---

## Non-functional requirements (Optional)

**E03 NFR1**  
Create a middleware for logging request details. Logs should include:  
* User IP Address  
* Target URL  
* Response status code  
* Request content  
* Response content  
* Elapsed time  

**E03 NFR2**  
Add exception details logging. Logs should include:  
* Exception type  
* Exception message  
* Inner exceptions  
* Exception details  
* Stack trace  

**E03 NFR3**  
Implement a logging system that stores all logs produced by the application in file storage.  
Use a separate file for exception logs.  

**E03 NFR4 [Optional]**  
Add logs in main business layer methods (for example: lesson creation, homework assignment, student profile update).  
