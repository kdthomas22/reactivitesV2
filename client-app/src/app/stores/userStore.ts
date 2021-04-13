import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import agent from "../API/activityApi";
import { User, UserFormValues } from "../models/user";
import { store } from "./store";

export default class UserStore {
  user: User | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  get isLoggedIn() {
    return !!this.user;
  }

  setImage = (image: string) => {
    if (this.user) {
      this.user.image = image;
    }
  };

  login = async (creds: UserFormValues) => {
    try {
      const user = await agent.Account.login(creds);
      store.commonStore.setToken(user.token);
      runInAction(() => {
        this.user = user;
      });
      history.push("/activities");
      store.modalStore.closeModal();
    } catch (err) {
      throw err;
    }
  };

  logout = () => {
    store.commonStore.setToken(null);
    window.localStorage.removeItem("jwt");
    this.user = null;
    history.push("/");
  };

  getCurrentUser = async () => {
    try {
      const user = await agent.Account.getCurrentUser();
      runInAction(() => {
        this.user = user;
      });
    } catch (err) {
      console.log(err);
    }
  };

  registerUser = async (cred: UserFormValues) => {
    try {
      const user = await agent.Account.register(cred);
      store.commonStore.setToken(user.token);
      runInAction(() => {
        this.user = user;
      });
      history.push("/activities");
      store.modalStore.closeModal();
    } catch (err) {
      throw err;
    }
  };
}
