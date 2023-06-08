import { RoleType } from "./schemas";

export class CurrentUser {
    regUserId?: string;
    username?: string;
    password?: string;
    firstName?: string;
    lastName?: string;
    roles?: RoleType[];
}