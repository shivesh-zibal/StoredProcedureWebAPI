IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Employee')
create table employee
(
id int identity(1,1) primary key,
name nvarchar(340),age int
)
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('AddEmployee'))
begin exec('
create proc AddEmployee(@name nvarchar(340),@age int)
as
begin
insert into employee(name,age)
values(@name,@age)
end'
)
end
go
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('EditEmployee'))
begin exec('
create proc EditEmployee(@id int,@name nvarchar(340),@age int)
as
begin
update employee set name=@name,age=@age where id=@id
end
')End
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('DeleteEmployee'))
begin exec('
create proc DeleteEmployee(@id int)
as
begin
delete from employee where id=@id
end
')end
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('GetEmployeeById'))
begin exec('
create proc GetEmployeeById(@id int)
as
begin
select * from employee where id=@id
end
')end
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('GetEmployees'))
begin exec('
create proc GetEmployees
as
begin
select * from employee
end')end
GO