# PV179 - Bookhub

The project is a web application that fulfills several key requirements, showcasing a data access layer, infrastructure, correct acces to the data dependency injection and various well-known designs and follows a MVC architecture patter. The application is a bookhub implementation and features an API with multiple endpoints.

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

4. **Docker:**
   Docker is recommended for containerization and running applications in isolated environments. Install Docker to support containerized development and deployment.

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
### Run the project
```shell
# Navigate to the project directory
cd <project_directory>

# Install project dependencies
dotnet restore

# Build the application
dotnet build

# Run the application
dotnet run
```
This will start your project. Access the application using a web browser at http://localhost:5000 (or the appropriate address).

## Folder Structure

## Project Folder Structure

The project is organized into several key folders to maintain a clean and organized codebase. Here's an overview of the main folders:

### BookhubWeAPI

- The presentation layer where API endpoints, controllers, mappers, and middleware are located.

### DataAccessLayer

- Handles database access, models, and related data operations.

### Infrastructure

- Contains repositories and follows the Unit of Work pattern for managing data operations.

### DAL.SQLite.Migrations

- A dedicated project for database migrations to manage changes to the database schema.


