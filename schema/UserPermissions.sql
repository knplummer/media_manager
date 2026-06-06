CREATE TABLE UserPermissions (
    UserPermissionId INTEGER PRIMARY KEY NOT NULL,
    UserId INTEGER NOT NULL,
    PermissionId INTEGER NOT NULL,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL
);

ALTER TABLE UserPermissions
ADD CONSTRAINT UserPermissions_UserId_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId);

ALTER TABLE UserPermissions
ADD CONSTRAINT UserPermissions_PermissionId_Permissions_PermissionId FOREIGN KEY (PermissionId) REFERENCES Permissions(PermissionId);

ALTER TABLE UserPermissions
ADD CONSTRAINT UserPermissions_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);
