import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { Employee } from './employee';
import { EmployeeService } from '../employees/employee.service';

@Component({
  selector: 'employee-profile',
  templateUrl: './employeeProfile.component.html',
  styleUrls: ['./employeeProfile.component.css']
})

export class EmployeeProfileComponent {
@Input() isNewEmployee: boolean | undefined;
  @Output() newEmployee = new EventEmitter<Employee>();

  employeeForm = this.fb.group({
    firstName: [''],
    lastName: [''],
    email: [''],
    title: ['']
  })
  constructor(private fb: FormBuilder, public activeModal: NgbActiveModal, private service: EmployeeService) { }
  onSubmit() { }

  addNewEmployee() {
    console.log(this.employeeForm.get('firstName')?.value);
    let employee = new Employee();
    employee.firstName = this.employeeForm.get('firstName')?.value;
    employee.lastName = this.employeeForm.get('lastName')?.value;
    employee.email = this.employeeForm.get('email')?.value;
    employee.title = this.employeeForm.get('title')?.value;
    this.service.addNewEmployee(employee).subscribe(
      (res: any) => {
        console.log('results: ', res);
      },
      (err: any) => {
        console.log('errors: ', err);
      }
    );
    this.newEmployee.emit(employee);
    this.activeModal.close(employee);
  }
}
