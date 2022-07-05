import { SessionKey, SessionStore } from "./SessionStore";
import axios, { AxiosError } from 'axios';
import { User } from "../types/User";
import { Navigate } from "react-router-dom";

const transformRequestOptions = params => {
  console.log(params)
  let options = '';
  for (const key in params) {
  if (typeof params[key] !== 'object' && params[key]) {
    options += `${key}=${params[key]}&`;
  } else if (typeof params[key] === 'object' && params[key] && params[key].length) {
      // eslint-disable-next-line no-loop-func
      params[key].forEach(el => {
          options += `${key}=${el}&`;
   });
  }
}
return options ? options.slice(0, -1) : options;
};


export class AuthService{
  static get axios(){
    const instance = axios.create({
      baseURL: 'https://localhost:7001/',
      ...(SessionStore.load(SessionKey.EncodedToken) ? {
        headers:{
          Authorization: `Basic ${SessionStore.load(SessionKey.EncodedToken)}`,
        }
      } : null),
      paramsSerializer: params => transformRequestOptions(params)
    })
    instance.interceptors.response.use((res)=>res,(err)=>AuthService.refreshInterceptor(err))
    return instance
  }
  static async refreshInterceptor(error: AxiosError) {
    if (error?.response?.status === 401 || error?.response?.status === 403) {
      SessionStore.remove(SessionKey.EncodedToken);
      SessionStore.remove(SessionKey.Profile);
      window.history.pushState({}, '', '/home');
      window.location.reload();
    }
    return Promise.reject(error);
  }

  static async login(username: string, password: string){
    return AuthService.axios.post<User>('users/login',{username,password})
  }
}
 
export default AuthService;
