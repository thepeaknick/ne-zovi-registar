import { Entity } from "./entity";
import { Operator } from "./operator";

export interface User extends Entity {
    firstName: string;
    lastName: string;
    phoneNumber: string;
    jmbg: string;
    operator: Operator;
}