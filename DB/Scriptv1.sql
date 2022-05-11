


create database testdb;

use testdb;

create table Users (
UserID int auto_increment primary key,
Username varchar(50) not null,
Password varchar(50) not null
);
insert into Users(Username,Password)
Values("Admin","Admin1"),
	  ("Admin2","Admin2"),
      ("Admin3","Admin3"),
      ("Admin4","Admin4"),
      ("Admin5","Admin5");


create table Chats(
	
    UserID1 int not null,
    UserID2 int not null,
    Primary key (UserID1, UserID2),
    foreign key (UserID1) references Users(UserID),
     foreign key (UserID2) references Users(UserID)
    
);
insert into Chats(UserID1,UserID2)
Values(1,2),
	  (1,3),
      (1,4),
      (1,5);

select * from Chats;
create table Messages(
MessageID int auto_increment primary key,
Message varchar(200),

ChatID1 int not null,
ChatID2 int not null,

foreign key (ChatID1 ,ChatID2) references Chats(UserID1,UserID2)
);

insert into Messages(Message,ChatID1,ChatID2)
Values("Hallo2",1,2),
	("Hallo3",1,3),
    ("Hallo4",1,4),
    ("Hallo5",1,5);
    
    select * from Messages;