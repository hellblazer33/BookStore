create Table FeedbackTab
(
	FeedbackId INT NOT NULL auto_increment PRIMARY KEY,
	Comment varchar(255) not null,
	Rating int not null,
	bookId int not null, 
	CONSTRAINT FOREIGN KEY (bookId) REFERENCES Books(bookId),
	UserId INT NOT NULL,
	CONSTRAINT FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

select * from FeedbackTab;
drop procedure AddFeedback;


DELIMITER &&
create Procedure AddFeedback
(
	_Comment varchar(255),
	_Rating int,
	_BookId int,
	_UserId int
)
BEGIN
Declare _AverageRating int;
	IF (EXISTS(SELECT * FROM FeedbackTab WHERE bookId = _BookId and UserId=_UserId))
		then select 1;
	Else
		IF (EXISTS(SELECT * FROM Books WHERE bookId = _BookId))
		then 
			select * from FeedbackTab;
					start transaction;
					Insert into FeedbackTab(Comment, Rating, bookId, UserId) values(_Comment, _Rating, _BookId, _UserId);		
					set _AverageRating = (Select AVG(Rating) from FeedbackTab where bookId = _BookId);
					Update Books set rating = _AverageRating,  totalRating = totalRating + 1 
								 where  bookId = _BookId;
				Commit;

			Else
				Select 2; 
		End if;
	end if;
End &&


DELIMITER &&
create Procedure DeleteFeedback
(
	_FeedbackId int,
	_UserId int
)
BEGIN
	Delete from FeedbackTab
		where
			FeedbackId = _FeedbackId
			and
			UserId = _UserId;
END &&

DELIMITER &&
create Procedure GetAllFeedback
(
	_BookId int
)
BEGIN
	Select FeedbackId, Comment, Rating, bookId, u.FullName
	From Users u
	Inner Join FeedbackTab f
	on f.UserId = u.UserId
	where
	 BookId = _BookId;
END &&

drop procedure UpdateFeedback

DELIMITER &&
create Procedure UpdateFeedback
(
	_Comment varchar(255),
	_Rating int,
	_BookId int,
	_FeedbackId int,
	_UserId int
)
BEGIN
Declare _AverageRating int;
	IF (EXISTS(SELECT * FROM FeedbackTab WHERE FeedbackId = _FeedbackId))
    then
					Update FeedbackTab set Comment = _Comment, Rating = _Rating, UserId = _UserId, bookId = _BookId
									where FeedbackId = _FeedbackId;			
					set _AverageRating = (select AVG(Rating) from FeedbackTab 
									where bookId = _BookId);
					Update Books set rating = _AverageRating,  totalRating = totalRating+1 
								    where bookId = _BookId;
				Commit;

			Else
				Select 2; 
		End if;
	
End &&






