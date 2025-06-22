import { useMutation } from "@tanstack/react-query";

import { axiosConfig } from "config";
import type { AuthenticationDto, LoginQuery } from "features";

const login = async (query: LoginQuery): Promise<AuthenticationDto> => {
  const { data } = await axiosConfig.post(`/api/authentication/login`, query);

  return data;
};

export function useLogin() {
  return useMutation({ mutationFn: (query: LoginQuery) => login(query) });
}
