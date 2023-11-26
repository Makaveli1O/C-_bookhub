# PV179 - Bookhub

- **Name**: Bookhub
- **Techstack**
   - **Backend**
      - C# (ASP.NET Core for the API)
      - Entity Framework Core for data access and migrations
      - SQLite Database
      - MSSql Database
      - Swagger for API documentation
      - XUnit and NSubstitute for testing
   - **Frontend** _(TODO)_
   - **Development tools**
      - Package Manager : NuGet
      - Version Control : Git
- **Team Leader**
   - Oliver Šintaj
- **Developers**
  - Pavol Baran
  - Jozef Mihale
  - Samuel Líška
- **Assignment :** Develop a digital platform for the company called "BookHub", a company that sells books of various genres. The platform should facilitate easy browsing and purchase of books, letting customers sort and filter by authors, publishers, and genres. After customers create accounts, they should be able to review their purchase history, rate books, and make wishlists. Administrators should have privileges to update book details, manage user accounts, and regulate book prices.
- **Additional Functionality :** Create multiple book store (branches), where each book store has its own manager that is responsible for managing stock (books). Orders should also be modified to adhere to this new functionality.


## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Folder Structure](#folder-structure)

## Prerequisites

1. **.NET 7.0 Runtime:**
   Ensure that you have the .NET 7.0 Runtime installed on your development machine. You can download it from the official [.NET website](https://dotnet.microsoft.com/download).

2. **Integrated Development Environment (IDE):**
   You will need an Integrated Development Environment (IDE) for .NET development. We recommend using Visual Studio as it provides a comprehensive set of tools for .NET development.

3. **Visual Studio Installer Workloads:**
   In Visual Studio, make sure to select the following workloads using the Visual Studio Installer:
     - "ASP .Net and web development."

4. **Docker: (Not yet implemented)**
  Install Docker to support containerized development and deployment.

## Project Tasks

### Milestone 1

1. **Design & Documentation**:
   - Make a use-case diagram and data model for the application.
   - Create a documentation Technical Overview (can be part of the readme) with the diagrams and information about the application.

2. **API & Backend**:
   - Create a REST web API capable of fetching products based on their 'name', 'description', 'price', 'Category', and 'Manufacturer'.
   - Setup the database and introduce a DAL (Data Access Layer) to your project.
   - Seed the database with real-looking data. Avoid using placeholders like 'AAAA', 'test', etc.
   - Create an Authentication Middleware (a simple middleware using only a hard-coded access token is acceptable).
   - Implement a Logging middleware that logs all incoming requests.

3. **Version Control & Collaboration**:
   - Set up a GitLab repository and set its visibility to Internal (not Private).
   - Divide the work between the Team Lead and Team members.
   - Each team member must contribute equally. Every member should:
     - Create a database entity.
     - Seed the created entity.
     - Create CRUD operations (Web API Endpoints) for the created entity.
   - All changes should be committed to separate branches and merged via merge requests.
     - At least one approval from a team member is required for merge requests.
     - The team member who approves a merge request is also responsible for the changes. This rule starts from the 2nd milestone.
   - All merge requests for milestone 1 should be merged into a branch named 'Milestone 1'. Once done with the milestone, initiate a merge request from 'Milestone 1' to 'master'. This allows reviewers to see all changes in one place. The 'Milestone 1' branch can be merged only after teacher approval.

4. **Documentation & Onboarding**:
   - Create a README that provides basic information about how to run the application, its components, etc.

### Milestone 2

1. **Revisiting the code and implementing notes from reviewers (outside of project)**:
   - Based on the reviews from other colleagues and tutors, revisit some parts of the code to improve it.
   - Create some new entities instead of strings (Author, Publisher).

2. **Infrastructure and Business Layer**:
   - (Optional) Create/update infrastructure to support patterns such as repository, unit of work and/or query.
   - Create layers Service and Facade to further split the presentation layer from any logic and keep it just for the view.
   - Create meaningful tests for some services and facades (required 1 for each member of the team).
   - Set-up CI/CD pipeline for merge request.

3. **New gitlab settings**:
   - Only successful request can be merged (successful pipeline).
   - 2 approvals from reviewers are required to merge each request.

4. **Integrate an Identity Framework**:
   - Develop a separate MVC application for this.
   - For the current milestone, only implement authentication using the Identity Framework.

5. **Json to XML Middleware**: 
   - Create new Middleware that can transform Json response to XML (based on user/client input).
   - Default format should be JSON unless specified in the query parameter.

6. **Simultaneous Running of Applications**:
   - Ensure that MVC and WebAPI are set up as separate projects.
   - Allow each presentation layer: Operate under different configurations, Use different database setups and Maintain clear boundaries, ensuring API endpoints are not accessible from MVC, and vice versa.
   - Each layer should also have different database (e.g. SQLite for WebApi and MSSql for MVC)



### Milestone 3

## Installation

### Step 1: Install .NET 7.0 Runtime

1. Visit the official [.NET website](https://dotnet.microsoft.com/download).

2. Download the .NET 7.0 Runtime for your operating system (Windows, macOS, or Linux).

3. Follow the installation instructions to install the .NET Runtime on your machine.

### Step 2: Install an Integrated Development Environment (reccomended IDE is Visual Studio)

1. Download and install Visual Studio from the official website: [Visual Studio](https://visualstudio.microsoft.com/).

2. During installation, make sure to select the appropriate workloads, such as "ASP .Net and web development," using the Visual Studio Installer.

### Step 3: Install Docker

1. Download Docker from the official website based on your operating system:
   - [Docker for Windows](https://docs.docker.com/desktop/install/windows-install/)
   - [Docker for macOS](https://docs.docker.com/desktop/install/mac-install/)
   - [Docker for Linux](https://docs.docker.com/desktop/install/linux-install/)

2. Follow the installation instructions for your specific operating system to install Docker.


## Usage

Follow these steps to use the project effectively:

### Clone the Repository

```shell
git clone https://gitlab.fi.muni.cz/xbaran4/bookhub.git
```
### Update Databse based on migration
Before running the program itself, use the DAL.SQLite.Migrations project to update the databse.  
When running with Visual Studio, open PMC (`Tools -> Nuget Package Manager -> Package Manager Console`).
```shell
# update databse based on existing snapshot(s)
Update-Database -project  DAL.SQLite.Migrations # use for WEBAPI

# or make changes to entities and create a new migration
Add-Migration <migration_name> -project DAL.SQLite.Migrations
# when using the newly created migration, do not forget to update the database

# to use the Web MVC use the MSSql Database (and project)
Update-Database -project DALMSSql.Migrations # or consider using new migration
```
### Set-up configuration (only for MSSql Database)
Navigate to appsettings.json in the MVC project and set-up your connection string (based on your MSSql Database server).

### Run the project
```shell
# Navigate to the project directory
cd <project_directory>

# Install project dependencies
dotnet restore

# Build the application
dotnet build <PROJECT> # specify which project you want to run WebApi or MVC

# Run the application
dotnet run --project <PROJECT> # again specify project
```
This will start your project. Access the application using a web browser at http://localhost:5000 (or the appropriate address).

## Folder Structure

The project is organized into several key folders to maintain a clean and organized codebase. Here's an overview of the main folders:

### BookhubWeAPI

- The presentation layer where API endpoints, controllers, mappers, and middleware are located.

### MVC

- Model-View-Controller project or different presentation layer for BookHub company.

###  BusinessLayer.Tests

- Facade and service layer tests for some entities.

###  BusinessLayer

- Layered architecture components to distinguish between DAL and presentation layer.

### DataAccessLayer

- Handles database access, models, and related data operations.

### Infrastructure

- Contains repositories and follows the Unit of Work pattern for managing data operations.

### DAL.SQLite.Migrations

- A dedicated project for SQLite database migrations to manage changes to the database schema.

###  DAL.SQLite.Migrations

- A dedicated project for MSSql database migrations to manage changes to the database schema.

### Infrastructure

- Project for patterns such as Repository, UnitOfWork and some (Naive) Query.

### TestUtilities

- Project for mocking data for tests.

## Technical overview

### Dependencies

- **[.NET 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)**
   - [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0)
   - [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
   - [Swashbuckle](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-7.0&tabs=visual-studio)

- **[SQLite](https://www.sqlite.org/index.html)**
   - [Microsoft.EntityFrameworkCore.Sqlite](https://learn.microsoft.com/en-us/ef/core/providers/sqlite/?tabs=dotnet-core-cli)

- **[MSSql](https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&ved=2ahUKEwiAg7Wi0uKCAxXPhv0HHZ7cDCIQFnoECBgQAQ&url=https%3A%2F%2Fwww.microsoft.com%2Fen-us%2Fsql-server%2Fsql-server-downloads&usg=AOvVaw0d74lgRcnfX6ZThGwL_ED6&opi=89978449)**
   - [Microsoft.EntityFrameworkCore.MSSql](https://learn.microsoft.com/en-us/ef/core/providers/sql-server/?tabs=dotnet-core-cli)


### Use-Case Diagram

![My UML Diagram](https://gitlab.fi.muni.cz/xbaran4/bookhub/-/raw/main/diagrams/usecase/usecase_diagram.png?ref_type=heads)

### ERD Diagram

![My UML Diagram](https://gitlab.fi.muni.cz/xbaran4/bookhub/-/raw/main/diagrams/ERD/erd.png?ref_type=heads)
