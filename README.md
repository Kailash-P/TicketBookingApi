# TicketBookingApi

# Application Stack

.NET Core Web Api & PostgreSQL (back-end) 

# Design Patterns

1. Dependency Injection
2. Repository Pattern
3. DTO pattern

# Entity Framework

Code-First Approach has been followed.

# Layers

1. Models - View Models & Common models for the service.
2. DAL - Data Access Layer
  This layer contains the data model and the data  repositories that interact with the database.
3. BAL - Business Access Layer
   This layer contains the services that are exposed to the client and the data transfers which convert from view model - data model & vice versa.

# Authorization/Authentication

The authorization & authentication have been achieved using Azure Active Directory & OAuth2.0

# Unit Test

xUnit has been for the unit test of the services.
