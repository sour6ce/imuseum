import { SessionKey, SessionStore } from "./SessionStore";
import axios, { AxiosError } from 'axios';


export class AuthService{
  static get axios(){
    const instance = axios.create({
      baseURL: '/api',
      ...(SessionStore.load(SessionKey.EncodedToken) ? {
        headers:{
          Authorization: `Basic ${SessionStore.load(SessionKey.EncodedToken)}`,
        }
      } : null)
    })
    instance.interceptors.response.use((res)=>res,(err)=>AuthService.refreshInterceptor(err))
    return instance
  }
  static async refreshInterceptor(error: AxiosError) {
    if (error?.response?.status === 401 || error?.response?.status === 403) {
      SessionStore.remove(SessionKey.EncodedToken);
      SessionStore.remove(SessionKey.Profile);
      globalThis.history.pushState({},'','/home');
    }
    return Promise.reject(error);
  }
}


//   static async login(usr:string,pass:string){
//     return await AuthService.axios.post<{profile:User}>(
//       'login/',{
//         username: usr,
//         password: pass,
//       })
//   }

//   static async logout(){
//     return await AuthService.axios.post('logout/')
//   }

//   static async getUsers(){
//     return await AuthService.axios.get('users/')
//   }
// }

// export default AuthService
