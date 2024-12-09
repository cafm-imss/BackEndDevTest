-- Create error logging table
CREATE TABLE [dbo].[ErrorsLog](
    [ID] [int] IDENTITY(1,1) NOT NULL,
    [CompanyID] [int] NOT NULL,
    [UserName] [nvarchar](15) NOT NULL,
    [LocalHost] [varchar](30) NULL,
    [ErrSource] [varchar](50) NOT NULL,
    [ErrMsg] [nvarchar](250) NOT NULL,
    [ErrFrom] [tinyint] NOT NULL,
    [ErrTime] [smalldatetime] NOT NULL DEFAULT(getdate()),
    [Serial] [varchar](500) NULL,
    [Notes] [nvarchar](500) NULL,
    [SendByEmail] [smalldatetime] NULL,
    CONSTRAINT [PK_ErrorsLog] PRIMARY KEY CLUSTERED ([ID] ASC)
)

-- Create error logging procedure
CREATE OR ALTER PROCEDURE [dbo].[ErrorsLogSave]
    @UserName nvarchar(15),
    @CompanyID int,
    @LocalHost varchar(30),
    @ErrSource varchar(50),
    @ErrMsg nvarchar(250),
    @ErrFrom tinyint,
    @Serial varchar(500),
    @Notes nvarchar(500)='',
    @rv tinyint=0 Output,
    @msg varchar(100)='' Output
AS
BEGIN
    BEGIN TRY 
        IF @ErrMsg not in ('Thread was being aborted.',N'تم إجهاض مسار التنفيذ.')
        BEGIN
            INSERT INTO ErrorsLog(UserName, CompanyID, LocalHost, ErrSource, ErrMsg, ErrFrom, Serial, Notes)
            VALUES(@UserName, @CompanyID, @LocalHost, @ErrSource, @ErrMsg, @ErrFrom, @Serial, @Notes)
            RETURN @@rowcount
        END
    END TRY
    BEGIN CATCH
        SELECT 
            ERROR_NUMBER() AS ErrorNumber,
            ERROR_SEVERITY() AS ErrorSeverity,
            ERROR_STATE() AS ErrorState,
            ERROR_PROCEDURE() AS ErrorProcedure,
            ERROR_LINE() AS ErrorLine,
            ERROR_MESSAGE() AS ErrorMessage
    END CATCH
END

-- Create core tables
CREATE TABLE [dbo].[TaskPriorities](
    [PriorityId] [int] IDENTITY(1,1) NOT NULL,
    [CompanyId] [int] NOT NULL,
    [PriorityName] [nvarchar](400) NOT NULL,
    [PriorityNameEn] [nvarchar](400) NULL,
    [PriorityColor] [nvarchar](10) NOT NULL,
    [IsDefault] [bit] NOT NULL DEFAULT 0,
    [PriorityOrder] [tinyint] NOT NULL,
    [LocationId] [bigint] NULL,
    [ResponseRateMinutes] [int] NOT NULL DEFAULT 0,
    [CompletionRateMinutes] [int] NOT NULL DEFAULT 0,
    [DueDateIntervalMinutes] [int] NOT NULL DEFAULT 0,
    CONSTRAINT [PK_TASK_PRIORITIES] PRIMARY KEY ([PriorityId])
)

CREATE TABLE [dbo].[TaskStatues](
    [ID] [int] IDENTITY(1,1) NOT NULL,
    [CompanyId] [int] NOT NULL,
    [StatusName] [nvarchar](500) NOT NULL,
    [StatusNameEn] [nvarchar](500) NULL,
    [IsStart] [bit] NOT NULL DEFAULT 0,
    [IsCompleted] [bit] NOT NULL DEFAULT 0,
    [IsCheckInstructions] [bit] NOT NULL DEFAULT 0,
    [IsMandatory] [bit] NOT NULL DEFAULT 0,
    [StatusOrder] [tinyint] NOT NULL,
    [IsRejected] [bit] NOT NULL DEFAULT 0,
    CONSTRAINT [PK_TaskStatues] PRIMARY KEY ([ID])
)

