import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { EmployeeService } from '../employees/employee.service';
import { EmployeeProjects, Project } from './projects';
import { ProjectService } from './project.service';

@Component({
  selector: 'projects-list',
  templateUrl: './projectsList.component.html',
  styleUrls: ['./projectsList.component.scss']
})

export class ProjectsListComponent implements OnInit {
  employeeId: string = '';
  projectDataSource: Project[] = [];
  tableData: any;
  showProjectInfo: boolean = false;
  project: Project = new Project();

  columnHeaders: string[] = ['name',
    'project',
    'afe',
    'project management',
    'requirement gathering',
    'solution design',
    'solution build',
    'data conversion',
    'testing',
    'total'
  ];
  //objectKeys = Object.keys;

  displayedColumns: string[] = ['name', 'project type', 'start date', 'description'];

  constructor(private router: Router, private route: ActivatedRoute, private service: ProjectService) {
  }

  ngOnInit(): void {
    this.employeeId = this.route.snapshot.paramMap.get("id") || '';
    if (this.employeeId.length > 0) {
      this.service.getProjectsForEmployee(this.employeeId).subscribe(
        (res: Project[]) => {
          this.projectDataSource = res;
        },
        (err: any) => {
          console.log('Errors: ', err);
        }
      );
    }
    else {
      //retrieve all projects for all employees
      this.service.getProjects().subscribe(
        (res: Project[]) => {
          this.projectDataSource = res;
        },
        (err: any) => {
          console.log('Errors: ', err);
        }
      );
    }
    this.initDataSource();
  }

  initDataSource(): void {
    //this.projectDataSource = new MatTableDataSource(this.tableData);
  }

  editProjectInfo(projectId: string) {
    console.log('project id: ', projectId);
    this.service.getProject(projectId).subscribe(
      (res: Project) => {
        this.project = res;
      }
    );
    this.showProjectInfo = true; 
  }

  cancel(): void {
    this.project = new Project();
    this.showProjectInfo = false;
  }

  saveChanges(): void {
    this.service.getProjectsForEmployee(this.employeeId).subscribe(
      (res: Project[]) => {
        this.projectDataSource = res;
        this.showProjectInfo = false;
      },
      (err: any) => {
        console.log('Errors: ', err);
      }
    );
  }
}
