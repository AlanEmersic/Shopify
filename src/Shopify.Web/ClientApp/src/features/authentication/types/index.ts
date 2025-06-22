export type RegisterCommand = {
  email: string;
  password: string;
  phone: string;
  address: string;
};

export type LoginQuery = {
  email: string;
  password: string;
};

export type AuthenticationDto = {
  email: string;
  token: string;
};

export type ValidationErrors = {
  [key: string]: string;
};

export type Role = "Admin" | "Customer";
