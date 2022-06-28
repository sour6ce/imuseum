import { Artwork } from "../types/Artwork";
import { Pagination } from "../ui-components/molecules/Pagination";
import { GetArtworksFilter } from "./ArtworkService.dto";
import AuthService from "./AuthService";



export class ArtworkService{
  static get axios(){
    return AuthService.axios
  }

  static async getArtworkById(id: string){
    return (await ArtworkService.axios.get<Artwork>(`/artworks/${id}`)).data
  }

  static async getArtworks(filters: GetArtworksFilter, pagination: Partial<Pagination>){
    return (await ArtworkService.axios.get<{artworks:Artwork[], count:number}>("/artworks",{
      params: {
        ...filters,
        ...pagination
      },
    })).data
  }

  static async moveToRoom(artworkId: string, roomId: string){
    return (await ArtworkService.axios.post<never>(`/artworks/${artworkId}/move-to-room`, {roomId})).data
  }

  static async moveToStorage(artworkId: string){
    return (await ArtworkService.axios.post<never>(`/artworks/${artworkId}/move-to-storage`)).data
  }

  static async createArtwork(artwork: Partial<Artwork>){
    return (await ArtworkService.axios.post<Artwork>("/artworks", artwork)).data
  }
}
