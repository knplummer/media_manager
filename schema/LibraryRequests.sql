CREATE TABLE LibraryRequests (
    LibraryRequestId INTEGER NOT NULL,
    RequestTitle VARCHAR(250) NOT NULL,
    Year INTEGER NOT NULL,
    RelevantLink VARCHAR(750),
    Note VARCHAR(1000),
    Votes INTEGER NOT NULL,
    IsAccepted BOOLEAN NOT NULL,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL,
    UpdatedBy INTEGER,
    UpdatedDate TIMESTAMP
);

ALTER TABLE LibraryRequests
ADD CONSTRAINT LibraryRequests_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE LibraryRequests
ADD CONSTRAINT LibraryRequests_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);
