import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter, Route } from '@angular/router';
import { importProvidersFrom } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { WorkOrderListComponent } from './app/components/work-order-list/work-order-list.component';
import { HttpClientModule } from '@angular/common/http';
const routes: Route[] = [
  { path: '', redirectTo: '/work-orders', pathMatch: 'full' },
  { path: 'work-orders', component: WorkOrderListComponent },
];

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes),
    importProvidersFrom(BrowserAnimationsModule,HttpClientModule),
  ],
}).catch((err) => console.error(err));
