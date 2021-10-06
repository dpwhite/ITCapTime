
export class Project {
  id: string = '';
  name: string = '';
  projectType: string = '';
  startDate: string = '';
  endDate: string = '';
  description: string = '';
  
}

export class Employee {
  id: string = '';
  name: string = '';
}

export class EmployeeProjects {
  projectId: string = '';
  employeeId: string = '';
  name: string = '';
  projectType: string = '';
  projectManagement: number = 0;
  gapAnalysis: number = 0;
  solutionDesign: number = 0;
  solutionBuild: number = 0;
  dataConversion: number = 0;
  testing: number = 0;
  trainig: number = 0;
  comments: string = '';
}

