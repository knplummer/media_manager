CREATE TABLE AccessRequests (
    UserRequestId INTEGER PRIMARY KEY NOT NULL,
    Note VARCHAR(1000),
    IsDenied BOOLEAN NOT NULL,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL,
    UpdatedBy INTEGER,
    UpdatedDate TIMESTAMP
);

ALTER TABLE AccessRequests
ADD CONSTRAINT AccessRequests_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE AccessRequests
ADD CONSTRAINT AccessRequests_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);
