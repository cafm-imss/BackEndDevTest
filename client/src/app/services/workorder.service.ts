import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WorkOrderService {
  private apiUrl = 'https://localhost:7061/api/workorder';

  constructor(private http: HttpClient) {}

  getWorkOrderById(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

}
