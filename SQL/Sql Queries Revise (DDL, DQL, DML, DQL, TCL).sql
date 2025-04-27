============================================================================= DDL =============================================================================

Q1. Write a SQL query to create a table with specific columns and constraints.
CREATE TABLE Employees(
	EmployeeID INT PRIMARY KEY, 
	Name VARCHAR(40) NOT NULL,
	Address VARCHAR(250),
	Age INT Check(Age>0));

Q2. Write a SQL query to create a table with three columns: id, name, and age. Add a primary key constraint on the id column.
CREATE TABLE Students(
	StudentID INT PRIMARY KEY,
	Name VARCHAR(20),
	Age INT);


Q3. Write a SQL query to create a table with four columns: product_id, product_name, price, and quantity. Ensure that the price column cannot have negative values.
CREATE TABLE Products(
	Product_id INT PRIMARY KEY,
	Product_name VARCHAR(255),
	Price DECIMAL(10,2) CHECK(Price >= 0),
	Quantity INT);

Q4. Write a SQL query to create a table with two columns: user_id and email. Make sure the email column only accepts unique values.
CREATE TABLE UserDetails(
	User_id INT PRIMARY KEY,
	email Varchar(255) UNIQUE);


Q5. Write a SQL query to create a table with five columns: order_id, customer_id, order_date, total_amount, and status. Add a NOT NULL constraint to the order_date column.
CREATE TABLE OrderDetails(
	order_id INT PRIMARY KEY,
	customer_id INT,
	order_date DATE NOT NULL,
	total_amount DECIMAL(10,2),
	Status Varchar(30),


