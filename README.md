# CarBazar
A Car Rental Management System that allows users to book cars, manage payments, and track rental history. Features include car ratings, CRUD operations, and real-time availability of vehicles.

## Table of Contents
- [About the Project](#about-the-project)
- [Key Features](#key-features)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Database Relationship Diagram](#database-relationship-diagram)
- [Database Schema](#database-schema)
  - [Admin Table](#admin-table)
  - [Customer Table](#customer-table)
  - [Car Table](#car-table)
  - [Booking Table](#booking-table)
  - [Report Table](#report-table)
- [Windows Forms Designs](#windows-forms-designs)
  - [Admin's Features](#admins-features)
- [Customer's Features](#customers-features)
- [Contact](#contact)


## About the Project
The Car Rental Management System is a comprehensive application designed to streamline the processes involved in car rentals for both customers and administrators. This system enables users to efficiently manage car bookings, customer information, and payment transactions while providing insights into business performance.
### The Problems It Solves:
Car rental businesses often struggle with:

- **Inefficient Booking Processes**: Traditional methods can lead to errors and lost revenue. This system automatically calculates the rental cost based on the selected dates, allowing users to easily add cars to their cart for a seamless booking experience.
- **Inventory Management**: Keeping track of available vehicles and their statuses can be cumbersome without a proper system.
- **Customer Feedback**: Many businesses lack a structured way to gather and analyze customer feedback, hindering service improvement.

### Who It Is For:
This system is designed for:

- **Car Rental Companies**: Both small local businesses and larger franchises looking to streamline their operations and improve customer satisfaction.
- **Customers**: Individuals looking to rent cars easily, manage bookings, and provide feedback on their experiences.


## Key Features 
- Both Admin and Customer can use this application.  
- Users can sign up as new Customers.  
- Admin can manage (add, remove, update,search) Cars, Customers and other Admins.  
- Admin can view business performance and revenue data.  
- Admin can see submitted feedback along with the corresponding Customer ID.  
- Customers can add cars to their cart.  
- Customers can submit feedback.  
- Customers can rent cars using two payment options and return the car.

## Technologies Used

- **Programming Language**: C#
- **User Interface**: Windows Forms (WinForms)
- **Database**: Microsoft SQL Server
- **IDE**: Visual Studio

# Getting Started

## Prerequisites
List the software and tools required to run the project. Example:

- [SQL Server](https://www.microsoft.com/en-us/sql-server) or an alternative (SQLite, MySQL)
- [Visual Studio](https://visualstudio.microsoft.com/) or another C# IDE

## Installation
Step-by-step guide on how to install and run the project locally:
1. Clone the repository.
   ````bash
       git clone https://github.com/your-username/your-repository.git
   ````
2. Navigate to the project directory.
   ````bash
   cd your-repository
   ````
3. Set up the database.
   - Create a new SQL Server database named `CarBazar` .
   - Run the provided SQL scripts in the `CarBazar.Sql` file to create the necessary tables and relationships. The scripts should include commands to set up tables such as Car, Customer, Booking, Admin, Report and Payment.
     
4. Open the solution in Visual Studio and build the project.

## Usage
**Editing the Connection String**
To connect the application to your SQL Server database, you will need to update the connection string in your project. Follow these steps:

1. **Open the Project**
   Open the Project file and then locate the `.Sln` file and double click on that file the and that should be open.

2. **Changing the Connection String**:
   - Use `Ctrl+F` to find the SqlConnection String. 
   ````bash
          SqlConnection con = new SqlConnection("Data Source=LAPTOP-J7JBFG4A\\SQLEXPRESS;Initial Catalog=EmpCRUD;Integrated Security=True");
   ````
   - Replace the existing Connection string with your connection String.
   ````bash
          SqlConnection con = new SqlConnection("Your Connection String");
   ````
4. **Save the edited code and run**

## Database Relationship Diagram

![image](https://github.com/user-attachments/assets/0e5a4287-a1ac-44b4-b93d-bd744273f712)


## Database schema

1. ### **Admin Table**
   
    ![WhatsApp Image 2024-09-22 at 12 19 06_77a8c5f0](https://github.com/user-attachments/assets/49ff521a-c758-4791-8708-4145de345a3c)
   
3. ### **Customer Table**
   
    ![image](https://github.com/user-attachments/assets/87421e0a-de18-47f0-93d3-7dbb7e1a38ad)

   
5. ### **Car Table**
   
    ![image](https://github.com/user-attachments/assets/80d63849-e0fd-43a7-8963-ebfb2e7b7739)
   
7. ### **Booking Table**
   
   ![WhatsApp Image 2024-09-22 at 12 19 33_fcde11c3](https://github.com/user-attachments/assets/3043e4fe-236e-4ca3-a155-4896fb5f3bdc)
   
9. ### **Report Table**
    
   ![image](https://github.com/user-attachments/assets/20a582f3-c351-47fa-b54d-a75d3c978e2f)


## Windows Forms Designs

### Admin's Features 

-  **Login Interface**:  User have to select the role first (Admin or Customer). Then  Fillup the required Boxes and click the Login button to Login to in the application

  ![image](https://github.com/user-attachments/assets/4d5cd211-498a-4faa-9508-aa5d9be685ed)

-  **Admin Home**:

  ![image](https://github.com/user-attachments/assets/90f45a9c-ba88-4141-89d9-0073dbebe2b0)

- **Admin DashBoaard**: Admin can see the information by clicking the respected buttons  

  ![image](https://github.com/user-attachments/assets/e8086c54-903f-42d0-a827-5c0e384ba0ae)

- **Add Admin,Customer and Car**:
  
  ![image](https://github.com/user-attachments/assets/761e0c1a-8327-410c-b8c8-38d669c7bd39)

  ![image](https://github.com/user-attachments/assets/aaf44202-f348-4cc7-b98b-e11c56d5fde3)

  ![image](https://github.com/user-attachments/assets/473e6643-580e-4949-8ffe-cf622edd00a9)

- **Edit Information**:

  ![image](https://github.com/user-attachments/assets/e9c8c991-ef01-4299-9224-ebbcd1e58cce)

  ![image](https://github.com/user-attachments/assets/3ddfbbd0-36c6-4469-a3ff-d562a6e3b532)

  ![image](https://github.com/user-attachments/assets/f205e655-0993-4cd4-9cc9-6d621a88f2c2)
 
- **See Feedbacks**:

  ![image](https://github.com/user-attachments/assets/4d984b38-8628-4d2b-a129-dbb52e81dafc)

### Customer's Features

- **SignUp Interface**: Only Customer can signup by clicking the SignUp button on the Login page. Then he/she need to fillup the required information and click the signup button of SignUP page

  ![image](https://github.com/user-attachments/assets/87394840-1516-4272-a2d4-7eff34c6667d)

- **Customer Home**:
  
  ![image](https://github.com/user-attachments/assets/b5ca389f-8257-4786-9172-54ed826f0a8f)

- **Rent Cars**:

  ![image](https://github.com/user-attachments/assets/14382d09-4a4b-41fe-adcf-2fcfe0e1315b)

  ![image](https://github.com/user-attachments/assets/f08cc0c9-fb6c-4ed8-816b-0b1fc7ec2a6c)


- **Return Cars**:

  ![image](https://github.com/user-attachments/assets/5ad32bad-a351-4305-ad2c-d6d4621d0b15)

- **Cart**:

  ![image](https://github.com/user-attachments/assets/32deae5f-f530-4908-9cde-8a62ec85e0f5)

  ![image](https://github.com/user-attachments/assets/b0f3ef9d-f2b8-459f-aaea-6c44c756524a)

- **Feedback Submission**:

  ![image](https://github.com/user-attachments/assets/abfdec14-68c3-412c-9b79-6eda0ff524f8)

- **Paymet**:

  ![image](https://github.com/user-attachments/assets/12f83b34-c7c1-4910-988b-f5bb6ca9c954)

## Contact
Provide information on how to reach the author or maintainers of the project:

- **Name**: Zihaul Islam Zihan
- **Email**: Zihanislam231@gmail.com
- **GitHub**: [Your GitHub Profile](https://github.com/Zihan231)
  




















  
