import axios, { AxiosResponse } from 'axios';
import { User } from '../models/user';
import { Team } from '../models/team';
import { ProjectTask } from '../models/projectTask';
import { ProjectTaskType } from '../models/projectTaskType';
import { ProjectTaskStatus } from '../models/projectTaskStatus';

axios.defaults.baseURL = 'http://localhost:5194/api';

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    post: <T> (url: string, body: object) => axios.post<T>(url, body).then(responseBody),
    put: <T> (url: string, body: object) => axios.put<T>(url, body).then(responseBody),
    del: <T> (url: string) => axios.delete<T>(url).then(responseBody),
}

const Users = {
    list: () => requests.get<User[]>('/users'),
    teamUsers: (id : number) => requests.get<User[]>(`/users/team/${id}`)
}

const Teams = {
    list: () => requests.get<Team[]>('/teams'),
    first: (id : number) => requests.get<Team>(`/teams/${id}`)
}

const ProjectTasks = {
    projectTaskListInTeam: (teamId: number) => requests.get<ProjectTask>(`/projecttasks/${teamId}`),
    todoTasks: (userId: number) => requests.get<ProjectTask>(`/projecttasks/users/${userId}`)
}

const ProjectTaskTypes = {
    projectTaskTypesList: () => requests.get <ProjectTaskType>(`/projecttasktype`)
}

const ProjectTaskStatuses = {
    projectTaskStatusesList: () => requests.get<ProjectTaskStatus>(`/projecttaskstatus`)
}

const apiHandler = {
    Users,
    Teams,
    ProjectTasks,
    ProjectTaskTypes,
    ProjectTaskStatuses
}

export default apiHandler;