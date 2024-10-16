import axios, { AxiosResponse } from 'axios';
import { User, UserLoginData, UserRegisterData } from '../models/user';
import { Team } from '../models/team';
import { ProjectTask } from '../models/projectTask';
import { ProjectTaskType } from '../models/projectTaskType';
import { ProjectTaskStatus } from '../models/projectTaskStatus';
import { store } from '../stores/store';
import { CommentInsertData } from '../models/comment';

axios.defaults.baseURL = 'http://localhost:5194/api';

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;

    return config
})

const requests = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    post: <T> (url: string, body: object) => axios.post<T>(url, body).then(responseBody),
    put: <T> (url: string, body: object) => axios.put<T>(url, body).then(responseBody),
    del: <T> (url: string) => axios.delete<T>(url).then(responseBody),

}
const Account = {   
    login: (loginData: UserLoginData) => requests.post<User>('/account/login', loginData),
    register: (registerData: UserRegisterData) => requests.post<User>('/account/register', registerData),
}

const Comments = {
    taskComments: (taskId: number) => requests.get<Comment>(`/comments/${taskId}`),
    insertComment: (commentData: CommentInsertData) => requests.post<Comment>(`/comments/`, commentData)
}

const Users = {
    getUserById: (userId: number) => requests.get<User>(`/users/${userId}`),
    list: () => requests.get<User[]>('/users'),
    teamUsers: (id : number) => requests.get<User[]>(`/users/team/${id}`)
}

const Teams = {
    list: () => requests.get<Team[]>('/teams'),
    first: (id: number) => requests.get<Team>(`/teams/${id}`),
    teamsByUsername: (username: string | undefined) => requests.get<Team[]>(`/teams/user/${username}`)
}

const ProjectTasks = {
    projectTaskListInTeam: (teamId: number) => requests.get<ProjectTask>(`/projecttasks/team/${teamId}`),
    todoTasks: (username: string | undefined) => requests.get<ProjectTask>(`/projecttasks/users/${username}`),
    getTask: (taskId: number) => requests.get<ProjectTask>(`/projecttasks/${taskId}`)
}

const ProjectTaskTypes = {
    projectTaskTypesList: () => requests.get <ProjectTaskType>(`/projecttasktype`)
}

const ProjectTaskStatuses = {
    projectTaskStatusesList: () => requests.get<ProjectTaskStatus>(`/projecttaskstatus`)
}

const apiHandler = {
    Account,
    Comments,
    Users,
    Teams,
    ProjectTasks,
    ProjectTaskTypes,
    ProjectTaskStatuses
}

export default apiHandler;