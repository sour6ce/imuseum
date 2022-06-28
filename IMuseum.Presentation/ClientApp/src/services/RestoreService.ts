import { Restoration } from "../types/Restoration";
import { Pagination } from "../ui-components/molecules/Pagination";
import { ArtworkService } from "./ArtworkService";
import AuthService from "./AuthService";
import { GetRestorationsFilter } from "./RestoreService.dto";

export class RestoreService{
  static get axios(){
    return AuthService.axios
  }

  static async getRestores(filters: GetRestorationsFilter, pagination: Partial<Pagination>){
    return (await RestoreService.axios.get<{restorations:Restoration[], count:number}>("/restorations",{
      params: {
        ...filters,
        ...pagination
      },
    })).data
  }

  static async startRestoration(artworkId: string,data:{restorationType:string}){
    return (await ArtworkService.axios.post<never>(`/artworks/${artworkId}/start-restoration`,data))
  }
  static async endRestoration(artworkId: string,data:{restorationType:string}){
    return (await ArtworkService.axios.post<never>(`/artworks/${artworkId}/end-restoration`,data))
  }
}