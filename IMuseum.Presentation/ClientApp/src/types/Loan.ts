import { LoanApplication } from "./LoanApplicacion";

export interface Loan {
  startDate: string;
  paymentAmount: number;
  loanApplication: LoanApplication;
}