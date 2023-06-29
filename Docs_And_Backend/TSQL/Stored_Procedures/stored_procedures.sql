USE [ffxi_equipment_db]
GO

/* Log-In Related Stored Procedures */
print '' print '*** Creating sp_authenticate_user'
GO
CREATE PROCEDURE	[dbo].[sp_authenticate_user]
(
	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
)
AS
	BEGIN
		SELECT	COUNT([UserID]) AS 'Authenticated'
		FROM		[User]
		WHERE		@Email = [Email]
			AND		@PasswordHash = [PasswordHash]
	END
GO

print '' print '*** Creating sp_select_user_by_email'
GO
CREATE PROCEDURE	[dbo].[sp_select_user_by_email]
(
	@Email			[nvarchar](100)
)
AS
	BEGIN
		SELECT	[UserID], [User_Name], [Email]
		FROM	[User]
		WHERE	@Email = [Email]
	END
GO
/* Added April 6, 2023 */
print '' print '*** Creating sp_insert_new_user'
GO
CREATE PROCEDURE	[dbo].[sp_insert_new_user]
(
	@User_Name		[nvarchar](50),
	@Email 			[nvarchar](100)
)
AS
	BEGIN
		INSERT INTO [dbo].[User]
			([User_Name], [Email])
		VALUES
			(@User_Name, @Email)
		SELECT SCOPE_IDENTITY()
	END
GO

print '' print '*** Creating sp_select_roles_by_userID'
GO
CREATE PROCEDURE	[dbo].[sp_select_roles_by_userID]
(
	@UserID		[int]
)
AS
	BEGIN
		SELECT	[RoleID]
		FROM	[User_Role]
		WHERE	@UserID = [UserID]
	END
GO

/* Added April 6, 2023 */
print '' print '*** Creating sp_add_role_to_user'
GO
CREATE PROCEDURE	[dbo].[sp_add_role_to_user]
(
	@UserID			[int],
	@RoleID			[nvarchar](50)
)
AS
	BEGIN
		INSERT INTO	[dbo].[User_Role]
			([UserID], [RoleID])
		VALUES
			(@UserID, @RoleID)
	END
GO
/* Added April 6, 2023 */
print '' print '*** Creating sp_remove_role_from_user'
GO
CREATE PROCEDURE	[dbo].[sp_remove_role_from_user]
(
	@UserID			[int],
	@RoleID			[nvarchar](50)
)
AS
	BEGIN
		DELETE FROM	[dbo].[User_Role]
			WHERE	[UserID]	=	@UserID
		AND			[RoleID]	=	@RoleID
	END
GO

print '' print '*** Creating sp_update_passwordHash'
GO
CREATE PROCEDURE	[dbo].[sp_update_passwordHash]
(
	@NewPasswordHash	[nvarchar](100),
	@OldPasswordHash	[nvarchar](100),
	@UserID				[int]
)
AS
	BEGIN
		UPDATE	[User]
			SET [PasswordHash]		=	@NewPasswordHash
		WHERE	@UserID				=	[UserID]
			AND	@OldPasswordHash	=	[PasswordHash]
			
		RETURN	@@ROWCOUNT
	END
GO
/* End Log-In Related Stored Procedures */

/*
	Select Stat
	Select all Stats in Stat to populate menus and validatation checks
*/
print '' print '*** Creating sp_select_stat'
GO
CREATE PROCEDURE [dbo].[sp_select_stat]
/* No parameters*/
AS
	BEGIN
		SELECT	[Stat].[StatID]
		FROM	[Stat]
	END
GO
/*
	Select Item
	Return Master Equipment List in basic format
*/
print '' print '*** Creating sp_select_item_simple'
GO
CREATE PROCEDURE [dbo].[sp_select_item_simple]
/* No parameters*/
AS
	BEGIN
		SELECT	[Item].[ItemID], [Item].[Item_Name]
		FROM	[Item]
	END
GO

/* Item CRUD functions */

/*	Create Item
	This creates an item instance on the Master Equipment. Apply item stats separately. */
print '' print '*** Creating sp_create_new_item'
GO
CREATE PROCEDURE [dbo].[sp_create_new_item]
(
	@ItemID			[int],
	@Item_Name		[nvarchar](50)	
)
AS
	BEGIN
		INSERT INTO [dbo].[Item]
			([ItemID], [Item_Name])
		VALUES
			(@ItemID, @Item_Name)
		RETURN	@@ROWCOUNT
	END
