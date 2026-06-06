CREATE TABLE MediaHistory (
    MediaHistoryId INTEGER PRIMARY KEY NOT NULL,
    MediaId INTEGER NOT NULL,
    StorageGuid VARCHAR(36),
    Action VARCHAR(10) NOT NULL,
    Description VARCHAR(200) NOT NULL,
    ActionUser INTEGER NOT NULL,
    ActionTime TIMESTAMP NOT NULL
);

ALTER TABLE MediaHistory
ADD CONSTRAINT MediaHistory_MediaId_Media_MediaId FOREIGN KEY (MediaId) REFERENCES Media(MediaId);

ALTER TABLE MediaHistory
ADD CONSTRAINT MediaHistory_ActionUser_Users_UserId FOREIGN KEY (ActionUser) REFERENCES Users(UserId);
