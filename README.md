# Lab11 Async Hotel

**Author**: Scott Falbo

**Version**: 1.0.1 


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


## Example
![Async ERD](./assets/AsyncInnERD.png)<br>

## Relationships
+ `Hotel` - 1 : many - `HotelRoom` (join table)
+ `HotelRoom` - many : 1 - `Room`
+ `Room` - 1 : many - `RoomAmenities` (join table)
+ `RoomAmenities` - many : 1 - `Amenities`

## Architecture 



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

## Attributions

+ [Code Fellows Entity Framework Setup](https://codefellows.github.io/seattle-dotnet-401d12/class-12/resources/ef-web-app)<br>
