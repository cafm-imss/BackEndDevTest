import { Routes } from '@angular/router';
import { WorkOrderComponent } from './components/workorder/workorder.component';

export const routes: Routes = [
  { path: '', redirectTo: '/workorder/1', pathMatch: 'full' },
  { path: 'workorder/:id', component: WorkOrderComponent },
];
