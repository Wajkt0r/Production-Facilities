# Production Facilities API - User Guide

## Authentication

Each request must include the `x-api-key` header with the provided access key.  
**Example:**  
`x-api-key: YOUR_API_KEY_HERE`

---

## Available Endpoints

### 1. Retrieve All Contracts

**Endpoint:**  
`GET https://your-api-url.com/Contract/GetAll`

**Headers:**  
```http
x-api-key: YOUR_API_KEY_HERE
```
Example Response:
```http
[
    {
        "productionFacilityCode": "FAC002",
        "equipmentTypeCode": "EQ003",
        "unitsNumber": 1
    },
    {
        "productionFacilityCode": "FAC001",
        "equipmentTypeCode": "EQ001",
        "unitsNumber": 5
    }
]
```

### 2. Add a New Contract

**Endpoint:**
`POST https://your-api-url.com/Contract/Add`

**Headers:**  
```http
Content-Type: application/json  
x-api-key: YOUR_API_KEY_HERE
```
**Body**
```http
{
  "productionFacilityCode": "FAC001",
  "equipmentTypeCode": "EQ001",
  "unitsNumber": 2
}
```

**Example Response (success):**

```http
{
  "isSuccess": true,
  "message": "Successfully added equipment type EQP300 to the facility FAC003"
}
```
**Example Response (error):**

```
{
    "isSuccess": false,
    "message": "No space for this contract",
    "statusCode": 400
}
```

## Seeded Data
The following data is seeded into the database when it is first created:

### Production Facilities:

* FAC001 - Main Production Facility (Standard Area for Equipment: 150.0)
* FAC002 - Secondary Facility (Standard Area for Equipment: 100.0)

### Equipment Types:

* EQ001 - Drill Machine (Area: 25.0)
* EQ002 - Lathe Machine (Area: 30.0)
* EQ003 - CNC Machine (Area: 40.0)
