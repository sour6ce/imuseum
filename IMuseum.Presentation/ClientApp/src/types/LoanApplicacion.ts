import { Artwork } from "./Artwork";
import { Museum } from "./Museum";

export interface LoanApplication{
  applicationDate: string;
  duration: number;
  artwork: Artwork;
  museum: Museum;
  "id": 0,
  "loanApplicationStatus": 0
}

export interface LoanApplicationPayload{
  applicationDate: string;
  duration: number;
  artwork: Artwork;
  museum: Museum;
}