export interface AddUserRequest {
  firstName: string;
  lastName: string;
  jmbg: string;
  phoneNumber: string;
  operatorId: number;
}

export interface ChangeRegUserPasswordRequest {
  password: string;
  newPassword: string;
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResultDto {
  regUserId: string;
  accessToken: string;
  refreshToken: RefreshToken;
}

export enum RoleType {
  //RATEL or partner
  Admin = 1,
  //Content provder
  Trgovac,
  //Operater
  Obveznik,
  //End user (from RATEL site), only check its own number
  Potrosac,
}

export interface ModifyRegUserRequest {
  name: string;
  address: string;
  regNumber: string;
  taxNumber: string;
  firstName: string;
  lastName: string;
  userName: string;
  roles: RoleType[];
}

export interface ModifyUserRequest {
  firstName: string;
  lastName: string;
  jmbg: string;
  phoneNumber: string;
  operatorId: number;
}

export interface RefreshToken {
  tokenString: string;
  expireAt: string; // "format": "date-time"
}

export interface RefreshTokenRequest {
  accessToken: string;
  refreshToken: string;
}

export interface RefreshTokenResultDto {
  accessToken: string;
  refreshToken: RefreshToken;
}

export interface RegUserDto {
  regNumber: string;
  taxNumber: string;
  // createdOn: '2023-06-11T12:58:03.3910839';
  guidId: string;
  name: string;
  id: number;
}

export interface RegUserDetailsDto {
  guidId: string;
  firstName: string;
  lastName: string;
  companyName: string;
  address: string;
  regNumber: string;
  taxNumber: string;
  role: RoleType;
}

export interface RegisterRegUserRequest {
  name: string;
  address: string;
  regNumber: string;
  taxNumber: string;
  firstName: string;
  lastName: string;
  userName: string;
  password: string;
  roles: RoleType[];
}

export interface TokenResult {
  accessToken: string;
  refreshToken: RefreshToken;
}

export interface UserDto {
  phoneNumber: string;
  createdModifiedOn: string;
}

export interface RegUserId {
  guId: string;
}
