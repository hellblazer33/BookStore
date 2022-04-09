create Table Admins
(
	AdminId int auto_increment primary key not null,
	FullName varchar(255) not null,
	Email varchar(255) not null,
	Password varchar(255) not null,
	MobileNumber varchar(255) not null
);



Insert into Admins(FullName,Email,Password,MobileNumber) 
values('Admin','pk110495@gmail.com', 'Pankaj@123', '9747388481');
select * from Admins;

ALTER table Books 
Add AdminId int not null DEFAULT 1, 
ADD FOREIGN KEY (AdminId) REFERENCES Admins(AdminId);


SELECT * FROM books;
SET foreign_key_checks = 0;

DELIMITER &&  
create Procedure LoginAdmin
(
	_Email varchar(255),
	_Password varchar(255)
)
BEGIN
	If(Exists(select * from Admins where Email = _Email and Password = _Password)) then
			select AdminId, FullName, Email, MobileNumber from Admins;
	else select 2;
	End if;
END&&


call LoginAdmin(@Email = 'pk110495@gmail.com',
@Password = 'Pankaj@123');


SELECT * FROM Admins;