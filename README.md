# Trainee Management API
---

# Tech Stack
- C#
- .NET

## How To Run

### Clone Repository
`git clone https://github.com/jayzeus113/Trainee-Management-Api`

### Navigate to Project directory
`cd Trainee-Management-Api`

  ---
### Database Setup Steps

#### Modify Connection string in appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": 
    "server=localhost;port=3306;database=trainee_management_db;user=root;password=root;"
  }
}
```

#### Apply migration to the database
`dotnet ef database update`

### Run Application
dotnet run

---

## API Endpoints

|Method| Endpoint |
|:---|:---|
|`GET`| `/api/health`|
|`GET`| `/api/trainees`|
|`GET`| `/api/trainees/{id}`|
|`POST`| `/api/trainees`|
|`PUT`| `/api/trainees/{id}`|
|`DELETE`| `/api/trainees/{id}`|
|`GET`| `/api/trainees?search={query}`|
|`GET`| `/api/mentors`|
|`GET`| `/api/mentors/{id}`|
|`POST`| `/api/mentors`|
|`PUT`| `/api/mentors/{id}`|
|`DELETE`| `/api/mentors/{id}`|
|`GET`| `/api/mentors?search={query}`|
|`GET`| `/api/learning-task`|
|`GET`| `/api/learning-task/{id}`|
|`POST`| `/api/learning-task`|
|`PUT`| `/api/learning-task/{id}`|
|`DELETE`| `/api/learning-task/{id}`|
|`GET`| `/api/learning-task?search={query}`|


---

GET
/api/HealthCheck
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/HealthCheck' \
  -H 'accept: */*'
```

#### Sample Response
```json
{
  "status": "running",
  "application": "Trainee Management API",
  "timestamp": "2026-06-08T12:46:04"
}
```

POST
/api/trainees

#### Sample Request

```bash
curl -X 'POST' \
  'https://localhost:7148/api/trainees' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
 "firstName": "Yash",
 "lastName": "Sharma",
 "email": "Yash.sharma@training.com",
 "techStack": "HTML",
 "status": "Active"
}'
```
##### Sample Request Json
```json
{
 "firstName": "Yash",
 "lastName": "Sharma",
 "email": "Yash.sharma@training.com",
 "techStack": "HTML",
 "status": "Active"
}
```

#### Sample Response

```json
{
  "id": 3,
  "firstName": "Yash",
  "lastName": "Sharma",
  "email": "Yash.sharma@training.com",
  "techStack": "HTML",
  "status": "Active",
  "createdDate": "2026-06-08T12:50:49.7688292Z",
  "updatedDate": "2026-06-08T12:50:49.7688295Z"
}
```

GET
/api/trainees
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/trainees' \
  -H 'accept: */*'
```
#### Sample Response

```json
[
  {
    "id": 4,
    "firstName": "Rheetik",
    "lastName": "Sharma",
    "email": "Rheetik.sharma@training.com",
    "techStack": "JAVA",
    "status": "Active",
    "createdDate": "2026-06-08T13:18:55.145839Z",
    "updatedDate": "2026-06-08T13:18:55.1458396Z"
  },
  {
    "id": 3,
    "firstName": "Yash",
    "lastName": "Sharma",
    "email": "Yash.sharma@training.com",
    "techStack": "HTML",
    "status": "Active",
    "createdDate": "2026-06-08T12:50:49.7688292Z",
    "updatedDate": "2026-06-08T12:50:49.7688295Z"
  }
]
```



GET
/api/trainees?search="query"
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/trainees?search=Amit' \
  -H 'accept: */*'
```
#### Sample Response

```json
[
  {
    "id": 1,
    "firstName": "Amit",
    "lastName": "Sharma",
    "email": "amit.sharma@training.com",
    "techStack": "HTML, CSS, JavaScript",
    "status": "Active",
    "createdDate": "2026-06-08T10:45:09.0138641Z",
    "updatedDate": "2026-06-08T10:45:09.0139184Z"
  }
]
```

GET
/api/trainees/{Id}
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/trainees/1' \
  -H 'accept: */*'
```
#### Sample Response

```json
{
  "id": 1,
  "firstName": "Amit",
  "lastName": "Sharma",
  "email": "amit.sharma@training.com",
  "techStack": "HTML, CSS, JavaScript",
  "status": "Active",
  "createdDate": "2026-06-08T10:45:09.0138641Z",
  "updatedDate": "2026-06-08T10:45:09.0139184Z"
}
```

PUT
/api/trainees/{Id}
#### Sample Request
```bash
curl -X 'PUT' \
  'https://localhost:7148/api/trainees/1' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "firstName": "Rheetik",
  "lastName": "Sharma",
  "email": "Rheetik.Sharama@trainee.com",
  "techStack": "JAVA",
  "status": "Inactive"
}'
```

