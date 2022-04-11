create Table Wishlist
(
	WishlistId INT NOT NULL auto_increment PRIMARY KEY,
    UserId int not null,
    bookId int not null,
	CONSTRAINT fk_UserId2 FOREIGN KEY (UserId) REFERENCES Users (UserId) on delete no action,
    CONSTRAINT fk_BookId2 FOREIGN KEY (bookId) REFERENCES Books (bookId) on delete no action
);


select * from Wishlist;

DELIMITER &&
create procedure AddInWishlist
(
	_UserId int,
	_BookId int
)
BEGIN
	If Exists (Select * from Wishlist where UserId = _UserId and bookId = _BookId)
		then 
			select 2;
	Else 
		if Exists (select * from Books where bookId = _BookId)
				then
					Insert into Wishlist(UserId, bookId)
					values (_UserId , _BookId);
			else
					select 1;
			end if;
	end if;
End &&

-- Procedure To Delete from Wishlist

DELIMITER &&
create procedure DeleteFromWishlist
(
	_WishlistId int,
	_UserId int
)
BEGIN 
	Delete from Wishlist 
	where WishlistId = _WishlistId
		  and
		  UserId = _UserId;
End &&	 

-- Procedure To Get Records from Wishlist

DELIMITER &&
create procedure GetAllRecordsFromWishlist
(
	_UserId int
)
BEGIN
	select w.WishlistId,w.UserId,w.bookId,
	b.bookName,b.authorName,b.discountPrice,b.originalPrice,
	b.bookImage
	from Wishlist w
	Inner join Books b
	on w.bookId = b.bookId
	where 
		UserId = _UserId;
END &&