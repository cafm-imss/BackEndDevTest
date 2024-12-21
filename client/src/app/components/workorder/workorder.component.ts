import { Component, OnInit, OnDestroy } from '@angular/core';
import { WorkOrderService } from '../../services/workorder.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import * as bootstrap from 'bootstrap';
import { ModalService } from '../../services/modal.service';
import { SignalRService } from '../../services/SignalRService';
import { ActivatedRoute } from '@angular/router';
import { WorkOrder } from '../../Models/WorkOrder';

@Component({
  selector: 'app-workorder',
  imports: [CommonModule, FormsModule],
  templateUrl: './workorder.component.html',
  styleUrls: ['./workorder.component.css'],
})
export class WorkOrderComponent implements OnInit, OnDestroy {
  workOrder!: WorkOrder;

  constructor(
    private workOrderService: WorkOrderService,
    private route: ActivatedRoute,
    private modalService: ModalService,
    private signalRService: SignalRService
  ) {}

  ngOnInit(): void {
    this.loadWorkOrder(this.route.snapshot.paramMap.get('id') ?? '');
    this.registerModals();
    this.signalRService.startConnection();
    this.signalRService.addWorkOrderStatusListener((message: string) => {
      console.log('Received Status Update:', message);
      this.loadWorkOrder(this.route.snapshot.paramMap.get('id') ?? '');
    });
  }

  ngOnDestroy(): void {
    this.signalRService.stopConnection();
  }

  private loadWorkOrder(id: string): void {
    this.workOrderService.getWorkOrderById(id).subscribe(
      (data) => {
        this.workOrder = data;
      },
      (error) => {
        displayErrorMessages(error.error);
      }
    );
  }

  private registerModals(): void {}

  private registerModal(
    modalId: string,
    setModal: (modal: bootstrap.Modal) => void
  ): void {
    setTimeout(() => {
      const modalElement = document.getElementById(modalId);
      if (modalElement) {
        const modal = new bootstrap.Modal(modalElement);
        setModal(modal);
        this.modalService.registerModal(modalId, modal);
      } else {
        console.error(`Modal element with id ${modalId} not found`);
      }
    }, 0);
  }
}

function displayErrorMessages(errors: string[]): void {
  const errorMessage = errors.join();
  alert(errorMessage);
}
