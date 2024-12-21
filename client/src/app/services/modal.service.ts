import { Injectable } from '@angular/core';
import * as bootstrap from 'bootstrap';

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  private modals: Map<string, bootstrap.Modal> = new Map();

  public registerModal(modalId: string, modal: bootstrap.Modal): void {
    this.modals.set(modalId, modal);
  }

  public openModal(modalId: string): void {
    const modal = this.modals.get(modalId);
    if (modal) {
      modal.show();
    } else {
      console.error(`Modal with id ${modalId} not found`);
    }
  }

  public closeModal(modalId: string): void {
    const modal = this.modals.get(modalId);
    if (modal) {
      modal.hide();
    } else {
      console.error(`Modal with id ${modalId} not found`);
    }
  }
}
