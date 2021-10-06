import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Project, EmployeeProjects } from '../projects/index'

@Injectable({
  providedIn: 'root'
})

export class ProjectService {

  constructor(private http: HttpClient) { }

  getProjects(): Observable<Project[]> {
    return this.http.get<Project[]>('api/projects');
  }

  getProjectsForEmployee(employeeId: string): Observable<Project[]> {
    return this.http.get<Project[]>(`api/projects/employee/${employeeId}`);
  }

  getProject(projectId: string): Observable<Project> {
    return this.http.get<Project>(`api/projects/${projectId}`);
  }

  updateProject(project: Project) {    
    return this.http.put<Project>(`api/projects/project/${project.id}`, project);
  }
}
