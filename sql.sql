create table Users
(UsersID int primary key identity(1000,1),
UserName nvarchar(50),
Password nvarchar(50),
UserRole varchar(50)
)
insert into Users values ('admin','admin','Administrator')
insert into Users values ('Revati','revati123','Faculty')

select * from Users
drop table Users



create table Faculty
(
FacultyID int primary key foreign key references Users(UsersID),
FirstName varchar(50), 
LastName varchar(50),
Address varchar(50), 
City varchar(50),
State varchar(50), 
Pincode int,
MobileNo varchar(50),
HireDate datetime, 
EmailAddress nvarchar(50),
DateofBirth datetime, 
DeptID int foreign key references Department(DeptID),
DesignationID int foreign key references Designation(DesignationID)
)
drop table Faculty
select * from Faculty

create table Publications 
(
PublicationID int primary key identity, 
FacultyID int foreign key references Faculty(FacultyID),
PublicationTitle varchar(50), 
ArticleName nvarchar(50), 
PublisherName varchar(20), 
PublicationLocation varchar(50), 
CitationDate datetime
)
insert into Publications values (1,1000,' abc','xyz','qwe','asd','2000/12/10')
insert into Publications values (4,1010,' abcd','xyz','qwe','asd','2000/12/10')
drop table Publications
select * from Publications


create table WorkHistory
(
WorkHistoryID int primary key identity, 
FacultyID int foreign key references Faculty(FacultyID),
Organization varchar(30), 
JobTitle varchar(30), 
JobBeginDate datetime, 
JobEndDate datetime, 
JobResponsibilities nvarchar(50), 
JobType VARCHAR(20) 
)

drop table WorkHistory
select * from WorkHistory

create table Degrees
(
DegreeID int primary key identity, 
FacultyID int foreign key references Faculty(FacultyID), 
Degree varchar(30), 
Specialization varchar(50), 
DegreeYear datetime, 
Grade nvarchar(20)
)
drop table Degrees


create table Grants
(
GrantID int primary key identity, 
FacultyID int foreign key references Faculty(FacultyID), 
GrantTitle varchar(50), 
GrantDescription varchar(50)
)

drop table Grants
select * from Grants

create table Courses
(
CourseID int primary key identity, 
CourseName varchar(30), 
CourseCredits nvarchar(20), 
DeptID int foreign key references Department(DeptID)
)

drop table Courses

select * from Courses

create table Department
(
DeptID int primary key identity, 
DeptName varchar(30)
)
drop table Department
insert into Department values (1,'IT')

select * from Department




create table Designation
(
DesignationID int primary key identity, 
DesignationName varchar(30)
)
select * from Designation
insert into Designation values (1,'qwery')
drop table Designation



create table Subjects
(
SubjectID int primary key identity, 
SubjectName varchar(30)
)

drop table Subjects
select * from Subjects

create table CourseSubject
(
CourseID int primary key foreign key references Courses(CourseID), 
SubjectID int foreign key references Subjects(SubjectID)
)


drop table CourseSubject


create table CoursesTaught 
(
CourseID int primary key foreign key references Courses(CourseID), 
FacultyID int foreign key references Faculty(FacultyID), 
SubjectID int foreign key references Subjects(SubjectID), 
FirstDateTaught datetime
)
insert into CoursesTaught values (2,1010,1,'2012/02/02')
select * from CoursesTaught
drop table CoursesTaught







