create table OrdersTable
(
	OrdersId int auto_increment not null primary key,
	TotalPrice int not null,
	BookQuantity int not null,
	OrderDate Date not null,
	UserId int not null, 
    CONSTRAINT fk_UserId3 FOREIGN KEY (UserId) REFERENCES Users(UserId),
	bookId int not null, 
    CONSTRAINT fk_BookId3 FOREIGN KEY (bookId) REFERENCES Books(bookId),
	AddressId int not null,
    CONSTRAINT fk_AddressId FOREIGN KEY (AddressId) REFERENCES AddressTab(AddressId)
);
drop table orderstable
drop procedure addorders
select * from orderstable
DELIMITER &&
Create Procedure AddOrders
(
	_BookQuantity int,
	_UserId int,
	_BookId int,
	_AddressId int
)

BEGIN

declare _TotalPrice int;
SET GLOBAL FOREIGN_KEY_CHECKS=0;
set _TotalPrice = (select discountPrice from Books where bookId = _BookId);
	If(Exists(Select * from Books where bookId = _BookId))
		then
			If(Exists (Select * from Users where UserId = _UserId))
            then
				start Transaction;
						Insert Into OrdersTable (TotalPrice, BookQuantity, OrderDate, UserId, bookId, AddressId)
						Values(_TotalPrice*_BookQuantity, _BookQuantity, CURDATE(), _UserId, _BookId, _AddressId);
						Update Books set BookCount=BookCount-_BookQuantity where bookId = _BookId;
						Delete from Carts where BookId = _BookId and UserId = _UserId;
						select * from OrdersTable;
                        COMMIT;

		else
			Select 3;
				End if;
		
	Else
			Select 2;
		End if;
END &&

DELIMITER &&
Create Procedure GetOrders
(
	_UserId int
)
begin
		Select 
		O.OrdersId, O.UserId, O.AddressId, b.bookId,
		O.TotalPrice, O.BookQuantity, O.OrderDate,
		b.bookName, b.AuthorName, b.bookImage
		FROM Books b
		inner join OrdersTable O on O.bookId = b.bookId 
		where 
			O.UserId = _UserId;
END &&

