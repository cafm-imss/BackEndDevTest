import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { WorkOrder } from '../models/work-order.model';

@Injectable({
  providedIn: 'root',
})
export class WorkOrderService {
  private apiUrl = 'https://localhost:7124/api/workorder/1'; // Correct base URL

  constructor(private http: HttpClient) {}

  // Fetch all work orders and map the nested structure
  getWorkOrders(): Observable<WorkOrder[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map((response) => {
        // Handle nested $values structure
        if (response['$values']) {
          return response['$values'].map((workOrder: any) => this.mapWorkOrder(workOrder));
        }
        return [this.mapWorkOrder(response)]; // Handle single object response
      })
    );
  }

  // Fetch a single work order by ID
  getWorkOrderById(id: number): Observable<WorkOrder> {
    return this.http.get<any>(`${this.apiUrl}/${id}`).pipe(
      map((response) => this.mapWorkOrder(response))
    );
  }

  // Map the JSON response to the WorkOrder model
  private mapWorkOrder(data: any): WorkOrder {
    return {
      ...data,
      workOrderDetails: data.workOrderDetails?.$values || [], // Extract $values for workOrderDetails
    };
  }
}
