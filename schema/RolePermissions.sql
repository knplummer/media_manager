CREATE TABLE RolePermissions (
    RolePermissionId INTEGER PRIMARY KEY NOT NULL,
    RoleId INTEGER NOT NULL,
    PermissionId INTEGER NOT NULL,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL
);

ALTER TABLE RolePermissions
ADD CONSTRAINT RolePermissions_RoleId_Role_RoleId FOREIGN KEY (RoleId) REFERENCES Roles(RoleId);

ALTER TABLE RolePermissions
ADD CONSTRAINT RolePermissions_PermissionId_Permissions_PermissionId FOREIGN KEY (PermissionId) REFERENCES Permissions(PermissionId);

ALTER TABLE RolePermissions
ADD CONSTRAINT RolePermissions_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);
