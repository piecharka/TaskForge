import { makeAutoObservable, reaction } from "mobx";


export default class CommonStore {
    token: string | null | undefined = localStorage.getItem('jwt');

    constructor() {
        makeAutoObservable(this);

        reaction(
            () => this.token,
            token => {
                if (token) {
                    localStorage.setItem('jwt', token);
                } else {
                    localStorage.removeItem('jwt');
                }
            }
        )
    }

    setToken = (token: string | null) => {
        this.token = token;
    }
}