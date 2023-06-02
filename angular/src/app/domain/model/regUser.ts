import { Entity } from "./entity";
import { RoleType } from "./roleType";

export interface RegUser extends Entity {
    guidId: string;
    companyName: string;
    firstName: string;
    lastName: string;
    address: string;
    taxNumber: string;
    regNumber: string;
    userName: string;
    password: string;
    email: string;
    roles: RoleType[] | null | undefined;
}