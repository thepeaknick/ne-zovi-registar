import { Entity } from "./entity";

export interface user extends Entity {
    firstName: string;
    lastName: string;
    phoneNumber: string;
    jmbg: string;
    operatorId: number;
}