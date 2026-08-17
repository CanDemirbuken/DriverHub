export interface JwtPayload {
  sub?: string;
  email?: string;

  role?:
    | string
    | string[];

  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'?: string;

  'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'?: string;

  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?:
    | string
    | string[];
}