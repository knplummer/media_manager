CREATE TABLE Permissions (
    PermissionId INTEGER PRIMARY KEY NOT NULL,
    Code VARCHAR(25) NOT NULL UNIQUE,
    Description VARCHAR(100) NOT NULL,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL,
    UpdatedBy INTEGER,
    UpdatedDate TIMESTAMP
);

ALTER TABLE Permissions
ADD CONSTRAINT Permissions_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE Permissions
ADD CONSTRAINT Permissions_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);
