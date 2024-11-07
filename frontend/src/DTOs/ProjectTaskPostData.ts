

export type ProjectTaskPostData = {
    taskName: string;
    taskStatusId: number;
    teamId: number;
    createdBy: number;
    taskDeadline: Date;
    taskTypeId: number;
    taskDescription: string;
    sprintId: number;
    userIds: number[];
};
