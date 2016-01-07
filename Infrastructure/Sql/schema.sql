CREATE TABLE Addresses
(
	Id INT PRIMARY KEY IDENTITY(1,1),
	Street NVARCHAR(500) NOT NULL,
	CITY NVARCHAR(100),
	LandMark NVARCHAR(100),
	Country NVARCHAR(100),
	PinCode NVARCHAR(10),
	Description NVARCHAR(MAX),
	IsActive bit,
	CreatedDate DateTime Default(GetDate()),
	ModifiedDate DateTime Default(GetDate()) 
)
GO

CREATE TABLE Companies
(
	Id  INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(500) NOT NULL,
	AddressId INT NOT NULL,
	CreatedDate DateTime Default(GetDate()),
	ModifiedDate DateTime Default(GetDate())
)

GO

CREATE TABLE Employees
(
	Id  INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(500) NOT NULL,
	AddressId INT NOT NULL,
	EmpCode NVARCHAR(20) NOT NULL,
	Designation NVARCHAR(200) NOT NULL,
	DOJ DateTime NOT NULL,
	DOB DateTime Not NUll,
	CompanyId INT NOT NULL,
	CreatedDate DateTime Default(GetDate()),
	ModifiedDate DateTime Default(GetDate())
)

GO
CREATE TABLE Accounts
(
	Id  INT PRIMARY KEY IDENTITY(1,1),
	EmployeeId INT NOT NULL,
	Amount Numeric(10,0),
	CreatedDate DateTime Default(GetDate()),
	ModifiedDate DateTime Default(GetDate())
)

GO
CREATE TABLE Transactions
(
	Id  INT PRIMARY KEY IDENTITY(1,1),
	TransactionType NVARCHAR(100) NOT NULL,
	WithdrawlAmount Numeric(10,0),
	CreatedDate DateTime Default(GetDate()),
	ModifiedDate DateTime Default(GetDate())
)
