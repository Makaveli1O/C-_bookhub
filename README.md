# PV179 - Bookhub

- **Name**: Bookhub
- **Techstack**
   - **Backend**
      - C# (ASP.NET Core for the API)
      - Entity Framework Core for data access layer and migrations
      - SQLite Database
      - MSSql Database
      - Swagger for API documentation
      - XUnit and NSubstitute for testing
   - **Frontend** Microsoft Model-View-Controller (MVC)
   - **Development tools**
      - Package Manager : NuGet
      - Version Control : Git
   - **Deployment tools**
      - Docker (DockerHub)
      - Openshift
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

4. **Docker (Optional):**
  Install Docker to support containerized development and deployment.

5. **Openshift (Optional)**
   Used for application deoplyment, registration for the sandbox and also need for download of the openshift tool (oc).

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

1. **Revisiting the code and implementing notes from reviewers (outside of project development team)**:
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
1. **Incorporate comments and suggestion from reviews**:
   - Same as for Milestone 2, use the reviews, revisit some parts of the code or gitlab repository and improve it  

2. **Create admin-specific endpoints in MVC for data modification**:
   - Include endpoints for CRUD operations on Books, Authors, Publishers, Orders etc.
   - Include endpoint to reset user passwords

3. **Ensure MVC directly connects to the Business layer, not using API endpoints**:
   - Create seperate project for MVC and WebAPI

4. **Develop a Search feature for Books, Publishers, Authors.**:
   - Implement search functionality
   - Add pagination for some entities

5. **Implement caching in your application**:
   - Cache frequently accessed data

6. **Minimize the differences in features between the API and the MVC**:
   - Ensure both API and MVC offer similar functionalities
   - Exception: Different authentication methods allowed for API

7. **Optimize code to minimize redundant database calls**:
   - Check for multiple database calls when using mappers and lazy loading

8. **Allow users to view their Orders with payment status**:
   - Order state was already introduced in the first milestone, now only correct presentation is needed

9. **Revert to the original database as per customer request**:
   - Revert back to SQLite after using the MSSQL database
   - Keep migration in separate projects

10. **Modify Category x Product relationship to many-to-many**:
   - NOTE: After consulation with the tutor, we decided that this feature/change request will be done on Authors x Books
   - Set a single primary author for each book
   - Display primary author on the book detail page (also in the listing)

11. **Implement request logging in MVC using existing API middleware**:
   - Distinguish and label logs from API and MVC sources


### Final Milestone/Submission
 1. **Again revisit and correct any mistakes/bugs that may have arisen during the development**:
   - Incorporate suggestions from reviews

2. **Deploy application**:
   - Use openshift for application deployment (chosen by the development team)


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

3. (Optional) For successfull replication of our deployment, you will also need DockerHub account or some other registry supported by openshift


### Step 4: Register for Openshift Sandbox (Optional for Deployment)

1. Register for free sandbox:
   - [Red Hat Openshift](https://www.redhat.com/en/technologies/cloud-computing/openshift)

2. Do not forget to also download openshift or oc:
   - Use the following [link](https://developers.redhat.com/products/openshift/download)


## Usage

Follow these steps to use the project effectively:

### Clone the Repository

```shell
git clone https://gitlab.fi.muni.cz/xbaran4/bookhub.git
```

### Many Paths but only you can choose the right one!
There are basically many paths you can take and create a demo for yourself. Let us briefly summarize them all.

### I. Visual Studio "Deployment"
After running the `git clone` command, this is the easiest path you can take to create a demo.  
Start the Visual Studio and firsty update the database. The SQLite database is default, but you can easily change this by introducing a new database in the DAL project, Dependency Injection folder, where a strategy pattern is waiting for your db needs. Do not forget to change also the application.settings.  
After handling the initial configuration, proceed with following:
1. Open PMC (`Tools -> Nuget Package Manager -> Package Manager Console`).
2. Update the database
```shell
# update databse based on existing snapshot(s)
Update-Database -project  DAL.SQLite.Migrations # default

# or make changes to entities and create a new migration
Add-Migration <migration_name> -project <project_name>
# when using the newly created migration, do not forget to update the database again

# or consider using existing MSSQL or even your newly created database
Update-Database -project DALMSSql.Migrations # you can even apply your own migration!
```
3. Database updated? Nice! Now you can just start the application from Visual Studio, we recommend MVC for better demo, or WebAPI for the Swagger and/or OpenAPI fans.

#### Alternative to the Visual Studio Start Button
For the hardcode CLI fans.
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

### II. Local Docker Deployment
Do not worry, we prepared dockerfiles just for your need (also with the SQLite built-in migrations), so if you are not tampering with configuration this might be even easier than the process with Visual Studio.  
Just follow these 2 steps:
1. Build the docker image
```shell
# Navigate to the project directory
cd <project directory>

docker build --no-cache --file MVCDockerfile -t your_tag . # the last 'dot' character is crucial also!
# no-cache flag is optional
# you can also use the WEBAPIDockerfile, again we will leave this to you
```
2. Run the image in newly created container
```shell
# after the image is created, you can run it
docker run -e ASPNETCORE_URLS=http://+:5000 -e ASPNETCORE_ENVIRONMENT=Production -d -p 8080:5000 -v your/local/storage:/app/data --name container_name your_tag
# do not forget to specify environment and corresponding appsettings
```


### III. Last but not least, the heavyweight Openshift (global) deployment
This will be (by far) the most painful one, because of the requirements and changes that have to be made to the existing files (mainly .yaml).  
1. The easiest approach is to just run the Powershell script that we included in OC_Deployment folder (sorry no-windows users).  
2. But there is of course a catch, this deployment works only for specific DockerHub user and also openshift required that the user is logged in.  
3. If you have any interest in doing these steps, you will have to change the TAG in this script (`$docker_image_name`) and this have to be also modified for the openshift yaml files e.g. pod.yaml and/or pvc.yaml. The files images must me matched based on your tag.
4. If you managed to do all of these 'easy' steps then good job!  
5. Now you can just run the powershell script:
```shell
# navigate to deployment folder
cd OC_Deployment
# do not forget to also enable the script execution policy this is disable by default
# check Set-ExecutionPolicy and Get-ExecutionPolicy (hint: unrestricted)
# read the script before executing, just to be sure we are not creating some backdoor
OC_Deploy.ps1 MVC
# alternative is WEBAPI, should be case insensitive, the script will translate it to upper/lower case
```


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
