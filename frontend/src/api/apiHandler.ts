import axios, { AxiosResponse } from 'axios';
import { User, UserLoginData, UserRegisterData } from '../models/user';
import { Team } from '../models/team';
import { ProjectTask } from '../models/projectTask';
import { ProjectTaskType } from '../models/projectTaskType';
import { ProjectTaskStatus } from '../models/projectTaskStatus';
import { store } from '../stores/store';
import { CommentInsertData } from '../models/comment';
import { ProjectTaskPostData } from '../DTOs/ProjectTaskPostData';
import { Sprint } from '../models/sprint';
import { SprintPostData } from '../DTOs/SprintPostData';
import { SprintEventPostData } from '../DTOs/SprintEventPostData';
import { TaskUserPostData } from '../DTOs/TaskUserPostData';
import { SprintTaskCountData } from '../DTOs/SprintTaskCountData';


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
    put: <T>(url: string, body: object) => axios.put<T>(url, body).then(responseBody),
    put2: <T>(url: string) => axios.put<T>(url).then(responseBody),
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
    teamsByUsername: (username: string | undefined) => requests.get<Team[]>(`/teams/user/${username}`),
    deleteUserFromTeam: (userId: number, teamId: number) => requests.del(`/teams/user?userId=${userId}&teamId=${teamId}`)
}

const TimeLogs = {
    sprintOverdueLogsCount : (sprintId: number) => requests.get(`/timelog/overdue-count/${sprintId}`),
}

const Permission = {
    getPermissionByUserId: (userId: number, teamId: number) => requests.get(`/permission/user?userId=${userId}&teamId=${teamId}`),
    updateUsersPermission: (userId: number, teamId: number, permissionId: number) =>
        requests.put(`/permission/user?userId=${userId}&teamId=${teamId}&permissionId=${permissionId}`, {}),
    getPermissions: () => requests.get(`/permission`),
}

const ProjectTasks = {
    projectTaskListInTeam: (teamId: number, sortBy: string, sortOrder: string) =>
        requests.get<ProjectTask>(`/projecttasks/team/${teamId}?SortBy=${sortBy}&SortOrder=${sortOrder}`),
    todoTasks: (username: string | undefined) => requests.get<ProjectTask>(`/projecttasks/users/${username}`),
    getTask: (taskId: number) => requests.get<ProjectTask>(`/projecttasks/${taskId}`),
    getSprintTasks: (sprintId: number) => requests.get<ProjectTask>(`/projecttasks/sprint/${sprintId}`),
    getUsersAssigned: (taskId: number) => requests.get<User[]>(`/projecttasks/user/${taskId}`),
    getTasksCount: (sprintId: number) => requests.get<number>(`/projecttasks/count/${sprintId}`),
    getTodoTasksCount: (sprintId: number) => requests.get<number>(`/projecttasks/count/to-do/${sprintId}`),
    getInProgressTasksCount: (sprintId: number) => requests.get<number>(`/projecttasks/count/in-progress/${sprintId}`),
    getDoneTasksCount: (sprintId: number) => requests.get<number>(`/projecttasks/count/done/${sprintId}`),
    getUserTasksInSprintCount: (teamId: number, sprintId: number) => requests.get<number>
        (`/projecttasks/count/usertasks?teamId=${teamId}&sprintId=${sprintId}`),
    postTask: (taskData: ProjectTaskPostData) => requests.post<ProjectTask>(`/projecttasks`, taskData),
    assignUserToTask: (taskData: TaskUserPostData) => requests.post(`projecttasks/users`, taskData),
    deleteUserFromTask: (taskId: number, userId: number) => requests.del(`projecttasks/users?taskId=${taskId}&userId=${userId}`),
    deleteTask: (taskId: number) => requests.del(`/projecttasks/${taskId}`),
    updateTaskStatus: (taskId: number, statusId: number) => requests.put2(`projecttasks/status?taskId=${taskId}&statusId=${statusId}`)
}

const ProjectTaskTypes = {
    projectTaskTypesList: () => requests.get <ProjectTaskType>(`/projecttasktype`)
}

const ProjectTaskStatuses = {
    projectTaskStatusesList: () => requests.get<ProjectTaskStatus>(`/projecttaskstatus`)
}

const Sprints = {
    getCurrentTeamSprint: (teamId: number) => requests.get(`/sprint/team-current/${teamId}`),
    getTeamsSprints: (teamId: number) => requests.get(`/sprint/team/${teamId}`),
    getSprintById: (sprintId: number) => requests.get<Sprint>(`/sprint/${sprintId}`),
    getDailyTaskCount: (sprintId: number) => requests.get<SprintTaskCountData[]>(`/sprint/task-count/${sprintId}`),
    postSprint: (sprint: SprintPostData) => requests.post<Sprint>(`/sprint`, sprint),
    deleteSprint: (sprintId: number) => requests.del(`/sprint/${sprintId}`),
}

const SprintEvents = {
    getSprintEventsByTeamId: (teamId: number) => requests.get(`/sprintevent/team/${teamId}`),
    getSprintEventsByUserId: (userId: number) => requests.get(`/sprintevent/user/${userId}`),
    getSprintEventById: (eventId: number) => requests.get(`/sprintevent/${eventId}`),
    getClosestThreeEventsByTeamId: (teamId: number) => requests.get(`/sprintevent/team/closest/${teamId}`),
    postSprintEvent: (sprintEvent: SprintEventPostData) => requests.post<Sprint>(`/sprintevent`, sprintEvent),
    deleteSprintEvent: (sprintEventId: number) => requests.del(`/sprintevent/${sprintEventId}`),
}

const SprintEventTypes = {
    getSprintEventTypes: () => requests.get('/sprinteventtype'),
}

const Notifications = {
    getUsersNotifications: (userId: number) => requests.get(`/notification/user/${userId}`),
    deleteNotification: async (notificationId: number) => requests.del(`/notification/${notificationId}`),
    updateNotificiationStatus: async (notificationId: number, statusId: number) =>
        requests.put2(`/notification?notificationId=${notificationId}&statusId=${statusId}`),
}

const apiHandler = {
    Account,
    Comments,
    Users,
    Teams,
    TimeLogs,
    Notifications,
    Permission,
    ProjectTasks,
    ProjectTaskTypes,
    ProjectTaskStatuses,
    Sprints,
    SprintEvents,
    SprintEventTypes
}

export default apiHandler;