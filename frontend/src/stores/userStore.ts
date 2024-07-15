import { makeAutoObservable, runInAction } from "mobx";
import { UserLoginData, UserStoredData } from "../models/user";
import apiHandler from "../api/apiHandler";
import { store } from "./store";

export default class UserStore {
    user: UserStoredData | null = null;

    constructor(){
        makeAutoObservable(this)
    }

    get isLoggedIn() {
        console.log(!!this.user);
        return !!this.user;
    }

    login = async (creds: UserLoginData) => {
        const user = await apiHandler.Account.login(creds);
        store.commonStore.setToken(user.token);
        runInAction(() => this.user = user);
    }

    logout = () => {
        store.commonStore.setToken(null);
        this.user = null;
    }
}