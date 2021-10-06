import { Project } from '../projects/projects';

export class Employee {
  id: string | undefined;
  firstName: string | undefined;
  lastName: string | undefined;
  title: string | undefined;
  email: string | undefined;
  projects: Project[] | undefined;
}
