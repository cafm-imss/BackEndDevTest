export interface WorkOrder {
    id: number;
    companyId: number;
    locationId: number;
    internalNumber: number;
    taskName: string;
    taskDescription: string;
    createdDate: string;
    modifiedDate?: string | null;
    startDate: string;
    dueDate: string;
    taskAssignmentId?: number | null;
    estimatedTime: number;
    taskTypeId: number;
    assetId?: number | null;
    completionDate?: string | null;
    completionNote?: string | null;
    priorityId: number;
    completionRatio: number;
    taskStatusId: number;
    assetDownTime: number;
    isDeleted: boolean;
    createdBy: number;
    asset?: any | null;
    priority?: any | null;
    taskStatus?: any | null;
    workOrderDetails: WorkOrderDetail[];
  }
  
  export interface WorkOrderDetail {
    id: number;
    workOrderId: number;
    companyId: number;
    locationId: number;
    latitude?: string | null;
    longitude?: string | null;
    zoom?: number | null;
    mapTypeId?: any | null;
    imgURL?: string | null;
    voiceURL?: string | null;
    createdBy: number;
    modifiedDate?: string | null;
    fileURL?: string | null;
    isDeleted: boolean;
  }
  