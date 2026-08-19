export interface UpdateCarStatusRequest{
    status: CarStatus
}

export enum CarStatus{
    Active = 1,
    Maintenance = 2,
    OutOfService = 3,
    Damaged = 4,
    Retired = 5
}