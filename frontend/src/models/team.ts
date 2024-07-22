import { ProjectTask } from "./projectTask";
import { User } from "./user";

export type Team = {
    teamId: number;
    teamName: string;
    projectTasks: ProjectTask[];
    users: User[];
};
