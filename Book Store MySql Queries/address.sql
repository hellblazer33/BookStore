create Table AddressType
(
	TypeId INT NOT NULL auto_increment PRIMARY KEY,
	TypeName varchar(255) not null
);
select * from addresstab;

Insert into AddressType(TypeName)
values('Home');
Insert into AddressType(TypeName)
values('Office');
Insert into AddressType(TypeName)
values('Other');
select * from addresstype;

drop table addresstab;

create Table AddressTab
(
	AddressId INT NOT NULL auto_increment PRIMARY KEY,
	Address varchar(255) not null,
	City varchar(255) not null,
	State varchar(255) not null,
	TypeId int not NULL, 
	CONSTRAINT fk_TypeId FOREIGN KEY (TypeId) REFERENCES AddressType(TypeId),
	UserId INT not NULL,
	CONSTRAINT fk_UserId1 FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
drop procedure AddAddress

DELIMITER &&  
create procedure AddAddress
(
	_Address varchar(255),
	_City varchar(255),
	_State varchar(255),
	_TypeId int,
	_UserId int
)
BEGIN
	If Exists (select * from AddressType where TypeId = _TypeId)
		then
			Insert into AddressTab(Address,City,State,TypeId,UserId) 
			values(_Address, _City, _State, _TypeId, _UserId);
	else
			select 2;
	end if;
End &&

select * from addresstab
DELIMITER &&  
create procedure UpdateAddress
(
	_AddressId int,
	_Address varchar(255),
	_City varchar(255),
	_State varchar(255),
	_TypeId int,
	_UserId int
)
BEGIN
	If Exists (select * from AddressType where TypeId = _TypeId)
		then
			Update AddressTab set
			Address = _Address, City = _City,
			State = _State , TypeId = _TypeId,
			UserId = _UserId
			where
				AddressId = _AddressId;
	Else
			select 2;
		end if;
End &&



DELIMITER &&
create procedure DeleteAddress
(
	_AddressId int,
	_UserId int
)
BEGIN
	Delete FROM AddressTab
	where 
		AddressId = _AddressId 
	and
		UserId = _UserId;
End &&

drop procedure GetAllAddress



DELIMITER &&
create procedure GetAllAddress
(
	_UserId int
)
BEGIN
	Select Address, City, State,UserId,AddressType.TypeId,AddressId
	from AddressTab Inner join AddressType on AddressType.TypeId = AddressTab.TypeId 
	where UserId = _UserId;
END &&
DELIMITER ;

select * from AddressTab;

