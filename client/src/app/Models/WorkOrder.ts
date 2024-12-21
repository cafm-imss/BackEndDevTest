import { Asset } from './Asset';
import { TaskStatus } from './TaskStatus';

export interface WorkOrder {
  id: number;
  companyId: number;
  locationId: number;
  internalNumber: number;
  taskName: string;
  taskDescription: string;
  startDate: string;
  dueDate: string;
  taskAssignmentId: number;
  estimatedTime: number;
  taskTypeId: number;
  assetId: number;
  completionDate: string;
  completionNote: string;
  priorityId: number;
  completionRatio: number;
  assetDownTime: number;
  taskStatus?: TaskStatus | null;
  asset?: Asset | null;
}
