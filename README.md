# Deposit and withdrawal management system

Transactions Management System

## server 
A RESTful API for processing and retrieving transaction history. This API enables users to process financial transactions and retrieve past transaction records based on their user ID.

Base URL: https://localhost:7104


### db
in appsettings.json change ConnectionStrings.DefaultConnection 

create Transactions table
`CREATE TABLE Transactions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(100) NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    BankAccount NVARCHAR(50) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    Date DATETIME NOT NULL
);
`

## frontend
A Redux-based transaction management system built with React, Redux Toolkit, and TypeScript. This project allows users to process financial transactions and view their transaction history.

Features
✅ User Authentication – Transactions are linked to a specific user ID.
✅ Process Transactions – Supports deposits and withdrawals.
✅ View Transaction History – Fetches and displays past transactions.
✅ State Management – Powered by Redux Toolkit.
✅ Data Persistence – Transactions are retrieved from an API.
✅ Error Handling – Displays meaningful error messages.

### how to run
`npm install`
`npm run dev`

