import { Roles } from "./Roles";

export interface User {
  username: string,
  email: string,
  role: Roles,
  roleId: number,
}