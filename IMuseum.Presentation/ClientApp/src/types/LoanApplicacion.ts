import { Artwork } from "./Artwork";
import { Museum } from "./Museum";

export interface LoanApplication{
  applicationDate: string;
  duration: number;
  artwork: Artwork;
  museum: Museum;
}