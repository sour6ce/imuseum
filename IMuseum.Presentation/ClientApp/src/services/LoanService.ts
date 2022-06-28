import { Loan } from "../types/Loan";
import { LoanApplication } from "../types/LoanApplicacion";
import { Pagination } from "../ui-components/molecules/Pagination";
import AuthService from "./AuthService";
import { GetLoanAppsFilters, GetLoansFilter } from "./LoanService.dto";


export class LoanService{
  static get axios(){
    return AuthService.axios
  }

  static async getLoans(filters: GetLoansFilter, pagination: Partial<Pagination>){
    return (await LoanService.axios.get<{loans:Loan[], count:number}>("/loans",{
      params: {
        ...filters,
        ...pagination
      },
    })).data
  }

  static async getLoanApps(filters: GetLoanAppsFilters, pagination: Partial<Pagination>){
    return (await LoanService.axios.get<{loanApps:LoanApplication[], count:number}>("/loan-apps",{
      params: {
        ...filters,
        ...pagination
      },
    })).data
  }
}
