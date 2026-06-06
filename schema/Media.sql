CREATE TABLE Media (
    MediaId INTEGER PRIMARY KEY NOT NULL,
    Type VARCHAR(5) NOT NULL,
    Path VARCHAR(250) NOT NULL,
    FileName VARCHAR(100) NOT NULL,
    Extension VARCHAR(5) NOT NULL,
    FileSize BIGINT NOT NULL,
    IsAlwaysAvailable BOOLEAN NOT NULL,
    IsOnMediaServer BOOLEAN NOT NULL,
    ExternalStorageKey VARCHAR(15),
    StorageGuid VARCHAR(36),
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL,
    UpdatedBy INTEGER,
    UpdatedDate TIMESTAMP
);

ALTER TABLE Media
ADD CONSTRAINT Media_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE Media
ADD CONSTRAINT Media_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);
