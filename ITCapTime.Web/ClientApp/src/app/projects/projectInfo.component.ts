import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { ProjectService } from './project.service';
import { Project } from './projects';

@Component({
  selector: 'project-info',
  templateUrl: './projectInfo.component.html',
  styleUrls: ['./projectInfo.component.scss']
})

export class ProjectInfoComponent implements OnInit {
  @Input() project: Project = new Project();
  @Output() closeProject = new EventEmitter();
  @Output() saveProject = new EventEmitter();

  projectForm = this.fb.group({
    projectId: new FormControl(''),
    projectName: new FormControl('', [Validators.required]),
    projectType: new FormControl(Number(), [Validators.required]),
    startDate: new FormControl(new Date(), [Validators.required]),
    endDate: new FormControl(new Date()),
    description: new FormControl(new String())
  });

  constructor(private router: Router, private route: ActivatedRoute, private service: ProjectService, private fb: FormBuilder) { }

  ngOnInit(): void {
    //this.initializeValues(); 
  }

  onSave(): void {
    if (this.projectForm.valid) {
      const project = new Project();
      project.description = this.projectForm.controls.description.value;
      project.id = this.projectForm.controls.projectId.value;
      project.name = this.projectForm.controls.projectName.value;
      project.projectType = this.projectForm.controls.projectType.value;
      project.startDate = this.projectForm.controls.startDate.value;
      project.endDate = this.projectForm.controls.endDate.value;
      this.service.updateProject(project).subscribe(
        (res: any) => {
          this.saveProject.emit();
        },
        (err: any) => {
          console.log('errors: ', err);
        }
      );
      
    }
  }

  cancel(): void {
    this.closeProject.emit();
  }

  initializeValues(): void {
    this.projectForm.controls.projectId.setValue(this.project.id);
    this.projectForm.controls.projectName.setValue(this.project.name);
    this.projectForm.controls.projectType.setValue(this.project.projectType);
    this.projectForm.controls.startDate.setValue(new Date(this.project.startDate));
    if (this.project.endDate.length > 0) {
      this.projectForm.controls.endDate.setValue(new Date(this.project.endDate));
    }
    this.projectForm.controls.description.setValue(this.project.description);
  }

  ngOnChanges() {
    this.initializeValues();
  }
}
