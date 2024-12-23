import { Component, OnInit } from '@angular/core';
import { WorkOrder, WorkOrderDetail } from '../../models/work-order.model';
import { WorkOrderService } from '../../services/work-order.service';

import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { SignalRService } from '../../services/signalr.service';
import { Inject } from '@angular/core';
@Component({
  selector: 'app-work-order-list',
  standalone: true,
  imports: [CommonModule, MatExpansionModule],
  templateUrl: './work-order-list.component.html',
  styleUrls: ['./work-order-list.component.scss'],
})
export class WorkOrderListComponent implements OnInit {
  workOrders: WorkOrder[] = []; // Define the correct type
  loading = true;
  error = '';

  constructor(
    private workOrderService: WorkOrderService,
    @Inject(SignalRService)  private signalRService: SignalRService
  ) {}

  ngOnInit(): void {
    this.fetchWorkOrders();

    // Subscribe to real-time updates
    this.signalRService.startConnection();
    this.signalRService.workOrderUpdates$.subscribe((update: WorkOrder) => {
      this.updateWorkOrderList(update);
    });
  }

  fetchWorkOrders(): void {
    this.workOrderService.getWorkOrders().subscribe({
      next: (data: WorkOrder[]) => {
        console.log('Fetched work orders:', data); // Debug log
        this.workOrders = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching work orders:', err);
        this.error = 'Failed to load work orders.';
        this.loading = false;
      },
    });
  }

  updateWorkOrderList(update: WorkOrder): void {
    const index = this.workOrders.findIndex((w) => w.id === update.id);
    if (index !== -1) {
      this.workOrders[index] = update; // Update existing work order
    } else {
      this.workOrders.push(update); // Add new work order
    }
  }
}
