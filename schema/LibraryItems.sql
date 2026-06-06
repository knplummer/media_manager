CREATE TABLE LibraryItems (
    LibraryItemId INTEGER PRIMARY KEY NOT NULL,
    LibraryBucketId INTEGER,
    ItemType VARCHAR(25) NOT NULL,
    OwnedFormat VARCHAR(25),
    "Order" INTEGER,
    Name VARCHAR(25),
    Year INTEGER,
    Metadata VARCHAR(750),
    MediaId INTEGER,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL,
    UpdatedBy INTEGER,
    UpdatedDate TIMESTAMP
);

ALTER TABLE LibraryItems
ADD CONSTRAINT LibraryItems_LibraryBucketId_LibraryBuckets_LibraryBucketId FOREIGN KEY (LibraryBucketId) REFERENCES LibraryBuckets(LibraryBucketId);

ALTER TABLE LibraryItems
ADD CONSTRAINT LibraryItems_MediaId_Media_MediaId FOREIGN KEY (MediaId) REFERENCES Media(MediaId);

ALTER TABLE LibraryItems
ADD CONSTRAINT LibraryItems_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE LibraryItems
ADD CONSTRAINT LibraryItems_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);