CREATE TABLE [dbo].[Assets](
    [ID] [bigint] IDENTITY(1,1) NOT NULL,
    [CompanyId] [int] NOT NULL,
    [LocationId] [bigint] NOT NULL,
    [AssetName] [nvarchar](1000) NOT NULL,
    [CategoryId] [bigint] NULL,
    [ImagePath] [nvarchar](500) NULL,
    [WeeklyOperationHours] [tinyint] NULL DEFAULT 50,
    [IsDeleted] [bit] NOT NULL DEFAULT 0,
    [ParentId] [bigint] NULL,
    [InternalId] [bigint] NULL,
    [AssetOrder] [smallint] NULL,
    [CreatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [ModifiedDate] [datetime] NULL,
    [Notes] [nvarchar](max) NULL,
    CONSTRAINT [PK_Assets] PRIMARY KEY ([ID])
)

CREATE TABLE [dbo].[WorkOrders](
    [ID] [bigint] IDENTITY(1,1) NOT NULL,
    [CompanyId] [int] NOT NULL,
    [LocationId] [bigint] NOT NULL,
    [InternalNumber] [bigint] NOT NULL,
    [TaskName] [nvarchar](1000) NOT NULL,
    [TaskDescription] [nvarchar](max) NULL,
    [CreatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
    [ModifiedDate] [datetime] NULL,
    [StartDate] [datetime] NULL,
    [DueDate] [datetime] NOT NULL,
    [TaskAssignmentId] [bigint] NULL,
    [EstimatedTime] [int] NOT NULL DEFAULT 0,
    [TaskTypeId] [smallint] NOT NULL,
    [AssetId] [bigint] NULL,
    [CompletionDate] [datetime] NULL,
    [CompletionNote] [nvarchar](max) NULL,
    [PriorityId] [int] NOT NULL,
    [CompletionRatio] [int] NOT NULL DEFAULT 0,
    [TaskStatusId] [int] NOT NULL,
    [AssetDownTime] [int] NOT NULL DEFAULT 0,
    [IsDeleted] [bit] NOT NULL DEFAULT 0,
    [CreatedBy] [bigint] NOT NULL,
    CONSTRAINT [PK_WorkOrders] PRIMARY KEY ([ID])
)

CREATE TABLE [dbo].[WorkOrderDetails](
    [ID] [int] IDENTITY(1,1) NOT NULL,
    [WorkOrderId] [bigint] NOT NULL,
    [CompanyId] [int] NOT NULL,
    [LocationId] [int] NOT NULL,
    [Latitude] [nvarchar](max) NULL,
    [Longitude] [nvarchar](max) NULL,
    [Zoom] [int] NULL,
    [MapTypeId] [nvarchar](max) NULL,
    [ImgURL] [nvarchar](max) NULL,
    [VoiceURL] [nvarchar](max) NULL,
    [CreatedBy] [bigint] NOT NULL,
    [ModifiedDate] [datetime] NULL,
    [FileURL] [nvarchar](max) NULL,
    [IsDeleted] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_WorkOrderDetails] PRIMARY KEY ([ID])
)

CREATE TABLE [dbo].[Messages_System](
    [MsgID] [smallint] NOT NULL,
    [Lang] [varchar](3) NOT NULL,
    [MsgText] [nvarchar](150) NOT NULL,
    CONSTRAINT [PK_MESSAGES_SYSTEM] PRIMARY KEY ([MsgID], [Lang])
)

-- Add relationships
ALTER TABLE [dbo].[WorkOrders] ADD CONSTRAINT [FK_WorkOrders_TaskPriorities] 
    FOREIGN KEY([PriorityId]) REFERENCES [dbo].[TaskPriorities] ([PriorityId])

ALTER TABLE [dbo].[WorkOrders] ADD CONSTRAINT [FK_WorkOrders_TaskStatues] 
    FOREIGN KEY([TaskStatusId]) REFERENCES [dbo].[TaskStatues] ([ID])

ALTER TABLE [dbo].[WorkOrders] ADD CONSTRAINT [FK_WorkOrders_Assets] 
    FOREIGN KEY([AssetId]) REFERENCES [dbo].[Assets] ([ID])

ALTER TABLE [dbo].[WorkOrderDetails] ADD CONSTRAINT [FK_WorkOrderDetails_WorkOrders] 
    FOREIGN KEY([WorkOrderId]) REFERENCES [dbo].[WorkOrders] ([ID])

-- Generate Internal Number procedure
CREATE OR ALTER PROCEDURE [dbo].[GenerateInternalNumber]
    @CompanyId int,
    @LocationId bigint,
    @InternalNumber bigint OUTPUT
AS
BEGIN
    SET @InternalNumber = ISNULL(
        (
            SELECT MAX(InternalNumber) + 1
            FROM WorkOrders
            WHERE CompanyId = @CompanyId
            AND LocationId = @LocationId
        ),
        1
    )
END

-- Main WorkOrder Save Procedure
CREATE OR ALTER PROCEDURE [dbo].[WorkOrdersSave]
    @WorkOrderJson nvarchar(max),
    @CompanyId int,
    @LocationId bigint,
    @CreatedBy bigint,
    @lang varchar(2) = 'en',
    @JsonObject nvarchar(max) = '' output
AS
BEGIN TRY
    -- Declare variables for WorkOrder fields
    DECLARE 
        @ID bigint = 0,
        @TaskName nvarchar(max),
        @StartDate datetime,
        @DueDate datetime,
        @EstimatedTime int,
        @TaskTypeId smallint,
        @PriorityId int,
        @TaskStatusId int,
        @AssetDownTime int,
        @TaskAssignmentId bigint,
        @CompletionRatio int = 0,
        @TaskDescription nvarchar(max),
        @AssetId bigint,
        @CompletionDate datetime,
        @CompletionNote nvarchar(max),
        @InternalNumber bigint

    -- Parse JSON input
    SELECT
        @TaskName = JSON_VALUE(@WorkOrderJson, '$.taskName'),
        @StartDate = JSON_VALUE(@WorkOrderJson, '$.startDate'),
        @DueDate = JSON_VALUE(@WorkOrderJson, '$.dueDate'),
        @EstimatedTime = JSON_VALUE(@WorkOrderJson, '$.estimatedTime'),
        @TaskTypeId = JSON_VALUE(@WorkOrderJson, '$.taskTypeId'),
        @PriorityId = JSON_VALUE(@WorkOrderJson, '$.priorityId'),
        @TaskStatusId = JSON_VALUE(@WorkOrderJson, '$.taskStatusId'),
        @AssetDownTime = JSON_VALUE(@WorkOrderJson, '$.assetDownTime'),
        @TaskAssignmentId = JSON_VALUE(@WorkOrderJson, '$.taskAssignmentId'),
        @TaskDescription = JSON_VALUE(@WorkOrderJson, '$.taskDescription'),
        @AssetId = JSON_VALUE(@WorkOrderJson, '$.assetId')

    -- Validation
    IF (@StartDate IS NULL)
    BEGIN
        set @JsonObject = (
            select 0 as rv,
                   IIF(@Lang = 'ar', N'تاريخ البدء مطلوب', 'Start date is required') as Msg
            For json path, include_null_values, Without_Array_wrapper
        )
        return
    END

    IF @TaskTypeId <> 134 and @StartDate > @DueDate
    BEGIN
        set @JsonObject = (
            select 0 as rv,
                   IIF(@Lang = 'ar', N'لا يمكن ان يكون تاريخ الاستحقاق اقل من تاريخ البدء',
                       'Due date cannot be less than the start date') as Msg
            For json path, include_null_values, Without_Array_wrapper
        )
        return
    END

    BEGIN TRANSACTION

    -- Generate Internal Number
    EXEC GenerateInternalNumber 
        @CompanyId = @CompanyId,
        @LocationId = @LocationId,
        @InternalNumber = @InternalNumber OUTPUT

    -- Insert WorkOrder
    INSERT INTO WorkOrders (
        CompanyId, LocationId, InternalNumber, TaskName, TaskDescription,
        StartDate, DueDate, TaskAssignmentId, EstimatedTime, TaskTypeId,
        AssetId, CompletionDate, CompletionNote, PriorityId, CompletionRatio,
        TaskStatusId, AssetDownTime, CreatedBy, CreatedDate, ModifiedDate
    )
    VALUES (
        @CompanyId, @LocationId, @InternalNumber, @TaskName, @TaskDescription,
        @StartDate, @DueDate, @TaskAssignmentId, @EstimatedTime, @TaskTypeId,
        @AssetId, @CompletionDate, @CompletionNote, @PriorityId, @CompletionRatio,
        1, @AssetDownTime, @CreatedBy, GETDATE(), GETDATE()
    )

    SET @ID = SCOPE_IDENTITY()

    -- Insert WorkOrder Details if provided
    IF EXISTS (SELECT 1 FROM OPENJSON(@WorkOrderJson, '$.details'))
    BEGIN
        INSERT INTO WorkOrderDetails (
            WorkOrderId, CompanyId, LocationId, Latitude, Longitude,
            Zoom, MapTypeId, ImgURL, VoiceURL, CreatedBy, FileURL, IsDeleted
        )
        SELECT
            @ID,
            @CompanyId,
            @LocationId,
            JSON_VALUE([value], '$.latitude'),
            JSON_VALUE([value], '$.longitude'),
            JSON_VALUE([value], '$.zoom'),
            JSON_VALUE([value], '$.mapTypeId'),
            JSON_VALUE([value], '$.imgUrl'),
            JSON_VALUE([value], '$.voiceUrl'),
            @CreatedBy,
            JSON_VALUE([value], '$.fileUrl'),
            'false'
        FROM OPENJSON(@WorkOrderJson, '$.details')
    END

    -- Prepare success response
    IF @@ROWCOUNT > 0
    BEGIN
        SET @JsonObject = (
            SELECT @ID as rv,
                   CONCAT(dbo.GetMessagesSystem(1, @lang), ' -> ', CAST(@InternalNumber as varchar(max))) as Msg,
                   (
                       SELECT wo.*, wod.* 
                       FROM WorkOrders wo
                       LEFT JOIN WorkOrderDetails wod ON wo.ID = wod.WorkOrderId
                       WHERE wo.ID = @ID
                       FOR JSON PATH
                   ) as data
            FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
        )

        COMMIT TRANSACTION
    END
    ELSE
    BEGIN
        ROLLBACK TRANSACTION
        SET @JsonObject = (
            SELECT 0 as rv,
                   dbo.GetMessagesSystem(2, @lang) as Msg
            FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
        )
    END

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION

    SET @JsonObject = (
        SELECT -1 as rv,
               ERROR_MESSAGE() as Msg
        FOR JSON PATH, INCLUDE_NULL_VALUES, WITHOUT_ARRAY_WRAPPER
    )

    -- Log error
    DECLARE @ErrSource varchar(50) = OBJECT_NAME(@@PROCID)
    EXEC dbo.ErrorsLogSave 
        @UserName = @CreatedBy,
        @CompanyId = @CompanyId,
        @LocalHost = '',
        @ErrSource = @ErrSource,
        @ErrMsg = @JsonObject,
        @ErrFrom = 1,
        @Serial = @ID
END CATCH