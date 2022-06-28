import { Colors } from "./constants";
export const selectStatusColor = (status: string): Colors => {
  switch (status) {
    case "exposed":
      return "success";
    case "available":
      return "success-dark";
    case "unavailable":
      return "warn-accent";
    case "restoring":
      return "warn-accent";
    case "loan":
      return "info";
    case "canceled":
      return "danger"
    
  }
};