import axios, { AxiosResponse } from "axios";
import { Activity } from "../models/activity";

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
};

axios.defaults.baseURL = "http://localhost:5000/api";

axios.interceptors.response.use(async function (response) {
  try {
    await sleep(1000);
    return response;
  } catch (err) {
    console.log(err);
    return await Promise.reject(err);
  }
});

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Activities = {
  getActivities: () => requests.get<Activity[]>("/activities"),
  getActivityDetails: (id: string) =>
    requests.get<Activity>(`/activities/${id}`),
  createActivity: (activity: Activity) =>
    requests.post("/activities", activity),
  updateActivity: (activity: Activity) =>
    requests.put(`/activities/${activity.id}`, activity),
  deleteActivity: (id: string) => requests.del(`/activities/${id}`),
};

const agent = {
  Activities,
};

export default agent;
