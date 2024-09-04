UPDATE UserProfile
SET IsActive = 1
WHERE Id = 1

ALTER TABLE UserProfile
ADD IsActive BIT

SELECT * FROM UserProfile

SELECT u.id, u.FirstName, u.LastName, u.DisplayName, u.Email,
                       u.CreateDateTime, u.ImageLocation, u.UserTypeId, u.IsActive,
                       ut.[Name] AS UserTypeName
                  FROM UserProfile u
                       LEFT JOIN UserType ut ON u.UserTypeId = ut.id
                 ORDER BY u.DisplayName ASC