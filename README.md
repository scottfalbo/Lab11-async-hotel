# Lab11 Async Hotel

**Author**: Scott Falbo

**Version**: 1.0.1 

+ [Overview](#overview)
+ [Getting Started](#getting-started)
+ [ERD](#erd)
+ [Relationships](#relationships)
+ [Routing](#routing)
+ [Identity](#identity)
+ [Change Log](#change-log)
+ [Attributions](#attributions)


## Overview
An API for an international hotel chain.  The API should be able to return information about specific hotels, rooms, and amenities.  The API uses a relational data base to organize and store the data.

## Getting Started
+ `git clone https://github.com/scottfalbo/Lab11-async-hotel.git`
+ Run the App from Visual Studio or another compiler.
  + Beyold the blankest of screens.
+ `/api` has three endpoints
  + `/hotels` - returns data about the locations
  + `/rooms`  - returns data about the rooms
  + `/amenities` - returns data about the amenities


## ERD
![Async ERD](./assets/AsyncInnERD.png)<br>

## Relationships
+ `Hotel` - 1 : many - `HotelRoom` (join table)
+ `HotelRoom` - many : 1 - `Room`
+ `Room` - 1 : many - `RoomAmenities` (join table)
+ `RoomAmenities` - many : 1 - `Amenities`

## Routing
+ `/api/hotels`
```
{
  "id": 0,
  "name": "string",
  "streetAddress": "string",
  "city": "string",
  "state": "string",
  "phone": "string",
  "rooms": [
    {
      "hotelId": 0,
      "roomNumber": 0,
      "rate": 0,
      "petFriendly": true,
      "roomId": 0,
      "room": {
        "id": 0,
        "name": "string",
        "layout": 0,
        "amenities": [
          {
            "id": 0,
            "amenityName": "string"
          }
        ]
      }
  }
```
+ `/api/hotelrooms`
```
  {
    "hotelId": 0,
    "roomNumber": 0,
    "rate": 0,
    "petFriendly": true,
    "roomId": 0,
    "room": {
      "id": 0,
      "name": "string",
      "layout": 0,
      "amenities": [
        {
          "id": 0,
          "amenityName": "string"
        }
      ]
    }
  }
```
+ `api/rooms`
```
{
  "id": 0,
  "name": "string",
  "layout": 0,
  "amenities": [
    {
      "id": 0,
      "amenityName": "string"
    }
  ]
}
```
+ /api/amenities
```
{
  "id": 0,
  "amenityName": "string"
}
```

<hr>

## Identity
Identity is used to verify users and then control their access to content based on authority level.  The process starts when user requests data or content that requires authorization.

The request goes through the server where the middle where calls the de fault handler's auth method.  This returns an object with any available context for the user.

Then we hit the controller.  If the route doesn't have an `[Authorize]` annotation the thing loads and we are done because there are no permissions set.

If the route does require auth then it checks if the user has the proper permissions.  If the user is not they are redirected for login.  Once logged in the user is redirected back, if they have authority the page load.  If not we redirect to forbidden message page.



![Identity visual](https://digitalmccullough.com/images/aspnetcore-auth-system-demystified/aspnetcore-auth-system-demystified_auth-flow.svg)


## Change Log
+ **version 1.0.0** *01/25/2021* - Finished the worlds worst ERD and half way understand what is happeing.
+ **version 1.0.1** - *01/26/2021* - 
  + Changed ERD image to the Code Fellows provided one.  
  + Created and configured a new `.NET Core Web Application`
  + Installed:
    + `Microsoft.EntityFrameworkCore.SqlServer`
    + `Microsoft.EntityFrameworkCore.Tools`
    + `Microsoft.EntityFrameworkCore.Sqlite`
    + `Microsoft.VisualStudio.Web.CodeGeneration.Design`
  + Added the classes for the `Hotel`, `Room`, and `Amenities` entities, each other their corresponding properties.
  + Migrated the entities to a database, and seeded each of the three tables with three entires of dummy data.
  + Created `API Controllers`for `Hotel`, `Room`, and `Amenities`
  + **version 1.0.2** *01/27/2021* -
    + Refactored for dependency injection.  Built interfaces for `IHotel`, `IRoom`, and `IAmenity`
    + Built `Repositories` for the above classes to handle their interactions with the database and app.
    + Added `Controllers` for the classes to handle routing.
  + **version 1.0.3** *01/28/2021 - 
    + Added an `IHotelRoom` interface, `HotelRoom` model, `RoomAmenities` model.
    + Updated services to `Include` all linked data when a query is made against any object.
    + Updated summary comments in service files. 
  + **version 1.0.4** *02/01/2021 -
    + Attempted to add some DTOs.  Everything is broken and nothing works now.  
  + **version 1.0.5** *02/03/2021* -
    + All of the DTOs are now properly implemented.
    + Added dependencies for swagger and implemented the service, the main index route now returns a swagger dos CRUD page.
    + Added unit testing to the project.  Add hotel, room, and amenity tests passing.  Delete method works properly, however tests is not passing, still need to work that out.
    + Added Authentication with Identity in the Microsoft Framework.
    + Can add and user to the database and login them in via post and get(post technically) CRUD actions. 

<hr>

## Attributions

+ [Code Fellows Entity Framework Setup](https://codefellows.github.io/seattle-dotnet-401d12/class-12/resources/ef-web-app)<br>
+ Alan Hung, Bade Habib and David Dicken were all around to bounce ideas and help me work through some blockers.