import { Entity } from "./entity";

export interface Operator extends Entity {
    operatorId: number;
    name: string;
}