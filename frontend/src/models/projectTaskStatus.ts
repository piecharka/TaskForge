import { ProjectTask } from "./projectTask";

export type ProjectTaskStatus = {
    statusId: number;
    statusName: string;
    projectTasks: ProjectTask[];
};
