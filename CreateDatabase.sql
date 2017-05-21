use master;
--create database talent_manager
go

use talent_manager
go


alter table dbo.employee drop FK_Employee_Department 
go

if object_id('dbo.department') is not null
	drop table dbo.department;
go

create table dbo.department (
	department_id int identity
, name varchar(20) not null
, row_version timestamp not null

, constraint PK_Department primary key (department_id)
)


if object_id('dbo.employee') is not null
	drop table dbo.employee;
go

create table dbo.employee (
	employee_id int identity
, first_name varchar(20) not null
, last_name varchar(20) not null
, department_id int not null
, row_version timestamp not null

constraint PK_Employee primary key (employee_id)
constraint FK_Employee_Department foreign key (department_id) references dbo.department (department_id)
)
go

insert dbo.department (name) values ('HR'), ('Finance'), ('Marketing');
insert dbo.employee (first_name, last_name, department_id)
values ('John', 'Human', 1)
, ('Joe', 'Border', 1)
, ('Pete', 'Callaghan', 1)
, ('Alan', 'Dime', 2)
, ('Rich', 'Nickel', 2)
, ('Nick', 'Greenback', 2);
