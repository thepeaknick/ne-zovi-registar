export interface AddUserRequest {
  firstName: string;
  lastName: string;
  jmbg: string;
  phoneNumbers: [string];
  operatorId: number;
}

export interface ChangeRegUserPasswordRequest {
  username: string;
  password: string;
  newPassword: string;
}

export interface ResetRegUserPasswordRequest {
  email: string;
  token: string;
  password: string;
}

export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResultDto {
  regUserId: string;
  userAccountGuidId: string;
  accessToken: string;
  accessTokenExpTime: string; // "format": "date-time"
  refreshToken: string;
  refreshTokenExpTime: string; // "format": "date-time"
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
  email: string;
  regNumber: string;
  taxNumber: string;
  firstName: string;
  lastName: string;
  userName: string;
  role: RoleType;
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
  accessTokenExpTime: string; // "format": "date-time"
  refreshToken: string;
  refreshTokenExpTime: string; // "format": "date-time"
}

export interface RegUserDto {
  regNumber: string;
  taxNumber: string;
  createdOn: string;
  guidId: string;
  name: string;
  id: number;
}

export interface RegUserAccountDto {
  firstName: string;
  lastName: string;
  username: string;
  password: string;
}

export interface RegUserAccountArrayDto {
  accounts: RegUserAccountDto[];
}

export interface RegUserAPRDetailsDto {
  companyName: string;
  address: string;
  regNumber: string;
  taxNumber: string;
  firstName: string;
  lastName: string;
}

export interface RegUserDetailsDto {
  guidId: string;
  firstName: string;
  lastName: string;
  companyName: string;
  email: string;
  address: string;
  regNumber: string;
  taxNumber: string;
  userName: string;
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
  email: string;
  role: RoleType;
}

export interface TokenResult {
  accessToken: string;
  refreshToken: RefreshToken;
}

export interface UserDto {
  phoneNumber: string;
  registeredOn: string;
  active: boolean;
  removedOn: string;
  operator: string;
}

export interface UserDtoPagedList {
  items: [UserDto];
  pageInfo: PageInfo;
}

export interface PageInfo {
  currentCursor: number;
  pageSize: number;
  pagingDisabled: boolean;
  totalCount: number;
}

export interface RegUserId {
  guId: string;
}

export interface DateDto {
  after: string;
}

export interface ContactEmailRequest {
  firstName: string;
  lastName: string;
  companyName: string;
  emailFrom: string;
  phoneNumber: string;
  content: string;
}
