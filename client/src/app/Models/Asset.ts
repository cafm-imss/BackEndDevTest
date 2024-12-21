export interface Asset {
  id: number;
  notes: string;
  parentId: number;
  internalId: number;
  assetOrder: number;
  companyId: number;
  locationId: number;
  assetName: string;
  categoryId: number;
  imagePath: string;
  weeklyOperationHours: number;
}
