SET IDENTITY_INSERT [dbo].[Users] ON;

INSERT INTO [dbo].[Users] ([Id], [Name], [Email], [Password], [RoleId]) VALUES (1, 'Portal Admin', 'portaladmin@gmail.com', '12345678', 1);

SET IDENTITY_INSERT [dbo].[Users]OFF;