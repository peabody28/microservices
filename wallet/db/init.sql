CREATE TABLE IF NOT EXISTS user (
	Id uuid PRIMARY KEY,
   	Name string NOT NULL
);

CREATE TABLE IF NOT EXISTS wallet (
	Id uuid NOT NULL PRIMARY KEY,
	UserFk uuid NOT NULL,
   	Number string NOT NULL, 
	FOREIGN KEY (UserFk) 
      REFERENCES user (Id) 
         ON DELETE CASCADE 
         ON UPDATE NO ACTION
);