##### Sample Request Json
```json
{
  "firstName": "Rheetik",
  "lastName": "Sharma",
  "email": "Rheetik.Sharama@trainee.com",
  "techStack": "JAVA",
  "status": "Inactive"
}
```

#### Sample Response

```json
{
  "id": 1,
  "firstName": "Rheetik",
  "lastName": "Sharma",
  "email": "Rheetik.Sharama@trainee.com",
  "techStack": "JAVA",
  "status": "Inactive",
  "createdDate": "2026-06-08T10:45:09.0138641Z",
  "updatedDate": "2026-06-08T13:05:28.5285887Z"
}
```

DELETE
/api/trainees/{Id}
#### Sample Request
```bash
curl -X 'DELETE' \
  'https://localhost:7148/api/trainees/1' \
  -H 'accept: */*'
```

POST
/api/mentors

#### Sample Request

```bash
curl -X 'POST' \
  'https://localhost:7148/api/mentors' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o' \
  -H 'Content-Type: application/json' \
  -d '{
 "firstName": "Amit",
 "lastName": "Sharma",
 "email": "amit.sharma@training.com",
 "expertise": "HTML, CSS, JavaScript",
 "status": "Active"
}'

```
##### Sample Request Json
```json
{
 "firstName": "Amit",
 "lastName": "Sharma",
 "email": "amit.sharma@training.com",
 "expertise": "HTML, CSS, JavaScript",
 "status": "Active"
}
```

#### Sample Response

```json
{
  "id": 4,
  "firstName": "Amit",
  "lastName": "Sharma",
  "email": "amit.sharma@training.com",
  "expertise": "HTML, CSS, JavaScript",
  "status": "Active",
  "createdDate": "2026-06-12T17:05:44",
  "updatedDate": "2026-06-12T17:05:44"
}
```

