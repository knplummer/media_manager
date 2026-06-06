CREATE TABLE LibraryBuckets (
    LibraryBucketId INTEGER PRIMARY KEY NOT NULL,
    GroupType VARCHAR(25) NOT NULL,
    Name VARCHAR(250) NOT NULL,
    Year INTEGER,
    Metadata VARCHAR(750),
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL,
    UpdatedBy INTEGER,
    UpdatedDate TIMESTAMP
);

ALTER TABLE LibraryBuckets
ADD CONSTRAINT LibraryBucket_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE LibraryBuckets
ADD CONSTRAINT LibraryBucket_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);
