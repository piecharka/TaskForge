import { ProjectTask } from "./projectTask";

export type ProjectTaskType = {
    typeId: number;
    typeName: string;
    projectTasks: ProjectTask[];
};