GO

/* Read Item
	Simple item retrieval (No latents or augments) 
*/
print '' print '*** Creating sp_select_item_from_item_by_itemID'
GO
CREATE PROCEDURE	[dbo].[sp_select_item_from_item_by_itemID]
(
	@ItemID			[int]
)
AS
	BEGIN
		SELECT	[Item].[ItemID],
				[Item].[Item_Name],
				[Stat].[StatID],
				[Equip_Stats].[Stat_Value]
		FROM		[Item]
			JOIN [Equip_Stats] ON [Item].[ItemID] = [Equip_Stats].[ItemID]
			/* Augment_Stats join will go here */
			JOIN [Stat] ON [Stat].[StatID] = [Equip_Stats].[StatID]
		WHERE		@ItemID = [Item].[ItemID]
	END
GO

/* Update Item */
/* Update the Item ID and Item Name on an item. Update stats separately */
print '' print '*** Creating sp_update_item'
GO
CREATE PROCEDURE [dbo].[sp_update_item]
(
	@OldItemID		[int],
	@NewItemID		[int],	
	@Item_Name		[nvarchar](50)
)
AS
	BEGIN
		/* First, update dependent tables */
		/* Equip Stats is now set to UPDATE CASCADE */
		UPDATE	[Item]
		SET		[ItemID]	= @NewItemID,
				[Item_Name]	= @Item_Name
		WHERE	[ItemID]	= @OldItemID
		RETURN	@@ROWCOUNT
	END
GO


/* Delete Item */

print '' print '*** Creating sp_delete_item_by_itemID'
GO
CREATE PROCEDURE	[dbo].[sp_delete_item_by_itemID]
(
	@ItemID			[int]
)
AS
	BEGIN
		/* Rename selected Item to Deleted Item (DO NOT DELETE MASTER EQUIPMENT LIST ENTRY */
		UPDATE	[Item]
		SET		[Item].[Item_Name] = 'Deleted Item'
		WHERE	[Item].[ItemID] = @ItemID
		
		/* Delete stats from item */
		/* Augment_Stats uses Inventory ID, not ItemID so don't touch it */
		DELETE FROM	[Equip_Stats]
		WHERE		[Equip_Stats].[ItemID] = @ItemID
		/* Insert a placeholder stat */
		INSERT INTO [dbo].[Equip_Stats]
			([ItemID], [StatID], [Stat_Value])
			VALUES
			(@ItemID, 'Placeholder_Stat', 0)

	END
GO

/* Stat CRUD Functions */

