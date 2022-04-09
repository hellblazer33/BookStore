create table Books
(
	bookId int AUTO_INCREMENT PRIMARY KEY,
	bookName varchar(255) not null,
	authorName varchar(255) not null,
    rating int,   
	totalRating int,
	discountPrice Decimal,
	originalPrice Decimal,
	description varchar(255) not null,
	bookImage varchar(255),
	BookCount int not null
);
drop table Books;

DELIMITER &&  
CREATE PROCEDURE AddBook(
	_bookName varchar(255),
	_authorName varchar(255),
    _rating int,   
	_totalRating int,
	_discountPrice Decimal,
	_originalPrice Decimal,
	_description varchar(255),
	_bookImage varchar(255),
	_BookCount int 
)
BEGIN  
    INSERT INTO Books(bookName,authorName,rating,totalRating,discountPrice,originalPrice,description,bookImage,BookCount)
VALUES (_bookName,_authorName,_rating,_totalRating,_discountPrice,_originalPrice,_description,_bookImage,_BookCount);   
END &&  
DELIMITER ; 





DELIMITER &&  
CREATE PROCEDURE UpdateBook(
	_bookId int,
    _bookName varchar(255),
	_authorName varchar(255),
    _rating int,   
	_totalRating int,
	_discountPrice Decimal,
	_originalPrice Decimal,
	_description varchar(255),
	_bookImage varchar(255),
	_BookCount int 
)
BEGIN
   Update Books set bookName = _bookName, 
					authorName = _authorName,
					rating = _rating, 
					totalRating = _totalRating, 
					discountPrice= _discountPrice,
					originalPrice = _originalPrice,
					description = _description,
					bookImage =_bookImage,
					BookCount = _bookCount
				where 
					bookId = _bookId;
END &&  
DELIMITER ;




DELIMITER &&  
CREATE PROCEDURE DeleteBook(
   _bookId int
)
BEGIN
	DELETE FROM Books WHERE bookId = _bookId;
END &&
DELIMITER ;

DROP PROCEDURE DeleteBook


DELIMITER &&  
CREATE PROCEDURE GetBookByBookId(
   _bookId int
)
BEGIN
	SELECT * FROM Books WHERE bookId = _bookId;
END &&
DELIMITER ;

DROP PROCEDURE GetAllBook

DELIMITER &&  
CREATE PROCEDURE GetAllBook()
BEGIN
	SELECT * FROM Books;
END &&
DELIMITER ;

SELECT * FROM Books;
