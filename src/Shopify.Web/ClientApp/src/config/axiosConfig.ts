import axios from "axios";

const API_URL: string = "https://localhost:7089";

const defaultOptions = {
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
};

const instance = axios.create(defaultOptions);

export const axiosConfig = instance;
