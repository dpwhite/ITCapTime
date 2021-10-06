import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { LoginService } from 'src/app/login/login.service';
import { AppService } from 'src/app/app.service';
import { Router } from '@angular/router';
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { EmployeeProfileComponent } from '../employees/employeeProfile.component';
import { Employee } from '../employees/employee';
import { EmployeeService } from '../employees/employee.service';

@Component({
  selector: 'app-welcome',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  selectedEmployee: string;
  newEmployee: any;

  employees: Employee[] = [];


  loginForm = this.fb.group({
    employeeName: ''
  });

  constructor(private fb: FormBuilder, private loginService: LoginService, private appService: AppService, private router: Router, private _modalService: NgbModal, private employeeService: EmployeeService) {
    this.selectedEmployee = "";
  }

  ngOnInit(): void {
    this.employeeService.getEmployees().subscribe(
      (res: Employee[]) => {
        this.employees = res;
      },
      (err: any) => {
        console.log('error: ', err);
      }
    );
  }

  getSelectedEmployee() {
    console.log('Employee, ', this.selectedEmployee);
  }

  onSubmit() {
    //console.log(this.loginForm.value);   // {first: 'Nancy', last; 'Drew'}
    //console.log(this.loginForm.status);
    //this.loginForm.markAllAsTouched();
    //this.loginForm.updateValueAndValidity();
    //console.warn(this.loginForm.value);
    if (this.selectedEmployee.length > 0) {
      let employee = this.employees.find(e => e.id == this.selectedEmployee);

      //this.loginForm.controls.employeeName.setValue(employee?.firstName + " " + employee?.lastName);
      this.loginService.loginUser(this.selectedEmployee).subscribe(
        (res: any) => {
          this.router.navigate(['/projects', this.selectedEmployee]);
        },
        (err: any) => {
          console.log('login error:', err);
        }
      );
    }
  }


  get userName() {
    //return this.loginForm.get('userName');
    return '';
  }

  get password() {
    //return this.loginForm.get('password');
    return '';
  }

  login() {

    if (this.selectedEmployee.length > 0) {
      let employee = this.employees.find(e => e.id == this.selectedEmployee);
      this.loginForm.controls.employeeName.setValue(employee?.firstName + " " + employee?.lastName)
    }

    this.loginService.loginUser(this.loginForm.controls.employeeName.value).subscribe(
      () => {
        console.log('logged on user');
      },
      (err: any) => {
        console.log('error', err);
      }
    );
  }

  cancelNewEmployee() {
    this.loginForm.controls.employeeName.setValue('');
    this.selectedEmployee = "";
  }

  addNewEmployee() {
    const modalRef = this._modalService.open(EmployeeProfileComponent, { backdrop: 'static', centered: true, size: 'lg' });
    modalRef.componentInstance.isNewEmployee = true;
    modalRef.result.then((e: Employee) => {
      console.log('results from addemployee, ', e);
      this.employees.push(e);
    });
    //this.newEmployee = modalRef.componentInstance.newEmployee.subscribe((newEmployee: Employee) => {
    //  this.employees.push(newEmployee);
    //})
  }
}
