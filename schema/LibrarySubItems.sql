CREATE TABLE LibrarySubItems (
    LibrarySubItemId INTEGER PRIMARY KEY NOT NULL,
    LibraryItemId INTEGER NOT NULL,
    Type VARCHAR(25) NOT NULL,
    OwnedFormat VARCHAR(15) NOT NULL,
    "Order" INTEGER NOT NULL,
    Name VARCHAR(100) NOT NULL,
    Metadata VARCHAR(750) NOT NULL,
    MediaId INTEGER,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL,
    UpdatedBy INTEGER,
    UpdatedDate TIMESTAMP
);

ALTER TABLE LibrarySubItems
ADD CONSTRAINT LibrarySubItems_LibraryItemId_LibraryItems_LibraryItemId FOREIGN KEY (LibraryItemId) REFERENCES LibraryItems(LibraryItemId);

ALTER TABLE LibrarySubItems
ADD CONSTRAINT LibrarySubItems_MediaId_Media_MediaId FOREIGN KEY (MediaId) REFERENCES Media(MediaId);

ALTER TABLE LibrarySubItems
ADD CONSTRAINT LibrarySubItems_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE LibrarySubItems
ADD CONSTRAINT LibrarySubItems_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);
