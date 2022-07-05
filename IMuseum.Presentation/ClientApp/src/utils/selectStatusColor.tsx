import { Colors } from "./constants";
export const selectStatusColor = (status: string): Colors => {
  switch (status) {
    case "on-display":
      return "success";
    case "available":
      return "success-dark";
    case "unavailable":
      return "warn-accent";
    case "in-restoration":
      return "warn-accent";
    case "on-loan":
      return "info";
    case "canceled":
      return "danger"
    
  }
};