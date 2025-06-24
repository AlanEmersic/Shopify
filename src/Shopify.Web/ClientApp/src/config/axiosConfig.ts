import axios, { AxiosError, type InternalAxiosRequestConfig } from "axios";
import { jwtDecode } from "jwt-decode";

import { useAuthStore } from "stores";

const API_URL: string = "https://localhost:7089";

const defaultOptions = {
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
};

type JwtPayload = {
  email?: string;
  address?: string;
  id?: string;
  roles?: string[];
  exp?: number;
  iss?: string;
  aud?: string;
};

const instance = axios.create(defaultOptions);

instance.interceptors.request.use(
  async (config: InternalAxiosRequestConfig) => {
    const { token } = useAuthStore.getState();

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;

      const decodedToken: JwtPayload = jwtDecode(token);

      if (decodedToken) {
        useAuthStore.setState({
          isAdmin: decodedToken.roles?.includes("Admin") ?? false,
        });
      }
    }

    return config;
  },
  async (error: AxiosError) => {
    return Promise.reject(error);
  },
);

export const axiosConfig = instance;
