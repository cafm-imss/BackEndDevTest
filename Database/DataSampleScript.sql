-- Populate Task Priorities
SET IDENTITY_INSERT TaskPriorities ON
INSERT INTO TaskPriorities (PriorityId, CompanyId, PriorityName, PriorityNameEn, PriorityColor, IsDefault, PriorityOrder, ResponseRateMinutes, CompletionRateMinutes, DueDateIntervalMinutes)
VALUES 
(1, 1, N'عاجل جداً', 'Critical', '#FF0000', 0, 1, 30, 120, 240),
(2, 1, N'عاجل', 'High', '#FFA500', 0, 2, 60, 240, 480),
(3, 1, N'متوسط', 'Medium', '#FFFF00', 1, 3, 120, 480, 1440),
(4, 1, N'منخفض', 'Low', '#008000', 0, 4, 240, 960, 2880);
SET IDENTITY_INSERT TaskPriorities OFF

-- Populate Task Statuses
SET IDENTITY_INSERT TaskStatues ON
INSERT INTO TaskStatues (ID, CompanyId, StatusName, StatusNameEn, IsStart, IsCompleted, IsCheckInstructions, IsMandatory, StatusOrder, IsRejected)
VALUES
(1, 1, N'جديد', 'New', 1, 0, 0, 1, 1, 0),
(2, 1, N'مفتوح', 'Open', 0, 0, 0, 1, 2, 0),
(3, 1, N'قيد التنفيذ', 'In Progress', 0, 0, 0, 1, 3, 0),
(4, 1, N'معلق', 'On Hold', 0, 0, 0, 1, 4, 0),
(5, 1, N'مكتمل', 'Completed', 0, 1, 1, 1, 5, 0),
(6, 1, N'ملغي', 'Cancelled', 0, 0, 0, 1, 6, 1);
SET IDENTITY_INSERT TaskStatues OFF

-- Populate Assets
SET IDENTITY_INSERT Assets ON
INSERT INTO Assets (ID, CompanyId, LocationId, AssetName, CategoryId, WeeklyOperationHours, IsDeleted, InternalId, AssetOrder, CreatedDate)
VALUES
(1, 1, 1, N'مكيف الطابق الأول', 1, 168, 0, 1001, 1, DATEADD(month, -6, GETDATE())),
(2, 1, 1, N'مصعد المبنى الرئيسي', 2, 168, 0, 1002, 2, DATEADD(month, -6, GETDATE())),
(3, 1, 1, N'نظام إطفاء الحريق', 3, 168, 0, 1003, 3, DATEADD(month, -6, GETDATE())),
(4, 1, 2, N'مولد الطاقة الاحتياطي', 4, 40, 0, 2001, 1, DATEADD(month, -5, GETDATE()));
SET IDENTITY_INSERT Assets OFF

-- Sample Work Orders with different statuses and priorities
DECLARE @CurrentDate datetime = GETDATE()

-- Insert Work Orders
INSERT INTO WorkOrders (
    CompanyId, LocationId, InternalNumber, TaskName, TaskDescription,
    CreatedDate, StartDate, DueDate, EstimatedTime, TaskTypeId,
    AssetId, PriorityId, TaskStatusId, CreatedBy, CompletionRatio
)
VALUES
-- Completed high priority work order
(1, 1, 1001, N'تصليح تسرب في مكيف الطابق الأول', 
 N'تم اكتشاف تسرب في وحدة التكييف الرئيسية. يحتاج إلى إصلاح فوري لمنع تلف السقف',
 DATEADD(day, -5, @CurrentDate),
 DATEADD(day, -5, @CurrentDate),
 DATEADD(day, -4, @CurrentDate),
 120,
 1,
 1,
 1, -- Critical Priority
 5, -- Completed
 1,
 100),

-- In Progress medium priority work order
(1, 1, 1002, N'صيانة دورية للمصعد',
 N'الصيانة الدورية الشهرية للمصعد وفحص أنظمة السلامة',
 DATEADD(day, -2, @CurrentDate),
 DATEADD(day, -2, @CurrentDate),
 DATEADD(day, +3, @CurrentDate),
 240,
 1,
 2,
 3, -- Medium Priority
 3, -- In Progress
 1,
 45),

-- New low priority work order
(1, 1, 1003, N'فحص نظام إطفاء الحريق',
 N'الفحص السنوي لنظام إطفاء الحريق وتحديث شهادات السلامة',
 @CurrentDate,
 DATEADD(day, +1, @CurrentDate),
 DATEADD(day, +7, @CurrentDate),
 480,
 1,
 3,
 4, -- Low Priority
 1, -- New
 1,
 0),

-- On Hold work order
(1, 2, 2001, N'صيانة المولد الاحتياطي',
 N'صيانة شاملة للمولد الاحتياطي وتغيير الزيت والفلاتر',
 DATEADD(day, -3, @CurrentDate),
 DATEADD(day, -3, @CurrentDate),
 DATEADD(day, +4, @CurrentDate),
 360,
 1,
 4,
 2, -- High Priority
 4, -- On Hold
 1,
 25);

-- Insert Work Order Details
INSERT INTO WorkOrderDetails (
    WorkOrderId, CompanyId, LocationId, 
    Latitude, Longitude, Zoom,
    CreatedBy, IsDeleted
)
SELECT 
    ID, -- WorkOrderId
    CompanyId,
    LocationId,
    '24.7136' as Latitude,
    '46.6753' as Longitude,
    14 as Zoom,
    CreatedBy,
    'false' as IsDeleted
FROM WorkOrders;

-- Add some specific details for the completed work order
INSERT INTO WorkOrderDetails (
    WorkOrderId, CompanyId, LocationId,
    ImgURL, FileURL, CreatedBy, IsDeleted
)
SELECT 
    ID,
    CompanyId,
    LocationId,
    '/maintenance-photos/ac-leak-before.jpg' as ImgURL,
    '/maintenance-reports/ac-repair-report.pdf' as FileURL,
    CreatedBy,
    'false' as IsDeleted
FROM WorkOrders 
WHERE TaskStatusId = 5; -- Completed status
