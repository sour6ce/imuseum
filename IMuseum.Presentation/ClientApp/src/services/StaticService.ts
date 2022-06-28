import { Museum } from "../types/Museum";
import { Room } from "../types/Room";
import { Totals } from "../types/Totals";
import AuthService from "./AuthService";


export class StaticService{
  static get axios(){
    return AuthService.axios
  }

  static async getArtists(){
    return (await StaticService.axios.get<{artists:string[],count:number}>('/artists')).data
  }

  static async getStyles(){
    return ( await StaticService.axios.get<{styles:string[],count:number}>('/styles')).data
  }

  static async getMuseums(){
    return ( await StaticService.axios.get<{museums:Museum[],count:number}>('/museums')).data
  }

  static async getRooms(){
    return ( await StaticService.axios.get<{rooms:Room[],count:number}>('/rooms')).data
  }

  static async getTotals(){
    return ( await StaticService.axios.get<Totals>('/totals'))
  }
}