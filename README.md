# BookStreetAPI

BookStreetAPI is a project aimed at providing an application for visitors to book streets in Ho Chi Minh City to search for book information, authors, and events, and for store managers and administrators to manage book street data efficiently.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [Running Migrations](#running-migrations)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Prerequisites

Ensure you have the following installed on your system:

- .NET SDK 7.0 or later
- MySQL Server
- Visual Studio or Visual Studio Code
- Git

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/vietanhdang/BookStreetAPI.git
   cd BookStreetAPI
   ```

2. Restore the dependencies:
   ```bash
   dotnet restore
   ```

## Configuration

1. Update the connection string in `appsettings.json` in the API project:
   ```json
   "ConnectionStrings": {
       "SystemDB": "Server=localhost;Database=BookStreet;User=yourusername;Password=yourpassword;"
   },
   ```

## Running the Application

1. Navigate to the API project directory:

   ```bash
   cd src/BookStreetAPI
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

The application should now be running on `https://localhost:5001`.

## Running Migrations

1. Navigate to the Infrastructure project directory:

   ```bash
   cd src/Infrastructure
   ```

2. Add a new migration:

   ```bash
   dotnet ef migrations add InitialCreate -s ../BookStreetAPI -p .
   ```

   - `-s ../BookStreetAPI`: Specifies the startup project which contains the application's `Program` class.
   - `-p .`: Specifies the project containing the `DbContext` (Infrastructure project).

3. Apply the migration to update the database:
   ```bash
   dotnet ef database update -s ../BookStreetAPI -p .
   ```

## Usage

Once the application is running, you can use tools like Postman to interact with the API endpoints. Refer to the API documentation for detailed information about the available endpoints.

## Contributing

Contributions are welcome! Please fork this repository and submit a pull request for any enhancements, bug fixes, or documentation updates.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