/* Create (Add a stat to an item) */
print '' print '*** Creating sp_add_stat_to_item_by_item_id'
GO
CREATE PROCEDURE [dbo].[sp_add_stat_to_item_by_item_id]
(
	@ItemID			[int],
	@StatID		[nvarchar](50),
	@Stat_Value		[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[Equip_Stats]
			([ItemID], [StatID], [Stat_Value])
		VALUES
			(@ItemID, @StatID, @Stat_Value)
		RETURN @@ROWCOUNT
	END
GO
/* Read single stat on item */

/* Update stat on item */

/* Delete single stat on item */
print '' print '*** Creating sp_delete_stat_from_item_by_item_id'
GO
CREATE PROCEDURE [dbo].[sp_delete_stat_from_item_by_item_id]
(
	@ItemID			[int],
	@StatID		[nvarchar](50)
)
AS
	BEGIN
		DELETE FROM [dbo].[Equip_Stats]
		WHERE		[ItemID] = @ItemID
		AND			[StatID] = @StatID
		RETURN @@ROWCOUNT
	END
GO
/*	Delete all stats on item
	This is for clearing all stats prior to adding a new set */
print '' print '*** Creating sp_delete_all_stats_on_item_by_itemID'
GO
CREATE PROCEDURE	[dbo].[sp_delete_all_stats_on_item_by_itemID]
(
	@ItemID			[int]
)
AS
	BEGIN
		DELETE FROM	[Equip_Stats]
		WHERE		[Equip_Stats].[ItemID] = @ItemID
		RETURN @@ROWCOUNT
	END
GO
/*	Retrieve list of User Roles
	This is for retrieving a list of roles that users can have */
print '' print '*** Creating sp_select_all_user_roles'
GO
CREATE PROCEDURE	[dbo].[sp_select_all_user_roles]
AS
	BEGIN
		SELECT		[RoleID]
		FROM		[dbo].[Role]
		ORDER BY	[RoleID]
	END
GO


/* New Stat-related stored procedures*/

/* Add a stat */
print '' print '*** Creating sp_add_stat'
GO
CREATE PROCEDURE	[dbo].[sp_add_stat]
(
	@StatID			[nvarchar](50)
)
AS
	BEGIN
		INSERT INTO [dbo].[Stat]([StatID])
		VALUES	(@StatID)
		RETURN @@ROWCOUNT
	END
GO
/* Update a stat */
print '' print '*** Creating sp_update_stat'
GO
CREATE PROCEDURE	[dbo].[sp_update_stat]
(
	@NewStatID	[nvarchar](50),
	@OldStatID	[nvarchar](50)
)
AS
	BEGIN
		UPDATE	[dbo].[Stat]
			SET [StatID]	=	@NewStatID
		WHERE	[StatID]	=	@OldStatID
		RETURN	@@ROWCOUNT
	END
GO
/* Add a stat alias */
print '' print '*** Creating sp_add_stat_alias'
GO
CREATE PROCEDURE	[dbo].[sp_add_stat_alias]
(
	@StatID			[nvarchar](50),
	@StatAlias		[nvarchar](100)
)
AS
	BEGIN
		INSERT INTO [dbo].[StatAlias]
			([StatID], [StatAlias])
		VALUES
			(@StatID, @StatAlias)
		RETURN @@ROWCOUNT
	END
GO
/* Update a stat alias */
print '' print '*** Creating sp_update_stat_alias'
GO
CREATE PROCEDURE	[dbo].[sp_update_stat_alias]
(
	@StatID			[nvarchar](50),
	@NewStatAlias	[nvarchar](100),
	@OldStatAlias	[nvarchar](100)
)
AS
	BEGIN
		UPDATE	[dbo].[StatAlias]
			SET [StatAlias]	=	@NewStatAlias
		WHERE	[StatID]	=	@StatID
			AND	[StatAlias]	=	@OldStatAlias
		RETURN	@@ROWCOUNT
	END
GO
/* Delete a stat alias */
print '' print '*** Creating sp_delete_stat_alias'
GO
CREATE PROCEDURE	[dbo].[sp_delete_stat_alias]
(
	@StatID			[nvarchar](50),
	@StatAlias		[nvarchar](50)
)
AS
	BEGIN
		DELETE FROM	[dbo].[StatAlias]
			WHERE	[StatID]	=	@StatID
		AND			[StatAlias]	=	@StatAlias
		RETURN	@@ROWCOUNT
	END
GO

/*
Select all Stats, along with their aliases
This is the same technique I used in sp_select_all_library_items from the Capstone project
*/
print '' print '*** Creating sp_select_all_stats_aliases'
GO
CREATE PROCEDURE	[dbo].[sp_select_all_stats_aliases]
AS
	BEGIN
		SELECT	[Stat].[StatID], STRING_AGG([StatAlias], '|') WITHIN GROUP(ORDER BY [StatAlias] ASC) AS 'Alias'
		FROM	[dbo].[Stat] LEFT JOIN [dbo].[StatAlias]
			ON	[Stat].[StatID] = [StatAlias].[StatID]
		GROUP BY [Stat].[StatID];
	END
GO
print '' print '*** Creating sp_select_stat_by_stat_alias'
GO
CREATE PROCEDURE	[dbo].[sp_select_stat_by_stat_alias]
(
	@StatAlias			[nvarchar](100)
)
/*
AS
	BEGIN
		SELECT	[Stat].[StatID], STRING_AGG([StatAlias], '|') WITHIN GROUP(ORDER BY [StatAlias] ASC) AS 'Alias'
		FROM	[dbo].[Stat] LEFT JOIN [dbo].[StatAlias]
			ON	[Stat].[StatID] = [StatAlias].[StatID]
		WHERE	[StatAlias]	=	@StatAlias
		GROUP BY [Stat].[StatID];
	END
GO
*/
AS
	BEGIN
		SELECT s.StatID, sa.StatAlias
		FROM [dbo].[Stat] s
		JOIN [dbo].[StatAlias] sa ON s.StatID = sa.StatID
		WHERE s.StatID IN (
			SELECT sa2.StatID
			FROM [dbo].[StatAlias] sa2
			WHERE sa2.StatAlias LIKE @StatAlias
			)
	ORDER BY s.StatID, sa.StatAlias
	END
GO
