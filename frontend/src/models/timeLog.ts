import { UsersTask } from "./usersTask";

export type TimeLog = {
    logId: number;
    userTaskId: number;
    startTime: Date;
    endTime: Date;
    userTask: UsersTask;
};