import { Routes } from '@angular/router';
import { WorkOrderListComponent } from './work-order-list/work-order-list.component';

export const routes: Routes = [
    {
        path: 'work-order/:companyId/:locationId',
        component: WorkOrderListComponent
      },
];
