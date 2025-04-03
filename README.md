# ASP-.NET
Operations in .NET

# DB tables

SELECT TOP (1000) [gender_id]
      ,[gender_name]
  FROM [Test].[dbo].[Gender]

SELECT TOP (1000) [dept_id]
      ,[dept_name]
  FROM [Test].[dbo].[Department]

SELECT TOP (1000) [emp_code]
      ,[name]
      ,[designation]
      ,[age]
      ,[gender]
      ,[salary]
      ,[department]
  FROM [Test].[dbo].[Employees]

#DB SP

ALTER proc [dbo].[sp_InsertEmployee]

 @name varchar(50),
 @designation varchar(50),
 @age int,
 @gender varchar(50),
 @salary DECIMAL(10,2),
 @department varchar(50)

 AS 
 Begin
 Insert into Employees(name,designation,age,gender,salary,department)
 values (@name,@designation,@age,@gender,@salary,@department)
 End

 ALTER proc [dbo].[sp_UpdateEmployee]
 @emp_code int,
 @name varchar(50),
 @designation varchar(50),
 @age int,
 @gender varchar(50),
 @salary decimal(10, 2),
 @department varchar(50)
 As
 begin
  update Employees
  set name = @name,
  designation = @designation,
  age = @age,
  gender = @gender,
  salary = @salary,
  department = @department
  where emp_code = @emp_code

  end

ALTER PROCEDURE [dbo].[sp_DeleteEmployee]
    @emp_code INT
AS
BEGIN
    DELETE FROM Employees WHERE emp_code = @emp_code
END
