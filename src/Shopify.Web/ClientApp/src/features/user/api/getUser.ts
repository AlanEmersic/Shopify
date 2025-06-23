import { useQuery, type UseQueryOptions } from "@tanstack/react-query";

import { axiosConfig } from "config";
import type { User } from "features";

const getUser = async (email: string): Promise<User> => {
  const { data } = await axiosConfig.get(`/api/users/${email}`);

  return data;
};

export function useUser(email: string, options?: UseQueryOptions<User>) {
  return useQuery<User>({
    queryKey: ["user", email],
    queryFn: () => getUser(email),
    refetchOnWindowFocus: false,
    enabled: !!email,
    ...options,
  });
}
