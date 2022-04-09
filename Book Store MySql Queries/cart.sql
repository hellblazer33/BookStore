CREATE TABLE Carts (
  _CartId int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  _Quantity int DEFAULT '1',
  _UserId int DEFAULT NULL,
  _BookId int DEFAULT NULL,
  CONSTRAINT fk_BookId FOREIGN KEY (BookId) REFERENCES Books (bookId),
  CONSTRAINT fk_UserId FOREIGN KEY (UserId) REFERENCES Users (UserId)
) 


DELIMITER $$
CREATE PROCEDURE AddCart(
	_Quantity int,
	_UserId int,
	_BookId int
)
BEGIN
	if(Exists (select * from Books where bookId = _BookId))
	then
		Insert Into Carts (Quantity, UserId, BookId)
		Values (_Quantity, _UserId, _BookId);
	else
		select 1;
	end	if;
END$$
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE UpdateCart(
	_Quantity int,
	_BookId int,
	_UserId int,
	_CartId int
)
BEGIN
	update Carts 
	set BookId = _BookId,
	Userid = _UserId,
	Quantity = _Quantity 
	where CartId = _CartId;
End$$
DELIMITER ;
DELIMITER $$
CREATE PROCEDURE DeleteCart(
	_CartId int,
	_UserId int
)
BEGIN
	Delete FROM Carts WHERE CartId=_CartId and UserId=_UserId;
END$$
DELIMITER ;
DELIMITER $$
CREATE PROCEDURE GetCartbyUser(
	_UserId int
)
BEGIN
	Select CartId, Quantity, UserId,c.BookId,
	bookName, authorName, discountPrice, originalPrice, bookImage
	from Carts c
	join Books b on
	c.BookId = b.bookId
	where
	UserId = _UserId;
End$$
DELIMITER ;
