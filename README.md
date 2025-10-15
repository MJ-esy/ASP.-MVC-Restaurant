# ASP.MVC - Restaurant Management System

## Introduction
This project is a **Restaurant Management System** built using **ASP.NET Core MVC** and **Razor Pages**. 
It is a Restaurant Website where anyone can see it's menu. 
Additionally, it offers a web-based interface for managing restaurant menu, tables and reservations to ADMINS.
It provides a streamlined way to manage restaurant tables, including functionalities such as viewing all tables, checking available tables, updating table details, and setting table availability. The solution is designed to be modular, scalable, and user-friendly, leveraging modern web development practices.

The project uses the following tools and technologies:
- **ASP.NET Core MVC**: A robust framework for building web applications with a Model-View-Controller architecture.
- **Razor Pages**: Simplifies the development of dynamic web pages with server-side rendering.
- **Bootstrap**: A popular CSS framework for responsive and mobile-first web design, built in MVC.
- **Bootswatch Lux Theme**: A custom theme from [Bootswatch](https://bootswatch.com/lux/) to enhance the visual appeal of the application.
- **.NET 8**: The latest version of the .NET platform, offering improved performance and features.
- **HttpClientFactory**: For making HTTP requests to external APIs in a clean and efficient manner.

## Integration with REST API
This solution integrates with another project I created, a **REST API** entitled **ASP.api-Reservations**. The API handles the backend logic for managing restaurant reservations and table data. You can find the API project here: [ASP.api-Reservations](https://github.com/MJ-esy/ASP.api-Reservations).

The integration is achieved using `HttpClientFactory` to communicate with the API endpoints, ensuring a seamless flow of data between the frontend and backend.

## Summary
The solution is structured to provide a seamless experience for managing restaurant tables. Key features include:
1. **View All Tables**: Displays a list of all tables in the restaurant.
2. **Check Available Tables**: Shows tables that are currently available for booking.
3. **Update Table Details**: Allows administrators to update table information, such as capacity or location.
4. **Set Table Availability**: Enables administrators to mark tables as available or unavailable.

### Project Highlights
- **Modular Design**: The project is divided into controllers, services, and views, ensuring a clean separation of concerns.
- **Responsive UI**: The use of Bootstrap and the Lux theme ensures the application is visually appealing and works well on all devices.
- **Error Handling**: Provides user-friendly error messages for scenarios like missing data or failed operations.
- **Integration with APIs**: Uses `HttpClientFactory` to interact with the **ASP.api-Reservations** REST API for fetching and updating table data.
- **Authentication and Authorization**: 
  - The system includes authentication to ensure only authorized users can access the admin functionalities.
  - Authorization is implemented to restrict access to administrative features, such as managing tables and reservations, to users with the "Admin" role.
  - This ensures that sensitive operations are protected and only accessible to verified administrators.
