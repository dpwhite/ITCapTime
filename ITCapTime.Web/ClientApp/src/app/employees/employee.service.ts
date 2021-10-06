import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Employee } from './index';
import { EmployeeProjects } from '../projects/index'

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>('api/employees');
  }

  addNewEmployee(employee: Employee) {
    return this.http.post('api/employees', employee)
  }

  getProjectsForEmployee(employeeId: string): Observable<EmployeeProjects[]> {
    return this.http.get<EmployeeProjects[]>(`api/employees/projects/${employeeId}`);
  }
}
