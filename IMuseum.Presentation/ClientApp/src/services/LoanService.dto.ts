export interface GetLoansFilter{
  ArtworkId?: string;
  MuseumId?: string;
  IncomeMin?: number;
  IncomeMax?: number;
}

export interface GetLoanAppsFilters{
  ArtworkId?: string;
  MuseumId?: string;
  Status?:string;
}