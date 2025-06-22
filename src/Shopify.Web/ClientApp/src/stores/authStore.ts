import { create } from "zustand";

type AuthState = {
  email: string | null;
  token: string | null;
  isAdmin: boolean;
  login: (email: string, token: string) => void;
  logout: () => void;
};

const useAuthStore = create<AuthState>((set, get) => ({
  email: null,
  token: null,
  isAdmin: false,
  login: (email, token) => {
    const state = get();
    if (state.email !== email || state.token !== token) {
      set({ email, token });
    }
  },
  logout: () => {
    const state = get();
    if (state.email !== null || state.token !== null) {
      set({ email: null, token: null, isAdmin: false });
    }
  },
}));

export { useAuthStore };
