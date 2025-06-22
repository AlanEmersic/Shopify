import { useMutation } from "@tanstack/react-query";

import { axiosConfig } from "config";
import type { AuthenticationDto, RegisterCommand } from "features";

const register = async (command: RegisterCommand): Promise<AuthenticationDto> => {
  const { data } = await axiosConfig.post(`/api/authentication/register`, command);

  return data;
};

export function useRegister() {
  return useMutation({ mutationFn: register });
}
