# Voyado tech assignment.
The service should make a search against two or more search engines (e.g.,
Google, Bing, Yahoo, Twitter, Web Search, Algolia, AltaVista…) and present the
total number of search hits from each search engine. Only the
number of hits should be presented to the user, not the hits themselves.

# Voyado Project

This repository contains a .NET 8 solution. The project is structured to support modern web application development, leveraging the latest .NET.

## Features

- **.NET 8 Backend**: High-performance, scalable API and business logic.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Git](https://git-scm.com/)

## Getting Started

1. **Clone the repository**
2. **Navigate to the project directory**
3. **The file appsettings.development.json contains configuration for the search engines and should be provided to you separetly. Add this to the project root**
4. **Run the application in debug mode using https**
5. **https://localhost:7177/swagger/index.html should automatically open. If not, paste the the url into the any browser**
6. **In Swagger, click on the endpoint GET: /search/GetNbrOfResults/{searchString} and 'Try it out'**
7. **Write your string into the 'searchString' input field**
8. **The total number of hit results from Google and MediaWiki should be visible under 'Response'**