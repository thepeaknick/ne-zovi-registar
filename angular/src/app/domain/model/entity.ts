export interface Entity {
    id: number;
    createdBy: string;
    ModifiedBy: string | null;
    createdOn: Date;
    modifiedOn: Date;
    deleted: boolean;
}