GET
/api/mentors
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/mentors' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o'
```
#### Sample Response

```json
{
  "data": [
    {
      "id": 1,
      "firstName": "Rheetik",
      "lastName": "Sharma",
      "email": "amit.sharma@training.com",
      "expertise": "HTML, CSS, JavaScript",
      "status": "Active",
      "createdDate": "2026-06-12T11:25:32",
      "updatedDate": "2026-06-12T11:27:57"
    },
    {
      "id": 2,
      "firstName": "Amit",
      "lastName": "Sharma",
      "email": "amit.sharma@training.com",
      "expertise": "HTML, CSS, JavaScript",
      "status": "Active",
      "createdDate": "2026-06-12T11:25:59",
      "updatedDate": "2026-06-12T11:25:59"
    },
    {
      "id": 4,
      "firstName": "Amit",
      "lastName": "Sharma",
      "email": "amit.sharma@training.com",
      "expertise": "HTML, CSS, JavaScript",
      "status": "Active",
      "createdDate": "2026-06-12T17:05:44",
      "updatedDate": "2026-06-12T17:05:44"
    }
  ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalRecords": 3
}
```



GET
/api/mentors?search="query"
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/mentors?Search=Rheetik' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o'
```
#### Sample Response

```json
{
  "data": [
    {
      "id": 1,
      "firstName": "Rheetik",
      "lastName": "Sharma",
      "email": "amit.sharma@training.com",
      "expertise": "HTML, CSS, JavaScript",
      "status": "Active",
      "createdDate": "2026-06-12T11:25:32",
      "updatedDate": "2026-06-12T11:27:57"
    }
  ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalRecords": 1
}
```

GET
/api/mentors/{Id}
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/mentors/1' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o'

```
#### Sample Response

```json
{
  "id": 1,
  "firstName": "Rheetik",
  "lastName": "Sharma",
  "email": "amit.sharma@training.com",
  "expertise": "HTML, CSS, JavaScript",
  "status": "Active",
  "createdDate": "2026-06-12T11:25:32",
  "updatedDate": "2026-06-12T11:27:57"
}
```

PUT
/api/mentors/{Id}
#### Sample Request
```bash
curl -X 'PUT' \
  'https://localhost:7148/api/mentors/1' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o' \
  -H 'Content-Type: application/json' \
  -d '{
  "firstName": "Rheetik",
  "lastName": "Sharma",
  "email": "Rheetik.sharma@training.com",
  "expertise": "HTML, CSS, JavaScript",
  "status": "Active"
}'
```

##### Sample Request Json
```json
{
  "firstName": "Rheetik",
  "lastName": "Sharma",
  "email": "Rheetik.sharma@training.com",
  "expertise": "HTML, CSS, JavaScript",
  "status": "Active"
}
```

#### Sample Response

```json
{
  "id": 1,
  "firstName": "Rheetik",
  "lastName": "Sharma",
  "email": "Rheetik.sharma@training.com",
  "expertise": "HTML, CSS, JavaScript",
  "status": "Active",
  "createdDate": "2026-06-12T11:25:32",
  "updatedDate": "2026-06-12T17:26:03"
}
```

DELETE
/api/mentors/{Id}
#### Sample Request
```bash
curl -X 'DELETE' \
  'https://localhost:7148/api/mentors/2' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o'
```


POST
/api/learning-tasks

#### Sample Request

```bash
curl -X 'POST' \
  'https://localhost:7148/api/learning-tasks' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o' \
  -H 'Content-Type: application/json' \
  -d '{
  "title": "task1",
  "description": "complete till the end of day",
  "expectedTechStack": ".NET",
  "dueDate": "2026-06-12T12:23:54.549Z",
  "status": "Draft"
}'
```
##### Sample Request Json
```json
{
  "title": "task1",
  "description": "complete till the end of day",
  "expectedTechStack": ".NET",
  "dueDate": "2026-06-12T12:23:54.549Z",
  "status": "Draft"
}
```

#### Sample Response

```json
{
  "id": 3,
  "title": "task1",
  "description": "complete till the end of day",
  "expectedTechStack": ".NET",
  "dueDate": "2026-06-12T12:23:54.549Z",
  "status": "Draft",
  "createdDate": "0001-01-01T00:00:00",
  "updatedDate": "0001-01-01T00:00:00"
}
```

GET
/api/learning-tasks
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/learning-tasks' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o'
```
#### Sample Response

```json
{
  "data": [
    {
      "id": 1,
      "title": "string3",
      "description": "string",
      "expectedTechStack": "string",
      "dueDate": "2026-06-12T10:43:37.347",
      "status": "Published",
      "createdDate": "0001-01-01T00:00:00",
      "updatedDate": "2026-06-12T16:16:29"
    },
    {
      "id": 3,
      "title": "task1",
      "description": "complete till the end of day",
      "expectedTechStack": ".NET",
      "dueDate": "2026-06-12T12:23:54.549",
      "status": "Draft",
      "createdDate": "0001-01-01T00:00:00",
      "updatedDate": "0001-01-01T00:00:00"
    }
  ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalRecords": 2
}
```



GET
/api/learning-tasks?search="query"
#### Sample Request
curl -X 'GET' \
  'https://localhost:7148/api/learning-tasks?Search=task1' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o'
#### Sample Response

```json
{
  "data": [
    {
      "id": 3,
      "title": "task1",
      "description": "complete till the end of day",
      "expectedTechStack": ".NET",
      "dueDate": "2026-06-12T12:23:54.549",
      "status": "Draft",
      "createdDate": "0001-01-01T00:00:00",
      "updatedDate": "0001-01-01T00:00:00"
    }
  ],
  "pageNumber": 1,
  "pageSize": 10,
  "totalRecords": 1
}
```

GET
/api/learning-tasks/{Id}
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/learning-tasks/3' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o'
```
#### Sample Response

```json
{
  "id": 3,
  "title": "task1",
  "description": "complete till the end of day",
  "expectedTechStack": ".NET",
  "dueDate": "2026-06-12T12:23:54.549",
  "status": "Draft",
  "createdDate": "0001-01-01T00:00:00",
  "updatedDate": "0001-01-01T00:00:00"
}
```

PUT
/api/learning-tasks/{Id}
#### Sample Request
```bash
curl -X 'PUT' \
  'https://localhost:7148/api/learning-tasks/1' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o' \
  -H 'Content-Type: application/json' \
  -d '{
  "title": "task management",
  "description": "simple task tracker",
  "expectedTechStack": "HTML, CSS, JavaScript",
  "dueDate": "2026-06-12T12:45:28.269Z",
  "status": "Published"
}'
```

##### Sample Request Json
```json
{
  "title": "task management",
  "description": "simple task tracker",
  "expectedTechStack": "HTML, CSS, JavaScript",
  "dueDate": "2026-06-12T12:45:28.269Z",
  "status": "Published"
}
```

#### Sample Response

```json
{
  "id": 1,
  "title": "task management",
  "description": "simple task tracker",
  "expectedTechStack": "HTML, CSS, JavaScript",
  "dueDate": "2026-06-12T12:45:28.269Z",
  "status": "Published",
  "createdDate": "0001-01-01T00:00:00",
  "updatedDate": "2026-06-12T18:16:37"
}
```

DELETE
/api/learning-tasks/{Id}
#### Sample Request
```bash
curl -X 'DELETE' \
  'https://localhost:7148/api/learning-tasks/3' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxIiwidXNlcm5hbWUiOiJhZG1pbiIsInJvbGUiOiJBZG1pbiIsImp0aSI6IjQ4MWY3OGEzLTk3YTQtNDRhNi1hNWMyLTA3YjY3ZDFjZWI4MyIsImV4cCI6MTc4MTI2NzM4MywiaXNzIjoiVHJhaW5lZU1hbmFnZW1lbnRBcGkiLCJhdWQiOiJUcmFpbmVlTWFuYWdlbWVudENsaWVudCJ9.pnYJuY0GJ4TkLtVMAEyj5vda0LauNFEnN3gv5CqOo2o'
```