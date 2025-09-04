# English School Platform API

A comprehensive .NET 9 Web API for managing an English school system with admin panel functionality.

## Description

The **English School Platform** is a modern web application designed to support online education and communication between students, teachers, and administrators.  
It provides a complete digital ecosystem for managing lessons, homework, calendars, and profiles, while also leveraging AI to enhance the learning experience.  

## Application Features

As a result of completing this task, you will receive an application that includes the following functionality:

- **User Roles and Management**: Admins, teachers, and students each have role-specific access and functionality.
- **User Authentication and Authorization**: Users can register, log in, and manage their personal accounts with role-based permissions.
- **Course Management**: Admins and teachers can create, edit, and remove courses; students can enroll and view course materials.
- **Schedule and Timetable**: Teachers and students can manage and view lesson schedules in real-time.
- **Student Progress Tracking**: Teachers and admins can monitor student attendance, homework completion, and test performance.
- **Unit Testing**: The platform includes unit tests to ensure backend reliability and correctness.
- **API Documentation**: Swagger is integrated to document all backend endpoints clearly.
- **Logging and Error Handling**: Comprehensive logging and error management systems are implemented.
- **Multi-language Support**: The platform supports multiple languages via localization features.
- **Notifications**: Students and teachers receive timely notifications regarding lessons, assignments, and messages.
- **UI Design Implementation**: A user-friendly UI aligned with provided Figma/mock-up designs.
- **Chat and Communication**: Real-time or asynchronous messaging between students and teachers.
- **Image and File Uploads**: Teachers can upload teaching materials, and students can submit assignments.
- **NoSQL Integration**: The application utilizes both relational and NoSQL databases where appropriate.
- **Cloud Readiness**: The platform is compatible with cloud services such as Microsoft Azure and AWS.

## .NET Platform

This project is built with the .NET 9 SDK and supports development using Visual Studio 2022 or Visual Studio Code with the .NET Core CLI.

## Branching

The application is divided into multiple epics. Each epic corresponds to a feature or major module and is implemented in a separate branch with its own `epic-##.md` description file.

| Epic | Epic Title | Epic Description | Feature Branch Name              |
|------|------------|------------------|----------------------------------|
| 1.   | Admin Dashboard | Create admin panel to manage users, courses, and schedules. | [english-school-epic-01](epic-01.md) |
| 2.   | Authentication and User Roles | Implement registration, login, and role-based access. | [english-school-epic-02](epic-02.md) |
| 3.   | Course Management | Enable teachers to create and manage course content. | [english-school-epic-03](epic-03.md) |
| 4.   | Scheduling | Add lesson scheduling features for students and teachers. | [english-school-epic-04](epic-04.md) |
| 5.   | Student Performance | Implement tracking for grades, attendance, and homework. | [english-school-epic-05](epic-05.md) |
| 6.   | File and Image Upload | Enable materials and assignment upload/download. | [english-school-epic-06](epic-06.md) |
| 7.   | Messaging System | Add chat or messaging functionality. | [english-school-epic-07](epic-07.md) |
| 8.   | Unit Tests and CI/CD | Create unit tests and integrate CI/CD tools. | [english-school-epic-08](epic-08.md) |
| 9.   | Logging and Swagger | Set up structured logging and API documentation. | [english-school-epic-09](epic-09.md) |
| 10.  | Notifications | Add support for real-time or scheduled notifications. | [english-school-epic-10](epic-10.md) |
| 11.  | NoSQL Integration | Use a NoSQL database for storing unstructured data. | [english-school-epic-11](epic-11.md) |
| 12.  | UI Design [Angular] | Apply the given design mock-ups using Angular. | [english-school-epic-12](epic-12.md) |
|      | UI Design [React] | Apply the given design mock-ups using React. | [english-school-epic-12-react](epic-12.md) |
| 13.  | Localization [Angular] | Add multi-language support using Angular. | [english-school-epic-13](epic-13.md) |
|      | Localization [React] | Add multi-language support using React. | [english-school-epic-13-react](epic-13.md) |
