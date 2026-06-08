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
/api/Trainee

#### Sample Request

```bash
curl -X 'POST' \
  'https://localhost:7148/api/Trainee' \
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
/api/Trainee
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/Trainee' \
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
/api/Trainee?search="query"
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/Trainee?search=Amit' \
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
/api/Trainee/{Id}
#### Sample Request
```bash
curl -X 'GET' \
  'https://localhost:7148/api/Trainee/1' \
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
/api/Trainee/{Id}
#### Sample Request
```bash
curl -X 'PUT' \
  'https://localhost:7148/api/Trainee/1' \
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
/api/Trainee/{Id}
#### Sample Request
```bash
curl -X 'DELETE' \
  'https://localhost:7148/api/Trainee/1' \
  -H 'accept: */*'
```

## Limitations
- In memory Db (No persistent)