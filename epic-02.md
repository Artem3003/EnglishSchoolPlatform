# Epic 2 - Headers and Unit Testing

## General requirements

System should support the following features:
* Total lessons count calculation.

Technical specifications:
* Code coverage rate must be not lower than **50%**.  
* Use **Moq** package for test data mocks.  
* Use **xUnit** package for unit tests.  

---

## Task Description

### E02 US1 - User story 1
Add `x-total-number-of-lessons` response header which represents the total lessons count in the system.  
A header should be added in **each API response**.  

**Hint**: The header should be exposed in CORS configuration.  

**Example:**

```
content-type: application/json; charset=utf-8
date: Fri, 17 Nov 2023 18:21:31 GMT
server: Kestrel
x-total-number-of-lessons: 12
```

---

### E02 US2 [Optional]
The `x-total-number-of-lessons` header value should be cached for **1 minute**.  

---

## Non-functional requirement (Optional)

**E02 NFR1**  
Cover application code with unit tests with code coverage not lower than **50%**. 
