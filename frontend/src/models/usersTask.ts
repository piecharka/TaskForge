import { ProjectTask } from "./projectTask";
import { TimeLog } from "./timeLog";
import { User } from "./user";

export type UsersTask = {
    userTaskId: number;
    userId: number;
    taskId: number;
    task: ProjectTask;
    timeLogs: TimeLog[];
    user: User;
};