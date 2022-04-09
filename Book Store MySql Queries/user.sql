CREATE DATABASE BookStore;

CREATE TABLE Users (
    UserId int AUTO_INCREMENT PRIMARY KEY,
    Fullname varchar(255),
    Email varchar(255),
    Password varchar(255),
    MobileNumber varchar(255)
);
drop table Users;
drop procedure userregister;

DELIMITER &&  
CREATE PROCEDURE UserRegister(
_Fullname varchar(255),
_Email varchar(255),
_Password varchar(255),
_MobileNumber varchar(255)
)
BEGIN  
    INSERT INTO Users(Fullname,Email,Password,MobileNumber)
VALUES (_Fullname,_Email,_Password,_MobileNumber);   
END &&  
DELIMITER ; 

drop procedure UserLogin;


DELIMITER &&  
CREATE PROCEDURE UserLogin(
_Email varchar(255),
_Password varchar(255)
)
BEGIN  
    SELECT * FROM Users WHERE Email=_Email AND Password=_Password;    
END &&  
DELIMITER ;


DELIMITER &&  
CREATE PROCEDURE UserForgotPassword(
_Email varchar(255)
)
BEGIN  
	UPDATE Users 
	SET 
		Password ='Null'
	WHERE 
		Email = _Email;
    SELECT * FROM Users WHERE Email=_Email;    
END &&  
DELIMITER ;


DELIMITER &&  
CREATE PROCEDURE UserResetPassword(
_Email varchar(255),
_Password varchar(255)
)
BEGIN  
	UPDATE Users 
	SET 
		Password = _Password
	WHERE 
		Email = _Email;
END &&  
DELIMITER ;

select * from users;