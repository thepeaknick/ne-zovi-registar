import { RoleType } from "./schemas";

export class CurrentUser {
    id?: string;
    username?: string;
    password?: string;
    firstName?: string;
    lastName?: string;
    roles?: RoleType[];
}