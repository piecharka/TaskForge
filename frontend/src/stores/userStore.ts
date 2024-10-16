import { makeAutoObservable, runInAction } from "mobx";
import { UserLoginData, UserRegisterData, UserStoredData } from "../models/user";
import apiHandler from "../api/apiHandler";
import { store } from "./store";

export default class UserStore {
    user: UserStoredData | null;

    constructor(){
        makeAutoObservable(this)
        this.user = this.loadUserFromLocalStorage();
    }

    get isLoggedIn() {
        return !!this.user;
    }

    login = async (creds: UserLoginData) => {
        const user = await apiHandler.Account.login(creds);
        store.commonStore.setToken(user.token);
        runInAction(() => {
            this.user = user;
            this.saveUserToLocalStorage(user);
        });
    }

    register = async (creds: UserRegisterData) => {
        const user = await apiHandler.Account.register(creds);
        store.commonStore.setToken(user.token);
        runInAction(() => {
            this.user = user;
            this.saveUserToLocalStorage(user); 
        });
    }

    logout = () => {
        store.commonStore.setToken(null);
        this.user = null;
        this.removeUserFromLocalStorage();
    }
    saveUserToLocalStorage(user: UserStoredData) {
        localStorage.setItem('user', JSON.stringify(user));
    }

    loadUserFromLocalStorage() {
        const userJson = localStorage.getItem('user');
        if (userJson) {
            return JSON.parse(userJson);
        }
    }

    removeUserFromLocalStorage() {
        localStorage.removeItem('user');
    }
}