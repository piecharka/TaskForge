import axios, { AxiosResponse } from 'axios';
import { User } from '../models/user';
import { Team } from '../models/team';

axios.defaults.baseURL = 'http://localhost:5194/api';

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    post: <T> (url: string, body: object) => axios.post<T>(url, body).then(responseBody),
    put: <T> (url: string, body: object) => axios.put<T>(url, body).then(responseBody),
    del: <T> (url: string) => axios.delete<T>(url).then(responseBody),
}

const Users = {
    list: () => requests.get<User[]>('/users')
}

const Teams = {
    list: () => requests.get<Team[]>('/teams'),
    first: (id : number) => requests.get<Team>(`/teams/${id}`)
}

const apiHandler = {
    Users,
    Teams
}

export default apiHandler;