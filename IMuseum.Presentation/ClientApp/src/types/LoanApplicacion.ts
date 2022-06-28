import { Artwork } from "./Artwork";
import { Museum } from "./Museum";

export interface LoanApplication{
  applicationDate: string;
  duration: number;
  artwork: Artwork;
  museum: string;
  "id": number,
  "loanApplicationStatus": string
}

export interface LoanApplicationPayload{
  applicationDate: string;
  duration: number;
  artwork: Artwork;
  museum: string;
}