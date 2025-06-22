import axios from "axios";
import { jwtDecode } from "jwt-decode";

import { useAuthStore } from "stores";

const API_URL: string = "https://localhost:7089";

const defaultOptions = {
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
};

const instance = axios.create(defaultOptions);

instance.interceptors.request.use(
  (config: AxiosRequestConfig) => {
    const { token } = useAuthStore.getState();

    if (token) {
      config.headers = { ...config.headers, Authorization: `Bearer ${token}` };

      const decodedToken = jwtDecode(token);

      if (decodedToken?.roles?.includes(Role.Admin)) {
        useAuthStore.setState({ isAdmin: true });
      } else {
        useAuthStore.setState({ isAdmin: false });
      }
    }

    return config;
  },
  error => {
    return Promise.reject(error);
  },
);

export const axiosConfig = instance;
