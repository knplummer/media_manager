-- Create tables
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

CREATE TABLE MediaHistory (
    MediaHistoryId INTEGER PRIMARY KEY NOT NULL,
    MediaId INTEGER NOT NULL,
    StorageGuid VARCHAR(36),
    Action VARCHAR(10) NOT NULL,
    Description VARCHAR(200) NOT NULL,
    ActionUser INTEGER NOT NULL,
    ActionTime TIMESTAMP NOT NULL
);

CREATE TABLE Users (
    UserId INTEGER PRIMARY KEY NOT NULL,
    Username VARCHAR(25) NOT NULL UNIQUE,
    IsActive BOOLEAN NOT NULL,
    LastLogin TIMESTAMP NOT NULL
);

CREATE TABLE Roles (
    RoleId INTEGER PRIMARY KEY NOT NULL,
    Name VARCHAR(50) NOT NULL UNIQUE,
    Description VARCHAR(250) NOT NULL,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL,
    UpdatedBy INTEGER,
    UpdatedDate TIMESTAMP
);

CREATE TABLE Permissions (
    PermissionId INTEGER PRIMARY KEY NOT NULL,
    Code VARCHAR(25) NOT NULL UNIQUE,
    Description VARCHAR(100) NOT NULL,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL,
    UpdatedBy INTEGER,
    UpdatedDate TIMESTAMP
);

CREATE TABLE RolePermissions (
    RolePermissionId INTEGER PRIMARY KEY NOT NULL,
    RoleId INTEGER NOT NULL,
    PermissionId INTEGER NOT NULL,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL
);

CREATE TABLE UserPermissions (
    UserPermissionId INTEGER PRIMARY KEY NOT NULL,
    UserId INTEGER NOT NULL,
    PermissionId INTEGER NOT NULL,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL
);

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

CREATE TABLE AccessRequests (
    UserRequestId INTEGER PRIMARY KEY NOT NULL,
    Note VARCHAR(1000),
    IsDenied BOOLEAN NOT NULL,
    CreatedBy INTEGER NOT NULL,
    CreatedDate TIMESTAMP NOT NULL,
    UpdatedBy INTEGER,
    UpdatedDate TIMESTAMP
);

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

-- Add Foreign Key Constraints
ALTER TABLE Media
ADD CONSTRAINT Media_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE Media
ADD CONSTRAINT Media_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);

ALTER TABLE MediaHistory
ADD CONSTRAINT MediaHistory_MediaId_Media_MediaId FOREIGN KEY (MediaId) REFERENCES Media(MediaId);

ALTER TABLE MediaHistory
ADD CONSTRAINT MediaHistory_ActionUser_Users_UserId FOREIGN KEY (ActionUser) REFERENCES Users(UserId);

ALTER TABLE Roles
ADD CONSTRAINT Roles_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE Roles
ADD CONSTRAINT Roles_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);

ALTER TABLE Permissions
ADD CONSTRAINT Permissions_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE Permissions
ADD CONSTRAINT Permissions_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);

ALTER TABLE RolePermissions
ADD CONSTRAINT RolePermissions_RoleId_Role_RoleId FOREIGN KEY (RoleId) REFERENCES Roles(RoleId);

ALTER TABLE RolePermissions
ADD CONSTRAINT RolePermissions_PermissionId_Permissions_PermissionId FOREIGN KEY (PermissionId) REFERENCES Permissions(PermissionId);

ALTER TABLE RolePermissions
ADD CONSTRAINT RolePermissions_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE UserPermissions
ADD CONSTRAINT UserPermissions_UserId_Users_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId);

ALTER TABLE UserPermissions
ADD CONSTRAINT UserPermissions_PermissionId_Permissions_PermissionId FOREIGN KEY (PermissionId) REFERENCES Permissions(PermissionId);

ALTER TABLE UserPermissions
ADD CONSTRAINT UserPermissions_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE LibraryBuckets
ADD CONSTRAINT LibraryBucket_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE LibraryBuckets
ADD CONSTRAINT LibraryBucket_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);

ALTER TABLE LibraryItems
ADD CONSTRAINT LibraryItems_LibraryBucketId_LibraryBuckets_LibraryBucketId FOREIGN KEY (LibraryBucketId) REFERENCES LibraryBuckets(LibraryBucketId);

ALTER TABLE LibraryItems
ADD CONSTRAINT LibraryItems_MediaId_Media_MediaId FOREIGN KEY (MediaId) REFERENCES Media(MediaId);

ALTER TABLE LibraryItems
ADD CONSTRAINT LibraryItems_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE LibraryItems
ADD CONSTRAINT LibraryItems_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);

ALTER TABLE LibrarySubItems
ADD CONSTRAINT LibrarySubItems_LibraryItemId_LibraryItems_LibraryItemId FOREIGN KEY (LibraryItemId) REFERENCES LibraryItems(LibraryItemId);

ALTER TABLE LibrarySubItems
ADD CONSTRAINT LibrarySubItems_MediaId_Media_MediaId FOREIGN KEY (MediaId) REFERENCES Media(MediaId);

ALTER TABLE LibrarySubItems
ADD CONSTRAINT LibrarySubItems_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE LibrarySubItems
ADD CONSTRAINT LibrarySubItems_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);

ALTER TABLE AccessRequests
ADD CONSTRAINT AccessRequests_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE AccessRequests
ADD CONSTRAINT AccessRequests_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);

ALTER TABLE LibraryRequests
ADD CONSTRAINT LibraryRequests_CreatedBy_Users_UserId FOREIGN KEY (CreatedBy) REFERENCES Users(UserId);

ALTER TABLE LibraryRequests
ADD CONSTRAINT LibraryRequests_UpdatedBy_Users_UserId FOREIGN KEY (UpdatedBy) REFERENCES Users(UserId);

