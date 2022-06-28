import { Artwork } from "./Artwork";

export interface Restoration{
  artwork: Partial<Artwork>,
  startDate: string,
  dueDate: string,
  restorationType: string,
}